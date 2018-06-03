using FileLibrary.Domain.Exception;
using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FileLibrary
{
    public class XmlFileReaderRoleBased : XmlFileReader, IFileReaderRoleBased
    {
        private readonly IUserAuthorizationService _userAuthorizationService;
        private readonly IFileRoleValidationService _fileRoleValidationService;
        private readonly IDecryptDataService _decryptDataService;
        private bool _ecryptedFile;

        public XmlFileReaderRoleBased(string filePath, string fileName, IUserAuthorizationService userAuthorizationService,
            IFileRoleValidationService fileRoleValidationService)
            : base(filePath, SetExtension(fileName))
        {
            _userAuthorizationService = userAuthorizationService ?? throw new ArgumentException("User Authorization service is not created");
            _fileRoleValidationService = fileRoleValidationService ?? throw new ArgumentException("File Role Validation service is not created");
        }

        public XmlFileReaderRoleBased(string filePath, string fileName, IUserAuthorizationService userAuthorizationService,
            IFileRoleValidationService fileRoleValidationService, IDecryptDataService decryptDataService)
            : base(filePath, SetExtension(fileName), decryptDataService)
        {
            _userAuthorizationService = userAuthorizationService ?? throw new ArgumentException("User Authorization service is not created");
            _fileRoleValidationService = fileRoleValidationService ?? throw new ArgumentException("File Role Validation service is not created");
            _decryptDataService = decryptDataService ?? throw new ArgumentException("Decrypt Data service is not created");
            _ecryptedFile = true;
        }

        public string Read(string role)
        {
            //Check if User claims such role
            if (!_userAuthorizationService.AuthorizeUser(role))
                throw new FileSecurityException($"User can't read this file - FileName: {Filename}");

            //Access xml content in order to validate root node attribute
            string xmlContent = base.ReadFromBase().AsString();

            //Decrypt file data first
            if (_ecryptedFile)
                xmlContent = _decryptDataService.DecryptData(xmlContent);

            if (string.IsNullOrEmpty(xmlContent))
                return string.Empty;

            //If user passed the first validation and is Admin then return content
            if (role.Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
                return xmlContent;

            //read XML
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlContent);

            //Get root node
            XmlElement rootNode = document.DocumentElement;

            //Get role attribute
            string xmlCurrentRole = rootNode.GetAttribute("role");

            //Check role attribute is not empty or null
            if (string.IsNullOrEmpty(xmlCurrentRole))
                return string.Empty;

            //Authorize File read per Role
            if (!_fileRoleValidationService.Validate(xmlCurrentRole, role))
                throw new FileSecurityException($"User can't read this file - FileName: {Filename}");

            return xmlContent;
        }
    }
}

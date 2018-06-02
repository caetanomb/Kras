using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FileLibrary
{
    public class XmlFileReaderRoleBased : XmlFileReader, IXmlFileReaderRoleBased
    {
        private readonly IUserAuthorizationService _userAuthorizationService;
        private readonly IFileRoleValidationService _fileRoleValidationService;

        public XmlFileReaderRoleBased(string filePath, string fileName, IUserAuthorizationService userAuthorizationService,
            IFileRoleValidationService fileRoleValidationService)
            : base(filePath, SetExtension(fileName))
        {
            _userAuthorizationService = userAuthorizationService ?? throw new ArgumentException("User Authorization service is not created");
            _fileRoleValidationService = fileRoleValidationService ?? throw new ArgumentException("File Role Validation service is not created");
        }

        public string ReadBaseOnRole(string role)
        {
            //Check if User claims such role
            if (!_userAuthorizationService.AuthorizeUser(role))
                return string.Empty;
            
            //Access xml content in order to validate root node attribute
            string xmlContent = base.Read().AsString();

            //If user was passed the first validation and is Admin then return content
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
                return string.Empty;

            return xmlContent;
        }
    }
}

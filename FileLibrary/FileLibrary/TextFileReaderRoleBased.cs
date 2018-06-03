using FileLibrary.Domain.Exception;
using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    public class TextFileReaderRoleBased : TextFileReader, IFileReaderRoleBased
    {
        private readonly IUserAuthorizationService _userAuthorizationService;
        private readonly IFileRoleValidationService _fileRoleValidationService;
        private readonly IDecryptDataService _decryptDataService;
        private bool _ecryptedFile;

        public TextFileReaderRoleBased(string filePath, string fileName, IUserAuthorizationService userAuthorizationService,
            IFileRoleValidationService fileRoleValidationService)
            : base(filePath, SetExtension(fileName))
        {
            _userAuthorizationService = userAuthorizationService ?? throw new ArgumentException("User Authorization service is not created");
            _fileRoleValidationService = fileRoleValidationService ?? throw new ArgumentException("File Role Validation service is not created");
        }

        public TextFileReaderRoleBased(string filePath, string fileName, IUserAuthorizationService userAuthorizationService,
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

            //Access text content in order to validate the header row
            IFileResult fileResult = base.ReadFromBase();            

            string textContent = fileResult.AsString();            
            string fileRole = string.Empty;

            //Decrypt file data first
            if (_ecryptedFile)
            {
                textContent = _decryptDataService.DecryptData(textContent);                
            }

            if (string.IsNullOrEmpty(textContent))
                return string.Empty;

            //If user passed the first validation and is Admin then return content
            if (role.Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
                return textContent.ToString();

            if (textContent.Contains("Role="))
            {
                string[] aux = textContent.Split('|');
                if (aux.Length >= 1)
                {
                    fileRole = aux[0];
                    fileRole = fileRole.Replace("Role=", string.Empty);
                }
            }

            //Check role attribute is not empty or null
            if (string.IsNullOrEmpty(fileRole))
                return string.Empty;

            //Authorize File read per Role
            if (!_fileRoleValidationService.Validate(fileRole, role))
                throw new FileSecurityException($"User can't read this file - FileName: {Filename}");

            return textContent;
        }
    }
}

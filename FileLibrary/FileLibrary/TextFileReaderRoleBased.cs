using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    public class TextFileReaderRoleBased : TextFileReader, ITextFileReaderRoleBased
    {
        private readonly IUserAuthorizationService _userAuthorizationService;
        private readonly IFileRoleValidationService _fileRoleValidationService;

        public TextFileReaderRoleBased(string filePath, string fileName, IUserAuthorizationService userAuthorizationService,
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

            //Access text content in order to validate the header row
            IFileResult fileResult = base.ReadFromBase();
            string textContent = fileResult.AsString();
            string headerLine = fileResult.Header;
            string fileRole = string.Empty;

            if (string.IsNullOrEmpty(textContent))
                return string.Empty;

            //If user passed the first validation and is Admin then return content
            if (role.Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
                return textContent.ToString();

            if (headerLine.Contains("Role="))
            {
                string[] aux = headerLine.Split('|');
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
                return string.Empty;

            return textContent;
        }
    }
}

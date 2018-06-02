using FileLibrary.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    public class JsonFileReaderRoleBased : JsonFileReader, IJsonFileReaderRoleBased
    {
        private readonly IUserAuthorizationService _userAuthorizationService;
        private readonly IFileRoleValidationService _fileRoleValidationService;

        public JsonFileReaderRoleBased(string filePath, string fileName, IUserAuthorizationService userAuthorizationService,
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
            string jsonContent = base.Read();            

            if (string.IsNullOrEmpty(jsonContent))
                return string.Empty;

            //If user passed the first validation and is Admin then return content
            if (role.Equals("Admin", StringComparison.CurrentCultureIgnoreCase))
                return jsonContent;

            JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonContent);
            string fileRole = string.Empty;
            
            if (jsonObject.ContainsKey("Role"))
            {
                fileRole = jsonObject.GetValue("Role").ToString();
            }

            //Check role attribute is not empty or null
            if (string.IsNullOrEmpty(fileRole))
                return string.Empty;

            //Authorize File read per Role
            if (!_fileRoleValidationService.Validate(fileRole, role))
                return string.Empty;

            return jsonContent;
        }
    }
}

using FileLibrary;
using FileLibrary.Domain.Exception;
using FileLibrary.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class JsonFileReaderRoleBasedTest
    {
        private Mock<IUserAuthorizationService> _mockUserAuthorizationService;
        private Mock<IFileRoleValidationService> _mockFileRoleValidationService;
        private Mock<IDecryptDataService> _mockDecryptService;

        string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Json";

        [Fact]
        public void A_User_Should_Be_Able_To_Read_JsonFile_In_RoleBased_Security()
        {            
            string fileName = "ContentJsonRoleBasedVisitor.json";

            string[] pauloUserRoles = new string[] { "Visitor" };

            ConfigureMocks(pauloUserRoles);

            var jsonFileReaderRoleBased =
                new JsonFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = jsonFileReaderRoleBased.Read("Visitor");

            Assert.Equal("{\"Role\": \"Visitor\",\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }

        [Fact]
        public void User_Cannot_Read_JsonFile_FileRole_Is_Employee_User_is_Visitor()
        {
            var exception = Assert.Throws<FileSecurityException>(() =>
            {
                string fileName = "ContentJsonRoleBasedEmployee.json"; //this file has role Employee

                string[] pauloUserRoles = new string[] { "Visitor", "Employee" };

                ConfigureMocks(pauloUserRoles);

                var jsonFileReaderRoleBased =
                    new JsonFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
                string contentFile = jsonFileReaderRoleBased.Read("Visitor");
            });

            Assert.Contains("User can't read this file", exception.Message);
        }

        [Fact]
        public void User_Cannot_Read_JsonFile_DoesNot_Have_Is_Employee_Role()
        {
            var exception = Assert.Throws<FileSecurityException>(() =>
            {
                string fileName = "ContentJsonRoleBasedEmployee.json"; //this file has role Employee

                string[] pauloUserRoles = new string[] { "Visitor" };

                ConfigureMocks(pauloUserRoles);

                var jsonFileReaderRoleBased =
                    new JsonFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
                string contentFile = jsonFileReaderRoleBased.Read("Employee");
            });

            Assert.Contains("User can't read this file", exception.Message);
        }

        [Fact]
        public void A_User_Should_Be_Able_To_Read_EncryptedJsonFile_In_RoleBased_Security()
        {
            string fileName = "EncryptedContentJsonRoleBasedEmployee.json";

            string[] pauloUserRoles = new string[] { "Employee" };

            ConfigureMocks(pauloUserRoles);

            var jsonFileReaderRoleBased =
                new JsonFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, 
                _mockFileRoleValidationService.Object, _mockDecryptService.Object);

            string contentFile = jsonFileReaderRoleBased.Read("Employee");

            Assert.Equal("{\"Role\": \"Employee\",\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }

        [Fact]
        public void User_Admin_Read_All_Files()
        {            
            string fileName = "ContentJsonRoleBasedEmployee.json"; //this file has role Employee

            string[] pauloUserRoles = new string[] { "Admin" };

            ConfigureMocks(pauloUserRoles);

            var jsonFileReaderRoleBased =
                new JsonFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = jsonFileReaderRoleBased.Read("Admin");

            Assert.Equal("{\"Role\": \"Employee\",\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }

        private void ConfigureMocks(string[] loggedUserRoles)
        {
            //Mock IUserAuthorizationService
            _mockUserAuthorizationService = new Mock<IUserAuthorizationService>();
            _mockUserAuthorizationService
                .Setup(mock => mock.AuthorizeUser(It.IsAny<string>()))
                .Returns((string requiredRole) =>
                {
                    if (!loggedUserRoles.Contains(requiredRole))
                        return false; //User does not have the role informed

                    return true;
                });

            //Mock IFileRoleValidationService
            _mockFileRoleValidationService = new Mock<IFileRoleValidationService>();
            _mockFileRoleValidationService
                .Setup(mock => mock.Validate(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string fileRole, string informedUserRole) =>
                {
                    if (!string.Equals(fileRole.ToLower(), informedUserRole.ToLower()))
                        return false; //User informed role does not match with file role

                    return true;
                });

            //Mock DecryptService Service
            _mockDecryptService = new Mock<IDecryptDataService>();
            _mockDecryptService
                .Setup(mock => mock.DecryptData(It.IsAny<string>()))
                .Returns((string param) =>
                {
                    string content = Encoding.UTF8.GetString(Convert.FromBase64String(param));
                    string[] splittedContent = content.Split("\r\n");

                    content = string.Empty;
                    foreach (var item in splittedContent)
                    {
                        content += item.Trim();
                    }

                    return content;
                });
        }
    }
}

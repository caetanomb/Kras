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
    public class TextFileReaderRoleBasedTest
    {
        private Mock<IUserAuthorizationService> _mockUserAuthorizationService;
        private Mock<IFileRoleValidationService> _mockFileRoleValidationService;
        private Mock<IDecryptDataService> _mockDecryptService;

        string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Text";

        [Fact]
        public void A_User_Should_Be_Able_To_Read_TextFile_In_RoleBased_Security()
        {            
            string fileName = "ContentRoleBasedVisitor.txt";

            string[] pauloUserRoles = new string[] { "Visitor" };

            ConfigureMocks(pauloUserRoles);

            var textFileReaderRoleBased =
                new TextFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = textFileReaderRoleBased.Read("Visitor");

            Assert.Equal(@"Role=Visitor|This is the file content", contentFile);
        }

        [Fact]
        public void User_Cannot_Read_TextFile_FileRole_Is_Employee_User_is_Visitor()
        {
            var exception = Assert.Throws<FileSecurityException>(() =>
            {
                string fileName = "ContentRoleBasedEmployee.txt"; //this file has role Employee

                string[] pauloUserRoles = new string[] { "Visitor", "Employee" };

                ConfigureMocks(pauloUserRoles);

                var textFileReaderRoleBased =
                    new TextFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
                string contentFile = textFileReaderRoleBased.Read("Visitor");
            });

            Assert.Contains("User can't read this file", exception.Message);
        }

        [Fact]
        public void User_Cannot_Read_TextFile_DoesNot_Have_Employee_Role()
        {
            var exception = Assert.Throws<FileSecurityException>(() =>
            {
                string fileName = "ContentRoleBasedEmployee.txt"; //this file has role Employee

                string[] pauloUserRoles = new string[] { "Visitor" };

                ConfigureMocks(pauloUserRoles);

                var textFileReaderRoleBased =
                    new TextFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
                string contentFile = textFileReaderRoleBased.Read("Employee");
            });

            Assert.Contains("User can't read this file", exception.Message);
        }

        [Fact]
        public void User_Admin_Read_All_Files()
        {            
            string fileName = "ContentRoleBasedEmployee.txt"; //this file has role Employee

            string[] pauloUserRoles = new string[] { "Admin" };

            ConfigureMocks(pauloUserRoles);

            var textFileReaderRoleBased =
                new TextFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = textFileReaderRoleBased.Read("Admin");

            Assert.Equal("Role=Employee|This is the file content", contentFile);
        }

        [Fact]
        public void A_User_Should_Be_Able_To_Read_EncryptedTextFile_In_RoleBased_Security()
        {            
            string fileName = "EncryptedContentRoleBasedEmployee.txt";

            string[] pauloUserRoles = new string[] { "Employee" };

            ConfigureMocks(pauloUserRoles);

            var textFileReaderRoleBased =
                new TextFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, 
                _mockFileRoleValidationService.Object, _mockDecryptService.Object);

            string contentFile = textFileReaderRoleBased.Read("Employee");

            Assert.Equal(@"Role=Employee|This is the file content", contentFile);
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
                    StringBuilder stringBuilder =
                        new StringBuilder(param);

                    char[] array = stringBuilder.ToString().ToCharArray();
                    Array.Reverse(array);

                    return new string(array);
                });
        }
    }
}

using FileLibrary;
using FileLibrary.Domain.Exception;
using FileLibrary.Interfaces;
using Moq;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class XmlFileReaderRoleBasedTests
    {
        private Mock<IUserAuthorizationService> _mockUserAuthorizationService;
        private Mock<IFileRoleValidationService> _mockFileRoleValidationService;
        private Mock<IDecryptDataService> _mockDecryptService;

        string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Xml";

        [Fact]
        public void A_User_Should_Be_Able_To_Read_XmlFile_In_RoleBased_Security()
        {            
            string fileName = "ContentXMLRoleBasedVisitor.xml";

            string[] pauloUserRoles = new string[] { "Visitor" };

            ConfigureMocks(pauloUserRoles);

            var xmlFileReaderRoleBased =
                new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = xmlFileReaderRoleBased.Read("Visitor");

            Assert.Equal("<note role=\"Visitor\"><to>Tove</to><from>Jani</from></note>", contentFile);
        }

        [Fact]
        public void User_Cannot_Read_XmlFile_FileRole_Is_Employee_User_is_Visitor()
        {
            var exception = Assert.Throws<FileSecurityException>(() =>
            {
                string fileName = "ContentXMLRoleBasedEmployee.xml"; //this file has role Employee

                string[] pauloUserRoles = new string[] { "Visitor", "Employee" };

                ConfigureMocks(pauloUserRoles);

                var xmlFileReaderRoleBased =
                    new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
                string contentFile = xmlFileReaderRoleBased.Read("Visitor");
            });

            Assert.Contains("User can't read this file", exception.Message);
        }

        [Fact]
        public void User_Cannot_Read_XmlFile_DoesNot_Have_Employee_Role()
        {
            var exception = Assert.Throws<FileSecurityException>(() =>
            {
                string fileName = "ContentXMLRoleBasedEmployee.xml"; //this file has role Employee

                string[] pauloUserRoles = new string[] { "Visitor" };

                ConfigureMocks(pauloUserRoles);

                var xmlFileReaderRoleBased =
                    new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
                string contentFile = xmlFileReaderRoleBased.Read("Employee");
            });

            Assert.Contains("User can't read this file", exception.Message);
        }

        [Fact]
        public void User_Admin_Read_All_Files()
        {            
            string fileName = "ContentXMLRoleBasedEmployee.xml"; //this file has role Employee

            string[] pauloUserRoles = new string[] { "Admin" };

            ConfigureMocks(pauloUserRoles);

            var xmlFileReaderRoleBased =
                new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = xmlFileReaderRoleBased.Read("Admin");

            Assert.Equal("<note role=\"Employee\"><to>Tove</to><from>Jani</from></note>", contentFile);
        }

        [Fact]
        public void A_User_Should_Be_Able_To_Read_EncryptedXmlFile_In_RoleBased_Security()
        {
            string fileName = "EncryptedContentXMLRoleBasedEmployee.xml";

            string[] pauloUserRoles = new string[] { "Employee" };

            ConfigureMocks(pauloUserRoles);

            var xmlFileReaderRoleBased =
                new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, 
                _mockFileRoleValidationService.Object, _mockDecryptService.Object);

            string contentFile = xmlFileReaderRoleBased.Read("Employee");

            Assert.Equal("<note role=\"Employee\"><to>Tove</to><from>Jani</from></note>", contentFile);
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

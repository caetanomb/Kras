using FileLibrary;
using FileLibrary.Interfaces;
using Moq;
using System.Linq;
using Xunit;

namespace UnitTests.Domain
{
    public class XmlFileReaderRoleBasedTests
    {
        private Mock<IUserAuthorizationService> _mockUserAuthorizationService;
        private Mock<IFileRoleValidationService> _mockFileRoleValidationService;        

        [Fact]
        public void A_User_Should_Be_Able_To_Read_XmlFile_In_RoleBased_Security()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "ContentXML2.xml";

            string[] pauloUserRoles = new string[] { "Visitor" };

            ConfigureMocks(pauloUserRoles);

            var xmlFileReaderRoleBased =
                new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = xmlFileReaderRoleBased.ReadBaseOnRole("Visitor");

            Assert.Equal("<note role=\"Visitor\"><to>Tove</to><from>Jani</from></note>", contentFile);
        }

        [Fact]
        public void User_Cannot_Read_XmlFile_FileRole_Is_Employee_User_is_Visitor()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "ContentXML3.xml"; //this file has role Employee

            string[] pauloUserRoles = new string[] { "Visitor", "Employee"};

            ConfigureMocks(pauloUserRoles);

            var xmlFileReaderRoleBased =
                new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = xmlFileReaderRoleBased.ReadBaseOnRole("Visitor");

            Assert.Equal("", contentFile);
        }

        [Fact]
        public void User_Cannot_Read_XmlFile_DoesNot_Have_Is_Employee_Role()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "ContentXML3.xml"; //this file has role Employee

            string[] pauloUserRoles = new string[] { "Visitor" };

            ConfigureMocks(pauloUserRoles);

            var xmlFileReaderRoleBased =
                new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = xmlFileReaderRoleBased.ReadBaseOnRole("Employee");

            Assert.Equal("", contentFile);
        }

        [Fact]
        public void User_Admin_Read_All_Files()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "ContentXML3.xml"; //this file has role Employee

            string[] pauloUserRoles = new string[] { "Admin" };

            ConfigureMocks(pauloUserRoles);

            var xmlFileReaderRoleBased =
                new XmlFileReaderRoleBased(filePath, fileName, _mockUserAuthorizationService.Object, _mockFileRoleValidationService.Object);
            string contentFile = xmlFileReaderRoleBased.ReadBaseOnRole("Admin");

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
        }
    }
}

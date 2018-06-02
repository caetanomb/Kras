using FileLibrary;
using FileLibrary.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class JsonFileReaderTest
    {
        [Theory]
        [InlineData("ContentJson.json")]
        [InlineData("ContentJson")]
        public void A_User_Should_be_Able_to_Read_A_JsonFile(string fileName)
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";

            var jsonFileReader = new JsonFileReader(filePath, fileName);
            string contentFile = jsonFileReader.Read();

            Assert.Equal("{\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }

        [Theory]
        [InlineData("ContentJson")]
        [InlineData("ContentJson.json")]
        public void Make_Sure_File_Has_TextFile_Extension(string fileName)
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";

            var jsonFileReader = new JsonFileReader(filePath, fileName);

            Assert.Equal("ContentJson.json", jsonFileReader.Filename);
        }

        //This unit test customize the Decrypt algorithm using Dependency Injection
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_TextFile_InjectingService()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "EncryptedContentJson.json";

            //Mock DecryptService Service
            var mockDecryptService = new Mock<IDecryptDataService>();
            mockDecryptService
                .Setup(mock => mock.DecryptData(It.IsAny<string>()))
                .Returns((string param) =>
                {
                    StringBuilder stringBuilder =
                        new StringBuilder(param);

                    char[] array = stringBuilder.ToString().ToCharArray();
                    Array.Reverse(array);

                    return new string(array);
                });

            var jsonFileReader = new JsonFileReader(filePath, fileName, mockDecryptService.Object);
            string contentFile = jsonFileReader.Read();

            Assert.Equal("{\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }

        //This unit test customize the Decrypt algorithm using Extension Method
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_TextFile_ExtensionMethod()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "EncryptedContentJson.json";

            var jsonFileReader = new JsonFileReader(filePath, fileName);
            string contentFile = jsonFileReader.ReadAndDecrypt();

            Assert.Equal("{\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }
    }
}

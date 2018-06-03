using FileLibrary;
using FileLibrary.Interfaces;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class JsonFileReaderTest
    {
        string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Json";

        [Theory]
        [InlineData("ContentJson.json")]
        [InlineData("ContentJson")]
        public void A_User_Should_be_Able_to_Read_A_JsonFile(string fileName)
        {            
            var jsonFileReader = new JsonFileReader(filePath, fileName);
            string contentFile = jsonFileReader.Read();

            Assert.Equal("{\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }

        [Theory]
        [InlineData("ContentJson")]
        [InlineData("ContentJson.json")]
        public void Make_Sure_File_Has_TextFile_Extension(string fileName)
        {            
            var jsonFileReader = new JsonFileReader(filePath, fileName);

            Assert.Equal("ContentJson.json", jsonFileReader.Filename);
        }

        //This unit test customize the Decrypt algorithm using Dependency Injection
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_TextFile_InjectingService()
        {            
            string fileName = "EncryptedContentJson.json";

            //Mock DecryptService Service
            var mockDecryptService = new Mock<IDecryptDataService>();
            mockDecryptService
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

            var jsonFileReader = new JsonFileReader(filePath, fileName, mockDecryptService.Object);
            string contentFile = jsonFileReader.Read();

            Assert.Equal("{\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }

        //This unit test customize the Decrypt algorithm using Extension Method
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_TextFile_ExtensionMethod()
        {            
            string fileName = "EncryptedContentJson.json";

            var jsonFileReader = new JsonFileReader(filePath, fileName);
            string contentFile = jsonFileReader.ReadAndDecrypt();

            Assert.Equal("{\"title\": \"Person\",\"type\": \"object\",\"lastName\": {\"type\": \"string\"}}", contentFile);
        }
    }
}

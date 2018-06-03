using FileLibrary;
using FileLibrary.Interfaces;
using Moq;
using System;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class XmlFileReaderTest
    {
        string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Xml";

        [Theory]
        [InlineData("ContentXML")]
        [InlineData("ContentXML.xml")]
        public void A_User_Should_be_Able_to_Read_A_XmlFile(string fileName)
        {            
            var xmlFileReader = new XmlFileReader(filePath, fileName);
            string contentFile = xmlFileReader.Read();

            Assert.Equal("<note><to>Tove</to><from>Jani</from></note>", contentFile);
        }

        [Theory]
        [InlineData("ContentXML")]
        [InlineData("ContentXML.xml")]
        public void Make_Sure_File_Has_XmlFile_Entension(string fileName)
        {            
            var xmlFileReader = new XmlFileReader(filePath, fileName);

            Assert.Equal("ContentXML.xml", xmlFileReader.Filename);
        }

        //This unit test customize the Decrypt algorithm using Dependency Injection
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_XmlFile_InjectingService()
        {            
            string fileName = "EncryptedContentXML.xml";

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

            var xmlFileReader = new XmlFileReader(filePath, fileName, mockDecryptService.Object);
            string contentFile = xmlFileReader.Read();

            Assert.Equal("<note><to>Tove</to><from>Jani</from></note>", contentFile);
        }

        //This unit test customize the Decrypt algorithm using Extension Method
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_XmlFile_ExtensionMethod()
        {            
            string fileName = "EncryptedContentXML.xml";

            var xmlFileReader = new XmlFileReader(filePath, fileName);
            string contentFile = xmlFileReader.ReadAndDecrypt();

            Assert.Equal("<note><to>Tove</to><from>Jani</from></note>", contentFile);
        }

        [Fact]
        public void Error_WrongConstructor_Read_An_Encrypted_XmlFile()
        {            
            string fileName = "EncryptedContentXML.xml";

            var xmlFileReader = new XmlFileReader(filePath, fileName);
            string contentFile = xmlFileReader.Read();

            Assert.NotEqual("<note><to>Tove</to><from>Jani</from></note>", contentFile);
        }
    }
}

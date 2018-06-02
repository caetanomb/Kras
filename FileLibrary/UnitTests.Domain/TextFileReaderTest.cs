using FileLibrary;
using FileLibrary.Interfaces;
using Moq;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class TextFileReaderTest
    {
        [Theory]
        [InlineData("Context.txt")]
        [InlineData("Context")]
        public void A_User_Should_be_Able_to_Read_A_TextFile(string fileName)
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";            

            var textFileReader = new TextFileReader(filePath, fileName);
            string contentFile = textFileReader.Read();

            Assert.Equal("This is the file content", contentFile);
        }

        [Theory]
        [InlineData("Context")]
        [InlineData("Context.txt")]
        public void Make_Sure_File_Has_TextFile_Entension(string fileName)
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";            

            var textFileReader = new TextFileReader(filePath, fileName);

            Assert.Equal("Context.txt", textFileReader.Filename);
        }

        //This unit test customize the Decrypt algorithm using Dependency Injection
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_TextFile_InjectingService()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "EncryptedContext.txt";

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

            var textFileReader = new TextFileReader(filePath, fileName, mockDecryptService.Object);
            string contentFile = textFileReader.Read();

            Assert.Equal("This is the file content", contentFile);
        }

        //This unit test customize the Decrypt algorithm using Extension Method
        [Fact]
        public void A_User_Should_be_Able_to_Read_An_Encrypted_TextFile_ExtensionMethod()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "EncryptedContext.txt";
            
            var textFileReader = new TextFileReader(filePath, fileName);
            string contentFile = textFileReader.ReadAndDecrypt();

            Assert.Equal("This is the file content", contentFile);
        }

        [Fact]
        public void Error_WrongConstructor_Read_An_Encrypted_TextFile()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "EncryptedContext.txt";

            var textFileReader = new TextFileReader(filePath, fileName);
            string contentFile = textFileReader.Read();

            Assert.NotEqual("This is the file content", contentFile);
        }
    }
}
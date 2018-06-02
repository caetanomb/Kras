using FileLibrary;
using System;
using System.IO;
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
    }
}
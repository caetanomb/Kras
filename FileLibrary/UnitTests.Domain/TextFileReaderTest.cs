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

        [Theory]
        [InlineData(@"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Context.txt", @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain", "Context.txt")]
        [InlineData(@"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Context.txt", @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\", "Context.txt")]        
        public void MakeSure_FullPath_Is_Well_Concaneted(string expectedFullPath, string filePath, string fileName)
        {
            var textFileReader = new TextFileReader(filePath, fileName);

            Assert.Equal(expectedFullPath, textFileReader.FullPath);
        }

        [Fact]
        public void Exception_If_Folder_Not_Exists()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\NotExists";

            var exception = Assert.Throws<DirectoryNotFoundException>(() => {
                new TextFileReader(filePath, "File.txt");
            });

            Assert.Equal("Folder not found", exception.Message);
        }

        [Fact]
        public void Exception_If_File_Not_Exists()
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";
            string fileName = "NotExists.txt";

            var exception = Assert.Throws<FileNotFoundException>(() => {
                new TextFileReader(filePath, fileName);
            });

            Assert.Equal("File not found", exception.Message);
        }        
    }
}
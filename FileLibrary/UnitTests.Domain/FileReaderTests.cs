using FileLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class FileReaderTests
    {
        [Theory]
        [InlineData(@"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Text\Content.txt", @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Text", "Content.txt")]
        [InlineData(@"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Text\Content.txt", @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Text\", "Content.txt")]
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
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\Files\Text\";
            string fileName = "NotExists.txt";

            var exception = Assert.Throws<FileNotFoundException>(() => {
                new TextFileReader(filePath, fileName);
            });

            Assert.Equal("File not found", exception.Message);
        }
    }
}

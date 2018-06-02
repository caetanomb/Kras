using FileLibrary;
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
    }
}

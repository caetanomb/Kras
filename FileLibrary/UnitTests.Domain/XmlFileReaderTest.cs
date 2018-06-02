using FileLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Domain
{
    public class XmlFileReaderTest
    {        
        [Theory]
        [InlineData("ContentXML")]
        [InlineData("ContentXML.xml")]
        public void A_User_Should_be_Able_to_Read_A_XmlFile(string fileName)
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";

            var textFileReader = new XmlFileReader(filePath, fileName);
            string contentFile = textFileReader.Read();

            Assert.Equal("<note><to>Tove</to><from>Jani</from></note>", contentFile);
        }

        [Theory]
        [InlineData("ContentXML")]
        [InlineData("ContentXML.xml")]
        public void Make_Sure_File_Has_XmlFile_Entension(string fileName)
        {
            string filePath = @"E:\Waes\Kras\Src\FileLibrary\UnitTests.Domain\";

            var textFileReader = new XmlFileReader(filePath, fileName);

            Assert.Equal("ContentXML.xml", textFileReader.Filename);
        }
    }
}

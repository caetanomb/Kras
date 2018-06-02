using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLibrary
{
    public class XmlFileReader : FileReader, IXmlFileReader
    {
        public XmlFileReader(string filePath, string fileName)
            : base(filePath, SetExtension(fileName))
        {
        }

        /// <summary>
        /// Sets .xml extension whether file does not have an extension or has a different extension
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <returns></returns>
        public static string SetExtension(string fileName)
        {
            return Path.ChangeExtension(fileName, ".xml");
        }
    }
}

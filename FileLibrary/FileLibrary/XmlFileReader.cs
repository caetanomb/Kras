using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace FileLibrary
{
    public class XmlFileReader : FileReader, IXmlFileReader
    {
        private IDecryptDataService _decryptDataService;

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

        //Overload to avoid change breaking
        /// <summary>
        /// Use this constructor passing in IDecryptDataService implementation otherwise the Read method 
        /// returns raw data
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <param name="fileName">File Name</param>
        /// <param name="decryptDataService">Decrypt Data Service</param>
        public XmlFileReader(string filePath, string fileName, IDecryptDataService decryptDataService)
            : base(filePath, SetExtension(fileName))
        {
            _decryptDataService = decryptDataService;
        }        

        public string Read()
        {
            if (_decryptDataService == null)
                return base.ReadFile().AsString();

            return _decryptDataService.DecryptData(base.ReadFile().AsString());
        }

        protected IFileResult ReadFromBase()
        {
            return base.ReadFile();
        }
    }
}

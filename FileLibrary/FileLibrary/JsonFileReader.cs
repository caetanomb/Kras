﻿using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLibrary
{
    public class JsonFileReader : FileReader, IJsonFileReader
    {
        private IDecryptDataService _decryptDataService;

        public JsonFileReader(string filePath, string fileName)
            : base(filePath, SetExtension(fileName))
        {
        }

        //Overload to avoid change breaking
        /// <summary>
        /// Use this constructor passing in IDecryptDataService implementation otherwise the Read method 
        /// returns raw data
        /// </summary>
        /// <param name="filePath">File Path</param>
        /// <param name="fileName">File Name</param>
        /// <param name="decryptDataService">Decrypt Data Service</param>
        public JsonFileReader(string filePath, string fileName, IDecryptDataService decryptDataService)
            : base(filePath, SetExtension(fileName))
        {
            _decryptDataService = decryptDataService;
        }

        /// <summary>
        /// Sets .json extension whether file does not have an extension or has a different extension
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <returns></returns>
        public static string SetExtension(string fileName)
        {
            return Path.ChangeExtension(fileName, ".json");
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

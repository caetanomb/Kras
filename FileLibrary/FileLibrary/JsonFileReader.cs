using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLibrary
{
    public class JsonFileReader : FileReader, IJsonFileReader
    {        
        public JsonFileReader(string filePath, string fileName)
            : base(filePath, SetExtension(fileName))
        {
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

        public new string Read()
        {
            return base.Read().AsString();            
        }
    }
}

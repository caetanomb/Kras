﻿using FileLibrary.Interfaces;
using System;
using System.IO;
using System.Text;

namespace FileLibrary
{
    public class TextFileReader : FileReader, ITextFileReader
    {                        
        public TextFileReader(string filePath, string fileName)
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
            return Path.ChangeExtension(fileName, ".txt");
        }
    }
}

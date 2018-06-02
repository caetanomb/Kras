using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileLibrary
{
    public class FileReader : IFileReader
    {
        public string FilePath { get; protected set; }
        public string Filename { get; protected set; }        
        public string FullPath => Path.Combine(FilePath, Filename);

        public FileReader(string filePath, string fileName)
        {
            FilePath = filePath;
            Filename = fileName;

            if (!Directory.Exists(filePath))
            {
                throw new DirectoryNotFoundException("Folder not found");
            }
            if (!File.Exists(FullPath))
            {
                throw new FileNotFoundException("File not found");
            }
        }

        public virtual string Read()
        {
            using (FileStream fs = File.Open(FullPath, FileMode.Open, FileAccess.Read))
            using (StreamReader sr = new StreamReader(fs))
            {
                StringBuilder stringBuilder = new StringBuilder();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    stringBuilder.Append(line);
                }

                return stringBuilder.ToString();
            };
        }
    }
}

using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    public static class DecryptJsonFileExtensionService
    {
        public static string ReadAndDecrypt(this IJsonFileReader fileReader)
        {
            string content = Encoding.UTF8.GetString(Convert.FromBase64String(fileReader.Read()));
            string[] splittedContent = content.Split("\r\n");

            content = string.Empty;
            foreach (var item in splittedContent)
            {
                content += item.Trim();
            }

            return content;
        }
    }
}

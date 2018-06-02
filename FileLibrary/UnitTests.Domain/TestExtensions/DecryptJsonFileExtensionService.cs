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
            StringBuilder stringBuilder =
                new StringBuilder(fileReader.Read());

            //Array.Reverse is working as expected for Json files
            //char[] array = stringBuilder.ToString().ToCharArray();
            //Array.Reverse(array);
            
            return stringBuilder.ToString();
        }
    }
}

﻿using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    public static class DecryptTextFileExtensionService
    {
        public static string ReadAndDecrypt(this ITextFileReader fileReader)
        {
            StringBuilder stringBuilder =
                new StringBuilder(fileReader.Read());

            char[] array = stringBuilder.ToString().ToCharArray();
            Array.Reverse(array);

            return new string(array);
        }
    }
}

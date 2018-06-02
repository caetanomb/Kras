using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    public static class ReturnAsStringExtension
    {
        public static string AsString(this IFileResult fileResult)
        {
            return fileResult.Content.ToString();
        }
    }
}

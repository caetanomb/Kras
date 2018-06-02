using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary
{
    public static class ReturnAsStringBuilderExtension
    {
        public static StringBuilder AsStringBuilder(this IFileResult fileResult)
        {
            return fileResult.Content;
        }
    }
}

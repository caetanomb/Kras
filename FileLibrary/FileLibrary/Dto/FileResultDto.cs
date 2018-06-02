using FileLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary.Dto
{
    public class FileResultDto : IFileResult
    {
        public StringBuilder Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary.Domain.Exception
{
    public class FileSecurityException : System.Exception
    {
        public FileSecurityException(string message)
            : base(message)
        {

        }
    }
}

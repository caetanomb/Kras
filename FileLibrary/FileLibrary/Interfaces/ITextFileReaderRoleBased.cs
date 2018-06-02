using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary.Interfaces
{
    public interface ITextFileReaderRoleBased
    {
        string ReadBaseOnRole(string role);
    }
}

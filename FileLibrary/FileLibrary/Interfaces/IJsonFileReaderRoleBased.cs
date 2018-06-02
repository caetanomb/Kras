using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary.Interfaces
{
    public interface IJsonFileReaderRoleBased
    {
        string ReadBaseOnRole(string role);
    }
}

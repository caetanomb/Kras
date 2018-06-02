using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary.Interfaces
{
    public interface IFileResult
    {
        string Header { get; set; }
        StringBuilder Content { get; set; }        
    }    
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary.Interfaces
{
    public interface IFileRoleValidationService
    {
        bool Validate(string foundFileRole, string informedUsedRole);
    }
}

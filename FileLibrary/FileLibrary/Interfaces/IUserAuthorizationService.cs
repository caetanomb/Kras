using System;
using System.Collections.Generic;
using System.Text;

namespace FileLibrary.Interfaces
{
    public interface IUserAuthorizationService
    {
        bool AuthorizeUser(string role);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordHashing
{
    public interface IPasswordHasher
    {
        string Hash(string password);

        bool Check(string hash, string password);
    }
}

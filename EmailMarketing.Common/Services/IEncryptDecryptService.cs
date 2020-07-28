using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Common.Services
{
    public interface IEncryptDecryptService
    {
        string EncryptString(string text, string keyString);
        string DecryptString(string cipherText, string keyString);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailMarketing.Web.Core
{
    public class AppSettings
    {
        public string UserDefaultPassword { get; set; }
        public string TokenSecretKey { get; set; }
        public int TokenExpiresHours { get; set; }
        public string EncryptionDecryptionKey { get; set; }
    }
}

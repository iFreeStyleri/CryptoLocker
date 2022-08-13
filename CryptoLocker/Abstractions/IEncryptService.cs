using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocker.Abstractions
{
    public interface IEncryptService
    {
        void Encrypt(string directoryPath);
    }
}

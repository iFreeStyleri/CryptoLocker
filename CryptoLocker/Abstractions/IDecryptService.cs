using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocker.Abstractions
{
    public interface IDecryptService
    {
        Task Decrypt(string directoryPath, string key);
    }
}

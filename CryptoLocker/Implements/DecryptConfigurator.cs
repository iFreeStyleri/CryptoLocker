using CryptoLocker.Abstractions;
using CryptoLocker.Common;
using CryptoLocker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocker.Implements
{
    public class DecryptConfigurator : ConfiguratorBase
    {
        private readonly Log _log;
        private readonly IDecryptService _service;

        public DecryptConfigurator(IDecryptService service,Log log)
        {
            _log = log;
            _service = service;
        }
        public async void Load()
        {
            var files = Directory.GetFiles(DirectoryPath);
            var fileSuperior = files.Where(w => w.Contains(".superuser")).FirstOrDefault();
            if (fileSuperior == null)
                return;

            var lines = await File.ReadAllLinesAsync(fileSuperior);
            _log.Key = lines[0];
            _log.HashKey = Encoding.Unicode.GetBytes(lines[1]);
        }
        private void DeleteSuperUserFile()
        {
            var files = Directory.GetFiles(DirectoryPath);
            var fileSuperior = files.Where(w => w.Contains(".superuser")).FirstOrDefault();
            if (fileSuperior == null)
                return;
            File.Delete(fileSuperior);
        }

        public async Task<bool> Execute(string key)
        {
            var hashKey = HashKey(key);
            if(IsValidKey(hashKey))
            {
                await _service.Decrypt(DirectoryPath, key);
                DeleteSuperUserFile();
                return true;
            }
            return false;
        }

        private byte[] HashKey(string key)
        {
            using var alg = SHA256.Create();
            return alg.ComputeHash(Encoding.UTF8.GetBytes(key));
        }

        private bool IsValidKey(byte[] hashKey)
        {
            for(int i = 0; i < hashKey.Length; ++i)
            {
                if (hashKey[i] != _log.HashKey[i])
                    return false;
            }
            return true;
        }

    }
}

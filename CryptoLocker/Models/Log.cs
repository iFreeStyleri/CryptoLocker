using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocker.Models
{
    public class Log
    {
        public string Key { get; set; }
        public byte[] HashKey { get; set; }

        public string[] GetLines() => new string[]
        {
            Key,
            Encoding.Unicode.GetString(HashKey)
        };
    }
}

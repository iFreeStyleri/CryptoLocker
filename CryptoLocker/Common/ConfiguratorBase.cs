using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocker.Common
{
    public abstract class ConfiguratorBase
    {
        protected string DirectoryPath => App.DirectoryPath;
        protected string[] GetFilesPath() => Directory.GetFiles(DirectoryPath);

    }
}

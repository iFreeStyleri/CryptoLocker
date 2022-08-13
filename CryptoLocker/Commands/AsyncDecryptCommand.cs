using CryptoLocker.Common;
using CryptoLocker.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoLocker.Commands
{
    public class AsyncDecryptCommand : CommandBase
    {
        private readonly DecryptConfigurator decryptConfigurator;

        public AsyncDecryptCommand(DecryptConfigurator decryptConfigurator)
        {
            this.decryptConfigurator = decryptConfigurator;
        }

        public override bool CanExecute(object parameter) => true;
        public override async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        private async Task ExecuteAsync(object parameter)
        {
            var isExecute = await decryptConfigurator.Execute((string)parameter);
            if (isExecute)
                App.Current.Shutdown();
        }
    }
}

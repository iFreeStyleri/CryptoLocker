using CryptoLocker.Commands;
using CryptoLocker.Common;
using CryptoLocker.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoLocker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _textKey;

        public string TextKey 
        { 
            get => _textKey;
            set => Set(ref _textKey, value);
        }
        public ICommand AcceptCommand { get; }
        public MainViewModel(DecryptConfigurator decryptConfigurator)
        {
            AcceptCommand = new AsyncDecryptCommand(decryptConfigurator);
        }

        public MainViewModel() { }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoLocker.Common
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string path = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(path));
        protected void Set<T>(ref T field, T value, [CallerMemberName] string path = "")
        {
            if (field == null) field = value;
            if (field.Equals(value))
                return;
            field = value;
            OnPropertyChanged(path);
        }
    }
}

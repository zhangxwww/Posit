using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Posit
{
    class ActivityNameViewModel : INotifyPropertyChanged
    {
        private string _name;

        public string ActivityName
        {
            get { return _name; }
            set 
            { 
                _name = value;
                OnPorpertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPorpertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

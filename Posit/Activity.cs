using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Posit
{
    class Activity : INotifyPropertyChanged
    {
        private DateTime _activityTime;
        private string _activityName;

        public Activity() { }

        public string ActivityName
        {
            get { return _activityName; }
            set
            {
                _activityName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

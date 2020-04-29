using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Posit
{
    public class Activity : INotifyPropertyChanged
    {
        private DateTime _activityTime;
        private string _activityName;
        private string _lastDays;

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

        public DateTime ActivityTime
        {
            set
            {
                _activityTime = value;
                TimeSpan span = _activityTime - DateTime.Now;
                Int32 last = span.Days;
                _lastDays = $"{last}天";
            }
        }

        public string LastDays
        {
            get { return _lastDays; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

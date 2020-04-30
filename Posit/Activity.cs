using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Posit
{
    public class Activity : INotifyPropertyChanged
    {
        private Int32 _id;
        private DateTime _activityTime;
        private string _activityName;
        private Int32 _lastDays;
        private string _lastDaysStr;

        public Activity() { }

        public void UpdateLastDays()
        {
            TimeSpan span = _activityTime - DateTime.Now;
            _lastDays = span.Days;
            _lastDaysStr = $"{_lastDays}天";
            OnPropertyChanged("LastDaysStr");
        }

        public Int32 ID
        {
            get { return _id; }
            set { _id = value; }
        }

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
            get { return _activityTime; }
            set
            {
                _activityTime = value;
                UpdateLastDays();
            }
        }

        public Int32 LastDays
        {
            get { return _lastDays; }
        }

        public string LastDaysStr
        {
            get { return _lastDaysStr; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print(propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

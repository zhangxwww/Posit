using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Posit
{
    /// <summary>
    /// ActivityCardList.xaml 的交互逻辑
    /// </summary>
    public partial class ActivityCardList : UserControl
    {
        private ObservableCollection<Activity> _activityList;
        private Int32 _lastUsedId = 0;
        private Storage storage;

        public ActivityCardList ()
        {
            InitializeComponent();
            InitActivityList();
        }

        private void InitActivityList()
        {
            storage = new Storage();
            _activityList = storage.Activities;
            _lastUsedId = storage.MaxId;
            activityCardList.DataContext = new ActivityDataModel
            {
                ActivityList = _activityList
            };
            SortActivitiesByDate();
        }

        public void Add(Activity activity)
        {
            activity.ID = ++_lastUsedId;
            _activityList.Add(activity);
            SortActivitiesByDate();
        }

        public void Update(Int32 id, string name, DateTime? time)
        {
            // TODO
        }

        public void Delete(Int32 id)
        {
            // TODO
        }

        public void UpdateActivities()
        {
            foreach (Activity activity in _activityList)
            {
                activity.UpdateLastDays();
            }
            while (_activityList.Count > 0 && _activityList[0].LastDays < 0)
            {
                _activityList.RemoveAt(0);
            }
        }

        public void Save()
        {
            storage.Activities = _activityList;
        }

        private void SortActivitiesByDate()
        {
            ObservableCollection<Activity> tmp = new ObservableCollection<Activity>(_activityList.OrderBy(item => item.LastDays));
            _activityList.Clear();
            foreach (Activity a in tmp)
            {
                _activityList.Add(a);
            }
        }
    }
}

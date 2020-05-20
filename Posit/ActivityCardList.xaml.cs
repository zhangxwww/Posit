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
            RemainOutOfDate = false;
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
            UpdateActivities();
        }

        public void Add(Activity activity)
        {
            activity.ID = ++_lastUsedId;
            _activityList.Add(activity);
            SortActivitiesByDate();
        }

        public void Delete(Activity activity)
        {
            _activityList.Remove(activity);
        }

        private void EditItemClicked(object sender, RoutedEventArgs e)
        {
            Activity activity = (e.Source as MenuItem).DataContext as Activity;
            EditActivityEvent(activity);
            Delete(activity);
        }

        private void DeleteItemClicked(object sender, RoutedEventArgs e)
        {
            Activity activity = (e.Source as MenuItem).DataContext as Activity;
            Delete(activity);
        }

        private void MouseDoubleClicked(object sender, MouseEventArgs e)
        {
            Activity activity = (e.Source as MaterialDesignThemes.Wpf.Card).DataContext as Activity;
            EditActivityEvent(activity);
            Delete(activity);
        }

        public delegate void EditActivity(Activity activity);

        public event EditActivity EditActivityEvent;

        public void UpdateActivities()
        {
            foreach (Activity activity in _activityList)
            {
                activity.UpdateLastDays();
            }
            while (RemainOutOfDate && _activityList.Count > 0 && _activityList[0].OutOfDate)
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

        public bool RemainOutOfDate { get; set; }
    }
}

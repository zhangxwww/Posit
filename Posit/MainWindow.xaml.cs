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
using System.Windows.Threading;

namespace Posit
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Activity> _activityList;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitActivityList();

            activityCardListWidget.activityCardList.DataContext = new ActivityDataModel
            {
                ActivityList = _activityList
            };

            newActivityWidget.AddClicked += AddActivity;
            newActivityWidget.Visibility = Visibility.Collapsed;

            addButton.Click += new RoutedEventHandler((sender, e) =>
            {
                newActivityWidget.Visibility = Visibility.Visible;
                addButton.Visibility = Visibility.Collapsed;
            });

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(30)
            };
            timer.Tick += new EventHandler((sender, e) =>
            {
                UpdateActivities();
            });
            timer.Start();
        }

        private void InitActivityList()
        {
            _activityList = new ObservableCollection<Activity>
            {
                new Activity { ActivityName="写作业", ActivityTime=DateTime.Parse("2020/5/1") },
                new Activity { ActivityName="hello world", ActivityTime=DateTime.Parse("2020/6/1") }
            };
        }

        private void AddActivity(Activity activity)
        {
            _activityList.Add(activity);
            ObservableCollection<Activity> tmp = new ObservableCollection<Activity>(_activityList.OrderBy(item => item.LastDays));
            _activityList.Clear();
            foreach (Activity a in tmp)
            {
                _activityList.Add(a);
            }
            newActivityWidget.Visibility = Visibility.Collapsed;
            addButton.Visibility = Visibility.Visible;
        }

        private void UpdateActivities()
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
    }
}

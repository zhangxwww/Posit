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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Activity> _activityList;

        public MainWindow ()
        {
            InitializeComponent();
            InitActivityList();

            activityCardListWidget.activityCardList.DataContext = new ActivityDataModel
            {
                ActivityList = _activityList
            };

            newActivityWidget.AddClicked += AddActivity;
            newActivityWidget.Visibility = Visibility.Collapsed;

            addButton.Click += OnAddButtonClick;
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
            ObservableCollection<Activity> tmp = new ObservableCollection<Activity>(_activityList);
            tmp.Add(activity);
            tmp = new ObservableCollection<Activity>(tmp.OrderBy(item => item.ActivityTime));
            _activityList.Clear();
            foreach (Activity a in tmp)
            {
                _activityList.Add(a);
            }
            newActivityWidget.Visibility = Visibility.Collapsed;
            addButton.Visibility = Visibility.Visible;
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            newActivityWidget.Visibility = Visibility.Visible;
            addButton.Visibility = Visibility.Collapsed;
        }
    }
}

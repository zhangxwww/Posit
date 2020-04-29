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
        public MainWindow ()
        {
            InitializeComponent();
            activityCardListWidget.activityCardList.DataContext = new ActivityDataModel
            {
                ActivityList = new ObservableCollection<Activity>
                {
                    new Activity { ActivityName="写作业", ActivityTime=DateTime.Parse("2020/5/1") },
                    new Activity { ActivityName="hello world", ActivityTime=DateTime.Parse("2020/6/1") }
                }
            };
        }
    }
}

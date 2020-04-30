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
        private readonly DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitWidgets();

            timer = new DispatcherTimer();

            BindEvent();

            timer.Start();
        }

        private void InitWidgets()
        {
            newActivityWidget.Visibility = Visibility.Collapsed;
        }

        private void BindEvent()
        {
            newActivityWidget.AddClicked += AddActivity;
            addButton.Click += new RoutedEventHandler((sender, e) =>
            {
                newActivityWidget.Visibility = Visibility.Visible;
                addButton.Visibility = Visibility.Collapsed;
            });
            timer.Interval = TimeSpan.FromMinutes(30);
            timer.Tick += new EventHandler((sender, e) =>
            {
                UpdateActivities();
                activityCardListWidget.Save();
            });
        }

        private void AddActivity(Activity activity)
        {
            activityCardListWidget.Add(activity);
            newActivityWidget.Visibility = Visibility.Collapsed;
            addButton.Visibility = Visibility.Visible;
        }

        private void UpdateActivities()
        {
            activityCardListWidget.UpdateActivities();
        }
    }
}

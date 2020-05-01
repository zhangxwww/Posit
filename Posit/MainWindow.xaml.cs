using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
            MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) =>
            {
                DragMove();
            });
            Loaded += new RoutedEventHandler((sender, e) =>
            {
                BlurHelper.EnableBlur(this);
            });
            newActivityWidget.AddClicked += AddActivity;
            newActivityWidget.CancelClicked += UnShowNewField;
            activityCardListWidget.EditActivityEvent += EditClicked;
            addButton.Click += new RoutedEventHandler((sender, e) =>
            {
                ShowNewField();
            });
            timer.Interval = TimeSpan.FromMinutes(30);
            timer.Tick += new EventHandler((sender, e) =>
            {
                UpdateActivities();
                activityCardListWidget.Save();
            });
            Closing += new System.ComponentModel.CancelEventHandler((sender, e) =>
            {
                activityCardListWidget.Save();
            });
        }

        private void AddActivity(Activity activity)
        {
            activityCardListWidget.Add(activity);
            UnShowNewField();
        }

        private void EditClicked(Activity activity)
        {
            newActivityWidget.SetDefaultValueAs(activity);
            ShowNewField();
        }

        private void UpdateActivities()
        {
            activityCardListWidget.UpdateActivities();
        }

        private void ShowNewField()
        {
            newActivityWidget.Visibility = Visibility.Visible;
            addButton.Visibility = Visibility.Collapsed;
        }

        private void UnShowNewField()
        {
            newActivityWidget.Visibility = Visibility.Collapsed;
            addButton.Visibility = Visibility.Visible;
        }
    }
}

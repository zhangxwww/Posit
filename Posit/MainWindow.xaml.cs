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
using System.Windows.Forms;
using System.Drawing;

namespace Posit
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        // timing to save
        private readonly DispatcherTimer timer;

        private NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            InitWidget();
            InitNotifyIcon();

            timer = new DispatcherTimer();

            BindEvent();

            timer.Start();
        }

        private void InitWidget()
        {
            UnShowNewField();
        }

        private void InitNotifyIcon()
        {
            ShowInTaskbar = false;

            System.Windows.Forms.MenuItem show = new System.Windows.Forms.MenuItem("Show") { Checked = true, Enabled = false };
            System.Windows.Forms.MenuItem hide = new System.Windows.Forms.MenuItem("Hide");
            System.Windows.Forms.MenuItem topMost = new System.Windows.Forms.MenuItem("Top Most") { Checked = true };
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("Exit");

            show.Click += new EventHandler((sender, e) =>
            {
                ShowWindow(show, hide);
            });
            hide.Click += new EventHandler((sender, e) =>
            {
                HideWindow(show, hide);
            });
            topMost.Click += new EventHandler((sender, e) =>
            {
                SwitchTopMost(topMost);
            });
            exit.Click += new EventHandler((sender, e) =>
            {
                ExitWindow();
            });

            notifyIcon = new NotifyIcon
            {
                Text = "Posit",
                Visible = true,
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath),
                ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] { show, hide, topMost, exit })
            };
            notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler((sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ShowWindow(show, hide);
                }
            });
        }

        private void BindEvent()
        {
            // Drag the window by left mouse button
            MouseLeftButtonDown += new MouseButtonEventHandler((sender, e) =>
            {
                DragMove();
            });
            // Blur style
            Loaded += new RoutedEventHandler((sender, e) =>
            {
                // BlurHelper.EnableBlur(this);
            });
            newActivityWidget.AddClicked += AddActivity;
            newActivityWidget.CancelClicked += UnShowNewField;
            activityCardListWidget.EditActivityEvent += EditClicked;
            addButton.Click += new RoutedEventHandler((sender, e) =>
            {
                ShowNewField();
            });
            // Save for every 30 minutes
            timer.Interval = TimeSpan.FromMinutes(30);
            timer.Tick += new EventHandler((sender, e) =>
            {
                UpdateActivities();
                activityCardListWidget.Save();
            });
            // Save before exit
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

        private void ShowWindow(System.Windows.Forms.MenuItem show, System.Windows.Forms.MenuItem hide)
        {
            Visibility = System.Windows.Visibility.Visible;
            Activate();
            show.Checked = true;
            show.Enabled = false;
            hide.Checked = false;
            hide.Enabled = true;
        }

        private void HideWindow(System.Windows.Forms.MenuItem show, System.Windows.Forms.MenuItem hide)
        {
            Visibility = System.Windows.Visibility.Hidden;
            show.Checked = false;
            show.Enabled = true;
            hide.Checked = true;
            hide.Enabled = false;
        }

        private void SwitchTopMost(System.Windows.Forms.MenuItem topMost)
        {
            if (topMost.Checked)
            {
                topMost.Checked = false;
                Topmost = false;
            }
            else
            {
                topMost.Checked = true;
                Topmost = true;
            }
        }

        private void ExitWindow()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// NewActivityFields.xaml 的交互逻辑
    /// </summary>
    public partial class NewActivityFields : UserControl
    {
        public NewActivityFields ()
        {
            InitializeComponent();

            activityNameTextBox.DataContext = new ActivityNameViewModel();

            futureDatePickerWidget.BlackoutDates.AddDatesInPast();
            futureDatePickerWidget.DataContext = new PickersViewModel();

            addButton.Click += new RoutedEventHandler((sender, e) =>
            {
                Debug.Print("add");
                string name = (activityNameTextBox.DataContext as ActivityNameViewModel).ActivityName;
                if (name == null || name.Equals("")) { return; }
                DateTime? time = (futureDatePickerWidget.DataContext as PickersViewModel).FutureValidatingDate;
                if (time == null) { return; }
                DateTime selected = ((DateTime) time).AddDays(1).AddSeconds(-1);
                AddClicked(new Activity { ActivityName = name, ActivityTime = selected });
                Clear();
            });

            closeButton.Click += new RoutedEventHandler((sender, e) =>
            {
                Debug.Print("close");
                CancelClicked();
                Clear();
            });
        }

        public void SetDefaultValueAs(Activity activity)
        {
            activityNameTextBox.Text = activity.ActivityName;
            futureDatePickerWidget.SelectedDate = activity.ActivityTime.Date;
        }

        public delegate void AddActivity (Activity activity);
        public delegate void Cancel();

        public event AddActivity AddClicked;
        public event Cancel CancelClicked;

        private void Clear()
        {
            activityNameTextBox.Clear();
            futureDatePickerWidget.SelectedDate = DateTime.Now;
        }

    }
}

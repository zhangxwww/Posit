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

            addButton.Click += OnClickAdd;
            closeButton.Click += OnClickClose;
        }

        public delegate void AddActivity (Activity activity);

        public event AddActivity AddClicked;

        private void OnClickAdd(object sender, RoutedEventArgs e)
        {
            Debug.Print("add");
            string name = (activityNameTextBox.DataContext as ActivityNameViewModel).ActivityName;
            if (name == null) { return; }
            DateTime? time = (futureDatePickerWidget.DataContext as PickersViewModel).FutureValidatingDate;
            AddClicked(new Activity { ActivityName = name, ActivityTime = (DateTime) time });
            Clear();
        }

        private void OnClickClose(object sender, RoutedEventArgs e)
        {
            Debug.Print("close");
            Clear();
        }

        private void Clear()
        {
            activityNameTextBox.Clear();
        }

    }
}

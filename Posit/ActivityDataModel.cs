using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posit
{
    class ActivityDataModel
    {
        public ObservableCollection<Activity> ActivityList { get; set; }

        public ActivityDataModel() { }
    }
}

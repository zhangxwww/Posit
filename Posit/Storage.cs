using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Diagnostics;

namespace Posit
{
    public class Storage
    {
        public Storage() { }

        public ObservableCollection<Activity> Activities
        {
            get
            {
                ObservableCollection<Activity> activities = new ObservableCollection<Activity>();
                XmlDocument doc = new XmlDocument();
                string path = ConfigurationManager.AppSettings["DataPath"];
                Debug.Print(System.IO.Directory.GetCurrentDirectory());
                try
                {
                    doc.Load(path);
                }
                catch (XmlException e)
                {
                    Debug.Print(e.Message);
                    return activities;
                }
                XmlElement root = doc.DocumentElement;
                foreach (XmlNode node in root.ChildNodes)
                {
                    XmlElement xmlElement = (XmlElement) node;
                    string name = xmlElement.GetAttribute("Name");
                    string dateTimeStr = xmlElement.GetAttribute("DateTime");
                    if (!CheckNameAndDateTime(name, dateTimeStr, out DateTime? time))
                    {
                        continue;
                    }
                    activities.Add(new Activity
                    {
                        ActivityName = name,
                        ActivityTime = (DateTime) time
                    });
                }
                return activities;
            }
            set
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root = doc.CreateElement("Activities");
                doc.AppendChild(root);
                foreach (Activity activity in value)
                {
                    string name = activity.ActivityName;
                    string time = activity.ActivityTime.ToString();
                    XmlElement ele = doc.CreateElement("Activity");
                    ele.Attributes.Append(doc.CreateAttribute("Name"));
                    ele.Attributes.Append(doc.CreateAttribute("DateTime"));
                    ele.SetAttribute("Name", name);
                    ele.SetAttribute("DateTime", time);
                    root.AppendChild(ele);
                }
                string path = ConfigurationManager.AppSettings["DataPath"];
                doc.Save(path);
            }
        }

        private bool CheckNameAndDateTime(string name, string dateTimeStr, out DateTime? time)
        {
            if (name == null || dateTimeStr == null)
            {
                time = null;
                return false;
            }
            if (!DateTime.TryParse((dateTimeStr ?? ""), out DateTime t))
            {
                time = null;
                return false;
            }
            time = t;
            if (time < DateTime.Now || name.Equals(""))
            {
                return false;
            }
            return true;
        }
    }
}

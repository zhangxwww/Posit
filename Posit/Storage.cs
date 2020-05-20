using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Configuration;
using System.Diagnostics;
using System.Xml.Schema;
using System.IO;

namespace Posit
{
    public class Storage
    {
        private Int32 _maxId = 0;

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
                catch (FileNotFoundException e)
                {
                    Debug.Print(e.Message);
                    activities.Add(new Activity 
                    { 
                        ID = 0, 
                        ActivityName = "Hello world", 
                        ActivityTime = DateTime.Now.AddDays(1) 
                    });
                    return activities;
                }
                XmlElement root = doc.DocumentElement;
                foreach (XmlNode node in root.ChildNodes)
                {
                    XmlElement xmlElement = (XmlElement) node;
                    Int32 id = int.Parse(xmlElement.GetAttribute("Id"));
                    if (id > _maxId)
                    {
                        _maxId = id;
                    }
                    string name = xmlElement.GetAttribute("Name");
                    string dateTimeStr = xmlElement.GetAttribute("DateTime");
                    if (!CheckNameAndDateTime(name, dateTimeStr, out DateTime? time))
                    {
                        continue;
                    }
                    activities.Add(new Activity
                    {
                        ID = id,
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
                    string id = activity.ID.ToString();
                    string name = activity.ActivityName;
                    string time = activity.ActivityTime.ToString();
                    XmlElement ele = doc.CreateElement("Activity");
                    ele.Attributes.Append(doc.CreateAttribute("Id"));
                    ele.Attributes.Append(doc.CreateAttribute("Name"));
                    ele.Attributes.Append(doc.CreateAttribute("DateTime"));
                    ele.SetAttribute("Id", id);
                    ele.SetAttribute("Name", name);
                    ele.SetAttribute("DateTime", time);
                    root.AppendChild(ele);
                }
                string path = ConfigurationManager.AppSettings["DataPath"];
                doc.Save(path);
            }
        }

        public Int32 MaxId
        {
            get { return _maxId; }
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
            time = t.Date;
            if (name.Equals(""))
            {
                return false;
            }
            return true;
        }
    }
}

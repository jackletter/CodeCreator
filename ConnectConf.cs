using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace CodeCreator
{
    public class ConnectConf
    {
        public ConnectConf()
        {
            ReadConf();
        }
        public ConntectItem DefaultItem { set; get; }
        public List<ConntectItem> Items { set; get; }
        public void ReadConf()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conn.conf");
            if (!File.Exists(path)) { DefaultItem = null; Items = new List<ConntectItem>(); }
            Items = new List<ConntectItem>();
            ConntectItem tmp = null;
            string[] lines = File.ReadAllLines(path);
            bool flag = false;
            foreach (var item in lines)
            {
                if (item == "#Item_Start")
                {
                    tmp = new ConntectItem();
                    Items.Add(tmp);
                    flag = true;
                }
                if (item == "#Item_Start_Default")
                {
                    tmp = new ConntectItem();
                    Items.Add(tmp);
                    DefaultItem = tmp;
                    flag = true;
                }
                else if (item == "#Item_End")
                {
                    flag = false;
                }
                else
                {
                    if (!flag) { continue; }
                    string str = item.Trim('\t').Trim();
                    if (str.StartsWith("#ItemName"))
                    {
                        str = str.Substring("#ItemName".Length).Trim().TrimStart('=');
                        tmp.ItemName = str;
                    }
                    else if (str.StartsWith("#DBType"))
                    {
                        str = str.Substring("#DBType".Length).Trim().TrimStart('=');
                        tmp.DBType = str;
                    }
                    else if (str.StartsWith("#IP"))
                    {
                        str = str.Substring("#IP".Length).Trim().TrimStart('=');
                        tmp.IP = str;
                    }
                    else if (str.StartsWith("#DBName"))
                    {
                        str = str.Substring("#DBName".Length).Trim().TrimStart('=');
                        tmp.DBName = str;
                    }
                    else if (str.StartsWith("#UserID"))
                    {
                        str = str.Substring("#UserID".Length).Trim().TrimStart('=');
                        tmp.UserID = str;
                    }
                    else if (str.StartsWith("#PWD"))
                    {
                        str = str.Substring("#PWD".Length).Trim().TrimStart('=');
                        tmp.PWD = str;
                    }
                }

            }
            if (DefaultItem == null && Items.Count > 0)
            {
                DefaultItem = Items[0];
            }
        }

        public class ConntectItem
        {
            public string ItemName { set; get; }
            public string DBType { set; get; }
            public string IP { set; get; }
            public string DBName { set; get; }
            public string UserID { set; get; }
            public string PWD { set; get; }
        }
    }
}

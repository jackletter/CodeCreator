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
                else if (item == "#Item_End")
                {
                    flag = false;
                }
                else
                {
                    if (!flag) { continue; }
                    string str = item.Trim('\t').Trim();
                    if (str.StartsWith("#ItemID"))
                    {
                        str = str.Substring("#ItemID".Length).Trim().TrimStart('=');
                        tmp.ItemID = str;
                    }
                    else if (str.StartsWith("#ItemName"))
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
                    if (string.IsNullOrEmpty(tmp.ItemID))
                    {
                        System.Threading.Thread.Sleep(10);
                        tmp.ItemID = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    }
                }

            }
            if (DefaultItem == null && Items.Count > 0)
            {
                DefaultItem = Items[0];
            }
        }

        public void WriteConf()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "conn.conf");
            StringBuilder sb = new StringBuilder();
            if (DefaultItem != null)
            {
                sb.AppendLine("#Item_Start");
                sb.AppendLine("    #ItemID=" + DefaultItem.ItemID);
                sb.AppendLine("    #ItemName=" + DefaultItem.ItemName);
                sb.AppendLine("    #DBType=" + DefaultItem.DBType);
                sb.AppendLine("    #IP=" + DefaultItem.IP);
                sb.AppendLine("    #DBName=" + DefaultItem.DBName);
                sb.AppendLine("    #UserID=" + DefaultItem.UserID);
                sb.AppendLine("    #PWD=" + DefaultItem.PWD);
                sb.AppendLine("#Item_End");
            }
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemName != DefaultItem.ItemName)
                {
                    sb.AppendLine("#Item_Start");
                    sb.AppendLine("    #ItemID=" + Items[i].ItemID);
                    sb.AppendLine("    #ItemName=" + Items[i].ItemName);
                    sb.AppendLine("    #DBType=" + Items[i].DBType);
                    sb.AppendLine("    #IP=" + Items[i].IP);
                    sb.AppendLine("    #DBName=" + Items[i].DBName);
                    sb.AppendLine("    #UserID=" + Items[i].UserID);
                    sb.AppendLine("    #PWD=" + Items[i].PWD);
                    sb.AppendLine("#Item_End");
                }
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.AppendAllText(path, sb.ToString());
        }

        public class ConntectItem
        {
            public string ItemID { set; get; }
            public string ItemName { set; get; }
            public string DBType { set; get; }
            public string IP { set; get; }
            public string DBName { set; get; }
            public string UserID { set; get; }
            public string PWD { set; get; }
        }
    }
}

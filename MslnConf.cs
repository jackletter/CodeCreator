using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace CodeCreator
{
    public class MslnConf
    {
        public string MslnPath { set; get; }
        public Hashtable ht = (Hashtable)DataTransfer.data;
        public List<TemplateItem> tmpItems { set; get; }
        public MslnConf(string mslnPath)
        {
            this.MslnPath = mslnPath;
            Init();
        }
        private void Init()
        {
            List<TemplateItem> list = new List<TemplateItem>();
            TemplateItem template = null;
            bool flag = false;
            string[] lines = File.ReadAllLines(MslnPath);
            foreach (var item in lines)
            {
                string tmp = item;
                tmp = tmp.Trim().Trim('\t');
                if (tmp.StartsWith("//"))
                {
                    continue;
                }
                if (tmp.StartsWith("#Item_Start"))
                {
                    template = new TemplateItem();
                    list.Add(template);
                    flag = true;
                }
                else if (tmp.StartsWith("#Item_End"))
                {
                    flag = false;
                }
                else
                {
                    if (flag)
                    {
                        if (tmp.StartsWith("#Path"))
                        {
                            template.Path = "TmpSolutions/" + tmp.Substring("#Path".Length).Trim('=');
                        }
                        else if (tmp.StartsWith("#OutName"))
                        {
                            template.OutName = tmp.Substring("#OutName".Length).Trim('=').Replace("#TableName#", ht["TableName"].ToString());
                        }
                        else if (tmp.StartsWith("#ClassFullName"))
                        {
                            template.ClassFullName = tmp.Substring("#ClassFullName".Length).Trim('=');
                        }
                        else if (tmp.StartsWith("#CreateMethod"))
                        {
                            template.CreateMethod = tmp.Substring("#CreateMethod".Length).Trim('=');
                        }
                    }
                }
            }
            tmpItems = list;
        }

        public class TemplateItem
        {
            public string Path { set; get; }
            public string OutName { set; get; }
            public string ClassFullName { set; get; }
            public string CreateMethod { set; get; }
        }
    }
}

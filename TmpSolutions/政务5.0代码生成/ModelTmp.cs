//#import system.dll
//#import system.core.dll
//#import system.data.dll
//#import system.xml.dll
//#import system.configuration.dll
//#import HanZi2PinYin.dll
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HanZi2PinYin;

namespace CodeCreator_ZW5_0
{
    public class ModelTmp
    {
        public string Create(Hashtable ht)
        {
            Chinese2Spell c = new Chinese2Spell();
            string tableName = ht["TableName"].ToString();
            List<string> listColumnNames = (List<string>)ht["listColumnNames"];
            List<string> listColumnTypes = (List<string>)ht["listColumnTypes"];
            List<string> listColumnInstructions = (List<string>)ht["listColumnInstructions"];
            List<bool> listColumnIsIdentities = (List<bool>)ht["listColumnIsIdentities"];
            string PrimaryKey = ht["PrimaryKey"].ToString();

            string res = "";
            string tmpRes = "";
            tmpRes = @"
//***************************************************************
//说明：#instruction#
//时间：#Now#
//类型：DataOperator
//***************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations;


namespace LandSite.Models
{
";
            tmpRes = tmpRes.Replace("#instruction#", "自动生成代码");
            tmpRes = tmpRes.Replace("#Now#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            res += tmpRes;
            tmpRes = @"
^##^    /// <summary>
^##^    /// 表[#TableName#]存取
^##^    /// 关键字段为[#PrimaryKey#]
^##^    public class #TableName#Models
^##^    {
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#PrimaryKey#", PrimaryKey);
            res += tmpRes.Replace("^##^","");
            tmpRes = @"
^##^        /// <summary>
^##^        /// 字段名称:#ColumnName# [字段类型:#ColumnType#] 说明 ##
^##^        /// </summary>
^##^        [DisplayFormat(ConvertEmptyStringToNull = false)]
^##^        public string #ColumnNameEnglish# { set; get; }

";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                string tmp = tmpRes.Replace("#ColumnName#", listColumnNames[i]).Replace("#ColumnType#", listColumnTypes[i]).Replace("#ColumnNameEnglish#", c.GetSpell(listColumnNames[i],ConvertModel.FirstCap));
                res += tmp.Replace("^##^", "");
            }
            tmpRes = @"
^##^        /// <summary>
^##^        /// 数据库字段名称
^##^        /// </summary>
^##^        public class Column
^##^        {
";
            res += tmpRes.Replace("^##^", "");
            tmpRes = @"
^##^            /// <summary>
^##^            /// 字段名称:#ColumnName# [字段类型:#ColumnType#]
^##^            /// </summary>
^##^            public const string #ColumnNameEnglish# = ""#ColumnName#"";
^##^";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                string tmp = tmpRes.Replace("#ColumnName#", listColumnNames[i]).Replace("#ColumnType#", listColumnTypes[i]).Replace("#ColumnNameEnglish#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap));
                res += tmp.Replace("^##^", "");
            }
            tmpRes = @"
^##^        }
^##^    }
^##^}";
            res += tmpRes.Replace("^##^", "");
            return res;
        }
    }
}

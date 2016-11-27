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

namespace CodeCreator_JACKOA
{
    public class ModelTmp
    {
        public string Create(Hashtable ht)
        {
            Chinese2Spell c = new Chinese2Spell();
            string tableName = ht["TableName"].ToString();//表名
            string tableInstructions = ht["TableInstructions"].ToString();//表说明
            List<string> listColumnNames = (List<string>)ht["listColumnNames"];//列名集合
            List<string> listColumnTypes = (List<string>)ht["listColumnTypes"];//列类型集合
            List<string> listColumnInstructions = (List<string>)ht["listColumnInstructions"];//列说明集合
            List<bool> listColumnIsIdentities = (List<bool>)ht["listColumnIsIdentities"];//列是否是自增集合
            string PrimaryKey = ht["PrimaryKey"].ToString();//表主键

            string res = "";
            string tmpRes = "";
            tmpRes = @"
//***************************************************************
//说明：#instruction#
//时间：#Now#
//类型：DataModel
//***************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations;


namespace JackOA.Models
{
";
            tmpRes = tmpRes.Replace("#instruction#", "自动生成代码");
            tmpRes = tmpRes.Replace("#Now#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            res += tmpRes;
            tmpRes = @"
^##^    /// <summary>
^##^    /// 表[#TableName#]数据模型 [说明:#tableInstructions#]
^##^    /// 关键字段为[#PrimaryKey#]
^##^    /// </summary>
^##^    public class #TableName#Models
^##^    {
";
            tmpRes = tmpRes.Replace("#TableName#", tableName)
                .Replace("#PrimaryKey#", PrimaryKey)
                .Replace("#tableInstructions#", tableInstructions);
            res += tmpRes.Replace("^##^", "");
            tmpRes = @"
^##^        /// <summary>
^##^        /// 字段名称:#ColumnName# [字段类型:#ColumnType#] [说明:#ColumnInstructions#]
^##^        /// </summary>
^##^        [DisplayFormat(ConvertEmptyStringToNull = false)]
^##^        public string #ColumnNameEnglish# { set; get; }
";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                string tmp = tmpRes.Replace("#ColumnName#", listColumnNames[i])
                    .Replace("#ColumnType#", listColumnTypes[i])
                    .Replace("#ColumnNameEnglish#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap))
                    .Replace("#ColumnInstructions#", listColumnInstructions[i]);
                res += tmp.Replace("^##^", "");
            }

            tmpRes = @"
^##^        /// <summary>
^##^        /// 扩展属性:序号-Index
^##^        /// </summary>
^##^        public string Index { set; get; }
";
            res += tmpRes.Replace("^##^", "");

            tmpRes = @"
^##^        /// <summary>
^##^        /// 表[#TableName#]字段名称
^##^        /// </summary>
^##^        public class Column
^##^        {
";
            res += tmpRes.Replace("#TableName#", tableName).Replace("^##^", "");
            tmpRes = @"
^##^            /// <summary>
^##^            /// 字段名称:#ColumnName# [字段类型:#ColumnType#] [说明:#ColumnInstructions#]
^##^            /// </summary>
^##^            public const string #ColumnNameEnglish# = ""#ColumnName#"";
^##^";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                string tmp = tmpRes.Replace("#ColumnName#", listColumnNames[i])
                    .Replace("#ColumnType#", listColumnTypes[i])
                    .Replace("#ColumnNameEnglish#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap))
                    .Replace("#TableName#", tableName)
                    .Replace("#ColumnInstructions#", listColumnInstructions[i]);
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

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
    public class ComponentTmp
    {
        public string Create(Hashtable ht)
        {
            Chinese2Spell c = new Chinese2Spell();
            string tableName = ht["TableName"].ToString();//表名
            string tableInstructions = ht["TableInstructions"].ToString();//表说明
            List<string> listColumnNames = (List<string>)ht["listColumnNames"];//列名集合
            List<string> listColumnTypes = (List<string>)ht["listColumnTypes"];//列类型集合
            List<string> listColumnInstructions = (List<string>)ht["listColumnInstructions"];//列说明集合
            List<bool> listColumnIsIdentities = (List<bool>)ht["listColumnIsIdentities"];//列是否自增集合
            string PrimaryKey = ht["PrimaryKey"].ToString();//主键
            string strColumnNames = "";//列名的字符串连接方式,如:"ID,NAME,AGE"
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (i == listColumnNames.Count - 1)
                {
                    strColumnNames += listColumnNames[i];
                }
                else
                {
                    strColumnNames += listColumnNames[i] + ",";
                }
            }

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
using System.Data;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using DBUtil;
using JackOA.Models;

namespace JackOA.Component
{
";
            tmpRes = tmpRes.Replace("#instruction#", "自动生成代码");
            tmpRes = tmpRes.Replace("#Now#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            res += tmpRes;
            tmpRes = @"
^##^    /// <summary>
^##^    /// 表[#TableName#]存取 [说明:#tableInstructions#]
^##^    /// 关键字段为[#PrimaryKey#]
^##^    public class #TableName#OP
^##^    {
^##^        /// <summary>
^##^        /// 新生成一行记录的ID
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <returns>新生成的ID</returns>
^##^        public static String NewID(IDbAccess iDb)
^##^        {
^##^            lock (typeof(#TableName#OP))
^##^            {
^##^                string id = iDb.GetFirstColumn(""exec usp_NextID '#TableName#'"").ToString();
^##^                return id;
^##^            }
^##^        }
";
            tmpRes = tmpRes.Replace("#TableName#", tableName)
                .Replace("#PrimaryKey#", PrimaryKey)
                .Replace("#tableInstructions#", tableInstructions);
            res += tmpRes.Replace("^##^", "");
            tmpRes = @"
^##^        /// <summary>
^##^        /// 获取一行记录
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sFilter"">条件字符串，以AND开头，不能为空</param>
^##^        /// <returns></returns>
^##^        public #TableName#Models Get(IDbAccess iDb, string sFilter)
^##^        {
^##^            if (string.IsNullOrWhiteSpace(sFilter))
^##^            {
^##^                throw new Exception(""条件字符串不能为空"");
^##^            }
^##^            #TableName#Models result = new #TableName#Models();
^##^            DataSet ds = iDb.GetDataSet(""SELECT #strColumnNames# FROM #TableName# WHERE 1=1 "" + sFilter);
^##^            if (ds.Tables[0].Rows.Count == 1)
^##^            {";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#strColumnNames#", strColumnNames);
            res += tmpRes.Replace("^##^", "");
            tmpRes = @"
^##^                    result.#ColumnName# = ds.Tables[0].Rows[0][#TableName#Models.Column.#ColumnName#].ToString();";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnTypes[i].ToUpper() == "datetime".ToUpper())
                {
                    string tmpr = @"
^##^                    result.#ColumnName# = ds.Tables[0].Rows[0][#TableName#.Column.#ColumnName#].ToString().Replace(""/"",""-"").Replace("" 0:00:00"","""");";
                    string tmp2 = tmpr.Replace("#ColumnName#", listColumnNames[i]).Replace("#TableName#", tableName).Replace("^##^", "");
                    res += tmp2;
                    continue;
                }
                string tmp = tmpRes.Replace("#ColumnName#", listColumnNames[i]).Replace("#TableName#", tableName).Replace("^##^", "");
                res += tmp;
            }
            tmpRes = @"
^##^                return result;
^##^            }				
^##^            return null;
^##^        }

^##^        /// <summary>
^##^        /// 插入新记录
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""result"">表对象</param>
^##^        /// <returns>保存是否成功</returns>
^##^        public bool Add(IDbAccess iDb, #TableName#Models result)
^##^        {
^##^            if (result==null||string.IsNullOrWhiteSpace(result.#PrimaryKey#))
^##^            {
^##^                throw new Exception(""模型或模型的关键字不能为空"");
^##^            }
^##^
^##^            Hashtable ht = new Hashtable();
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).
                Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            tmpRes = @"
^##^            if (result.#ColumnName# != null)
^##^            {
^##^                if(result.#ColumnName#==""""){ht.Add(#TableName#.Column.#ColumnName#, DBNull.Value);}else{ht.Add(#TableName#.Column.#ColumnName#, result.#ColumnName#);}
^##^            }
";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnIsIdentities[i]) { continue; }
                string tmp = tmpRes.Replace("#TableName#", tableName).Replace("#ColumnName#", listColumnNames[i]).Replace("^##^", "");
                res += tmp;
            }
            tmpRes = @"
^##^            return iDb.AddData(""#TableName#"", ht);
^##^        }
^##^            
^##^        /// <summary>
^##^        /// 保存
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""result"">表对象</param>
^##^        /// <returns>保存是否成功</returns>
^##^        public bool Save(IDbAccess iDb, #TableName#Models result)
^##^        {
^##^            if (result==null||string.IsNullOrWhiteSpace(result.#PrimaryKey#))
^##^            {
^##^                throw new Exception(""模型或模型的关键字不能为空"");
^##^            }
^##^                
^##^            Hashtable ht = new Hashtable();
";
            tmpRes = tmpRes.Replace("#TableName#", tableName)
                .Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            tmpRes = @"
^##^            if (result.#ColumnName# != null)
^##^            {
^##^                if(result.#ColumnName#==""""){ht.Add(#TableName#.Column.#ColumnName#, DBNull.Value);}else{ht.Add(#TableName#.Column.#ColumnName#, result.#ColumnName#);}
^##^            }
";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnIsIdentities[i]) { continue; }
                string tmp = tmpRes.Replace("#TableName#", tableName)
                    .Replace("#ColumnName#", listColumnNames[i]).Replace("^##^", "");
                res += tmp;
            }

            tmpRes = @"
^##^            return iDb.UpdateData(""#TableName#"", ht, "" AND #PrimaryKey#='"" + result.#PrimaryKey# + ""'"");
^##^        }

^##^        /// <summary>
^##^        /// 删除
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sFilter"">条件字符串，以AND开头，不能为空</param>
^##^        /// <returns>返回删除的记录行数</returns>
^##^        public int Delete(IDbAccess iDb, string sFilter)
^##^        {
^##^            if (string.IsNullOrWhiteSpace(sFilter))
^##^            {
^##^                throw new Exception(""条件字符串不能为空"");
^##^            }
^##^            return iDb.DeleteTableRow(""#TableName#"",sFilter);
^##^        }
^##^            
^##^        /// <summary>
^##^        /// 获得指定过滤条件的记录行数
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sFilter"">查询条件，以AND开头,可以为空</param>
^##^        /// <returns>数据库中的记录行数</returns>
^##^        public int GetDataCount(IDbAccess iDb,string sFilter)
^##^        {
^##^            string strSql = ""select count(1) from #TableName# t where 1=1 "" + sFilter;
^##^            return Convert.ToInt32(iDb.GetFirstColumn(strSql));
^##^        }
^##^            
^##^        /// <summary>
^##^        /// 返回全部数据List
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sFilter"">查询条件，以AND开头,可以为空</param>
^##^        /// <param name=""sOrderColumn"">排序字段，可以指定多个字段，以','分隔</param>
^##^        /// <param name=""sOrderType"">排序类型 ASC/DESC</param>
^##^        /// <returns>返回全部数据List</returns>
^##^        public List<#TableName#Models> SearchList(IDbAccess iDb,string sFilter,string sOrderColumn,string sOrderType,string sOrderStr)
^##^        {
^##^            return SearchListByPage(iDb, 0, 0, sFilter, sOrderColumn, sOrderType,sOrderStr).DataList;
^##^        }
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).
                Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            tmpRes = @"
^##^        /// <summary>
^##^        /// 返回分页数据List
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sKey"">表关键字</param>
^##^        /// <param name=""iPageSize"">单页记录行数</param>
^##^        /// <param name=""iPageIndex"">分页索引</param>
^##^        /// <param name=""sFilter"">查询条件，以AND开头,可以为空</param>
^##^        /// <param name=""sOrderColumn"">排序字段，可以指定多个字段，以','分隔</param>
^##^        /// <param name=""sOrderType"">排序类型 ASC/DESC</param>
^##^        /// <returns>返回分页数据List</returns>
^##^        public SearchResult<#TableName#Models> SearchListByPage(IDbAccess iDb, int iPageSize, int iPageIndex, string sFilter, string sOrderColumn, string sOrderType,string sOrderStr)
^##^        {
^##^            SearchResult<#TableName#Models> sr = new SearchResult<#TableName#Models>();
^##^            sr.Count =  GetDataCount(iDb,sFilter);
^##^            string sOrderString = """";
^##^
^##^			if (!string.IsNullOrWhiteSpace(sOrderStr))
^##^            {
^##^                sOrderString = sOrderStr;
^##^            }
^##^            else if(!string.IsNullOrWhiteSpace(sOrderColumn))
^##^			{
^##^			    if (sOrderColumn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) != null && sOrderColumn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
^##^			    {
^##^				    string[] orderClomuns = sOrderColumn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
^##^				    string[] orderTypes = sOrderType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
^##^				    for (int i = 0; i < orderClomuns.Length; i++)
^##^				    {
^##^					    if (i == 0)
^##^					    {
^##^						    string orderType = ""ASC"";
^##^						    if (!(orderTypes == null || string.IsNullOrWhiteSpace(orderTypes[i])))
^##^						    {
^##^							    orderType = orderTypes[i];
^##^						    }
^##^						    sOrderString += "" ORDER BY "" + orderClomuns[i] + "" "" + orderType;
^##^					    }
^##^					    else
^##^					    {
^##^						    string orderType = ""ASC"";
^##^						    if (!(orderTypes == null || orderTypes.Length < i || string.IsNullOrWhiteSpace(orderTypes[i])))
^##^						    {
^##^							    orderType = orderTypes[i];
^##^						    }
^##^						    sOrderString += "", "" + orderClomuns[i] + "" "" + orderType;
^##^					    }
^##^				    }
^##^
^##^			    }else{
^##^				    sOrderString += "" ORDER BY "" + sOrderColumn + "" "" + sOrderType;
^##^			    }
^##^			}

^##^            sFilter = "" 1=1 "" + sFilter;
^##^            string strSql = ""SELECT #strColumnNames# FROM #TableName# t WHERE "" + sFilter;
^##^            if (iPageSize > 0)
^##^            {
^##^                strSql = iDb.GetSqlForPageSize(strSql, sOrderString, iPageSize, iPageIndex);
^##^            }else{
^##^                strSql+=sOrderString;
^##^            }
^##^            DataSet ds = iDb.GetDataSet(strSql);
^##^            List<#TableName#Models> list = new List<#TableName#Models>();
^##^            for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
^##^            {
^##^                #TableName#Models result = new #TableName#Models();
^##^                result.Index = (iPageSize * (iPageIndex - 1) + 1 + ii).ToString();
";
            tmpRes = tmpRes.Replace("#TableName#", tableName)
                .Replace("#strColumnNames#", strColumnNames)
                .Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            tmpRes = @"
^##^                result.#ColumnName# = ds.Tables[0].Rows[ii][#TableName#Models.Column.#ColumnName#].ToString();
";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnTypes[i].ToUpper() == "datetime".ToUpper())
                {
                    string tmpr = @"
^##^                result.#ColumnName# = ds.Tables[0].Rows[ii][#TableName#.Column.#ColumnName#].ToString().Replace(""/"",""-"").Replace("" 0:00:00"","""");";
                    string tmp2 = tmpr.Replace("#ColumnName#", listColumnNames[i]).Replace("#TableName#", tableName).Replace("^##^", "");
                    res += tmp2;
                    continue;
                }
                string tmp = tmpRes.Replace("#TableName#", tableName).Replace("#ColumnName#", listColumnNames[i]).Replace("^##^", "");
                res += tmp;
            }
            tmpRes = @"
^##^                list.Add(result);
^##^            }
^##^            sr.DataList = list;
^##^            return sr;
^##^        }
^##^            
^##^        /// <summary>
^##^        /// 返回全部数据DataSet
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sFilter"">查询条件，以AND开头,可以为空</param>
^##^        /// <param name=""sOrderColumn"">排序字段，可以指定多个字段，以','分隔</param>
^##^        /// <param name=""sOrderType"">排序类型 ASC/DESC</param>
^##^        /// <param name=""sOrderStr"">排序字符串</param>
^##^        /// <returns>返回全部数据DataSet</returns>
^##^        public DataSet Search(IDbAccess iDb,string sFilter,string sOrderColumn,string sOrderType,string sOrderStr)
^##^        {
^##^            string sOrderString = """";
^##^
^##^			if (!string.IsNullOrWhiteSpace(sOrderStr))
^##^            {
^##^                sOrderString = sOrderStr;
^##^            }
^##^            else if(!string.IsNullOrWhiteSpace(sOrderColumn))
^##^			{
^##^			    if (sOrderColumn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) != null && sOrderColumn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length > 1)
^##^			    {
^##^				    string[] orderClomuns = sOrderColumn.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
^##^				    string[] orderTypes = sOrderType.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
^##^				    for (int i = 0; i < orderClomuns.Length; i++)
^##^				    {
^##^					    if (i == 0)
^##^					    {
^##^						    string orderType = ""ASC"";
^##^						    if (!(orderTypes == null || string.IsNullOrWhiteSpace(orderTypes[i])))
^##^						    {
^##^							    orderType = orderTypes[i];
^##^						    }
^##^						    sOrderString += "" ORDER BY "" + orderClomuns[i] + "" "" + orderType;
^##^					    }
^##^					    else
^##^					    {
^##^						    string orderType = ""ASC"";
^##^						    if (!(orderTypes == null || orderTypes.Length < i || string.IsNullOrWhiteSpace(orderTypes[i])))
^##^						    {
^##^							    orderType = orderTypes[i];
^##^						    }
^##^						    sOrderString += "", "" + orderClomuns[i] + "" "" + orderType;
^##^					    }
^##^				    }
^##^
^##^			    }else{
^##^				    sOrderString += "" ORDER BY "" + sOrderColumn + "" "" + sOrderType;
^##^			    }
^##^			}

^##^            sFilter = "" 1=1 "" + sFilter;
^##^            string strSql = ""SELECT #strColumnNames# FROM #TableName# t WHERE "" + sFilter + sOrderString;
^##^            DataSet ds = iDb.GetDataSet(strSql);
^##^            return ds;
^##^        }
^##^    }
}
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).
                Replace("#strColumnNames#", strColumnNames).
                Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            return res;
        }
    }
}
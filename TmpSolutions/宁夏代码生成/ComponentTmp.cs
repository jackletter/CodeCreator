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

namespace CodeCreator_NX
{
    public class ComponentTmp
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
            string strColumnNames = "";
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
//类型：数据操作
//***************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using GS.DataBase;
using LandSite.ProPlan.Models;
using System.Data;


namespace LandSite.ProPlan.Components
{
";
            tmpRes = tmpRes.Replace("#instruction#", "自动生成代码");
            tmpRes = tmpRes.Replace("#Now#", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            res += tmpRes;
            tmpRes = @"
^##^    /// <summary>
^##^    /// 表[#TableName#]存取
^##^    /// 关键字段为[#PrimaryKey#]
^##^    /// </summary>
^##^    public class #TableName#
^##^    {
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#PrimaryKey#", PrimaryKey);
            res += tmpRes.Replace("^##^", "");
            tmpRes = @"
^##^            /// <summary>
^##^            /// 获取一行记录
^##^            /// </summary>
^##^            /// <param name=""iDb"">数据库对象</param>
^##^            /// <param name=""sFilter"">条件字符串，以AND开头，不能为空</param>
^##^            /// <returns></returns>
^##^            public #TableName#Models Get(IDbAccess iDb, string sFilter)
^##^            {
^##^                if (string.IsNullOrWhiteSpace(sFilter))
^##^                {
^##^                    throw new Exception(""条件字符串不能为空"");
^##^                }
^##^                #TableName#Models result = new #TableName#Models();
^##^                DataSet ds = iDb.GetDataSet(""SELECT #strColumnNames# FROM #TableName# WHERE 1=1 "" + sFilter);
^##^                if (ds.Tables[0].Rows.Count == 1)
^##^                {";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#strColumnNames#", strColumnNames);
            res += tmpRes.Replace("^##^", "");
            tmpRes = @"
^##^                    result.#ColumnName# = ds.Tables[0].Rows[0][#TableName#Models.Column.#ColumnName#].ToString();";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnTypes[i].ToUpper() == "datetime".ToUpper())
                {
                    string tmpr = @"
^##^                    result.#ColumnName# = ds.Tables[0].Rows[0][#TableName#Models.Column.#ColumnName#].ToString().Replace(""/"",""-"").Replace("" 0:00:00"","""");";
                    string tmp2 = tmpr.Replace("#ColumnName#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap)).Replace("#TableName#", tableName).Replace("^##^", "");
                    res += tmp2;
                    continue;
                }
                string tmp = tmpRes.Replace("#ColumnName#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap)).Replace("#TableName#", tableName).Replace("^##^", "");
                res += tmp;
            }
            tmpRes = @"
^##^                    return result;
^##^                }				
^##^                return null;
^##^            }

^##^        /// <summary>
^##^        /// 插入新记录
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""result"">表对象</param>
^##^        /// <returns>保存是否成功</returns>
^##^        public bool Add(IDbAccess iDb, #TableName#Models result)
^##^        {
^##^
^##^            Hashtable ht = new Hashtable();
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            tmpRes = @"
^##^            if (result.#ColumnName# != null)
^##^            {
^##^                if(result.#ColumnName#==""""){ht.Add(#TableName#Models.Column.#ColumnName#, DBNull.Value);}else{ht.Add(#TableName#Models.Column.#ColumnName#, result.#ColumnName#);}
^##^            }
";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnIsIdentities[i]) { continue; }
                string tmp = tmpRes.Replace("#TableName#", tableName).Replace("#ColumnName#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap)).Replace("^##^", "");
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
^##^            if (string.IsNullOrWhiteSpace(result.#PrimaryKey#.ToString()))
^##^            {
^##^                throw new Exception(""关键字的值不能为空"");
^##^            }
^##^                
^##^            Hashtable ht = new Hashtable();
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            tmpRes = @"
^##^            if (result.#ColumnName# != null)
^##^            {
^##^                if(result.#ColumnName#==""""){ht.Add(#TableName#Models.Column.#ColumnName#, DBNull.Value);}else{ht.Add(#TableName#Models.Column.#ColumnName#, result.#ColumnName#);}
^##^            }
";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnIsIdentities[i]) { continue; }
                string tmp = tmpRes.Replace("#TableName#", tableName).Replace("#ColumnName#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap)).Replace("^##^", "");
                res += tmp;
            }

            tmpRes = @"
^##^            return iDb.UpdateData2(""#TableName#"", ht, "" #PrimaryKey#='""+result.#PrimaryKey#+""'"");
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
^##^            return iDb.DeleteTableRow(""#TableName#"","" 1=1 ""+ sFilter);
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
^##^            string strSql = ""select count(*) from #TableName# t where 1=1 "" + sFilter;
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
^##^        public List<#TableName#Models> SearchList(IDbAccess iDb,string sFilter,string sOrderColumn,string sOrderType)
^##^        {
^##^            return SearchListByPage(iDb, ""#PrimaryKey#"", 0, 0, sFilter, sOrderColumn, sOrderType).ListResult;
^##^        }
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
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
^##^        public SearchListResult<#TableName#Models> SearchListByPage(IDbAccess iDb, string sKey, int iPageSize, int iPageIndex, string sFilter, string sOrderColumn, string sOrderType)
^##^        {
^##^            SearchListResult<#TableName#Models> slr = new SearchListResult<#TableName#Models>();
^##^            slr.Count =  GetDataCount(iDb,sFilter);
^##^            string sOrderString = """";
^##^            if (sOrderColumn != """"&&sOrderColumn != null)
^##^            {
^##^                sOrderString = "" ORDER BY ""+sOrderColumn;
^##^                if (sOrderType.ToLower() == ""desc"")
^##^                {
^##^                    sOrderString += "" DESC "";
^##^                }
^##^                else
^##^                {
^##^                    sOrderString += "" ASC "";
^##^                }
^##^            }
^##^            sFilter = "" 1=1 "" + sFilter;
^##^            string strSql = ""SELECT #strColumnNames# FROM #TableName# t WHERE "" + sFilter + sOrderString;
^##^            if (iPageSize > 0)
^##^            {
^##^                strSql = iDb.GetSqlForPageSize(""#TableName#"", ""#PrimaryKey#"", iPageSize, iPageIndex, sFilter, sOrderString);
^##^            }
^##^            DataSet ds = iDb.GetDataSet(strSql);
^##^            List<#TableName#Models> list = new List<#TableName#Models>();
^##^            for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
^##^            {
^##^                #TableName#Models result = new #TableName#Models();
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#strColumnNames#", strColumnNames).Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            tmpRes = @"
^##^                result.#ColumnName# = ds.Tables[0].Rows[ii][#TableName#Models.Column.#ColumnName#].ToString();
";
            for (int i = 0; i < listColumnNames.Count; i++)
            {
                if (listColumnTypes[i].ToUpper() == "datetime".ToUpper())
                {
                    string tmpr = @"
^##^                result.#ColumnName# = ds.Tables[0].Rows[ii][#TableName#Models.Column.#ColumnName#].ToString().Replace(""/"",""-"").Replace("" 0:00:00"","""");";
                    string tmp2 = tmpr.Replace("#ColumnName#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap)).Replace("#TableName#", tableName).Replace("^##^", "");
                    res += tmp2;
                    continue;
                }
                string tmp = tmpRes.Replace("#TableName#", tableName).Replace("#ColumnName#", c.GetSpell(listColumnNames[i], ConvertModel.FirstCap)).Replace("^##^", "");
                res += tmp;
            }
            tmpRes = @"
^##^                list.Add(result);
^##^            }
^##^            slr.ListResult = list;
^##^            return slr;
^##^        }
^##^            
^##^        /// <summary>
^##^        /// 返回全部数据DataSet
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sFilter"">查询条件，以AND开头,可以为空</param>
^##^        /// <param name=""sOrderColumn"">排序字段，可以指定多个字段，以','分隔</param>
^##^        /// <param name=""sOrderType"">排序类型 ASC/DESC</param>
^##^        /// <returns>返回全部数据DataSet</returns>
^##^        public DataSet Search(IDbAccess iDb,string sFilter,string sOrderColumn,string sOrderType)
^##^        {
^##^            string sOrderString = """";
^##^            if (sOrderColumn != """")
^##^            {
^##^                sOrderString = "" ORDER BY ""+sOrderColumn;
^##^                if (sOrderType.ToLower() == ""desc"")
^##^                {
^##^                    sOrderString += "" DESC "";
^##^                }
^##^                else
^##^                {
^##^                    sOrderString += "" ASC "";
^##^                }
^##^            }
^##^            sFilter = "" 1=1 "" + sFilter;
^##^            string strSql = ""SELECT #strColumnNames# FROM #TableName# t WHERE "" + sFilter + sOrderString;
^##^            DataSet ds = iDb.GetDataSet(strSql);
^##^            return ds;
^##^        }
^##^            
^##^        /// <summary>
^##^        /// 返回分页数据DataSet
^##^        /// </summary>
^##^        /// <param name=""iDb"">数据库对象</param>
^##^        /// <param name=""sKey"">表关键字</param>
^##^        /// <param name=""iPageSize"">单页记录行数</param>
^##^        /// <param name=""iPageIndex"">分页索引</param>
^##^        /// <param name=""sFilter"">查询条件，以AND开头,可以为空</param>
^##^        /// <param name=""sOrderColumn"">排序字段，可以指定多个字段，以','分隔</param>
^##^        /// <param name=""sOrderType"">排序类型 ASC/DESC</param>
^##^        /// <returns>返回分页数据DataSet</returns>
^##^        public SearchResult SearchByPage(IDbAccess iDb, string sKey, int iPageSize, int iPageIndex, string sFilter, string sOrderColumn, string sOrderType)
^##^        {
^##^            SearchResult sr = new SearchResult();
^##^            sr.Count = GetDataCount(iDb,sFilter);
^##^            string sOrderString = """";
^##^            if (sOrderColumn != """")
^##^            {
^##^                sOrderString = "" ORDER BY ""+sOrderColumn;
^##^                if (sOrderType.ToLower() == ""desc"")
^##^                {
^##^                    sOrderString += "" DESC "";
^##^                }
^##^                else
^##^                {
^##^                    sOrderString += "" ASC "";
^##^                }
^##^            }
^##^            sFilter = "" 1=1 "" + sFilter;
^##^            string strSql = ""SELECT #strColumnNames# FROM #TableName# t WHERE "" + sFilter + sOrderString;
^##^            if (iPageSize > 0)
^##^            {
^##^                strSql = iDb.GetSqlForPageSize(""#TableName#"", ""#PrimaryKey#"", iPageSize, iPageIndex, sFilter, sOrderString);
^##^            }
^##^            DataSet ds = iDb.GetDataSet(strSql);
^##^            sr.TableResult = ds;
^##^            return sr;
^##^        }
^##^    }
^##^}
";
            tmpRes = tmpRes.Replace("#TableName#", tableName).Replace("#strColumnNames#", strColumnNames).Replace("#PrimaryKey#", PrimaryKey).Replace("^##^", "");
            res += tmpRes;
            return res;
        }
    }
}

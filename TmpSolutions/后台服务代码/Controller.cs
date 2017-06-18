//#import system.dll
//#import system.core.dll
//#import system.data.dll
//#import system.xml.dll
//#import system.configuration.dll
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CodeCreator
{
    public class Server
    {
        public string Create(Hashtable ht)
        {
            string tableName = ht["TableName"].ToString();
            List<string> listColumnNames = (List<string>)ht["listColumnNames"];
            List<string> listColumnTypes = (List<string>)ht["listColumnTypes"];
            List<string> listColumnInstructions = (List<string>)ht["listColumnInstructions"];
            string PrimaryKey = ht["PrimaryKey"].ToString();
            string TableNameClearPrefix = tableName.Contains("_") ? (tableName.IndexOf('_') > tableName.Length - 1) ? tableName : tableName.Substring(tableName.IndexOf('_') + 1) : tableName;
            char[] chars = TableNameClearPrefix.ToLower().ToCharArray();
            chars[0] = chars[0].ToString().ToUpper().First();
            string TableNameClearPrefixFormat = new string(chars);
            string res = "";
            string tmpRes = "";
            tmpRes = @"
package com.kingtopware.controller.onemap;

import javax.annotation.Resource;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.kingtopware.controller.BaseController;
import com.kingtopware.entity.onemap.{0}Entity;
import com.kingtopware.service.onemap.{0}Service;

@RestController
@RequestMapping(value = ""/{1}"")
public class {0}Controller extends BaseController<{0}Entity> {{
	@Resource
	public {0}Service srv;

	@Override
	public void getBaseSrv() {{
		if (baseSrv == null)
			baseSrv = srv;
	}}
}}";
            res += string.Format(tmpRes, TableNameClearPrefixFormat, TableNameClearPrefixFormat.ToLower()).Replace("^##^", "");
            return res;
        }

        public string UpperFirst(string colname)
        {
            colname = colname.ToLower();
            char[] chars = colname.ToCharArray();
            chars[0] = chars[0].ToString().ToUpper().First();
            colname = new string(chars);
            return colname;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DBUtil;

namespace CodeCreator
{
    public class DataTransfer
    {
        public static IDbAccess iDb = null;
        public static Object data = null;
        public static ConnectConf connnect = null;
        public static string TableName = "";
    }
}

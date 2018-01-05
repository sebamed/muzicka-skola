using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuzickaSkola
{
    class Connection
    {
        public static SqlConnection getConnection()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = @"SEBAMED-PC\SQLEXPRESS";
            scsb.InitialCatalog = @"sebamed";
            scsb.IntegratedSecurity = true;

            return new SqlConnection(scsb.ToString());
        }

    }
}

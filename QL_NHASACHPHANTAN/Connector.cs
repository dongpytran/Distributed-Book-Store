using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_NHASACHPHANTAN
{
    class Connector
    {
        public string server { get; set; }
        public string db_name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        SqlConnection conn = new SqlConnection();
        public Connector() {
        }
        public Connector(string srv, string db, string user, string pass) {
            conn.ConnectionString =
              "Data Source=" + srv + ";" +
              "Initial Catalog=" + db + ";" +
              "User id=" + user + ";" +
              "Password=" + pass + ";";
        }
        public void openConnect() {

            conn.Open();
        }
        public void closeConnect() {
            conn.Close();
        }
        public DataTable excuteProcReturnTable(string query) {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        public SqlCommand excuteProcC(string nameProc) {
            SqlCommand cmd = new SqlCommand(nameProc, conn);
            return cmd;
            /*DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);*/
        }
    }
}

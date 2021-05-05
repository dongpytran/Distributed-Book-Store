using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NHASACHPHANTAN
{
    public partial class Setting : Form
    {
        public static string srvgoc;
        public static string srvcn1;
        public static string srvcn2;
        public static string username;
        public static string password;
        public Setting()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            loadServer();
        }
        private void loadServer() {
            textBox1.Text = "sa";
            string myServer = Environment.MachineName;

            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
            for (int i = 0; i < servers.Rows.Count; i++)
            {
                if (myServer == servers.Rows[i]["ServerName"].ToString()) ///// used to get the servers in the local machine////
                {
                    if ((servers.Rows[i]["InstanceName"] as string) != null)
                    {
                        comboBox1.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                        comboBox2.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                        comboBox3.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                    }
                    else
                    {
                        comboBox1.Items.Add(servers.Rows[i]["ServerName"].ToString());
                        comboBox2.Items.Add(servers.Rows[i]["ServerName"].ToString());
                        comboBox3.Items.Add(servers.Rows[i]["ServerName"].ToString());
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Nhập đầy đủ!");
            }
            else {
                localServer svgoc = new localServer(comboBox1.SelectedItem.ToString().Trim(), "TT");
                localServer svcn1 = new localServer(comboBox2.SelectedItem.ToString().Trim(), "CN1");
                localServer svcn2 = new localServer(comboBox3.SelectedItem.ToString().Trim(), "CN2");
                username = textBox1.Text.Trim();
                password = textBox2.Text.Trim();
                List<localServer> lst = new List<localServer>();
                lst.Add(svgoc);
                lst.Add(svcn1);
                lst.Add(svcn2);
                Connector con = new Connector(svgoc.svname.Trim(), "QL_NHASACH", username, password);
                try
                {
                    con.openConnect();
                    SqlCommand cmd = con.excuteProcC("sp_DeleteConfigDb");
                    cmd.CommandText = "sp_DeleteConfigDb";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ee) {
                    MessageBox.Show(ee.Message);
                }

                try {
                    SqlCommand cmd2 = con.excuteProcC("sp_AddConfig");
                    cmd2.CommandText = "sp_AddConfig";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    for (int i = 0; i < lst.Count; i++)
                    {
                        cmd2.Parameters.Clear();
                        cmd2.Parameters.Add(new SqlParameter("@srv", SqlDbType.NVarChar)).Value = lst[i].svname.Trim();
                        cmd2.Parameters.Add(new SqlParameter("@user_name", SqlDbType.NVarChar)).Value = username;
                        cmd2.Parameters.Add(new SqlParameter("@pass", SqlDbType.NVarChar)).Value = password;
                        cmd2.Parameters.Add(new SqlParameter("@macn", SqlDbType.NChar)).Value = lst[i].macn.Trim();


                        cmd2.ExecuteNonQuery();

                    }
                    MessageBox.Show("Lưu thay đổi thành công!");
                    srvgoc = svgoc.svname.Trim();
                    srvcn1 = svcn1.svname.Trim();
                    srvcn2 = svcn2.svname.Trim();
                    con.closeConnect();
                    Login l = new Login();
                    l.Show();
                    this.Visible = false;
                }
                catch (Exception ee) {
                    MessageBox.Show(ee.Message);
                }
            }
        }
    }
}

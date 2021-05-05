using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NHASACHPHANTAN
{
    public partial class Login : Form
    {
        public static string svname = Setting.srvgoc;
        public static string svcn1 = Setting.srvcn1;
        public static string svcn2 = Setting.srvcn2;

        public static string username = Setting.username;
        public static string pass = Setting.password;
        public static string MACHINHANH = "";
        public static string MACHUCVU = "";
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString =
              "Data Source="+svname+";" +
              "Initial Catalog=QL_NHASACH;" +
              "User id="+username+";" +
              "Password="+pass+";";
            conn.Open();
            DataTable dt = new DataTable();
            string getSite = "SELECT MACHINHANH, TENCHINHANH FROM CHI_NHANH";
            SqlCommand cmd = new SqlCommand(getSite, conn);
            SqlDataAdapter da =  new SqlDataAdapter(cmd);
            da.Fill(dt);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "TENCHINHANH";
            comboBox1.ValueMember = "MACHINHANH";
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MACHINHANH = comboBox1.SelectedValue.ToString().Trim();


            string user_name = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();

            if (MACHINHANH.Equals("CN1"))
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString =
                  "Data Source="+ svcn1+";" +
                  "Initial Catalog=QL_NHASACH;" +
                  "User id="+username+";" +
                  "Password="+pass+";";
                string query = "SELECT MANV, SDT FROM QL_NHASACH.DBO.NHAN_VIEN N WHERE N.MANV = '" + user_name + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0]["SDT"].ToString().Trim().Equals(password))
                    {
                        MainForm m = new MainForm();
                        m.Visible = true;
                        this.Visible = false;
                        Connector con = new Connector(svcn1, "QL_NHASACH",username,pass);
                        try
                        {
                            con.openConnect();
                            SqlCommand cmd2 = con.excuteProcC("sp_getChucVuTuNhanVien");
                            cmd2.CommandText = "sp_getChucVuTuNhanVien";
                            cmd2.Parameters.Add(new SqlParameter("@manv", SqlDbType.NChar)).Value = user_name;
                            cmd2.CommandType = CommandType.StoredProcedure;
                            DataTable dt2 = new DataTable();
                            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            da2.Fill(dt2);
                            string cv= (from DataRow dr in dt2.Rows
                                      select (string)dr["MACHUCVU"]).FirstOrDefault();
                            MACHUCVU = cv.Trim();
                            con.closeConnect();
                        }
                        catch (Exception ee) {
                            MessageBox.Show(ee.Message);
                        }

                        MessageBox.Show("Đăng nhập thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản của bạn không thể đăng nhập ở chi nhánh này!");
                }
                conn.Close();
            }
            else {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString =
                  "Data Source="+svcn2+";" +
                  "Initial Catalog=QL_NHASACH;" +
                  "User id="+username+";" +
                  "Password="+pass+";";
                string query = "SELECT MANV, SDT FROM QL_NHASACH.DBO.NHAN_VIEN N WHERE N.MANV = '" + user_name + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0]["SDT"].ToString().Trim().Equals(password))
                    {
                        MainForm m = new MainForm();
                        m.Visible = true;
                        this.Visible = false;
                        Connector con = new Connector(svcn2, "QL_NHASACH", username, pass);
                        try
                        {
                            con.openConnect();
                            SqlCommand cmd2 = con.excuteProcC("sp_getChucVuTuNhanVien");
                            cmd2.CommandText = "sp_getChucVuTuNhanVien";
                            cmd2.Parameters.Add(new SqlParameter("@manv", SqlDbType.NChar)).Value = user_name;
                            cmd2.CommandType = CommandType.StoredProcedure;
                            DataTable dt2 = new DataTable();
                            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            da2.Fill(dt2);
                            string cv = (from DataRow dr in dt2.Rows
                                         select (string)dr["MACHUCVU"]).FirstOrDefault();
                            MACHUCVU = cv.Trim();
                            con.closeConnect();
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message);
                        }
                        MessageBox.Show("Đăng nhập thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản của bạn không thể đăng nhập ở chi nhánh này!");
                }
                conn.Close();
            }
                
            }

        private void button3_Click(object sender, EventArgs e)
        {
        }
    }
}

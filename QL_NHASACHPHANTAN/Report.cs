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
    public partial class Report : Form
    {
        public string srv = "";
        public int reCn1;
        public int reCn2;
        public int sl1;
        public int sl2;

        public static string srvcn1 = Login.svcn1;
        public static string srvcn2 = Login.svcn2;
        public static string d_username = Login.username;
        public static string d_pass = Login.pass;
        public Report()
        {
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            loadReportDoanhThu();
            loadReportNv();
            
        }
        private void loadReportDoanhThu() {
            if (MainForm.server.ToString().Trim() == "CN2")
            {
                srv = srvcn2;
            }
            else
            {
                srv = srvcn1;
            }
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            string query1 = "sp_getRevenueThisSite";
            string query2 = "sp_getRevenueOtherSite";
            SqlCommand cmd = new SqlCommand();

            try
            {
                con.openConnect();
                cmd = con.excuteProcC(query1);
                cmd.CommandText = query1;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reCn1 = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                cmd = con.excuteProcC(query2);
                cmd.CommandText = query2;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reCn2 = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();

                //
                chart1.Series.Add(MainForm.server.ToString().Trim());
                chart1.Series.Add("ALL");
                chart1.Titles.Add("Tổng Doanh Thu");
                chart1.Series[MainForm.server.ToString().Trim()].Points.AddXY(MainForm.server.ToString().Trim(), reCn1);
                chart1.Series["ALL"].Points.AddXY("ALL", reCn1 + reCn2);

                //
                label1.Text = "DOANH THU CHI NHÁNH " + MainForm.server.ToString().Trim();
                dtcn.Text = reCn1.ToString().Trim() + " VND";
                dtht.Text = (reCn1 + reCn2).ToString().Trim() + " VND";
                //
                con.closeConnect();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void loadReportNv() {
            if (MainForm.server.ToString().Trim() == "CN2")
            {
                srv = srvcn2;
            }
            else
            {
                srv = srvcn1;
            }
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            string query1 = "sp_getCountNvThisSite";
            string query2 = "sp_getCountNvOtherSite";
            SqlCommand cmd = new SqlCommand();

            try
            {
                con.openConnect();
                cmd = con.excuteProcC(query1);
                cmd.CommandText = query1;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sl1 = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                cmd = con.excuteProcC(query2);
                cmd.CommandText = query2;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sl2 = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();

                //
                chart2.Series.Add(MainForm.server.ToString().Trim());
                chart2.Series.Add("ALL");
                chart2.Titles.Add("Số Lượng Nhân Viên");
                chart2.Series[MainForm.server.ToString().Trim()].Points.AddXY(MainForm.server.ToString().Trim(), sl1);
                chart2.Series["ALL"].Points.AddXY("ALL", sl1 + sl2);

                //
                slnvText.Text = "SỐ LƯỢNG NHÂN VIÊN CHI NHÁNH " + MainForm.server.ToString().Trim();
                slnvcn.Text = sl1.ToString().Trim()+ " NV";
                slnvht.Text = (sl1 + sl2).ToString().Trim()+ " NV";
                con.closeConnect();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}

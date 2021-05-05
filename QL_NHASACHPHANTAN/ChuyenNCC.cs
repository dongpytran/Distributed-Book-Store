using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NHASACHPHANTAN
{
    public partial class ChuyenNCC : Form
    {
        public static string d_username = NhaCungCap.d_username;
        public static string d_pass = NhaCungCap.d_pass;
        public string srv = NhaCungCap.srv;
        public string selectedCn { get; set; } 
        public ChuyenNCC()
        {
            InitializeComponent();
        }

        private void ChuyenNCC_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox1.Text = NhaCungCap.ma;
            string cn = MainForm.server.ToString().Trim();
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            con.openConnect();
            string query = "EXEC	[dbo].[sp_OthersChiNhanh] @macn = N'"+cn+"'";

            DataTable dt = con.excuteProcReturnTable(query);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "TENCHINHANH";
            comboBox1.ValueMember = "MACHINHANH";

            con.closeConnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedCn = comboBox1.SelectedValue.ToString().Trim();
        }
    }
}

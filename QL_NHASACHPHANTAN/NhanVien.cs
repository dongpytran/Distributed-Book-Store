using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_NHASACHPHANTAN
{
    public partial class NhanVien : Form
    {
        public static string srvcn1 = Login.svcn1;
        public static string srvcn2 = Login.svcn2;
        public static string d_username = Login.username;
        public static string d_pass = Login.pass;


        public static string srv = "";
        public static string ma;

        public static string macv = Login.MACHUCVU;
        public NhanVien()
        {
            InitializeComponent();
        }

        private void loadGrid(string srvs) {
            Connector con = new Connector(srvs, "QL_NHASACH", d_username, d_pass);
            con.openConnect();
            string query = "EXEC	[dbo].[sp_NhanVien] @manv = NULL,@tennv = NULL,"
            + "@sdt = NULL,@email = NULL,@diachi = NULL,@macv = NULL,@macn = NULL, @tgvaolam = NULL, @StatementType = N'SELECT'";

            DataTable dt = con.excuteProcReturnTable(query);

            //Binding to dataGridview
            dataGridView1.DataSource = dt;
            con.closeConnect();
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            comboBox2.Enabled = false;
            textBox1.Enabled = false;
            dataGridView1.MultiSelect = false;
            //LOAD GRIDVIEW
            if (MainForm.server.ToString().Trim() == "CN2") {
                srv = srvcn2;
            }
            else
            {
                srv = srvcn1;
            }
            loadGrid(srv);
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            con.openConnect();

            //LOAD COMBOBOX CHINHANH
            string query2 = "EXEC sp_getChiNhanh";
            DataTable dtt = con.excuteProcReturnTable(query2);
            comboBox2.DataSource = dtt;
            comboBox2.DisplayMember = "TENCHINHANH";
            comboBox2.ValueMember = "MACHINHANH";


            //LOAD COMBOBOX CHUC VU
            string query3 = "EXEC sp_getChucVu";
            DataTable dt2 = con.excuteProcReturnTable(query3);
            comboBox1.DataSource = dt2;
            comboBox1.DisplayMember = "TENCHUCVU";
            comboBox1.ValueMember = "MACHUCVU";
            //Close cn
            con.closeConnect();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //KT MA NV TRUNG
            string manv = textBox1.Text.Trim();
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            con.openConnect();
            string query = "DECLARE @kq int exec @kq = sp_KiemTraMaNhanVien @manv = N'"+ manv +"'"+ " SELECT 'KQ' = @kq";
            DataTable dt = con.excuteProcReturnTable(query);
            
            int check = dt.Rows[0].Field<int>("KQ");
            if (check == 1)
            {
                MessageBox.Show("Nhân viên này đã tồn tại trong hệ thống!");
                return;
            }//THEM NV
            else {
                string ma = textBox1.Text.Trim();
                string ten = textBox2.Text.Trim();
                string sdt = textBox3.Text.Trim();
                string email = textBox4.Text.Trim();
                string dc = textBox5.Text.Trim();
                string chucvu = comboBox1.SelectedValue.ToString();
                string cn = comboBox2.SelectedValue.ToString();
                string ngay = dateTimePicker1.Text;

                string query2 = "DECLARE @return_value int EXEC [dbo].[sp_NhanVien] @manv = '" + ma+"'"+", @tennv = N'"+ten+"'"+", @sdt = '"+sdt+"'"+", @email = N'"+email+"'"+","
		    +"@diachi = N'"+ dc +"'"+", @macv = '"+chucvu+"'"+", @macn = '"+cn+"'"+", @tgvaolam = '"+ngay+"'"+ ", @StatementType = N'INSERT' SELECT	'KQ' = @return_value";
                
                try {
                    con.excuteProcReturnTable(query2);
                    MessageBox.Show("Thêm Nhân Viên: " + ma + " thành công!");
                    loadGrid(srv);
                }catch(Exception ee){
                    MessageBox.Show("Something went wrong!" + ee.Message);
                }
            }
           
            con.closeConnect();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Enabled = false;
            if (e.RowIndex == (dataGridView1.Rows.Count - 1) || e.RowIndex == -1) {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                dateTimePicker1.Value = DateTime.Now;
            }
            else  {
                DataGridViewRow dr = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = dr.Cells[0].Value.ToString();
                textBox2.Text = dr.Cells[1].Value.ToString();
                textBox3.Text = dr.Cells[2].Value.ToString();
                textBox4.Text = dr.Cells[3].Value.ToString();
                textBox5.Text = dr.Cells[4].Value.ToString();

                string mcv = dr.Cells[5].Value.ToString().Trim();
                comboBox1.SelectedValue = mcv;
                DateTime dt = DateTime.ParseExact(dr.Cells[7].Value.ToString().Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dateTimePicker1.Value = dt;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && textBox1.Enabled ==false && textBox1.Text != "" && dataGridView1.CurrentCell.RowIndex != dataGridView1.Rows.Count -1) {
                string manv = textBox1.Text.Trim();
                string tennv = textBox2.Text.Trim();
                string sdt = textBox3.Text.Trim();
                string email = textBox4.Text.Trim();
                string diachi = textBox5.Text.Trim();
                string macv = comboBox1.SelectedValue.ToString().Trim();
                string macn = comboBox2.SelectedValue.ToString().Trim();
                string tg = dateTimePicker1.Text.ToString().Trim();
                Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
                try {
                    con.openConnect();
                    string qr = "DECLARE	@return_value int EXEC	@return_value = [dbo].[sp_NhanVien] @manv = '" + manv + "'" + ", @tennv = N'" + tennv + "'" + ", " +
                    "@sdt = '" + sdt + "'" + ",@email = N'" + email + "'" + ", @diachi = N'" + diachi + "'" + ", @macv = '" + macv + "'" + ", @macn = '" + macn + "'" + ", @tgvaolam = '" + tg + "'" + ", @StatementType = N'UPDATE' SELECT 'KQ' = @return_value";
                    con.excuteProcReturnTable(qr);
                    MessageBox.Show("Cập nhật thành công thông tin nhân viên: "+ manv);
                    loadGrid(srv);
                }
                catch {
                    MessageBox.Show("Somethings went wrong!");
                }
            }
            else
            {
                MessageBox.Show("Chọn 1 nhân viên!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && textBox1.Enabled == false && textBox1.Text != "" && dataGridView1.CurrentCell.RowIndex != dataGridView1.Rows.Count - 1)
            {
                string manv = textBox1.Text.Trim();
                Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
                try
                {
                    con.openConnect();
                    string qr = "DECLARE	@return_value int EXEC @return_value = [dbo].[sp_NhanVien] @manv = N'" + manv + "'" + ", @tennv = NULL, @sdt = NULL, @email = NULL, " +
                        "@diachi = NULL, @macv = NULL, @macn = NULL, @tgvaolam = NULL, @StatementType = N'DELETE' SELECT  'Return Value' = @return_value";
                    DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        con.excuteProcReturnTable(qr);
                        MessageBox.Show("Xoá thành công nhân viên: " + manv);
                        loadGrid(srv);
                    }
                    else
                    {
                        return;
                    }
                }
                catch(Exception ee)
                {
                    MessageBox.Show("Somethings went wrong!" + ee.Message);
                }
            }
            else {
                MessageBox.Show("Chọn 1 nhân viên!");
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0 && currentMouseOverRow < dataGridView1.Rows.Count - 1)
                {
                    MenuItem i = new MenuItem(string.Format("Chuyển Nhân Viên {0}", dataGridView1.CurrentCell.Value));
                    m.MenuItems.Add(i);
                    if (macv.Trim().Equals("MCV05"))
                    {
                        i.Click += new System.EventHandler(this.menuItem1_Click);
                        m.Show(dataGridView1, new Point(e.X, e.Y));
                        
                    }
                    else {
                        m.Show(dataGridView1, new Point(e.X, e.Y));
                        MessageBox.Show("Bạn không có quyền này!");
                        return;
                    }
                }

                
                
            }
        }

        void menuItem1_Click(object sender, EventArgs e)
        {
            using (ChuyenNhanVien c = new ChuyenNhanVien())
            {
                string tennv = textBox2.Text.Trim();
                string sdt = textBox3.Text.Trim();
                string email = textBox4.Text.Trim();
                string diachi = textBox5.Text.Trim();
                string macv = comboBox1.SelectedValue.ToString().Trim();
                string tg = dateTimePicker1.Text.ToString().Trim();
                ma = textBox1.Text.Trim();
                if (c.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string cn = c.macn.ToString().Trim();

                    //

                    Connector con = new Connector(srv, "QL_NHASACH", "sa", "123");
                    con.openConnect();
                    string query = "DECLARE @kq int exec @kq = sp_CheckMaNvTrongHoaDon @manv = N'" + ma + "'" + " SELECT 'KQ' = @kq";
                    DataTable dt = con.excuteProcReturnTable(query);
                    int check = dt.Rows[0].Field<int>("KQ");
                    if (check == 1)
                    {
                        MessageBox.Show("Không thể chuyển vì đang tham chiếu!");
                        con.closeConnect();
                        return;
                    }
                    else
                    {
                        try
                        {
                            SqlCommand cmd = con.excuteProcC("sp_ChuyenNhanVien");
                            cmd.CommandText = "sp_ChuyenNhanVien";
                            cmd.Parameters.Add(new SqlParameter("@manv", SqlDbType.NChar)).Value = ma;
                            cmd.Parameters.Add(new SqlParameter("@tennv", SqlDbType.NVarChar)).Value = tennv;
                            cmd.Parameters.Add(new SqlParameter("@sdt", SqlDbType.NChar)).Value = sdt;
                            cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar)).Value = email;
                            cmd.Parameters.Add(new SqlParameter("@diachi", SqlDbType.NVarChar)).Value = diachi;
                            cmd.Parameters.Add(new SqlParameter("@macv", SqlDbType.NChar)).Value = macv;
                            cmd.Parameters.Add(new SqlParameter("@macn", SqlDbType.NChar)).Value = cn;
                            cmd.Parameters.Add(new SqlParameter("@tgvaolam", SqlDbType.NVarChar)).Value = tg;
                            cmd.CommandType = CommandType.StoredProcedure;

                            //
                            cmd.ExecuteNonQuery();
                            con.closeConnect();
                            MessageBox.Show("Chuyển Nhân Viên "+ ma + " Thành Công!");
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message);
                        }
                    }
                }
            }
            loadGrid(srv);
        }
    }
}

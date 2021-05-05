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
    public partial class NhaCungCap : Form
    {
        public static string srvcn1 = Login.svcn1;
        public static string srvcn2 = Login.svcn2;
        public static string d_username = Login.username;
        public static string d_pass = Login.pass;


        public static string srv = "";
        public static string ma = "";

        public string macv = Login.MACHUCVU;
        public NhaCungCap()
        {
            InitializeComponent();
        }
        
        public void ShowNhaCungCap() {
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            con.openConnect();
            SqlCommand cmd = con.excuteProcC("sp_NhaCungCap");
            cmd.CommandText = "sp_NhaCungCap";
            cmd.Parameters.Add(new SqlParameter("@mancc", SqlDbType.NChar)).Value = "NULL";
            cmd.Parameters.Add(new SqlParameter("@tenncc", SqlDbType.NVarChar)).Value = "NULL";
            cmd.Parameters.Add(new SqlParameter("@macn", SqlDbType.NChar)).Value = "NULL";
            cmd.Parameters.Add(new SqlParameter("@StatementType", SqlDbType.NVarChar)).Value = "SELECT";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.closeConnect();
        }
        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            //

            if (MainForm.server.ToString().Trim() == "CN2")
            {
                srv = srvcn2;
            }
            else
            {
                srv = srvcn1;
            }
            string query2 = "EXEC sp_getChiNhanh";
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            con.openConnect();
            DataTable dtt = con.excuteProcReturnTable(query2);
            comboBox1.DataSource = dtt;
            comboBox1.DisplayMember = "TENCHINHANH";
            comboBox1.ValueMember = "MACHINHANH";
            ShowNhaCungCap();
            con.closeConnect();



        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string ma = textBox1.Text.Trim();
            string ten = textBox2.Text.Trim();
            string mcn = comboBox1.SelectedValue.ToString().Trim();
            //KT MA NCC TRUNG
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            con.openConnect();
            string query = "DECLARE @kq int exec @kq = sp_KiemTraMaNhaCc @MANCC = N'" + ma + "'" + " SELECT 'KQ' = @kq";
            DataTable dt = con.excuteProcReturnTable(query);
            int check = dt.Rows[0].Field<int>("KQ");
            if (check == 1)
            {
                MessageBox.Show("Nhà cung cấp này đã tồn tại trong hệ thống!");
                con.closeConnect();
                return;
            }//THEM NCC
            else
            {
                SqlCommand cmd = con.excuteProcC("sp_NhaCungCap");
                cmd.CommandText = "sp_NhaCungCap";
                cmd.Parameters.Add(new SqlParameter("@mancc", SqlDbType.NChar)).Value = ma;
                cmd.Parameters.Add(new SqlParameter("@tenncc", SqlDbType.NVarChar)).Value = ten;
                cmd.Parameters.Add(new SqlParameter("@macn", SqlDbType.NChar)).Value = mcn;
                cmd.Parameters.Add(new SqlParameter("@StatementType", SqlDbType.NVarChar)).Value = "INSERT";
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    cmd.ExecuteNonQuery();
                    con.closeConnect();
                    MessageBox.Show("Thêm Nhà Cung Cấp :" + ma + " thành công!");
                }
                catch
                {
                    MessageBox.Show("Something went wrong!");
                }
                ShowNhaCungCap();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Enabled = false;
            if (e.RowIndex == (dataGridView1.Rows.Count - 1) || e.RowIndex == -1)
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                DataGridViewRow dr = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = dr.Cells[0].Value.ToString();
                textBox2.Text = dr.Cells[1].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && textBox1.Enabled == false && textBox1.Text != "" && dataGridView1.CurrentCell.RowIndex != dataGridView1.Rows.Count - 1)
            {
                string ma = textBox1.Text.Trim();
                string ten = textBox2.Text.Trim();
                string macn = comboBox1.SelectedValue.ToString().Trim();
                Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
                try
                {
                    con.openConnect();
                    SqlCommand cmd = con.excuteProcC("sp_NhaCungCap");
                    cmd.CommandText = "sp_NhaCungCap";
                    cmd.Parameters.Add(new SqlParameter("@macn", SqlDbType.NChar)).Value = macn;
                    cmd.Parameters.Add(new SqlParameter("@mancc", SqlDbType.NChar)).Value = ma;
                    cmd.Parameters.Add(new SqlParameter("@tenncc", SqlDbType.NVarChar)).Value = ten;
                    
                    cmd.Parameters.Add(new SqlParameter("@StatementType", SqlDbType.NVarChar)).Value = "UPDATE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật nhà cung cấp: " + ma + " thành công!");
                    con.closeConnect();
                    ShowNhaCungCap();
                }
                catch
                {
                    MessageBox.Show("Somethings went wrong!");
                }
            }
            else
            {
                MessageBox.Show("Chọn 1 nhà cung cấp!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && textBox1.Enabled == false && textBox1.Text != "" && dataGridView1.CurrentCell.RowIndex != dataGridView1.Rows.Count - 1)
            {
                string ma = textBox1.Text.Trim();
                string ten = textBox2.Text.Trim();
                Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
                try
                {
                    con.openConnect();
                    SqlCommand cmd = con.excuteProcC("sp_NhaCungCap");
                    cmd.CommandText = "sp_NhaCungCap";
                    cmd.Parameters.Add(new SqlParameter("@mancc", SqlDbType.NChar)).Value = ma;
                    cmd.Parameters.Add(new SqlParameter("@tenncc", SqlDbType.NVarChar)).Value = "NULL";
                    cmd.Parameters.Add(new SqlParameter("@macn", SqlDbType.NChar)).Value = "NULL";
                    cmd.Parameters.Add(new SqlParameter("@StatementType", SqlDbType.NVarChar)).Value = "DELETE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);

                    if (dialogResult == DialogResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xoá nhà cung cấp: " + ma + " thành công!");
                        con.closeConnect();
                        ShowNhaCungCap();
                    }
                    else
                    {
                        con.closeConnect();
                        return;
                    }
                    
                }
                catch(Exception ee)
                {
                    MessageBox.Show("Somethings went wrong!" + ee.Message);
                }
            }
            else
            {
                MessageBox.Show("Chọn 1 nhà cung cấp!");
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();

                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (currentMouseOverRow >= 0 && currentMouseOverRow < dataGridView1.Rows.Count -1)
                {
                    MenuItem i = new MenuItem(string.Format("Chuyển Nhà Cung Cấp {0}", dataGridView1.CurrentCell.Value));
                    m.MenuItems.Add(i);
                    if (macv.Trim().Equals("MCV05"))
                    {
                        i.Click += new System.EventHandler(this.menuItem1_Click);
                        m.Show(dataGridView1, new Point(e.X, e.Y));

                    }
                    else
                    {
                        m.Show(dataGridView1, new Point(e.X, e.Y));
                        MessageBox.Show("Bạn không có quyền này!");
                        return;
                    }
                }

                m.Show(dataGridView1, new Point(e.X, e.Y));

            }
        }
        void menuItem1_Click(object sender, EventArgs e) {
            using (ChuyenNCC c = new ChuyenNCC()) {

                ma = textBox1.Text.Trim();
                if (c.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string cn = c.selectedCn.ToString().Trim();
                    string tenncc = textBox2.Text.Trim();



                    //

                    Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
                    con.openConnect();
                    string query = "DECLARE @kq int exec @kq = sp_KiemTraMaNhaCcTrongSach @mancc = N'" + ma + "'" + " SELECT 'KQ' = @kq";
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
                            SqlCommand cmd = con.excuteProcC("sp_ChuyenNCC");
                            cmd.CommandText = "sp_ChuyenNCC";
                            cmd.Parameters.Add(new SqlParameter("@mancc", SqlDbType.NChar)).Value = ma;
                            cmd.Parameters.Add(new SqlParameter("@tenncc", SqlDbType.NVarChar)).Value = tenncc;
                            cmd.Parameters.Add(new SqlParameter("@macn", SqlDbType.NChar)).Value = cn;
                            cmd.CommandType = CommandType.StoredProcedure;

                            //
                            cmd.ExecuteNonQuery();
                            con.closeConnect();
                            MessageBox.Show("Chuyển Nhà Cung Cấp "+ ma+" Thành Công");
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message);
                        }
                    }
                }
            }
            ShowNhaCungCap();
        }
    }
}

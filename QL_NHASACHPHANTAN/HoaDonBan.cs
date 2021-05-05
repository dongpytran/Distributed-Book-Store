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
    public partial class HoaDonBan : Form
    {
        public static string srvcn1 = Login.svcn1;
        public static string srvcn2 = Login.svcn2;
        public static string d_username = Login.username;
        public static string d_pass = Login.pass;
        public string srv = "";
        public HoaDonBan()
        {
            InitializeComponent();
        }
        private void loadHoaDon() {
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            try {
                con.openConnect();
                SqlCommand cmd = con.excuteProcC("sp_GetAllHoaDon");
                cmd.CommandText = "sp_GetAllHoaDon";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dtt = new DataTable();
                da.Fill(dtt);
                dataGridView1.DataSource = dtt;
                con.closeConnect();
            }
            catch (Exception ee) {
                MessageBox.Show(ee.Message);
            }

        }
        private void HoaDonBan_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            textBox5.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            textBox4.Enabled = false;
            if (MainForm.server.ToString().Trim() == "CN2")
            {
                srv = srvcn2;
            }
            else
            {
                srv = srvcn1;
            }

            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            con.openConnect();
            //COMBOBOX NHAN VIEN

            string query = "EXEC	[dbo].[sp_NhanVien] @manv = NULL,@tennv = NULL,"
            + "@sdt = NULL,@email = NULL,@diachi = NULL,@macv = NULL,@macn = NULL, @tgvaolam = NULL, @StatementType = N'SELECT'";
            
            DataTable dt = con.excuteProcReturnTable(query);
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "TENNV";
            comboBox1.ValueMember = "MANV";

            //COMBOBOX SACH

            string query2 = "EXEC sp_getAllSach";
            DataTable ds = con.excuteProcReturnTable(query2);
            comboBox2.DataSource = ds;
            comboBox2.DisplayMember = "TENSACH";
            comboBox2.ValueMember = "MASACH";

            con.closeConnect();

            //load all hd
            loadHoaDon();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Enabled = false;
            textBox1.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;

            listView1.Items.Clear();
            if (e.RowIndex == (dataGridView1.Rows.Count - 1) || e.RowIndex == -1)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            else
            {
                DataGridViewRow dr = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = dr.Cells[0].Value.ToString().Trim();
                comboBox1.SelectedValue = dr.Cells[1].Value.ToString().Trim();
                textBox4.Text = dr.Cells[2].Value.ToString().Trim();
                textBox5.Text = dr.Cells[3].Value.ToString().Trim();
                string d = dr.Cells[4].Value.ToString().Trim();
                DateTime date = DateTime.Parse(d);

                dateTimePicker1.Value = date;
                comboBox2.SelectedValue = dr.Cells[5].Value.ToString().Trim();

                textBox2.Text = dr.Cells[6].Value.ToString().Trim();
                textBox3.Text = dr.Cells[7].Value.ToString().Trim();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            textBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox5.Enabled = false;

            dateTimePicker1.Value = DateTime.Now;

            textBox2.Text = "";
            textBox3.Text = "";

            textBox5.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            textBox1.Enabled = true;
        }


        private string generateChar(int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals("") || textBox3.Text.Equals(""))
            {
                MessageBox.Show("Nhap day du!");
                return;
            }
            else
            {
                string mahd = textBox1.Text.Trim();
                string manv = comboBox1.SelectedValue.ToString().Trim();
                int max1 = 0;
                int max2 = 0;


                string ngayban = DateTime.Now.ToShortDateString();
                DateTime date = DateTime.Parse(ngayban);

                int sl = Convert.ToInt32(textBox2.Text.Trim());
                int gia = Convert.ToInt32(textBox3.Text.Trim());

                //
                Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
                con.openConnect();
                SqlCommand cmdCt = con.excuteProcC("sp_GetMaxCthdAtThisSite");
                cmdCt.CommandText = "sp_GetMaxCthdAtThisSite";
                cmdCt.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmdCt.ExecuteReader();
                while (reader.Read())
                {
                    max1 = Convert.ToInt32(reader[0].ToString());
                }
                reader.Close();
                SqlCommand cmdCt2 = con.excuteProcC("sp_GetMaxCthdAtOtherSite");
                cmdCt2.CommandText = "sp_GetMaxCthdAtOtherSite";
                cmdCt2.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader2 = cmdCt2.ExecuteReader();
                while (reader2.Read())
                {
                    max2 = Convert.ToInt32(reader2[0].ToString());
                }
                reader2.Close();
                if (listView1.Items.Count == 0)
                {

                    string masach = comboBox2.SelectedValue.ToString().Trim();
                    int macthd = 0;
                    if (max1 > max2)
                    {
                        macthd += max1 + 1;
                    }
                    else
                    {
                        macthd += max2 + 1;
                    }
                    string tong = (sl * gia).ToString();
                    try
                    {
                        //Insert hoa don
                        try
                        {
                            SqlCommand cmd = con.excuteProcC("sp_InsertHoaDon");
                            cmd.CommandText = "sp_InsertHoaDon";
                            cmd.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = mahd;
                            cmd.Parameters.Add(new SqlParameter("@manv", SqlDbType.NChar)).Value = manv;
                            cmd.Parameters.Add(new SqlParameter("@tong", SqlDbType.Int)).Value = Convert.ToInt32(tong);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message);
                        }

                        try
                        {
                            //Insert cthd
                            SqlCommand cmd2 = con.excuteProcC("sp_InsertCtHoaDon");
                            cmd2.CommandText = "sp_InsertCtHoaDon";
                            cmd2.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = mahd;
                            cmd2.Parameters.Add(new SqlParameter("@macthd", SqlDbType.NChar)).Value = macthd.ToString().Trim();
                            cmd2.Parameters.Add(new SqlParameter("@ngayban", SqlDbType.Date)).Value = date.ToString("yyyy-MM-dd");
                            cmd2.Parameters.Add(new SqlParameter("@masach", SqlDbType.NChar)).Value = masach;
                            cmd2.Parameters.Add(new SqlParameter("@sl", SqlDbType.Int)).Value = sl;
                            cmd2.Parameters.Add(new SqlParameter("@gia", SqlDbType.Int)).Value = gia;
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.ExecuteNonQuery();

                            con.closeConnect();
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message);
                        }
                        MessageBox.Show("Thêm hoá đơn: " + mahd + " thành công!");
                        loadHoaDon();


                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
                else
                {
                    List<ChiTietHd> listCtHd = new List<ChiTietHd>();
                    for (int i = listView1.Items.Count - 1; i >= 0; i--)
                    {
                        ChiTietHd ct = new ChiTietHd();
                        ct.mash = listView1.Items[i].SubItems[0].Text;
                        ct.tensh = listView1.Items[i].SubItems[1].Text;
                        ct.soluong = Convert.ToInt32(listView1.Items[i].SubItems[2].Text);
                        ct.giaban = Convert.ToInt32(listView1.Items[i].SubItems[3].Text);
                        ct.tongtien = Convert.ToInt32(listView1.Items[i].SubItems[4].Text);

                        listCtHd.Add(ct);
                    }

                    try
                    {
                        int tong = 0;
                        foreach (ChiTietHd ct in listCtHd)
                        {
                            tong += ct.tongtien;
                        }
                        //HD
                        SqlCommand cmd = con.excuteProcC("sp_InsertHoaDon");
                        cmd.CommandText = "sp_InsertHoaDon";
                        cmd.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = mahd;
                        cmd.Parameters.Add(new SqlParameter("@manv", SqlDbType.NChar)).Value = manv;
                        cmd.Parameters.Add(new SqlParameter("@tong", SqlDbType.Int)).Value = Convert.ToInt32(tong);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                        try
                        {
                            for (int i = 0; i < listCtHd.Count; i++)
                            {
                                //CTHD
                                string macthd = "";
                                if (max1 > max2)
                                {
                                    macthd = (max1 + i + 1).ToString().Trim();
                                }
                                else
                                {
                                    macthd = (max2 + i + 1).ToString().Trim();
                                }


                                SqlCommand cmd2 = con.excuteProcC("sp_InsertCtHoaDon");
                                cmd2.CommandText = "sp_InsertCtHoaDon";
                                cmd2.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = mahd;
                                cmd2.Parameters.Add(new SqlParameter("@macthd", SqlDbType.NChar)).Value = macthd;
                                cmd2.Parameters.Add(new SqlParameter("@ngayban", SqlDbType.Date)).Value = date.ToString("yyyy-MM-dd");
                                cmd2.Parameters.Add(new SqlParameter("@masach", SqlDbType.NChar)).Value = listCtHd[i].mash.ToString().Trim();
                                cmd2.Parameters.Add(new SqlParameter("@sl", SqlDbType.Int)).Value = Convert.ToInt32(listCtHd[i].soluong.ToString().Trim());
                                cmd2.Parameters.Add(new SqlParameter("@gia", SqlDbType.Int)).Value = Convert.ToInt32(listCtHd[i].giaban.ToString().Trim());
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.ExecuteNonQuery();



                            }
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message);
                        }
                        MessageBox.Show("Thêm thành công hoá đơn: " + mahd + ", " + listCtHd.Count.ToString().Trim() + " sản phẩm");
                        loadHoaDon();
                        con.closeConnect();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && textBox1.Enabled == false && textBox1.Text != "" && dataGridView1.CurrentCell.RowIndex != dataGridView1.Rows.Count - 1)
            {
                string mahd = textBox1.Text.Trim();

                //Xoa CTHD truoc
                Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
                DialogResult dialogResult = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) {
                    try
                    {
                        con.openConnect();
                        SqlCommand cmd = con.excuteProcC("sp_DeleteCtHoaDon");
                        cmd.CommandText = "sp_DeleteCtHoaDon";
                        cmd.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = mahd;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                        //Xoa Hoa Don

                        try
                        {
                            SqlCommand cmd1 = con.excuteProcC("sp_DeleteHoaDon");
                            cmd1.CommandText = "sp_DeleteHoaDon";
                            cmd1.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = mahd;
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.ExecuteNonQuery();
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(ee.Message);
                        }

                        MessageBox.Show("Xoá hoá đơn số: " + mahd + " thành công!");
                        con.closeConnect();
                        loadHoaDon();

                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                }
                else {
                    return;
                }
            }
            else {
                MessageBox.Show("Chọn 1 hoá đơn!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tien1 = 0;
            Connector con = new Connector(srv, "QL_NHASACH", d_username, d_pass);
            try {
                con.openConnect();
                try
                {
                    SqlCommand cmd2 = con.excuteProcC("sp_getThanhToanNotThis");
                    cmd2.CommandText = "sp_getThanhToanNotThis";
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = textBox1.Text.Trim();
                    cmd2.Parameters.Add(new SqlParameter("@macthd", SqlDbType.NChar)).Value = textBox5.Text.Trim();
                    SqlDataReader reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        tien1 = Convert.ToInt32(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch(Exception ee) {
                    MessageBox.Show(ee.Message);
                }
                SqlCommand cmd = con.excuteProcC("sp_UpdateHoaDon");
                cmd.CommandText = "sp_UpdateHoaDon";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@mahd", SqlDbType.NChar)).Value = textBox1.Text.Trim();
                cmd.Parameters.Add(new SqlParameter("@macthd", SqlDbType.NChar)).Value = textBox5.Text.Trim();
                cmd.Parameters.Add(new SqlParameter("@manv", SqlDbType.NChar)).Value = comboBox1.SelectedValue.ToString().Trim();
                cmd.Parameters.Add(new SqlParameter("@sl", SqlDbType.Int)).Value = Convert.ToInt32(textBox2.Text.Trim());
                cmd.Parameters.Add(new SqlParameter("@dongia", SqlDbType.Int)).Value = Convert.ToInt32(textBox3.Text.Trim());
                cmd.Parameters.Add(new SqlParameter("@tongtien", SqlDbType.Int)).Value = tien1 + (Convert.ToInt32(textBox2.Text.Trim()) * Convert.ToInt32(textBox3.Text.Trim()));

                cmd.ExecuteNonQuery();
                con.closeConnect();
                MessageBox.Show("Cập nhật hoá đơn " + textBox1.Text.Trim() + " thành công!");
                loadHoaDon();
            }
            catch (Exception ee) {
                MessageBox.Show(ee.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Equals("") || textBox3.Text.Equals(""))
            {
                MessageBox.Show("Nhap Day Du Thong Tin");
                return;
            }
            else {
                List<ChiTietHd> listCt = new List<ChiTietHd>();
                ChiTietHd ct = new ChiTietHd(comboBox2.SelectedValue.ToString().Trim(),
                    this.comboBox2.GetItemText(this.comboBox2.SelectedItem), Convert.ToInt32(textBox2.Text.Trim()), Convert.ToInt32(textBox3.Text.Trim()));
                //thêm Item vào ListView
                if (listView1.Items.Count != 0)
                {
                    if (!listView1.Items.ContainsKey(ct.mash))
                    {
                        string[] cthd = { ct.mash, ct.tensh, ct.soluong.ToString(), ct.giaban.ToString(), ct.tongtien.ToString() };
                        var listViewItem = new ListViewItem(cthd);
                        listViewItem.Name = ct.mash;
                        listView1.Items.Add(listViewItem);
                    }
                    else
                    {
                        MessageBox.Show("Sách đã tồn tại trong hoá đơn!");
                    }
                }
                else
                {
                    string[] cthd = { ct.mash, ct.tensh, ct.soluong.ToString(), ct.giaban.ToString(), ct.tongtien.ToString() };
                    var listViewItem = new ListViewItem(cthd);
                    listViewItem.Name = ct.mash;
                    listView1.Items.Add(listViewItem);
                }
            }
            
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Selected)
                {
                    listView1.Items[i].Remove();
                }
                else {
                    MessageBox.Show("Chọn 1 sản phẩm!");
                }
            }
        }


    }
}

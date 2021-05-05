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
    public partial class MainForm : Form
    {
        public static string server = "";
        public static string macv = Login.MACHUCVU;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            server = Login.MACHINHANH;
            đóngToolStripMenuItem.Visible = false;


        }
        private Form activeForm = null;
        private void openChildForm(Form childForm){
            if (activeForm != null) {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void hóaĐơnBánSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new HoaDonBan());
            đóngToolStripMenuItem.Visible = true;
        }

        private void qLNhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new NhanVien());
            đóngToolStripMenuItem.Visible = true;
        }

        private void qLNhàCungCấpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openChildForm(new NhaCungCap());
            đóngToolStripMenuItem.Visible = true;
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm = null;
            Login l = new Login();
            l.Show();
            this.Visible = false;
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void xemBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Report());
            đóngToolStripMenuItem.Visible = true;
        }

        private void đóngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activeForm.Close();
            activeForm = null;
            đóngToolStripMenuItem.Visible = false;
        }
    }
}

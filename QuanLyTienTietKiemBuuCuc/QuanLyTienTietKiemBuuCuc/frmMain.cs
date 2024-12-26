using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();  //Mở kết nối
        }

        private void quảnLýBưuCụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyBuuCuc frmQuanLyBuuCuc = new frmQuanLyBuuCuc();
            frmQuanLyBuuCuc.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyTaiKhoan frmQuanLyTaiKhoan = new frmQuanLyTaiKhoan();
            frmQuanLyTaiKhoan.ShowDialog();
        }
    }
}

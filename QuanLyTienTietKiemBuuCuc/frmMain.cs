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
        // Thuộc tính lưu quyền người dùng
        public string UserRole { get; set; } = string.Empty;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Class.Functions.Connect();  // Mở kết nối
            UpdateMenuByRole();         // Khóa menu khi chưa đăng nhập
        }

        private void UpdateMenuByRole()
        {
            // Kiểm tra quyền người dùng và cập nhật menu
            if (UserRole == "Admin")
            {
                // Mở tất cả menu cho Admin
                quảnLýBưuCụcToolStripMenuItem.Enabled = true;
                quảnLýTàiKhoảnToolStripMenuItem.Enabled = true;
                quảnLýGiaoDịchToolStripMenuItem.Enabled = true;
                tìmKiếmKháchHàngToolStripMenuItem.Enabled = true;
                tìmKiếmGiaoDịchToolStripMenuItem.Enabled = true;
                báoCáoThốngKêToolStripMenuItem.Enabled = true;
            }
            else if (UserRole == "Customer")
            {
                // Chỉ mở menu tìm kiếm và báo cáo cho Khách hàng
                quảnLýBưuCụcToolStripMenuItem.Enabled = false;
                quảnLýTàiKhoảnToolStripMenuItem.Enabled = false;
                quảnLýGiaoDịchToolStripMenuItem.Enabled = false;
                tìmKiếmKháchHàngToolStripMenuItem.Enabled = true;
                tìmKiếmGiaoDịchToolStripMenuItem.Enabled = true;
                báoCáoThốngKêToolStripMenuItem.Enabled = true;
            }
            else
            {
                // Khóa tất cả menu khi chưa đăng nhập
                quảnLýBưuCụcToolStripMenuItem.Enabled = false;
                quảnLýTàiKhoảnToolStripMenuItem.Enabled = false;
                quảnLýGiaoDịchToolStripMenuItem.Enabled = false;
                tìmKiếmKháchHàngToolStripMenuItem.Enabled = false;
                tìmKiếmGiaoDịchToolStripMenuItem.Enabled = false;
                báoCáoThốngKêToolStripMenuItem.Enabled = false;
            }
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangNhap frmDangNhap = new frmDangNhap();
            frmDangNhap.ShowDialog();

            if (frmDangNhap.IsLoggedIn) // Nếu đăng nhập thành công
            {
                UserRole = frmDangNhap.UserRole; // Lấy quyền từ form đăng nhập
                UpdateMenuByRole();             // Cập nhật menu dựa trên quyền
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận trước khi thoát
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit(); // Thoát chương trình
            }
        }
        
        private void quảnLýBưuCụcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyBuuCuc frmQuanLyBuuCuc = new frmQuanLyBuuCuc();
            frmQuanLyBuuCuc.ShowDialog();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyTaiKhoan frmQuanLyTaiKhoan = new frmQuanLyTaiKhoan();
            frmQuanLyTaiKhoan.ShowDialog();
        }

        private void tìmKiếmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimKiemKhachHang frmTimKiemKhachHang = new frmTimKiemKhachHang();
            frmTimKiemKhachHang.ShowDialog();
        }

        private void tìmKiếmGiaoDịchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimKiemGiaoDich frmTimKiemGiaoDich = new frmTimKiemGiaoDich();
            frmTimKiemGiaoDich.ShowDialog();
        }

        private void báoCáoThốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoCaoThongKe frmBaoCaoThongKe = new frmBaoCaoThongKe();
            frmBaoCaoThongKe.ShowDialog();
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTroGiup frmTroGiup = new frmTroGiup();
            frmTroGiup.ShowDialog();
        }

        private void quảnLýGiaoDịchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyGiaoDich frmQuanLyGiaoDich = new frmQuanLyGiaoDich();
            frmQuanLyGiaoDich.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UserRole = string.Empty; // Xóa quyền hiện tại
            UpdateMenuByRole();      // Khóa tất cả menu

            MessageBox.Show("Bạn đã đăng xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

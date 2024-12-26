using System;
using System.Windows.Forms;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmTroGiup : Form
    {
        public frmTroGiup()
        {
            InitializeComponent();
        }

        private void frmTroGiup_Load(object sender, EventArgs e)
        {
            // Nội dung hướng dẫn và thông tin liên hệ
            string helpText = "Đề Tài: Viết Chương Trình Đăng Ký Môn Học\n" +
                              "Nhóm 10\n" +
                              "Sinh viên thực hiện:\n" +
                              "1. Nguyễn Trường Khang\n" +
                              "2. Võ Tấn Phát\n" +
                              "3. Nguyễn Thanh Nguyên\n\n" +
                              "Thông tin liên hệ:\n" +
                              "Email: truongkhang17042004@gmail.com\n" +
                              "Số điện thoại: 0869729181\n" +
                              "Zalo: 0869729181\n" +
                              "Địa chỉ: Đại học Bạc Liêu, Thành phố Bạc liêu";

            richTextBox1.Text = helpText;
        }
        
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
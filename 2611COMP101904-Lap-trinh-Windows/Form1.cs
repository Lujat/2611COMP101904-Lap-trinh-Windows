using System;
using System.Windows.Forms;

namespace _2611COMP101904_Lap_trinh_Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cboKhoa.Items.Add("Công nghệ thông tin");
            cboKhoa.Items.Add("Kinh tế");
            cboKhoa.Items.Add("Quản trị kinh doanh");
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoten.Text))
            {
                MessageBox.Show("Họ tên không được rỗng!");
                txtHoten.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNamsinh.Text))
            {
                MessageBox.Show("Năm sinh không được rỗng!");
                txtNamsinh.Focus();
                return;
            }
            int namSinh;

            if (!int.TryParse(txtNamsinh.Text, out namSinh))
            {
                MessageBox.Show("Năm sinh phải là số nguyên!");
                txtNamsinh.Focus();
                return;
            }
            int namHienTai = DateTime.Now.Year;

            if (namSinh < 1900 || namSinh > namHienTai)
            {
                MessageBox.Show("Năm sinh phải từ 1900 đến " + namHienTai + "!");
                txtNamsinh.Focus();
                return;
            }
            if (!radNam.Checked && !radNu.Checked)
            {
                MessageBox.Show("Vui lòng chọn giới tính!");
                return;
            }
            string gioiTinh;

            if (radNam.Checked)
                gioiTinh = "Nam";
            else
                gioiTinh = "Nữ";
            int tuoi = DateTime.Now.Year - namSinh;
            lblKetQua.Text =
                "Họ tên: " + txtHoten.Text + "\n" +
                "tuoi " + tuoi + "\n" +
                "Email: " + txtEmail.Text + "\n" +
                "Giới tính: " + gioiTinh + "\n" +
                "Khoa: " + cboKhoa.Text;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtHoten.Clear();
            txtNamsinh.Clear();
            txtEmail.Clear();

            radNam.Checked = false;
            radNu.Checked = false;

            cboKhoa.SelectedIndex = -1;

            lblKetQua.Text = "";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "Bạn có muốn thoát không?",
        "Xác nhận",
        MessageBoxButtons.YesNo
    );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}

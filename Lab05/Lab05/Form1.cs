using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab05
{
    public partial class Form1 : Form
    {
        // Danh sách khóa học và học phí
        Dictionary<string, decimal> danhSachKhoaHoc =
            new Dictionary<string, decimal>()
        {
            { "C# WinForms cơ bản", 800000 },
            { "SQL Server cơ bản", 700000 },
            { "Web Frontend cơ bản", 750000 },
            { "Lập trình Python cơ bản", 650000 }
        };

        public Form1()
        {
            InitializeComponent();

            // Đăng ký các sự kiện
            this.Load += Form1_Load;
            cboKhoaHoc.SelectedIndexChanged += cboKhoaHoc_SelectedIndexChanged;

            btnDangKy.Click += btnDangKy_Click;
            btnLamMoi.Click += btnLamMoi_Click;
            btnThoat.Click += btnThoat_Click;
        }

        // 1. Sự kiện Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "ĐĂNG KÝ KHÓA HỌC";

            // Nạp danh sách khóa học
            cboKhoaHoc.Items.Clear();

            foreach (string khoaHoc in danhSachKhoaHoc.Keys)
            {
                cboKhoaHoc.Items.Add(khoaHoc);
            }

            // Thiết lập mặc định
            cboKhoaHoc.SelectedIndex = 0;

            radOnline.Checked = true;

            numSoThang.Minimum = 1;
            numSoThang.Maximum = 12;
            numSoThang.Value = 1;

            dtpNgaySinh.Value = DateTime.Now;

            chkNhanEmail.Checked = false;

            // Hiển thị tổng tiền
            TinhTongTien();
        }

        // 2. Hàm tính tổng học phí
        private void TinhTongTien()
        {
            if (cboKhoaHoc.SelectedItem == null)
                return;

            string khoaHoc = cboKhoaHoc.SelectedItem.ToString();

            decimal hocPhi = danhSachKhoaHoc[khoaHoc];

            decimal tongTien = hocPhi * numSoThang.Value;

            lblTongTien.Text = tongTien.ToString("N0") + " VNĐ";
        }

        // Khi thay đổi khóa học
        private void cboKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        // Khi thay đổi số tháng
        private void numSoThang_ValueChanged(object sender, EventArgs e)
        {
            TinhTongTien();
        }

        // 3. Nút Đăng ký
        private void btnDangKy_Click(object sender, EventArgs e)
        {
            // Kiểm tra họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtHoTen.Focus();
                return;
            }

            // Kiểm tra số điện thoại
            if (string.IsNullOrWhiteSpace(txtSoDienThoai.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtSoDienThoai.Focus();
                return;
            }

            // Kiểm tra khóa học
            if (cboKhoaHoc.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn khóa học!");
                cboKhoaHoc.Focus();
                return;
            }

            // Lấy thông tin
            string hoTen = txtHoTen.Text.Trim();
            string soDienThoai = txtSoDienThoai.Text.Trim();

            string ngaySinh = dtpNgaySinh.Value.ToString("dd/MM/yyyy");

            string khoaHoc = cboKhoaHoc.SelectedItem.ToString();

            string hinhThuc = radOnline.Checked
                ? "Online" : "Trực tiếp";

            decimal soThang = numSoThang.Value;

            decimal tongTien =
                danhSachKhoaHoc[khoaHoc] * soThang;

            string nhanEmail = chkNhanEmail.Checked
                ? "Có" : "Không";

            // Hiển thị phiếu đăng ký
            string thongTin =
                "PHIẾU ĐĂNG KÝ KHÓA HỌC\n\n" +
                "Họ tên: " + hoTen + "\n" +
                "Số điện thoại: " + soDienThoai + "\n" +
                "Ngày sinh: " + ngaySinh + "\n" +
                "Khóa học: " + khoaHoc + "\n" +
                "Hình thức: " + hinhThuc + "\n" +
                "Số tháng: " + soThang + "\n" +
                "Tổng tiền: " + tongTien.ToString("N0") + " VNĐ\n" +
                "Nhận email: " + nhanEmail;

            MessageBox.Show(
                thongTin,
                "Đăng ký thành công",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // 4. Nút Làm mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtHoTen.Clear();

            txtSoDienThoai.Clear();

            dtpNgaySinh.Value = DateTime.Now;

            chkNhanEmail.Checked = false;

            cboKhoaHoc.SelectedIndex = 0;

            radOnline.Checked = true;

            numSoThang.Value = 1;

            TinhTongTien();

            txtHoTen.Focus();
        }

        // 5. Nút Thoát
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult ketQua = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (ketQua == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}

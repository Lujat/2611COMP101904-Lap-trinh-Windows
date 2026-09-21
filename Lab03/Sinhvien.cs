using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03
{
    public class SinhVien : Nguoi
    {
        private double diemTB;
        public string maSV { get; set; }
        public string maLop { get; set; }
        // Property kiểm tra điểm trung bình trong khoảng 0 - 10
        public double DiemTrungBinh
        {
            get => diemTB;
            set
            {
                if (value < 0 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("Điểm trung bình phải nằm trong khoảng 0 đến 10.");
                }
                diemTB = value;
            }
        }
        public SinhVien() : base() { }
        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, string maLop, double diemTrungBinh)
            : base(hoTen, ngaySinh)
        {
            maSV = maSV;
            maLop = maLop;
            DiemTrungBinh = diemTrungBinh;
        }
        // Phương thức xếp loại học lực
        public string XepLoai()
        {
            if (DiemTrungBinh >= 8.5) return "Xuất sắc/Giỏi";
            if (DiemTrungBinh >= 7.0) return "Khá";
            if (DiemTrungBinh >= 5.0) return "Trung bình";
            return "Yếu/Kém";
        }

        // Override phương thức của class Nguoi
        public override string getInfor()
        {
            return $"Mã SV: {maSV,-8} | Họ tên: {HoTen,-20} | Ngày sinh: {NgaySinh:dd/MM/yyyy} | Lớp: {maLop,-6} | ĐTB: {DiemTrungBinh,4:F1} | Xếp loại: {XepLoai()}";
        }
    }
}

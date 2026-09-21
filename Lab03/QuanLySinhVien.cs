using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03
{
    public class QuanLySinhVien
    {
        private readonly List<SinhVien> _danhSachSinhVien;

        public QuanLySinhVien()
        {
            _danhSachSinhVien = new List<SinhVien>();
        }

        public List<SinhVien> LayDanhSach()
        {
            return _danhSachSinhVien;
        }

        public bool Them(SinhVien sv)
        {
            // Kiểm tra trùng mã sinh viên
            if (_danhSachSinhVien.Any(s => s.maSV.Equals(sv.maSV, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            _danhSachSinhVien.Add(sv);
            return true;
        }

        public SinhVien TimTheoMa(string maSv)
        {
            return _danhSachSinhVien.FirstOrDefault(s => s.maSV.Equals(maSv, StringComparison.OrdinalIgnoreCase));
        }

        public List<SinhVien> TimTheoTen(string tuKhoa)
        {
            return _danhSachSinhVien
                .Where(s => s.HoTen.IndexOf(tuKhoa, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }

        public bool SuaDiem(string maSv, double diemMoi)
        {
            var sv = TimTheoMa(maSv);
            if (sv == null) return false;

            sv.DiemTrungBinh = diemMoi;
            return true;
        }

        public bool Xoa(string maSv)
        {
            var sv = TimTheoMa(maSv);
            if (sv == null) return false;

            _danhSachSinhVien.Remove(sv);
            return true;
        }

        public List<SinhVien> SapXepTheoDiem()
        {
            return _danhSachSinhVien.OrderByDescending(s => s.DiemTrungBinh).ToList();
        }

        public List<SinhVien> LocSinhVienDat()
        {
            return _danhSachSinhVien.Where(s => s.DiemTrungBinh >= 5.0).ToList();
        }
    }
}

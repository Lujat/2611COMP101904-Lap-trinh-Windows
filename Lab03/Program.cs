using Lab03;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Lab03_QuanLySinhVienOOP
{
    internal class Program
    {
        private static readonly QuanLySinhVien qlsv = new QuanLySinhVien();

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool tiepTuc = true;
            while (tiepTuc)
            {
                Console.Clear();
                Console.WriteLine("================ QUẢN LÝ SINH VIÊN ================");
                Console.WriteLine("1. Thêm sinh viên");
                Console.WriteLine("2. Xuất danh sách sinh viên");
                Console.WriteLine("3. Tìm sinh viên theo mã");
                Console.WriteLine("4. Tìm sinh viên theo tên");
                Console.WriteLine("5. Sửa điểm sinh viên");
                Console.WriteLine("6. Xóa sinh viên");
                Console.WriteLine("7. Sắp xếp danh sách theo điểm giảm dần");
                Console.WriteLine("8. Lọc danh sách sinh viên đạt (ĐTB >= 5.0)");
                Console.WriteLine("0. Thoát");
                Console.WriteLine("===================================================");
                Console.Write("Chọn chức năng (0-8): ");

                string chon = Console.ReadLine();
                Console.WriteLine();

                switch (chon)
                {
                    case "1":
                        ChucNangThem();
                        break;
                    case "2":
                        ChucNangXuatDanhSach();
                        break;
                    case "3":
                        ChucNangTimTheoMa();
                        break;
                    case "4":
                        ChucNangTimTheoTen();
                        break;
                    case "5":
                        ChucNangSuaDiem();
                        break;
                    case "6":
                        ChucNangXoa();
                        break;
                    case "7":
                        ChucNangSapXep();
                        break;
                    case "8":
                        ChucNangLocDat();
                        break;
                    case "0":
                        tiepTuc = false;
                        Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại.");
                        break;
                }

                if (tiepTuc)
                {
                    Console.WriteLine("\nNhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            }
        }

        #region Các hàm chức năng bổ trợ

        private static void ChucNangThem()
        {
            Console.WriteLine("--- THÊM SINH VIÊN MỚI ---");

            string maSv;
            while (true)
            {
                Console.Write("Nhập mã sinh viên: ");
                maSv = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(maSv)) break;
                Console.WriteLine("Mã sinh viên không được để trống!");
            }

            if (qlsv.TimTheoMa(maSv) != null)
            {
                Console.WriteLine("Lỗi: Mã sinh viên này đã tồn tại trong hệ thống!");
                return;
            }

            Console.Write("Nhập họ và tên: ");
            string hoTen = Console.ReadLine()?.Trim();

            DateTime ngaySinh = NhapNgaySinh("Nhập ngày sinh (dd/MM/yyyy): ");

            Console.Write("Nhập mã lớp: ");
            string maLop = Console.ReadLine()?.Trim();

            double diemTB = NhapDiemTrungBinh("Nhập điểm trung bình (0 - 10): ");

            SinhVien sv = new SinhVien(maSv, hoTen, ngaySinh, maLop, diemTB);
            if (qlsv.Them(sv))
            {
                Console.WriteLine("Thêm sinh viên thành công!");
            }
            else
            {
                Console.WriteLine("Thêm sinh viên thất bại!");
            }
        }

        private static void ChucNangXuatDanhSach()
        {
            Console.WriteLine("--- DANH SÁCH SINH VIÊN ---");
            HienThiDanhSach(qlsv.LayDanhSach());
        }

        private static void ChucNangTimTheoMa()
        {
            Console.Write("Nhập mã sinh viên cần tìm: ");
            string maSv = Console.ReadLine()?.Trim();

            SinhVien sv = qlsv.TimTheoMa(maSv);
            if (sv != null)
            {
                Console.WriteLine("Kết quả tìm kiếm:");
                Console.WriteLine(sv.getInfor());
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã: {maSv}");
            }
        }

        private static void ChucNangTimTheoTen()
        {
            Console.Write("Nhập từ khóa họ tên cần tìm: ");
            string tuKhoa = Console.ReadLine()?.Trim();

            var ketQua = qlsv.TimTheoTen(tuKhoa);
            Console.WriteLine($"\nKết quả tìm kiếm cho từ khóa \"{tuKhoa}\":");
            HienThiDanhSach(ketQua);
        }

        private static void ChucNangSuaDiem()
        {
            Console.Write("Nhập mã sinh viên cần sửa điểm: ");
            string maSv = Console.ReadLine()?.Trim();

            SinhVien sv = qlsv.TimTheoMa(maSv);
            if (sv == null)
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã: {maSv}");
                return;
            }

            Console.WriteLine($"Thông tin hiện tại: {sv.getInfor()}");
            double diemMoi = NhapDiemTrungBinh("Nhập điểm trung bình mới (0 - 10): ");

            if (qlsv.SuaDiem(maSv, diemMoi))
            {
                Console.WriteLine("Cập nhật điểm thành công!");
            }
            else
            {
                Console.WriteLine("Cập nhật điểm thất bại!");
            }
        }

        private static void ChucNangXoa()
        {
            Console.Write("Nhập mã sinh viên cần xóa: ");
            string maSv = Console.ReadLine()?.Trim();

            if (qlsv.Xoa(maSv))
            {
                Console.WriteLine("Xóa sinh viên thành công!");
            }
            else
            {
                Console.WriteLine($"Không tìm thấy sinh viên có mã: {maSv} để xóa.");
            }
        }

        private static void ChucNangSapXep()
        {
            Console.WriteLine("--- DANH SÁCH SẮP XẾP THEO ĐIỂM GIẢM DẦN ---");
            var dsSapXep = qlsv.SapXepTheoDiem();
            HienThiDanhSach(dsSapXep);
        }

        private static void ChucNangLocDat()
        {
            Console.WriteLine("--- DANH SÁCH SINH VIÊN ĐẠT (ĐTB >= 5.0) ---");
            var dsDat = qlsv.LocSinhVienDat();
            HienThiDanhSach(dsDat);
        }

        #endregion

        #region Các hàm Validate dữ liệu vào (chống crash chương trình)

        private static DateTime NhapNgaySinh(string ghiChu)
        {
            DateTime ngaySinh;
            while (true)
            {
                Console.Write(ghiChu);
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ngaySinh))
                {
                    return ngaySinh;
                }
                Console.WriteLine("Định dạng ngày sinh không đúng! Vui lòng nhập theo dạng dd/MM/yyyy (VD: 15/08/2003).");
            }
        }

        private static double NhapDiemTrungBinh(string ghiChu)
        {
            double diem;
            while (true)
            {
                Console.Write(ghiChu);
                string input = Console.ReadLine();
                if (double.TryParse(input, out diem) && diem >= 0 && diem <= 10)
                {
                    return diem;
                }
                Console.WriteLine("Điểm trung bình không hợp lệ! Vui lòng nhập số thực từ 0.0 đến 10.0.");
            }
        }

        private static void HienThiDanhSach(List<SinhVien> danhSach)
        {
            if (danhSach == null || danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách trống!");
                return;
            }

            foreach (var sv in danhSach)
            {
                Console.WriteLine(sv.getInfor());
            }
        }

        #endregion
    }
}
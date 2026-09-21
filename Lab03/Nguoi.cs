using System;
using System.Collections.Generic;
using System.Text;

namespace Lab03
{
    public class Nguoi
    {
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public Nguoi() { }
        public Nguoi(string hoTen, DateTime ngaySinh)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
        }
        public virtual string getInfor()
        {
            return $"Họ tên: {HoTen} | Ngày sinh: {NgaySinh:dd/MM/yyyy}";
        }
    }
}

using System;

class Program
{
    static int nhapsoNguyen(string message)
    {
        int so;
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out so)){
                return so;
            }
            Console.WriteLine("Vui long nhap mot so nguyen!");
        }
    }
    static int nhapsoNguyenDuong(string message)
    {
        int so;
        while (true){
            so = nhapsoNguyen(message);

            if (so > 0)
            {
                return so;
            }
            Console.WriteLine("So phan tu phai la so nguyen duong!");
        }
    }
    static int[] NhapMang()
    {
        int n = nhapsoNguyenDuong("Nhap so luong phan tu n: ");
        int[] a = new int[n];
        for (int i = 0; i < n; i++)
        {
            a[i] = nhapsoNguyen($"Nhap a[{i}]: ");
        }
        return a;
    }
    static void XuatMang(int[] a)
    {
        Console.Write("Mang: ");
        for (int i = 0; i < a.Length; i++){
            Console.Write(a[i] + " ");
        }
        Console.WriteLine();
    }
    static int Tong(int[] a)
    {
        int tong = 0;
        for (int i = 0; i < a.Length; i++)
        {
            tong += a[i];
        }
        return tong;
    }
    static int Max(int[] a)
    {
        int max = a[0];
        for (int i = 1; i < a.Length; i++){
            if (a[i] > max)
            {
                max = a[i];
            }
        }
        return max;
    }
    static int Min(int[] a)
    {
        int min = a[0];
        for (int i = 1; i < a.Length; i++){
            if (a[i] < min)
            {
                min = a[i];
            }
        }

        return min;
    }
    static int Chan(int[] a)
    {
        int dem = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] % 2 == 0)
            {
                dem++;
            }
        }
        return dem;
    }
    static int Le(int[] a)
    {
        int dem = 0;
        for (int i = 0; i < a.Length; i++){
            if (a[i] % 2 != 0)
            {
                dem++;
            }
        }
        return dem;
    }
    static void SapXepTangDan(int[] a)
    {
        for (int i = 0; i < a.Length - 1; i++){
            for (int j = i + 1; j < a.Length; j++)
            {
                if (a[i] > a[j])
                {
                    int temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }
        }
    }
    static int TimKiem(int[] a, int x)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == x)
            {
                return i;
            }
        }
        return -1;
    }
    static void HienThiMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== MENU =====");
        Console.WriteLine("1. Nhap mang");
        Console.WriteLine("2. Xuat mang");
        Console.WriteLine("3. Tinh tong");
        Console.WriteLine("4. Tim max/min");
        Console.WriteLine("5. Dem chan/le");
        Console.WriteLine("6. Sap xep tang dan");
        Console.WriteLine("7. Tim kiem");
        Console.WriteLine("0. Thoat");
    }
    static void Main()
    {
        int[] a = null;
        int luaChon;
        do
        {
            HienThiMenu();
            luaChon = nhapsoNguyen("Chon chuc nang: ");
            switch (luaChon)
            {
                case 1:
                    a = NhapMang();
                    Console.WriteLine("Nhap mang thanh cong!");
                    break;
                case 2:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }
                    XuatMang(a);
                    break;
                case 3:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }
                    Console.WriteLine("Tong = " + Tong(a));
                    break;
                case 4:
                    if (a == null){
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }
                    Console.WriteLine("Max = " + Max(a));
                    Console.WriteLine("Min = " + Min(a));
                    break;
                case 5:
                    if (a == null){
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }
                    Console.WriteLine("So phan tu chan = " + Chan(a));
                    Console.WriteLine("So phan tu le = " + Le(a));
                    break;
                case 6:
                    if (a == null){
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }
                    SapXepTangDan(a);

                    Console.WriteLine("Mang sau khi sap xep tang dan:");
                    XuatMang(a);
                    break;
                case 7:
                    if (a == null)
                    {
                        Console.WriteLine("Vui long nhap mang truoc!");
                        break;
                    }
                    int x = nhapsoNguyen("Nhap gia tri x can tim: ");
                    int viTri = TimKiem(a, x);
                    if (viTri != -1)
                    {
                        Console.WriteLine(
                            $"Tim thay {x} tai vi tri dau tien: {viTri}");
                    }
                    else
                    {
                        Console.WriteLine($"Khong tim thay {x} trong mang!");
                    }
                    break;
                case 0:
                    Console.WriteLine("Ket thuc chuong trinh!");
                    break;

                default:
                    Console.WriteLine("Lua chon khong hop le! Vui long chon lai.");
                    break;
            }
        } while (luaChon != 0);
    }
}
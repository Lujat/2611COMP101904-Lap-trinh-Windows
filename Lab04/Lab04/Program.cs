using System;
using System.Collections.Generic;

class Program
{
    static ProductService service = new ProductService();

    static void Main(string[] args)
    {
        service.ProductChanged += message =>
        {
            Console.WriteLine("\n[THONG BAO] " + message);
        };

        int choice;

        do
        {
            ShowMenu();

            Console.Write("Chon chuc nang: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Vui long nhap so hop le!");
                choice = -1;
                continue;
            }

            try
            {
                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;

                    case 2:
                        ShowProducts();
                        break;

                    case 3:
                        SearchById();
                        break;

                    case 4:
                        SearchByName();
                        break;

                    case 5:
                        FilterByPrice();
                        break;

                    case 6:
                        RemoveProduct();
                        break;

                    case 7:
                        CalculateTotal();
                        break;

                    case 0:
                        Console.WriteLine("Thoat chuong trinh!");
                        break;

                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        break;
                }
            }
            catch (DuplicateProductException ex)
            {
                Console.WriteLine("Loi trung ma: " + ex.Message);
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine("Loi khong tim thay: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Loi du lieu: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }

        } while (choice != 0);
    }
    static void ShowMenu()
    {
        Console.WriteLine("\n===== PRODUCT MANAGER =====");
        Console.WriteLine("1. Them san pham");
        Console.WriteLine("2. Xuat danh sach");
        Console.WriteLine("3. Tim theo ma");
        Console.WriteLine("4. Tim theo ten");
        Console.WriteLine("5. Loc theo khoang gia");
        Console.WriteLine("6. Xoa san pham");
        Console.WriteLine("7. Tinh tong gia tri kho");
        Console.WriteLine("0. Thoat");
        Console.WriteLine("===========================");
    }
    static void AddProduct()
    {
        Console.Write("Nhap ma SP: ");
        string ma = Console.ReadLine();

        Console.Write("Nhap ten SP: ");
        string ten = Console.ReadLine();

        Console.Write("Nhap don gia: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal gia))
            throw new ArgumentException("Don gia phai la so hop le!");

        Console.Write("Nhap so luong: ");

        if (!int.TryParse(Console.ReadLine(), out int soLuong))
            throw new ArgumentException("So luong phai la so nguyen!");

        Product product = new Product(ma, ten, gia, soLuong);

        service.AddProduct(product);
    }
    static void ShowProducts()
    {
        List<Product> products = service.GetAll();

        if (products.Count == 0)
        {
            Console.WriteLine("Danh sach san pham rong!");
            return;
        }

        Console.WriteLine("\n===== DANH SACH SAN PHAM =====");

        foreach (Product p in products)
        {
            Console.WriteLine(p);
        }
    }
    static void SearchById()
    {
        Console.Write("Nhap ma san pham can tim: ");
        string ma = Console.ReadLine();

        Product p = service.Search(ma);

        if (p == null)
            throw new ProductNotFoundException(
                "Khong tim thay san pham!");

        Console.WriteLine("\nKet qua:");
        Console.WriteLine(p);
    }
    static void SearchByName()
    {
        Console.Write("Nhap ten san pham can tim: ");
        string keyword = Console.ReadLine();

        List<Product> result = service.SearchByName(keyword);

        if (result.Count == 0)
        {
            Console.WriteLine("Khong tim thay san pham!");
            return;
        }

        foreach (Product p in result)
        {
            Console.WriteLine(p);
        }
    }
    static void FilterByPrice()
    {
        Console.Write("Nhap gia thap nhat: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal min))
            throw new ArgumentException("Gia khong hop le!");

        Console.Write("Nhap gia cao nhat: ");

        if (!decimal.TryParse(Console.ReadLine(), out decimal max))
            throw new ArgumentException("Gia khong hop le!");

        if (min < 0 || max < 0)
            throw new ArgumentException("Gia khong duoc am!");

        if (min > max)
            throw new ArgumentException("Gia thap nhat phai nho hon gia cao nhat!");

        List<Product> result = service.Filter(
            p => p.Price >= min && p.Price <= max
        );

        if (result.Count == 0)
        {
            Console.WriteLine("Khong co san pham trong khoang gia nay!");
            return;
        }

        Console.WriteLine("\n===== KET QUA LOC =====");

        foreach (Product p in result)
        {
            Console.WriteLine(p);
        }
    }
    static void RemoveProduct()
    {
        Console.Write("Nhap ma san pham can xoa: ");
        string ma = Console.ReadLine();

        service.RemoveProduct(ma);
    }
    static void CalculateTotal()
    {
        decimal total = service.CalculateTotalValue();

        Console.WriteLine(
            "Tong gia tri kho: " + total.ToString("N0") + " VND");
    }
}

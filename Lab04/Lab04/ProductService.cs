
using System;
using System.Collections.Generic;
using System.Linq;

public class ProductService
{
    private Repository<Product> repository =
        new Repository<Product>();
    public event Action<string> ProductChanged;
    private void OnProductChanged(string message)
    {
        ProductChanged?.Invoke(message);
    }
    public void AddProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.MaSP))
            throw new ArgumentException("Ma san pham khong duoc rong!");

        if (repository.FindById(product.MaSP) != null)
            throw new DuplicateProductException(
                "Ma san pham da ton tai!");

        repository.Add(product);

        OnProductChanged(
            "Da them san pham: " + product.TenSP);
    }
    //xoa
    public void RemoveProduct(string id)
    {
        Product product = repository.FindById(id);

        if (product == null)
            throw new ProductNotFoundException(
                "Khong tim thay san pham co ma: " + id);

        repository.Remove(id);

        OnProductChanged(
            "Da xoa san pham: " + product.TenSP);
    }
    // Tim ma
    public Product Search(string id)
    {
        return repository.FindById(id);
    }
    // Tim ten
    public List<Product> SearchByName(string keyword)
    {
        return repository.Find(p =>
            p.TenSP.Contains(
                keyword,
                StringComparison.OrdinalIgnoreCase));
    }
    // Loc san pham
    public List<Product> Filter(Func<Product, bool> condition)
    {
        return repository.Find(condition);
    }
    // print danh sach
    public List<Product> GetAll()
    {
        return repository.GetAll();
    }
    // Tinh tongkho
    public decimal CalculateTotalValue()
    {
        return repository.GetAll().Sum(
            p => p.Price * p.Quantity);
    }
}

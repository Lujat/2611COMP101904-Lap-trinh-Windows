
using System;

public class Product : IEntity
{
    public string MaSP { get; set; }
    public string TenSP { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public string Id => MaSP;

    public Product(string maSP, string tenSP,
                   decimal price, int quantity)
    {
        if (price < 0)
            throw new ArgumentException("Don gia khong duoc am!");

        if (quantity < 0)
            throw new ArgumentException("So luong khong duoc am!");

        MaSP = maSP;
        TenSP = tenSP;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"Ma SP: {MaSP,-10} | " +
               $"Ten: {TenSP,-20} | " +
               $"Gia: {Price:N0} | " +
               $"So luong: {Quantity}";
    }
}



using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T> where T : IEntity
{
    private List<T> items = new List<T>();

    // Them doi tuong
    public void Add(T item)
    {
        items.Add(item);
    }

    // Xoa theo ma
    public bool Remove(string id)
    {
        T item = FindById(id);

        if (item == null)
            return false;

        items.Remove(item);
        return true;
    }

    // Tim theo ma
    public T FindById(string id)
    {
        return items.FirstOrDefault(
            x => x.Id.Equals(id,
            StringComparison.OrdinalIgnoreCase));
    }

    // Tim kiem theo dieu kien Func
    public List<T> Find(Func<T, bool> predicate)
    {
        return items.Where(predicate).ToList();
    }

    // Lay toan bo danh sach
    public List<T> GetAll()
    {
        return items.ToList();
    }
}

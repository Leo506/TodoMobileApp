using System.Collections.Generic;

namespace GoalApp.Data;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();

    public T Get(int id);

    public int AddNew(T item);

    public void Update(T item);

    public void Delete(T item);
}
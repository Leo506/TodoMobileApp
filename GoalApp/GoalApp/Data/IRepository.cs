using System.Collections.Generic;
using GoalApp.ErrorWrapping;

namespace GoalApp.Data;

public interface IRepository<T>
{
    public ActionResult<IEnumerable<T>> GetAll();

    public ActionResult<T> Get(int id);

    public ActionResult<int> AddNew(T item);

    public ActionResult Update(T item);

    public ActionResult Delete(T item);
}
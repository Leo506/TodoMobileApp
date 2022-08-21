using System.Collections.Generic;
using System.Linq;
using GoalApp.Models;
using SQLite;

namespace GoalApp.Data.Tasks;

public class TasksRepository : IRepository<TaskModel>
{
    private readonly SQLiteConnection _database;

    public TasksRepository(string databasePath)
    {
        _database = new SQLiteConnection(databasePath);
        _database.CreateTable<TaskModel>();
    }

    public IEnumerable<TaskModel> GetAll() => _database.Table<TaskModel>().ToList();

    public TaskModel Get(int id) => _database.Get<TaskModel>(id);

    public int AddNew(TaskModel item)
    {
        _database.Insert(item);
        return item.Id;
    }

    public void Update(TaskModel item) => _database.Update(item);

    public void Delete(TaskModel task) => _database.Delete<TaskModel>(task.Id);
}
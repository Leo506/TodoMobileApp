using System.Collections.Generic;
using System.Linq;
using GoalApp.Models;
using SQLite;

namespace GoalApp.Data.Tasks;

public class TasksRepository
{
    private readonly SQLiteConnection _database;

    public TasksRepository(string databasePath)
    {
        _database = new SQLiteConnection(databasePath);
        _database.CreateTable<TaskModel>();
    }

    public IEnumerable<TaskModel> GetAll()
    {
        return _database.Table<TaskModel>().ToList();
    }

    public int AddNewTask(TaskModel task)
    {
        if (task.Id != 0)
            UpdateTask(task);
        else
            _database.Insert(task);

        return task.Id;
    }

    public void UpdateTask(TaskModel task)
    {
        _database.Update(task);
    }

    public void Delete(TaskModel task)
    {
        _database.Delete(task);
    }
}
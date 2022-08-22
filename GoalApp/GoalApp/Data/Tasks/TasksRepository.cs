using System;
using System.Collections.Generic;
using System.Linq;
using GoalApp.ErrorWrapping;
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

    public ActionResult<IEnumerable<TaskModel>> GetAll()
    {
        var result = new ActionResult<IEnumerable<TaskModel>>();
        try
        {
            result.Result = _database.Table<TaskModel>().ToList();
        }
        catch (Exception e)
        {
            result.Error = e;
        }

        return result;
    }

    public ActionResult<TaskModel> Get(int id)
    {
        var result = new ActionResult<TaskModel>();
        try
        {
            result.Result = _database.Get<TaskModel>(id);
        }
        catch (Exception e)
        {
            result.Error = e;
        }

        return result;
    }

    public ActionResult<int> AddNew(TaskModel item)
    {
        var result = new ActionResult<int>();
        try
        {
            _database.Insert(item);
            result.Result = item.Id;
        }
        catch (Exception e)
        {
            result.Error = e;
        }

        return result;
    }

    public ActionResult Update(TaskModel item)
    {
        var result = new ActionResult();

        try
        {
            _database.Update(item);
        }
        catch (Exception e)
        {
            result.Error = e;
        }

        return result;
    }

    public ActionResult Delete(TaskModel task)
    {
        var result = new ActionResult();

        try
        {
            _database.Delete<TaskModel>(task.Id);
        }
        catch (Exception e)
        {
            result.Error = e;
        }

        return result;
    }
}
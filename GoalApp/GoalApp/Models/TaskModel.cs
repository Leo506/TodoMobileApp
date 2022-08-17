using SQLite;

namespace GoalApp.Models;

[Table("MyTasks")]
public class TaskModel
{
    [PrimaryKey, AutoIncrement, Column("_id")]  
    public int Id { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
}
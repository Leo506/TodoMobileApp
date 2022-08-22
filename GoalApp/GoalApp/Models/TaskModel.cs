using System.ComponentModel;
using System.Runtime.CompilerServices;
using GoalApp.Annotations;
using SQLite;

namespace GoalApp.Models;

[Table("MyTasks")]
public class TaskModel : INotifyPropertyChanged
{
    [PrimaryKey, AutoIncrement, Unique, Column("_id")]  
    public int Id { get; set; }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    private string _title;

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    private string _description;

    public Urgency Urgency
    {
        get => _urgency;
        set
        {
            _urgency = value;
            OnPropertyChanged(nameof(Urgency));
        }
    }
    private Urgency _urgency;
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
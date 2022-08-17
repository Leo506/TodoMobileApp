using System.ComponentModel;
using System.Runtime.CompilerServices;
using GoalApp.Annotations;
using GoalApp.Models;

namespace GoalApp.ViewModels;

public class TaskViewModel :  INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string _title;

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private string _description;

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }
    
    
    public int Id { get; set; }
    
    public TaskListViewModel ListViewModel { get; set; }
    
    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        App.TasksRepository.UpdateTask(new TaskModel()
        {
            Title = Title,
            Description = Description,
            Id = Id
        });
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
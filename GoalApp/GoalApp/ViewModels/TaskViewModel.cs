using System.ComponentModel;
using System.Runtime.CompilerServices;
using GoalApp.Annotations;

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
    
    public TaskPageViewModel PageViewModel { get; set; }
    
    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GoalApp.Annotations;
using Xamarin.Forms;

namespace GoalApp.ViewModels;

public class TaskPageViewModel : INotifyPropertyChanged
{
    public ObservableCollection<TaskViewModel> Tasks { get; set; }
    
    public TaskViewModel NewTask { get; set; }
    
    public ICommand CreateCommand { get; set; }
    
    public event PropertyChangedEventHandler PropertyChanged;

    public TaskPageViewModel()
    {
        NewTask = new TaskViewModel();
        Tasks = new ObservableCollection<TaskViewModel>()
        {
            new TaskViewModel()
            {
                Title = "Title 1",
                Description = "Make it and it",
                PageViewModel = this
            },

            new TaskViewModel()
            {
                Title = "Title 2",
                Description = "Make make make",
                PageViewModel = this
            }
        };

        CreateCommand = new Command(Create);
    }

    private void Create()
    {
        Tasks.Add(new TaskViewModel()
        {
            Title = NewTask.Title,
            Description = NewTask.Description
        });
        
        OnPropertyChanged(nameof(Tasks));
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
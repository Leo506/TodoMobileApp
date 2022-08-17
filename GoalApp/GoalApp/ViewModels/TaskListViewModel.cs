using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GoalApp.Annotations;
using GoalApp.Models;
using Xamarin.Forms;

namespace GoalApp.ViewModels;

public class TaskListViewModel : INotifyPropertyChanged
{
    public ObservableCollection<TaskViewModel> Tasks { get; set; }
    
    public TaskViewModel NewTask { get; set; }
    
    public ICommand ShowAddFormCommand { get; set; }
    public ICommand AddCommand { get; set; }
    public ICommand BackCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    
    public event PropertyChangedEventHandler PropertyChanged;

    private readonly INavigation _navigation;

    public TaskListViewModel(INavigation navigation)
    {
        _navigation = navigation;
        NewTask = new TaskViewModel()
        {
            ListViewModel = this
        };
        Tasks = new ObservableCollection<TaskViewModel>(App.TasksRepository.GetAll().Select(t => 
            new TaskViewModel()
        {
            Title = t.Title,
            Description = t.Description,
            ListViewModel = this,
            Id = t.Id
        }));

        AddCommand = new Command(async () => await Create());
        ShowAddFormCommand = new Command(async () => await ShowAddForm());
        BackCommand = new Command(async () => await _navigation.PopModalAsync(true));
        DeleteCommand = new Command(Delete);
    }

    private async Task Create()
    {
        var taskToAdd = new TaskViewModel()
        {
            Title = NewTask.Title,
            Description = NewTask.Description,
            ListViewModel = this
        };
        
        Tasks.Add(taskToAdd);

        App.TasksRepository.AddNewTask(new TaskModel()
        {
            Title = taskToAdd.Title,
            Description = taskToAdd.Description
        });

        await _navigation.PopModalAsync(true);
        
        OnPropertyChanged(nameof(Tasks));

    }

    private async Task ShowAddForm()
    {
        await _navigation.PushModalAsync(new AddingPage(this), true);
    }

    private void Delete(object taskObj)
    {
        if (!(taskObj is TaskViewModel task))
            return;

        Tasks.Remove(task);
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
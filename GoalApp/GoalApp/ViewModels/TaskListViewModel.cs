using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GoalApp.Annotations;
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
        Tasks = new ObservableCollection<TaskViewModel>()
        {
            new TaskViewModel()
            {
                Title = "Title 1",
                Description = "Make it and it",
                ListViewModel = this
            },

            new TaskViewModel()
            {
                Title = "Title 2",
                Description = "Make make make",
                ListViewModel = this
            }
        };

        AddCommand = new Command(async () => await Create());
        ShowAddFormCommand = new Command(async () => await ShowAddForm());
        BackCommand = new Command(async () => await _navigation.PopModalAsync(true));
        DeleteCommand = new Command(Delete);
    }

    private async Task Create()
    {
        Tasks.Add(new TaskViewModel()
        {
            Title = NewTask.Title,
            Description = NewTask.Description,
            ListViewModel = this
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
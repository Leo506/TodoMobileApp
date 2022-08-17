using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using AutoMapper;
using GoalApp.Annotations;
using GoalApp.Data;
using GoalApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

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

    private readonly IRepository<TaskModel> _repository;

    private readonly IMapper _mapper;

    public TaskListViewModel(INavigation navigation)
    {
        _navigation = navigation;
        _repository = App.ServiceProvider.GetRequiredService<IRepository<TaskModel>>();
        _mapper = App.ServiceProvider.GetRequiredService<IMapper>();
        
        NewTask = new TaskViewModel()
        {
            ListViewModel = this
        };

        Tasks = new ObservableCollection<TaskViewModel>(_repository.GetAll()
            .Select(t => _mapper.Map<TaskViewModel>(t)));
        Tasks.ForEach(t => t.ListViewModel = this);

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

        taskToAdd.Id = _repository.AddNew(_mapper.Map<TaskModel>(taskToAdd));

        await _navigation.PopModalAsync(true);
        
        OnPropertyChanged(nameof(Tasks));

    }

    private async Task ShowAddForm()
    {
        await _navigation.PushModalAsync(new AddingPage(this), true);
    }

    private void Delete(object taskObj)
    {
        if (taskObj is not TaskViewModel task)
            return;
        

        Tasks.Remove(task);
        _repository.Delete(_mapper.Map<TaskModel>(task));
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
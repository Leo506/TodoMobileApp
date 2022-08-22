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
    public ObservableCollection<TaskModel> Tasks { get; set; }
    
    public TaskModel NewTask { get; set; }
    
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

        NewTask = new TaskModel();

        Tasks = new ObservableCollection<TaskModel>(_repository.GetAll().Result);
        foreach (var taskModel in Tasks)
        {
            taskModel.PropertyChanged += OnTaskPropertyChange;
        }

        AddCommand = new Command(async () => await Create());
        ShowAddFormCommand = new Command(async () => await ShowAddForm());
        BackCommand = new Command(async () => await _navigation.PopModalAsync(true));
        DeleteCommand = new Command(Delete);
    }

    private void OnTaskPropertyChange(object sender, PropertyChangedEventArgs e)
    {
        if (sender is not TaskModel)
            return;

        _repository.Update((TaskModel)sender);
    }

    private async Task Create()
    {
        var taskToAdd = new TaskModel()
        {
            Title = NewTask.Title,
            Description = NewTask.Description,
            Urgency = NewTask.Urgency
        };
        
        Tasks.Add(taskToAdd);

        var addResult = _repository.AddNew(taskToAdd);
        if (!addResult.Success)
            return;;

        taskToAdd.Id = addResult.Result;
        taskToAdd.PropertyChanged += OnTaskPropertyChange;

        await _navigation.PopModalAsync(true);
        
        OnPropertyChanged(nameof(Tasks));

    }

    private async Task ShowAddForm()
    {
        await _navigation.PushModalAsync(new AddingPage(this), true);
    }

    private void Delete(object taskObj)
    {
        if (taskObj is not TaskModel task)
            return;
        

        Tasks.Remove(task);
        _repository.Delete(task);
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AutoMapper;
using GoalApp.Annotations;
using GoalApp.Data;
using GoalApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

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

    
    private readonly IRepository<TaskModel> _repository;
    private readonly IMapper _mapper;

    public TaskViewModel()
    {
        _repository = App.ServiceProvider.GetRequiredService<IRepository<TaskModel>>();
        _mapper = App.ServiceProvider.GetRequiredService<IMapper>();
    }

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        _repository.Update(_mapper.Map<TaskModel>(this));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
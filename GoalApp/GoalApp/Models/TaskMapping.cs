using AutoMapper;
using GoalApp.ViewModels;

namespace GoalApp.Models;

public class TaskMapping : Profile
{
    public TaskMapping()
    {
        CreateMap<TaskModel, TaskViewModel>()
            .ForMember(d => d.ListViewModel, s => s.Ignore())
            .ReverseMap();
    }
}
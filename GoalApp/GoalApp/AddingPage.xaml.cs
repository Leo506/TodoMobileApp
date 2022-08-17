using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoalApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoalApp;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddingPage : ContentPage
{
    public AddingPage(TaskListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
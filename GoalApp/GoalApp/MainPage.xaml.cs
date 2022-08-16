using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GoalApp.ViewModels;
using Xamarin.Forms;

namespace GoalApp
{
    public partial class MainPage : ContentPage
    {
        private TaskPageViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new TaskPageViewModel();
            BindingContext = _viewModel;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            DisplayAlert("Button clicked!!!",
                $"Current count tasks: {_viewModel.Tasks.Count}\nNew task: {_viewModel.NewTask.Title }", "Ok");
        }
    }
}
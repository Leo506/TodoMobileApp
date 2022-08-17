using System;
using GoalApp.Data.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace GoalApp
{
    public partial class App : Application
    {
        private const string DatabaseName = "Tasks.db";

        private static TasksRepository _repository;
        public static TasksRepository TasksRepository =>
            _repository ??= new TasksRepository(System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName));

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
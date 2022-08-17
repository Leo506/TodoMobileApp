using System;
using GoalApp.Data;
using GoalApp.Data.Tasks;
using GoalApp.Models;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace GoalApp
{
    public partial class App : Application
    {
        private const string DatabaseName = "Tasks.db";

        public static IServiceProvider ServiceProvider;
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection()
                .AddSingleton<IRepository<TaskModel>>(provider => new TasksRepository(System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName)))
                .AddAutoMapper(typeof(App));

            ServiceProvider = services.BuildServiceProvider();

            var mapper = ServiceProvider.GetRequiredService<AutoMapper.IConfigurationProvider>();
            mapper.CompileMappings();

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
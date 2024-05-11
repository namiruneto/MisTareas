using MisTarea.Services;
using MisTarea.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using MisTarea.Class;
using System.IO;

namespace MisTarea
{
    public partial class App : Application
    {
        static ConexionSqlite db;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }      

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MisTarea.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
       
        public AboutViewModel()
        {
            Title = "Pendientes";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
          
        }

     
        public ICommand OpenWebCommand { get; }

       
    }
}
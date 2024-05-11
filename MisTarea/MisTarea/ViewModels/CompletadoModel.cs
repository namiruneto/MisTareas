using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MisTarea.ViewModels
{
    public class CompletadoModel : BaseViewModel
    {
     
        public CompletadoModel()
        {
            Title = "Pendientes";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));           

        }

       
        public ICommand OpenWebCommand { get; }

       
    }
}
using MisTarea.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static MisTarea.Models.tareas;

namespace MisTarea.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<TareaPendiente> ItemTapped { get; }

        public AboutViewModel()
        {
            Title = "Pendientes";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            ItemTapped = new Command<TareaPendiente>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(TareaPendiente item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
        public ICommand OpenWebCommand { get; }

       
    }
}
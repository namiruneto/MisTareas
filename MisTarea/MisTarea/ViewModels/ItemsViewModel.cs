using MisTarea.Models;
using MisTarea.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using static MisTarea.Models.tareas;
using System.Collections.Generic;
using MisTarea.Class;

namespace MisTarea.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private TareaPendiente _selectedItem;

        public ObservableCollection<TareaPendiente> TareasPendientes { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<TareaPendiente> ItemTapped { get; }
        public List<TareaPendiente> TareasPendiente { get; set; } = new List<TareaPendiente>();




        public ItemsViewModel()
        {
            Title = "Pendiente";
            TareasPendientes = new ObservableCollection<TareaPendiente>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<TareaPendiente>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        private void CargarDatosDeEjemplo()
        {
            // Agregar algunas tareas de ejemplo a la lista
            TareasPendiente.Add(new TareaPendiente
            {
                Id = 1,
                Nombre = "Hacer compras",
                IdCategoria = "Personal",
                FechaInicio = "2024-05-11",
                FechaFin = "2024-05-15",
                Hora = "10:00"
            });

            TareasPendiente.Add(new TareaPendiente
            {
                Id = 2,
                Nombre = "Preparar presentación",
                IdCategoria = "Trabajo",
                FechaInicio = "2024-05-12",
                FechaFin = "2024-05-14",
                Hora = "15:00"
            });

            TareasPendiente.Add(new TareaPendiente
            {
                Id = 3,
                Nombre = "Hacer ejercicio",
                IdCategoria = "Personal",
                FechaInicio = "2024-05-13",
                FechaFin = "2024-05-13",
                Hora = "08:00"
            });
        }
    

    async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                ConexionSqlite conexionSqlite = new ConexionSqlite("");
                TareasPendientes.Clear();
                var tareas = conexionSqlite.TareasPendiente().Result;
                foreach (var tarea in tareas)
                {
                    TareasPendientes.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public TareaPendiente SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
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
    }
}
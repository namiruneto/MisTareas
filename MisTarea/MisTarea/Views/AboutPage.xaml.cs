using MisTarea.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MisTarea.Models.tareas;

namespace MisTarea.Views
{
    public partial class AboutPage : ContentPage
    {
        public IList<Models.tareas.TareaPendiente> TareasPendiente { get; private set; }
        public int Consecutivo { get; set; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<TareaPendiente> ItemTapped { get; }

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
            TareasPendiente = new ObservableCollection<Models.tareas.TareaPendiente>();
            Class.ConexionSqlite sqlite = new Class.ConexionSqlite("");
            var resultado = sqlite.TareasPendiente();
            resultado.Wait();
            TareasPendiente = resultado.Result;
            // Comprueba si la lista está vacía o no
            if (TareasPendiente != null && TareasPendiente.Count > 0)
            {
                // La lista tiene elementos
                Console.WriteLine("La lista TareasPendiente tiene elementos.");
            }
            else
            {
                // La lista está vacía
                Console.WriteLine("La lista TareasPendiente está vacía.");
            }
            Consecutivo = 0;
            btnAtras.Clicked += BtnAtras_Clicked;
            btnSiguiente.Clicked += BtnSiguiente_Clicked;
            btnCompletado.Clicked += BtnCompletado_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
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

        private void BtnAtras_Clicked(object sender, EventArgs e)
        {
            Consecutivo = TareasPendiente.Count == Consecutivo ? TareasPendiente.Count - 1 : Consecutivo--;
            if(TareasPendiente.Count == 0)
            {
                lblTitulo.Text = "Sin tareas Pendiente";
            }
            else
            {
                lblTitulo.Text = TareasPendiente[Consecutivo].Nombre;
                lblDescripcion.Text = TareasPendiente[Consecutivo].Descripcion;
                lblTipoCategoria.Text = TareasPendiente[Consecutivo].IdCategoria;
                lblHoraFinalizacion.Text = TareasPendiente[Consecutivo].Hora;
                lblFecha.Text = TareasPendiente[Consecutivo].FechaFin;
            }
        }

        private void BtnSiguiente_Clicked(object sender, EventArgs e)
        {
            Consecutivo = TareasPendiente.Count == Consecutivo ? 0 : Consecutivo++;
            if (TareasPendiente.Count == 0)
            {
                lblTitulo.Text = "Sin tareas Pendiente";
            }
            else
            {
                lblTitulo.Text = TareasPendiente[Consecutivo].Nombre;
                lblDescripcion.Text = TareasPendiente[Consecutivo].Descripcion;
                lblTipoCategoria.Text = TareasPendiente[Consecutivo].IdCategoria;
                lblHoraFinalizacion.Text = TareasPendiente[Consecutivo].Hora;
                lblFecha.Text = TareasPendiente[Consecutivo].FechaFin;
            }
        }

        private void BtnCompletado_Clicked(object sender, EventArgs e)
        {
            Class.ConexionSqlite sqlite = new Class.ConexionSqlite("");
            sqlite.Guardar(new NuevaTarea
            {
                Id = TareasPendiente[Consecutivo].Id,
                Estado = "Completada",
                Repetir = TareasPendiente[Consecutivo].Diaria,
                FecFinal = DateTime.Now.ToString("yyyy-MM-dd"),
                Nombre = TareasPendiente[Consecutivo].Nombre,
                Descripcion = TareasPendiente[Consecutivo].Descripcion,
                IdTipoTarea = TareasPendiente[Consecutivo].IdCategoria,
                FecIngreso = TareasPendiente[Consecutivo].FechaInicio  
            });           
        }
        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            Class.ConexionSqlite sqlite = new Class.ConexionSqlite("");
            sqlite.Guardar(new NuevaTarea
            {
                Id = TareasPendiente[Consecutivo].Id,
                Estado = "Eliminar",
                Repetir = TareasPendiente[Consecutivo].Diaria,
                FecFinal = DateTime.Now.ToString("yyyy-MM-dd"),
                Nombre = TareasPendiente[Consecutivo].Nombre,
                Descripcion = TareasPendiente[Consecutivo].Descripcion,
                IdTipoTarea = TareasPendiente[Consecutivo].IdCategoria,
                FecIngreso = TareasPendiente[Consecutivo].FechaInicio,
            });
        }
    }
}
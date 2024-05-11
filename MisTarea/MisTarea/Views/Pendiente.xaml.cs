using MisTarea.Models;
using MisTarea.ViewModels;
using MisTarea.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MisTarea.Models.tareas;

namespace MisTarea.Views
{
    public partial class Pendiente : ContentPage
    {
        ItemsViewModel _viewModel;

        public Pendiente()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
        }
       
        public List<TareaPendiente> TareasPendiente { get; set; } = new List<TareaPendiente>();

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
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
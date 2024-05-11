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
    public partial class Completado : ContentPage
    {
        public ObservableCollection<Models.tareas.TareaPendiente> TareasPendiente { get; private set; }
        public int Consecutivo { get; set; }
       
        public Completado()
        {
            InitializeComponent();
            BindingContext = new CompletadoModel();
            TareasPendiente = new ObservableCollection<Models.tareas.TareaPendiente>();

            // Llena la lista con datos
            LlenarListaTareas();     
        }

        private void LlenarListaTareas()
        {           
            Class.ConexionSqlite sqlite = new Class.ConexionSqlite("");
            var resultado = sqlite.TareasCompletada();
            var tareasCompletadas = resultado.Result;

            // Agrega las tareas completadas a la lista TareasPendiente
            foreach (var tarea in tareasCompletadas)
            {
                TareasPendiente.Add(tarea);
            }

            // Establece el ItemsSource del ListView como la lista TareasPendiente
            listaTareasCompletadas.ItemsSource = TareasPendiente;
        }
    }
}
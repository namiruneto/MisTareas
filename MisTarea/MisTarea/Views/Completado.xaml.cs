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
        public IList<Models.tareas.TareaPendiente> TareasPendiente { get; private set; }
        public int Consecutivo { get; set; }
       
        public Completado()
        {
            InitializeComponent();
            BindingContext = new CompletadoModel();
            TareasPendiente = new ObservableCollection<Models.tareas.TareaPendiente>();
            Class.ConexionSqlite sqlite = new Class.ConexionSqlite("");
            var resultado = sqlite.TareasCompletada();
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
           

        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisTarea.Views
{
    public partial class AboutPage : ContentPage
    {
        public ObservableCollection<Models.tareas.TareaPendiente> TareasPendiente { get; private set; }
        public AboutPage()
        {
            InitializeComponent();
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
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.tareas.TareaPendiente seleccion = e.SelectedItem as Models.tareas.TareaPendiente;
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.tareas.TareaPendiente seleccion = e.Item as Models.tareas.TareaPendiente;
        }
    }
}
using MisTarea.Class;
using MisTarea.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MisTarea.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string nombre;
        private string description;
        private DateTime hora;
        private DateTime fecha;
        
        private bool repetir;
        private bool repetirInv = true;
        private bool lunes;
        private bool martes;
        private bool miercoles;
        private bool jueves;
        private bool viernes;
        private bool sabado;
        private bool domingo;
        private webService _webService;
        private List<webService.Categoria> _categorias;
        private string _selectedCategoria;
        public ObservableCollection<string> ListaElementos { get; set; } = new ObservableCollection<string>();
        #region Variables
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }

        public string Descripcion
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public DateTime Hora
        {
            get => hora;
            set => SetProperty(ref hora, value);
        }
        public DateTime Fecha
        {
            get => fecha;
            set => SetProperty(ref fecha, value);
        }       
        public bool Repetir
        {
            get => repetir;
            set
            {
                if (SetProperty(ref repetir, value))
                {
                    RepetirInv = !repetir;
                }
            }

        }


        public bool RepetirInv
        {
            get => repetirInv;
            set => SetProperty(ref repetirInv, value);
        }


        public bool Lunes
        {
            get => lunes;
            set => SetProperty(ref lunes, value);
        }

        public bool Martes
        {
            get => martes;
            set => SetProperty(ref martes, value);
        }

        public bool Miercoles
        {
            get => miercoles;
            set => SetProperty(ref miercoles, value);
        }

        public bool Jueves
        {
            get => jueves;
            set => SetProperty(ref jueves, value);
        }

        public bool Viernes
        {
            get => viernes;
            set => SetProperty(ref viernes, value);
        }

        public bool Sabado
        {
            get => sabado;
            set => SetProperty(ref sabado, value);
        }

        public bool Domingo
        {
            get => domingo;
            set => SetProperty(ref domingo, value);
        }
        #endregion

        public NewItemViewModel()
        {
            _webService = new webService();
            LoadCategorias(); // Método para cargar las categorías al inicializar la vista modelo
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        public List<webService.Categoria> Categorias
        {
            get => _categorias;
            set
            {
                _categorias = value;
                OnPropertyChanged(nameof(Categorias));
            }
        }

        public string SelectedCategoria
        {
            get => _selectedCategoria;
            set
            {
                _selectedCategoria = value;
                OnPropertyChanged(nameof(SelectedCategoria));
            }
        }

        private async void LoadCategorias()
        {
            try
            {
                Categorias = await _webService.CargarCategorias();
                if (Categorias != null && Categorias.Count > 0)
                {
                    foreach(var categoria in Categorias)
                    {
                        ListaElementos.Add(categoria.Category);
                    }                   
                }
                else
                {
                    // Manejo de error si no se pueden cargar las categorías
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
            }
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(nombre)
                && !String.IsNullOrWhiteSpace(description);
        }



        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            CRUD crud = new CRUD();
            crud.Guardar(new tareas.NuevaTarea { 
                Nombre = Nombre, 
                Descripcion = Descripcion, 
                IdTipoTarea = _selectedCategoria, 
                FecIngreso = DateTime.Now.ToString("yyyy-MM-dd"), 
                Hora = Hora.ToString("HH:mm"), 
                FecFinal = Fecha.ToString("yyyy-MM-dd HH:mm"), 
                Repetir = Repetir, 
                Dias = String.Concat(Lunes ? "1-" : "", Martes ? "2-" : "", Miercoles ? "3-" : "", Jueves ? "4-" : "", Viernes ? "5-" : "", Sabado ? "6-" : "", Domingo ? "7" : "") 
            });           

        

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }


    }
}

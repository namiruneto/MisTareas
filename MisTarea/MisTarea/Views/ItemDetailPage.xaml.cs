using MisTarea.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MisTarea.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
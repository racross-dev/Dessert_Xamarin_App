using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantallaProductos : ContentPage
    {
        private readonly ProductosViewModel _viewModel;

        public PantallaProductos()
        {
            InitializeComponent();
            // Crear la instancia de ViewModel y pasar el contexto de navegación
            _viewModel = new ProductosViewModel(Navigation);
            BindingContext = _viewModel;  // Establecer el BindingContext
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            guardarProducto();
        }

        private async Task guardarProducto()
        {
            Productos mProductos = new Productos
            {
                Nombre_Producto = nombre.Text,
                Descripcion = descripcion.Text,
                Cantidad = int.Parse(cantidad.Text),
                Precio = int.Parse(precio.Text),
                Icono = icono.Text
            };

            await _viewModel.InsertarProducto(mProductos);  // Llamar al método InsertarProducto del ViewModel
            await DisplayAlert("Alerta", "Producto Guardado", "OK");
        }
    }
}

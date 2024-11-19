using System.Collections.ObjectModel;
using Xamarin.Forms;
using ProyectoFinal.Models;
using System.Linq;

namespace ProyectoFinal
{
    public partial class PantallaCarrito : ContentPage
    {
        public ObservableCollection<Productos> Carrito { get; set; }

        public PantallaCarrito(ObservableCollection<Productos> carrito)
        {
            InitializeComponent();
            Carrito = carrito ?? new ObservableCollection<Productos>(); // Asegura que Carrito no sea null
            BindingContext = this;
        }

        // Comando para incrementar la cantidad de un producto en el carrito
        public Command<Productos> IncreaseQuantityCommand => new Command<Productos>(async producto =>
        {
            if (producto != null)
            {
                // Validación para no exceder la cantidad en inventario
                if (producto.Cantidad < GetAvailableStock(producto.Id_User))
                {
                    producto.Cantidad += 1;
                    ActualizarCarrito();
                }
                else
                {
                    await DisplayAlert("Cantidad Excedida", $"No puedes agregar más del inventario disponible ({GetAvailableStock(producto.Id_User)})", "OK");
                }
            }
        });

        // Comando para decrementar la cantidad de un producto en el carrito
        public Command<Productos> DecreaseQuantityCommand => new Command<Productos>(producto =>
        {
            if (producto != null)
            {
                if (producto.Cantidad > 1)
                {
                    producto.Cantidad -= 1;
                }
                else
                {
                    // Si la cantidad es 1 o menos, elimina el producto del carrito
                    Carrito.Remove(producto);
                }
                ActualizarCarrito();
            }
        });

        // Comando para proceder al pago
        public Command ProceedToPaymentCommand => new Command(async () =>
        {
            // Aquí puedes navegar a la pantalla de pago o realizar otra acción
            await Navigation.PushAsync(new PantallaPago { Carrito = Carrito });
        });

        // Método para obtener la cantidad disponible en inventario
        private int GetAvailableStock(string productId)
        {
            // Aquí se simula el stock disponible. En una aplicación real, consultaría la base de datos.
            var productoEnInventario = Carrito.FirstOrDefault(p => p.Id_User == productId);
            return productoEnInventario != null ? productoEnInventario.Cantidad : 0;
        }

        // Método para actualizar la lista en la UI
        private void ActualizarCarrito()
        {
            // Actualizar el ItemsSource para reflejar los cambios en la UI
            CarritoListView.ItemsSource = null;
            CarritoListView.ItemsSource = Carrito;
        }
    }
}
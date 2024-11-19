// PantallaInicio.xaml.cs
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ProyectoFinal.Models;
using Firebase.Database;
using System;

namespace ProyectoFinal
{
    public partial class PantallaInicio : ContentPage
    {
        private ObservableCollection<Productos> productos;
        public ObservableCollection<Productos> Carrito { get; set; } = new ObservableCollection<Productos>();

        public PantallaInicio()
        {
            InitializeComponent();
            BindingContext = this;
            LoadProductos();
        }

        // Comando para navegar al carrito, pasando la lista de productos seleccionados
        public Command NavigateToCartCommand => new Command(async () =>
        {
            var carritoPage = new PantallaCarrito(Carrito);
            await Navigation.PushAsync(carritoPage);
        });

        // Comando para agregar un producto al carrito con validación de cantidad
        public Command AddToCartCommand => new Command<Productos>(async producto =>
        {
            if (producto.Cantidad > 0)
            {
                Carrito.Add(producto);
                await DisplayAlert("Producto Agregado", $"{producto.Nombre_Producto} ha sido agregado al carrito", "OK");
            }
            else
            {
                await DisplayAlert("Producto no disponible", $"{producto.Nombre_Producto} tiene una cantidad de 0 y no se puede agregar al carrito", "OK");
            }
        });

        private async void btnVolver_Clicked(object sender, EventArgs e)
        {
            // Navega a la nueva página llamada PaginaContenido
            await Navigation.PushAsync(new PantallaLogin());
        }


        private async void LoadProductos()
        {
            var firebase = new FirebaseClient("https://proyectofinal-3d2d2-default-rtdb.firebaseio.com/");
            var productosData = await firebase
                .Child("Productos")
                .OnceAsync<Productos>();

            productos = new ObservableCollection<Productos>();

            foreach (var item in productosData)
            {
                productos.Add(new Productos
                {
                    Id_User = item.Object.Id_User,
                    Nombre_Producto = item.Object.Nombre_Producto,
                    Descripcion = item.Object.Descripcion,
                    Cantidad = item.Object.Cantidad,
                    Precio = item.Object.Precio,
                    Icono = item.Object.Icono
                });
            }

            ProductosListView.ItemsSource = productos;
        }
    }
}

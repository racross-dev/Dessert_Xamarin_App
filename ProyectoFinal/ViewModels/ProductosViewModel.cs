// ProductosViewModel.cs
using ProyectoFinal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database.Query;
using ProyectoFinal.Conexion;
using System;

namespace ProyectoFinal.ViewModels
{
    public class ProductosViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;
        private List<Productos> ProductoA = new List<Productos>();
        private string IdProducto;

        // Comando para navegar a la pantalla de órdenes
        public Command NavigateToOrdenesCommand { get; }

        public ProductosViewModel(INavigation navigation)
        {
            _navigation = navigation;  // Asignar el contexto de navegación

            // Inicializar el comando de navegación para ir a PantallaOrdenes
            NavigateToOrdenesCommand = new Command(async () =>
            {
                if (_navigation != null)
                {
                    await _navigation.PushAsync(new PantallaOrdenes());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Navegación no configurada", "OK");
                }
            });
        }

        // Método para mostrar productos
        public async Task<List<Productos>> Mostrar_Productos()
        {
            List<Productos> ProductoA = new List<Productos>();

            try
            {
                var productos = await BaseDatos.firebase
                    .Child("Productos")
                    .OnceAsync<Productos>();

                foreach (var user in productos)
                {
                    if (user.Object != null)
                    {
                        Productos mProductos = new Productos
                        {
                            Id_User = user.Key,
                            Nombre_Producto = user.Object.Nombre_Producto,
                            Descripcion = user.Object.Descripcion,
                            Cantidad = user.Object.Cantidad,
                            Precio = user.Object.Precio,
                            Icono = user.Object.Icono
                        };

                        ProductoA.Add(mProductos);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Se produjo un error: {ex.Message}", "OK");
            }

            return ProductoA;
        }

        // Método para insertar un producto
        public async Task<string> InsertarProducto(Productos productos)
        {
            var data = await BaseDatos.firebase
                .Child("Productos")
                .PostAsync(new Productos()
                {
                    Nombre_Producto = productos.Nombre_Producto,
                    Descripcion = productos.Descripcion,
                    Cantidad = productos.Cantidad,
                    Precio = productos.Precio,
                    Icono = productos.Icono
                });

            IdProducto = data.Key;
            return IdProducto;
        }
    }
}

using Firebase.Database;
using ProyectoFinal.Models;
using ProyectoFinal;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Database.Query;
using System.Linq;
using Xamarin.Essentials;

namespace ProyectoFinal
{
    public partial class PantallaPago : ContentPage
    {
        public ObservableCollection<Productos> Carrito { get; set; }

        public PantallaPago()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command ConfirmPaymentCommand => new Command(async () =>
        {
            await UpdateProductQuantities();
            await DisplayAlert("Pago Confirmado", "Gracias por su compra", "OK");
            await Navigation.PopToRootAsync(); // Regresa a la pantalla de inicio
        });

        private async Task UpdateProductQuantities()
        {
            var firebase = new FirebaseClient("https://proyectofinal-3d2d2-default-rtdb.firebaseio.com/");

            foreach (var producto in Carrito)
            {
                // Obtener el producto actual de Firebase
                var firebaseProduct = (await firebase
                    .Child("Productos")
                    .OnceAsync<Productos>())
                    .FirstOrDefault(p => p.Object.Id_User == producto.Id_User);

                if (firebaseProduct != null)
                {
                    int nuevaCantidad = firebaseProduct.Object.Cantidad - 1; // Disminuye la cantidad

                    // Actualizar la cantidad en Firebase
                    await firebase
                        .Child("Productos")
                        .Child(firebaseProduct.Key)
                        .PutAsync(new Productos
                        {
                            Id_User = producto.Id_User,
                            Nombre_Producto = producto.Nombre_Producto,
                            Descripcion = producto.Descripcion,
                            Cantidad = nuevaCantidad,
                            Precio = producto.Precio,
                            Icono = producto.Icono
                        });
                }
            }
        }

        // Comando para copiar texto al portapapeles
        public Command<string> CopyCommand => new Command<string>((texto) =>
        {
            Clipboard.SetTextAsync(texto);  // Copiar al portapapeles
            DisplayAlert("Copiado", "Los datos han sido copiados al portapapeles", "OK");
        });
    }
}
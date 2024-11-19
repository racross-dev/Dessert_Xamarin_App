// PantallaOrdenes.xaml.cs
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Firebase.Database;
using ProyectoFinal.Models;

namespace ProyectoFinal
{
    public partial class PantallaOrdenes : ContentPage
    {
        public ObservableCollection<Orden> Ordenes { get; set; }

        public PantallaOrdenes()
        {
            InitializeComponent();
            Ordenes = new ObservableCollection<Orden>();
            BindingContext = this;
            CargarOrdenes();
        }

        private async void CargarOrdenes()
        {
            var firebase = new FirebaseClient("https://proyectofinal-3d2d2-default-rtdb.firebaseio.com/");

            // Obtener todas las órdenes desde Firebase
            var ordenesData = await firebase
                .Child("Ordenes")
                .OnceAsync<Orden>();

            // Convertir los datos y agregarlos a la colección
            foreach (var orden in ordenesData)
            {
                orden.Object.Id_Orden = orden.Key;
                Ordenes.Add(orden.Object);
            }

            // Asignar la lista de órdenes al ListView
            OrdenesListView.ItemsSource = Ordenes;
        }
    }
}

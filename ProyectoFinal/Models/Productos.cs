using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProyectoFinal.Models
{
    public class Productos
    {
        public string Id_User { get; set; }
        public string Nombre_Producto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Icono { get; set; }

        public Command AddToCartCommand { get; set; }

        public Productos()
        {
            // Inicializar el comando
            AddToCartCommand = new Command(() => App.Current.MainPage.DisplayAlert("Agregar", $"{Nombre_Producto} agregado al carrito", "OK"));
            Cantidad = 1;
        }
    }

}

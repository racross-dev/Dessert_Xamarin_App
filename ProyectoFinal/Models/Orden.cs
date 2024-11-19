// Models/Orden.cs
using System.Collections.Generic;

namespace ProyectoFinal.Models
{
    public class Orden
    {
        public string Id_Orden { get; set; }        // ID único de la orden
        public string EmailField { get; set; }      // Correo electrónico del usuario
        public List<ProductoOrden> Productos { get; set; }  // Lista de productos comprados
    }

    public class ProductoOrden
    {
        public string Nombre_Producto { get; set; } // Nombre del producto
        public int Cantidad { get; set; }           // Cantidad comprada
        public decimal Precio { get; set; }         // Precio unitario del producto
    }
}

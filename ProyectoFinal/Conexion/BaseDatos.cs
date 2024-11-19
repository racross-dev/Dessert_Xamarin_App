using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Conexion
{
     public class BaseDatos
    {
        public static FirebaseClient firebase = new FirebaseClient("https://proyectofinal-3d2d2-default-rtdb.firebaseio.com/");
    }
}

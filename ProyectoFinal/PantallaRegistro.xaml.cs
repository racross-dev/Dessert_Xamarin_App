using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoFinal
{
    public partial class PantallaRegistro : ContentPage
    {
        public PantallaRegistro()
        {
            InitializeComponent();
        }
        private async void btnInciasesion_Clicked(object sender, EventArgs e)
        {
            // Navega a la nueva página llamada PaginaContenido
            await Navigation.PushAsync(new PantallaLogin());
        }


        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            guardarUsuario();
        }

        private async Task guardarUsuario()
        {
            Usuario usuarios = new Usuario();
            RegisterViewModel metodo = new RegisterViewModel();

            usuarios.Nombre = usuario.Text;
            usuarios.ApellidoPaterno = ApellidoP.Text;
            usuarios.ApellidoMaterno = ApellidoM.Text;

            await metodo.InsertarUsuario(usuarios);
            //await DisplayAlert("Alerta", "Usuario Guardado", "ok");
        }
    }
}

// LoginAdmin.xaml.cs
using System;
using Xamarin.Forms;
using Firebase.Auth;

namespace ProyectoFinal
{
    public partial class LoginAdmin : ContentPage
    {
        private FirebaseAuthProvider authProvider;

        public LoginAdmin()
        {
            InitializeComponent();
            BindingContext = this;

            // Initialize FirebaseAuthProvider with API Key
            authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyARDhROMzauZUX1Y6FsL0-h-hTsk8Dw4O0"));
        }

        public Command AdminLoginCommand => new Command(async () => await OnAdminLogin());

        private async System.Threading.Tasks.Task OnAdminLogin()
        {
            string email = CorreoEntry.Text;
            string password = PasswordEntry.Text;

            if (email == "adminpostreria@gmail.com")
            {
                try
                {
                    // Authenticate using the FirebaseAuthProvider instance
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

                    // Navigate to PantallaProductos.xaml after successful login
                    await Navigation.PushAsync(new PantallaProductos());
                }
                catch (Exception)
                {
                    await DisplayAlert("Error", "No se pudo iniciar sesión. Verifica tus credenciales.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Acceso Denegado", "You should not pass", "OK");
            }
        }
    }
}

using Firebase.Auth;
using ProyectoFinal.Conexion;
using ProyectoFinal.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoFinal.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private string email;
        private string clave;
        #endregion

        #region Propiedades
        public string txtcorreo
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }

        public string txtcontraseña
        {
            get { return clave; }
            set { SetValue(ref clave, value); }
        }
        #endregion

        #region Command
        public Command LoginCommand { get; }
        #endregion

        #region Metodo
        public async Task LoginUsuario()
        {
            var objusuario = new UserModel()
            {
                EmailField = email,
                PasswordField = clave,
            };

            try
            {
                var autenticacion = new FirebaseAuthProvider(new FirebaseConfig(DBConn.WepApyAuthentication));
                var authuser = await autenticacion.SignInWithEmailAndPasswordAsync(objusuario.EmailField.ToString(), objusuario.PasswordField.ToString());
                string obternertoken = authuser.FirebaseToken;

                // Guardar el correo en las propiedades de la aplicación
                Application.Current.Properties["UserEmail"] = objusuario.EmailField;
                await Application.Current.SavePropertiesAsync();

                // Navegar a PantallaInicio en lugar de PantallaProductos
                var Propiedades_NavigationPage = new NavigationPage(new PantallaInicio());
                Propiedades_NavigationPage.BarBackgroundColor = Color.RoyalBlue;
                App.Current.MainPage = Propiedades_NavigationPage;
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Advertencia", "Los datos introducidos son incorrectos o el usuario se encuentra inactivo.", "Aceptar");
            }
        }
        #endregion

        #region Constructor

        public Command NavigateToRegisterCommand { get; }
        public Command NavigateToAdminLoginCommand { get; }

        public LoginViewModel(INavigation navegar)
        {
            Navigation = navegar;
            LoginCommand = new Command(async () => await LoginUsuario());
            NavigateToRegisterCommand = new Command(async () => await Navigation.PushAsync(new PantallaRegistro()));
            NavigateToAdminLoginCommand = new Command(async () => await Navigation.PushAsync(new LoginAdmin()));
        }
        #endregion
    }
}

using Firebase.Auth;
using Firebase.Database.Query;
using ProyectoFinal.Conexion;
using ProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoFinal.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Atributos
        private string email;
        private string password;
        private string confirmPassword;
        private string IdUsuario;
        private List<Usuario> UsuarioA = new List<Usuario>();
        private string nombreField;
        private string apellidoPField;
        private string apellidoMField;
        #endregion

        #region Propiedades
        public string NombreField
        {
            get { return nombreField; }
            set { SetValue(ref nombreField, value); }
        }

        public string ApellidoPField
        {
            get { return apellidoPField; }
            set { SetValue(ref apellidoPField, value); }
        }

        public string ApellidoMField
        {
            get { return apellidoMField; }
            set { SetValue(ref apellidoMField, value); }
        }

        public string EmailField
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }

        public string PasswordField
        {
            get { return password; }
            set { SetValue(ref password, value); }
        }

        public string ConfirmPasswordField
        {
            get { return confirmPassword; }
            set { SetValue(ref confirmPassword, value); }
        }
        #endregion

        #region Commands
        public Command RegisterCommand { get; }
        #endregion

        #region Métodos

        public async Task<List<Usuario>> Mostrar_Usuarios()
        {
            var usuarios = await BaseDatos.firebase
                .Child("Usuarios")
                .OnceAsync<Usuario>();

            foreach (var user in usuarios)
            {
                Usuario mUsuario = new Usuario
                {
                    Id_User = user.Key,
                    Nombre = user.Object.Nombre,
                    ApellidoPaterno = user.Object.ApellidoPaterno,
                    ApellidoMaterno = user.Object.ApellidoMaterno
                };

                UsuarioA.Add(mUsuario);
            }
            return UsuarioA;
        }

        public async Task<string> InsertarUsuario(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Nombre) ||
                string.IsNullOrEmpty(usuario.ApellidoPaterno) ||
                string.IsNullOrEmpty(usuario.ApellidoMaterno))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Los campos de nombre y apellidos son obligatorios.", "Aceptar");
                return null;
            }

            try
            {
                var data = await BaseDatos.firebase
                    .Child("Usuarios")
                    .PostAsync(new Usuario
                    {
                        Nombre = usuario.Nombre,
                        ApellidoPaterno = usuario.ApellidoPaterno,
                        ApellidoMaterno = usuario.ApellidoMaterno
                    });

                IdUsuario = data.Key;
                return IdUsuario;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Hubo un problema al guardar el usuario: {ex.Message}", "Aceptar");
                return null;
            }
        }

        public async Task RegisterUser()
        {
            if (string.IsNullOrEmpty(NombreField) || string.IsNullOrEmpty(ApellidoPField) || string.IsNullOrEmpty(ApellidoMField))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Los campos de nombre y apellidos son obligatorios.", "Aceptar");
                return;
            }

            var objUsuario = new UserModel
            {
                EmailField = EmailField,
                PasswordField = PasswordField,
            };

            if (string.IsNullOrEmpty(objUsuario.EmailField) || string.IsNullOrEmpty(objUsuario.PasswordField) || string.IsNullOrEmpty(ConfirmPasswordField))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "Aceptar");
                return;
            }

            if (objUsuario.PasswordField != ConfirmPasswordField)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Las contraseñas no coinciden.", "Aceptar");
                return;
            }

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(DBConn.WepApyAuthentication));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(objUsuario.EmailField, objUsuario.PasswordField);
                await App.Current.MainPage.DisplayAlert("Éxito", "Usuario registrado correctamente.", "Aceptar");
                await App.Current.MainPage.Navigation.PopAsync(); // Navegar a la pantalla anterior
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error con el registro", "Aceptar");
            }
        }

        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await RegisterUser());
        }
        #endregion
    }
}

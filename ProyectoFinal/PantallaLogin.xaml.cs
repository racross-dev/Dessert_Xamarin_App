
using ProyectoFinal.Models;
using ProyectoFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PantallaLogin : ContentPage
    {
        public PantallaLogin()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

        private void btnAdmin_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PantallaInicio());
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinal.Models
{
    public class UserModel
    {
        internal string NombreField;

        public string EmailField { get; set; }
        public string PasswordField { get; set; }

        public string ConfirmPasswordField { get; set; }
    }
}

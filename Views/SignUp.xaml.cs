using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TapPay.Services;


namespace TapPay.Views
{
    public partial class SignUp : ContentPage
    {
          private DatabaseService databaseService;

        public SignUp()
        {
            InitializeComponent();
            
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.104,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
        }


        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text;
            var apellido = ApellidoEntry.Text;
            var cedula = CedulaEntry.Text;
            var correo = EmailEntry.Text;
            var telefono = TelefonoEntry.Text;
            var contrasena = PasswordEntry.Text; 
            var confirmarContrasena = ConfirmarContrasenaEntry.Text;   

            if(contrasena != confirmarContrasena){
                await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
                return;
            }
            await databaseService.OnRegisterClicked(cedula, nombre, apellido, correo, telefono, contrasena);
            await DisplayAlert("Éxito", "Registro Completado.", "OK");
        }

         private void OnTogglePasswordButtonClicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            ConfirmarContrasenaEntry.IsPassword = !ConfirmarContrasenaEntry.IsPassword;
            TogglePasswordButton.Source = PasswordEntry.IsPassword ? "hidden.png" : "visible.png";
        }
    }
}




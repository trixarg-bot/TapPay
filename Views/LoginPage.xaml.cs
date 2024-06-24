using System;
// using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TapPay.Services;


namespace TapPay.Views
{
    public partial class LoginPage : ContentPage
    {
        private string connectionString = "Server=192.168.68.114,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
        private DatabaseService databaseService;
        private NavigationHelper navigationHelper = new NavigationHelper();

        public LoginPage()
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.114,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string correo_electronico = EmailEntry.Text;
            string password = PasswordEntry.Text;
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            if (await ValidateLoginAsync(correo_electronico, password))
            {
                // Obtener el usuario_id
                var (id, existe) = await databaseService.ObtenerIdUsuarioAsync(correo_electronico);

                if (existe && id != -1)
                {
                    await App.SyncDatabaseAsync(id);
                    //* Si el login es exitoso, navega a la página principal pasando el usuario_id
                    DataPage dataPage = new DataPage(id);
                    await NavigationHelper.NavigateWithLoadingIndicatorAsync(this, dataPage, LoadingIndicator); ;
                    // await Navigation.PushAsync(new DataPage(id));
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo obtener el ID del usuario.", "OK");
                    //* Manejar el caso de error, por ejemplo, redirigir al inicio de sesión.
                }

            }
            else
            {
                //* Mostrar un mensaje de error si las credenciales no son correctas
                await DisplayAlert("Error", "Correo electrónico o contraseña incorrectos.", "OK");
            }
        }

        private async Task<bool> ValidateLoginAsync(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT COUNT (1) FROM Usuario WHERE correo_electronico = @Email AND contrasena = @Contrasena";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Contrasena", password);

                        object? result = await command.ExecuteScalarAsync();
                        if (result != null && result != DBNull.Value && int.TryParse(result.ToString(), out int count))
                        {
                            return count > 0;
                        }
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                //* Manejo de excepciones
                await DisplayAlert("Error", $"Ocurrió un error al intentar iniciar sesión: {ex.Message}", "OK");
                return false;
            }

        }

        //*metodo del boton de cambio de vista de la contra
        private void OnTogglePasswordButtonClicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            TogglePasswordButton.Source = PasswordEntry.IsPassword ? "hidden.png" : "visible.png";
        }


        //* Boton para viajar a la pagina de registro de nuevo usuario.
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUp());
        }

        //*Boton para viaje a la pagina de Registros
        //  private async void OnPageRegisterClicked(object sender, EventArgs e)
        // {
        //     string correo_electronico = EmailEntry.Text;
        //     string password = PasswordEntry.Text;

        //     if (await ValidateLoginAsync(correo_electronico, password))
        //     {
        //          // Obtener el usuario_id
        //         var (id, existe) = await databaseService.ObtenerIdUsuarioAsync(correo_electronico);

        //         if (existe && id != -1)
        //         {
        //             //  await App.SyncDatabaseAsync(id);
        //             //* Si el login es exitoso, navega a la página principal pasando el usuario_id
        //             await Navigation.PushAsync(new RegisterPage(id));
        //         }
        //         else
        //         {
        //             await DisplayAlert("Error", "No se pudo obtener el ID del usuario.", "OK");
        //             //* Manejar el caso de error, por ejemplo, redirigir al inicio de sesión.
        //         }
        //     }
        //     else
        //     {
        //         //* Mostrar un mensaje de error si las credenciales no son correctas
        //         await DisplayAlert("Error", "Correo electrónico o contraseña incorrectos.", "OK");
        //     }

        // }

    }



}
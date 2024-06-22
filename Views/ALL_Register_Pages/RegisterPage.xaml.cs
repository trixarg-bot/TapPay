// using System;
// using System.Data.SqlClient;
// using System.Threading.Tasks;
// using Microsoft.Maui.Controls;
// using TapPay.Services;

// namespace TapPay.Views
// {
//     public partial class RegisterPage : ContentPage
//     {
//          private string connectionString = "Server=192.168.68.104,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
//          private DatabaseService databaseService;
//          public int usuario_id;

//         public RegisterPage(int usuario_id)
//         {
//            InitializeComponent();
//            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
//            string sqlServerConnectionString = "Server=192.168.68.104,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
//            databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
//            this.usuario_id = usuario_id;
//         }


// //*metodo del boton para entrar a la pagina de registro de organizador.
//         private async void OnNewOrganizadorClicked(object sender, EventArgs e)
//         {
//             await Navigation.PushAsync(new ADD_Organizador(usuario_id));
//         }
// //*metodo del boton para entrar a la pagina de registro de evento.
//         private async void OnNewEventoClicked(object sender, EventArgs e)
//         {
//            await Navigation.PushAsync(new ADD_Evento(usuario_id));
//         }

        

//     }

   

// }
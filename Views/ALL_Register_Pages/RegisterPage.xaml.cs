using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TapPay.Services;

namespace TapPay.Views
{
    public partial class RegisterPage : ContentPage
    {
         private string connectionString = "Server=192.168.68.104,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
         private DatabaseService databaseService;
         public int usuario_id;

        public RegisterPage(int usuario_id)
        {
           InitializeComponent();
           string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
           string sqlServerConnectionString = "Server=192.168.68.104,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
           databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
           this.usuario_id = usuario_id;
        }


        //TODO IMPLEMENTAR LA FUNCIONALIDAD DE ENTRADA A LAS PAGINA DE REGISTRO DE ORGANIZADOR Y EVENTO. 
        //TODO ESTO EN LA RESPECTIVA PAGINA DE CADA UNO DE LOS REGISTROS: CONSIGUIR EL ID DEL ORGANIZADOR A TRAVES DE SU RNC EN EL REGISTRO DE EVENTOS. IMPLEMENTAR EL GUARDADO DE LOS REGISTRO A LA BASE DE DATOS.




        private async void OnNewOrganizadorClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ADD_Organizador(usuario_id));
        }

        private async void OnNewEventoClicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new ADD_Evento(usuario_id));
        }

    }

   

}
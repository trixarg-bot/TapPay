using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TapPay.Models;
using TapPay.Services;

namespace TapPay.Views
{
    public partial class ADD_Organizador : ContentPage
    {
         
        private DatabaseService databaseService;
        public int usuario_id;



        public ADD_Organizador(int usuario_id)
        {
            InitializeComponent();
           string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
           string sqlServerConnectionString = "Server=10.0.0.159,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
           databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
           this.usuario_id = usuario_id;

        }


      

        private async void OnOrganizadorRegisterClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text;
            var correo_electronico = EmailEntry.Text;
            var telefono = TelefonoEntry.Text;
            var Rnc = RncEntry.Text;
            var id = usuario_id;
            

            await databaseService.OnRegisterOrganizadorClicked(nombre, correo_electronico, telefono, Rnc, id);
            await DisplayAlert("Éxito", "Registro Completado.", "OK");
        }
    }

   

}
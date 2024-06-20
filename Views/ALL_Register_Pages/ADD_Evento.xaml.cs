using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TapPay.Models;
using TapPay.Services;

namespace TapPay.Views
{
    public partial class ADD_Evento : ContentPage
    {
         
         private DatabaseService databaseService;
        public int usuario_id;
        private int organizador_id;

        public ADD_Evento(int usuario_id)
        {
            InitializeComponent();
           string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
           string sqlServerConnectionString = "Server=192.168.68.103,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
           databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
           this.usuario_id = usuario_id;
        }



         public async void OnVerificarEmpresaClicked(object sender, EventArgs e)
        {
            string RNC = RncEntry.Text;

            // Obtener el Organizador_id
            var (id, existe) = await databaseService.ObtenerIdOrganizadorAsync(RNC);

            if (existe && id != -1)
            {
                // Si la verificación es exitosa, guarda el organizador_id en una variable
                organizador_id = id;
                await DisplayAlert("Éxito", $"Organizador ID: {organizador_id} verificado exitosamente.", "OK");
                
                // Aquí puedes realizar otras acciones utilizando el organizador_id
            }
            else
            {
                await DisplayAlert("Error", "No se pudo obtener el ID del organizador.", "OK");
                // Manejar el caso de error, por ejemplo, redirigir al inicio de sesión.
            }
        }


        private async void OnEventoRegisterClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text;
            var Fecha = FechaEntry.Text;
            var HoraInicio = HoraInicioEntry.Text;
            var HoraFin = HoraFinEntry.Text;
            var Ubicacion = UbicacionEntry.Text;
            var id_usuario = usuario_id;
            var id_organizador = organizador_id;
            

            await databaseService.OnRegisterEventoClicked(nombre, Fecha, HoraInicio, HoraFin, Ubicacion, id_usuario, id_organizador);
            await DisplayAlert("Éxito", "Registro Completado.", "OK");
        }

    }

   

}
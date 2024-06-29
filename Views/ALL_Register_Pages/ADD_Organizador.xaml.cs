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
            string sqlServerConnectionString = "Server=192.168.68.108,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
            this.usuario_id = usuario_id;

        }



        //*METODO PARA REGISTRAR UN NUEVO ORGANIZADOR
        private async void OnOrganizadorRegisterClicked(object sender, EventArgs e)
        {
            try
            {
                var nombre = NombreEntry.Text;
                var correo_electronico = EmailEntry.Text;
                var telefono = TelefonoEntry.Text;
                var Rnc = RncEntry.Text;
                var id = usuario_id;


                await databaseService.OnRegisterOrganizadorClicked(nombre, correo_electronico, telefono, Rnc, id);
                await DisplayAlert("Éxito", "Registro Completado.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al Editar el producto: {ex.Message}", "OK");
            }


        }


        //*METODO PARA EDITAR UN ORGANIZADOR
        private async void OnEditRegisterClicked(object sender, EventArgs e)
        {

            try
            {

                if (BindingContext is Organizador organizador)
                {
                    var id_organizador = organizador.OrganizadorId;
                    var nombre = NombreEntry.Text;
                    var Correo_electronico = EmailEntry.Text;
                    var telefono = TelefonoEntry.Text;
                    var rnc = RncEntry.Text;

                    await databaseService.OnEditOrganizadorClicked(id_organizador, nombre, Correo_electronico, rnc);
                    await DisplayAlert("Éxito", "Organziador Editado Correctamente.", "OK");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", $"Ocurrió un error al Editar el producto: {ex.Message}", "OK");

            }
        }



        public void SetOrganizadorData(Organizador organizador)
        {
            NombreEntry.Text = organizador.Nombre;
            EmailEntry.Text = organizador.CorreoElectronico;
            TelefonoEntry.Text = organizador.Telefono;
            RncEntry.Text = organizador.rnc;
            BindingContext = organizador;
        }

    }



}
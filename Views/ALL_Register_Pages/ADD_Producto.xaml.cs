using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using TapPay.Models;
using TapPay.Services;

namespace TapPay.Views
{
    public partial class ADD_producto : ContentPage
    {

        private DatabaseService databaseService;
        public int usuario_id;

        private string namepage = "producto";

        public ADD_producto(int usuario_id)
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.114,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
            this.usuario_id = usuario_id;
        }

        //*Metodo para registrar el evento en la base de datos
        private async void OnProductoRegisterClicked(object sender, EventArgs e)
        {
            var nombre = NombreEntry.Text;
            var descripcion = descripcionEntry.Text;
            var precio = decimal.Parse(precioEntry.Text);
            var id_usuario = usuario_id;



            await databaseService.OnRegisterProductoClicked(nombre, descripcion, precio, id_usuario);
            await DisplayAlert("Éxito", "Registro Completado.", "OK");
        }




    }



}
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



        public ADD_producto(int usuario_id)
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.108,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            databaseService = new DatabaseService(dbPath, sqlServerConnectionString);
            this.usuario_id = usuario_id;
            // NombreEntry.Text = nombre;
            // descripcionEntry.Text = descripcion;
            // precioEntry.Text = precio.ToString();
        }



        public void SetProductoData(Producto producto)
        {
            NombreEntry.Text = producto.Nombre;
            descripcionEntry.Text = producto.Descripcion;
            precioEntry.Text = producto.Precio.ToString();
            BindingContext = producto;
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



        
        //*METODO PARA EDITAR UN PRODUCTO
        private async void OnEditRegisterClicked(object sender, EventArgs e)
        {

            try
            {

                if (BindingContext is Producto producto_id)
                {
                    var id_producto = producto_id.ProductoId;
                    var nombre = NombreEntry.Text;
                    var descripcion = descripcionEntry.Text;
                    var precio = decimal.Parse(precioEntry.Text);


                    await databaseService.OnEditClicked(id_producto, nombre, descripcion, precio);
                    await DisplayAlert("Éxito", "Producto Editado Correctamente.", "OK");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Error", $"Ocurrió un error al Editar el producto: {ex.Message}", "OK");

            }
        }
    }
}
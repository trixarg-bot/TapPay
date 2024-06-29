using Microsoft.Maui.Controls;
using System.Collections.Generic;
using TapPay.Models;
using TapPay.Services;
using System.Windows.Input;

namespace TapPay.Views
{
    public partial class ProductoView : ContentPage
    {
        private DatabaseService _databaseService;
        private int usuario_id;
        public ProductoView(int usuario_id)
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.108,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            LoadData();
            _databaseService = new DatabaseService(dbPath, sqlServerConnectionString);

            this.usuario_id = usuario_id;


        }





        //*METODO PARA RECARGAR LOS DATOS DE LA PAGIANA CADA QUE SE INGRESA A LA MISMA.
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await App.SyncDatabaseAsync(usuario_id);
            LoadData();
        }


        //*Metodo QUE MUESTRA LOS DATOS DE LA BASE DE DATOS EN PANTALLA.
        public async void LoadData()
        {
            // Cargar los datos de la base de datos
            List<Producto> productos = await App.Database.GetProductosAsync(usuario_id);


            // Vincular los datos a los CollectionView
            ProductosCollectionView.ItemsSource = productos;
            // Agrega más llamadas a métodos de carga de datos y vinculación para Usuarios, Tarjetas NFC, Productos, Transacciones según sea necesario
        }


        //*METODO QUE SE LLAMA PARA ELEIMINAR UN ELEMENTO
        private async void OnDeleteInvoked(object sender, EventArgs e)

        {

            try
            {
                if (sender is SwipeItem swipeItem)
                {
                    if (swipeItem.BindingContext is Producto producto)
                    {
                        // Eliminar el producto de la base de datos
                        await _databaseService.OnDeleteInvoked(producto.ProductoId);

                        // Recargar los datos
                        await App.SyncDatabaseAsync(usuario_id);
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                await DisplayAlert("Error", $"Ocurrió un error al eliminar el producto: {ex.Message}", "OK");
                Console.WriteLine(ex); // O usar un logger si tienes uno configurado
            }


        }


        //*METODO PARA EDITAR UN ELEMENTO
        private async void OnEditInvoked(object sender, EventArgs e)
        {
            try
            {
                if (sender is SwipeItem swipeItem)
                {
                    if (swipeItem.BindingContext is Producto producto)
                    {
                        // Crear instancia de la página de agregar propductos
                        var addProductoPage = new ADD_producto(usuario_id);
                        
                        // Navegar a la página de edición
                        await Navigation.PushAsync(addProductoPage);

                        // Asignar los detalles del producto a la página de edición
                        addProductoPage.SetProductoData(producto);
                        // var add_producto = new ADD_producto(usuario_id, producto.Nombre, producto.Descripcion, producto.Precio);

                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                await DisplayAlert("Error", $"Ocurrió un error al Editar el producto: {ex.Message}", "OK");
                Console.WriteLine(ex); // O usar un logger si tienes uno configurado
            }
        }


        //*BOTON PARA VIAJAR HACIA LA PAGINA PARA AGREGAR UN PRODUCTO
        private async void OnAgregarProductoClicked(object sender, EventArgs e)
        {
            try
            {
                ADD_producto add_producto = new ADD_producto(usuario_id);
                LoadingIndicator.IsRunning = true;
                LoadingIndicator.IsVisible = true;
                await NavigationHelper.NavigateWithLoadingIndicatorAsync(this, add_producto, LoadingIndicator);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error al eliminar el producto: {ex.Message}", "OK");
                Console.WriteLine(ex);
            }


        }





    }
}

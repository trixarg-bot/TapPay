using Microsoft.Maui.Controls;
using System.Collections.Generic;
using TapPay.Models;
using TapPay.Services;


namespace TapPay.Views
{
    public partial class DataPage : ContentPage
    {
        private DatabaseService _databaseService;
        private int usuario_id;


        public DataPage(int usuario_id)
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.114,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            // LoadData();
            _databaseService = new DatabaseService(dbPath, sqlServerConnectionString);

            this.usuario_id = usuario_id;

        }

        
        //*Boton para viaje hacia la pagian de Organizadores
        private async void OnOrganizadorClicked(object sender, EventArgs e)
        {

           LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            OrganizadorView organizadorView = new OrganizadorView(usuario_id);
            await NavigationHelper.NavigateWithLoadingIndicatorAsync(this, organizadorView, LoadingIndicator);
        }
       
       //*Boton para viaje hacia la pagian de eventos
        private async void OnEventoClicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            EventoView eventoView = new EventoView(usuario_id);
            await NavigationHelper.NavigateWithLoadingIndicatorAsync(this, eventoView, LoadingIndicator);
        }

        //*Boton para viaje hacia la pagina de Productos
         private async void OnProductoClicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsRunning = true;
            LoadingIndicator.IsVisible = true;
            ProductoView productoView = new ProductoView(usuario_id);
            await NavigationHelper.NavigateWithLoadingIndicatorAsync(this, productoView, LoadingIndicator);
        }



        // private async void LoadData()
        // {
        //     // Cargar los datos de la base de datos
        //     List<Organizador> organizadores = await App.Database.GetOrganizadoresAsync(usuario_id);
        //     List<Evento> eventos = await App.Database.GetEventosAsync(usuario_id);
        //     List<Usuario> Usuarios = await App.Database.GetUsuariosAsync();
        //     List<Producto> productos = await App.Database.GetProductosAsync();
        //     List<Cliente> clientes = await App.Database.GetClientesAsync(usuario_id);

        //     // Vincular los datos a los CollectionView
        //     OrganizadoresCollectionView.ItemsSource = organizadores;
        //     UsuariosCollectionView.ItemsSource = Usuarios;
        //     EventosCollectionView.ItemsSource = eventos;
        //     ProductosCollectionView.ItemsSource = productos;
        //     ClientesCollectionView.ItemsSource = clientes;



        // }
    }
}

using Microsoft.Maui.Controls;
using System.Collections.Generic;
using TapPay.Models;
using TapPay.Services;

namespace TapPay.Views
{
    public partial class EventoView : ContentPage
    {
        private DatabaseService _databaseService;
        private int usuario_id;
        public EventoView(int usuario_id)
        {
            InitializeComponent();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.103,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
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
        private async void LoadData()
        {
            // Cargar los datos de la base de datos
            List<Evento> eventos = await App.Database.GetEventosAsync(usuario_id);


            // Vincular los datos a los CollectionView
            EventosCollectionView.ItemsSource = eventos;
            // Agrega más llamadas a métodos de carga de datos y vinculación para Usuarios, Tarjetas NFC, Productos, Transacciones según sea necesario
        }
        //*BOTON PARA VIAJAR HACIA LA PAGINA PARA AGREGAR UN EVENTO
         private async void OnAgregarEventoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ADD_Evento(usuario_id));
        }
    }
}

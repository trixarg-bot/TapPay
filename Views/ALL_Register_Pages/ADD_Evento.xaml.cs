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
            LoadData();
            this.usuario_id = usuario_id;
        }

        //*Metodo para registrar el evento en la base de datos
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


        private async void LoadData()
        {
            //* Cargar los datos de la base de datos
            List<Organizador> organizadores = await App.Database.GetOrganizadoresAsync(usuario_id);


            //* Vincular los datos a los CollectionView
            OrganizadorPicker.ItemsSource = organizadores;




        }
        // *METODO DE LA LISTA(PICKER) PARA MOSTRAR LOS ORGANIZADORES
        private void OnOrganizadorSelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            if (picker.SelectedIndex != -1)
            {
                var selectedOrganizador = (Organizador)picker.SelectedItem;
                organizador_id = selectedOrganizador.OrganizadorId; //* Almacenar el organizador_id
                DisplayAlert("Organizador Seleccionado", $"Has seleccionado a {selectedOrganizador.Nombre}", "OK");
            }
        }

    }



}
using Microsoft.Maui.Controls;
using System;
using System.IO;
using TapPay.Services;

namespace TapPay
{
    public partial class App : Application
    {
        private static DatabaseService _database;
        
        public static DatabaseService Database => _database;
        public App()
        {
            InitializeComponent();

            // Inicializa los datos de ejemplo
            InitializeDatabaseAsync();

            MainPage = new AppShell();
        }

         private static void InitializeDatabaseAsync()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TapPay.db3");
            string sqlServerConnectionString = "Server=192.168.68.104,1433;Database=TapPay;User Id=sa;Password=5a$Rv9&d2!Fm;TrustServerCertificate=True";
            _database = new DatabaseService(dbPath, sqlServerConnectionString);
        }
        
        public static async Task SyncDatabaseAsync(int usuario_id)
        {
            await Database.SyncWithSqlServerAsync(usuario_id);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

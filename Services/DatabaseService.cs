using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using TapPay.Models;
using TapPay.Views;
// using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;


namespace TapPay.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        private readonly string sqlServerConnectionString;
        public DatabaseService(string dbPath, string sqlServerConnectionString)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            this.sqlServerConnectionString = sqlServerConnectionString;
            _database.CreateTableAsync<Cliente>().Wait();
            _database.CreateTableAsync<Organizador>().Wait();
            _database.CreateTableAsync<Evento>().Wait();
            _database.CreateTableAsync<Usuario>().Wait();
            _database.CreateTableAsync<TarjetaNFC>().Wait();
            _database.CreateTableAsync<Producto>().Wait();
            _database.CreateTableAsync<Transaccion>().Wait();
        }



        public async Task SyncWithSqlServerAsync(int usuario_id)
        {

            // Limpiar la base de datos (opcional)
            await _database.DeleteAllAsync<Organizador>();
            await _database.DeleteAllAsync<Evento>();
            await _database.DeleteAllAsync<Usuario>();
            await _database.DeleteAllAsync<TarjetaNFC>();
            await _database.DeleteAllAsync<Producto>();
            await _database.DeleteAllAsync<Transaccion>();
            await _database.DeleteAllAsync<Cliente>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlServerConnectionString))
                {
                    await sqlConnection.OpenAsync();

                    // sincronización de la tabla Organizador
                    string query = "SELECT * FROM Organizador where usuario_id = @usuario_id";
                    using (SqlCommand commandOrganizador = new SqlCommand(query, sqlConnection))
                    {
                        commandOrganizador.Parameters.AddWithValue("@usuario_id", usuario_id);
                        using (SqlDataReader reader = await commandOrganizador.ExecuteReaderAsync())
                        {
                            var organizadores = new List<Organizador>();
                            while (await reader.ReadAsync())
                            {
                                var organizador = new Organizador
                                {
                                    OrganizadorId = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    CorreoElectronico = reader.GetString(2),
                                    Telefono = reader.GetString(3),
                                    rnc = reader.GetString(4),
                                    //usuario_id = reader.GetInt32(5) 
                                    usuario_id = usuario_id
                                };
                                organizadores.Add(organizador);
                            }
                            await _database.InsertAllAsync(organizadores, runInTransaction: true);
                        }
                    }
                    // sincronización de la tabla Usuario
                    string queryUsuario = "SELECT * FROM Usuario";
                    using (SqlCommand commandUsuario = new SqlCommand(queryUsuario, sqlConnection))
                    {
                        using (SqlDataReader reader = await commandUsuario.ExecuteReaderAsync())
                        {
                            var usuarios = new List<Usuario>();
                            while (await reader.ReadAsync())
                            {
                                var usuario = new Usuario
                                {
                                    UsuarioId = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    CorreoElectronico = reader.GetString(3),
                                    Telefono = reader.GetString(4),
                                    Contrasena = reader.GetString(5),
                                    Cedula = reader.GetString(6)

                                };
                                usuarios.Add(usuario);
                            }
                            await _database.InsertAllAsync(usuarios, runInTransaction: true);
                        }
                    }

                    // sincronización de la tabla Evento
                    string queryEvento = "SELECT * FROM evento where usuario_id = @usuario_id";
                    using (SqlCommand commandEvento = new SqlCommand(queryEvento, sqlConnection))
                    {
                        commandEvento.Parameters.AddWithValue("@usuario_id", usuario_id);
                        using (SqlDataReader reader = await commandEvento.ExecuteReaderAsync())
                        {
                            var Eventos = new List<Evento>();
                            while (await reader.ReadAsync())
                            {
                                var Evento = new Evento
                                {
                                    EventoId = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Fecha = reader.GetDateTime(2),
                                    HoraInicio = reader.GetTimeSpan(3),
                                    HoraFin = reader.GetTimeSpan(4),
                                    Ubicacion = reader.GetString(5),
                                    OrganizadorId = reader.GetInt32(6),
                                    usuario_id = usuario_id

                                };
                                Eventos.Add(Evento);
                            }
                            await _database.InsertAllAsync(Eventos, runInTransaction: true);
                        }
                    }

                    //sincronización de la tabla Producto
                    string queryProducto = "SELECT * FROM Producto";
                    using (SqlCommand commandProducto = new SqlCommand(queryProducto, sqlConnection))
                    {
                        using (SqlDataReader reader = await commandProducto.ExecuteReaderAsync())
                        {
                            var Productos = new List<Producto>();
                            while (await reader.ReadAsync())
                            {
                                var Producto = new Producto
                                {
                                    ProductoId = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Descripcion = reader.GetString(2),
                                    Precio = reader.GetDecimal(3),

                                };
                                Productos.Add(Producto);
                            }
                            await _database.InsertAllAsync(Productos, runInTransaction: true);
                        }
                    }
                    // Sincronización de la tabla Cliente
                    string queryCliente = "SELECT * FROM Cliente where usuario_id = @usuario_id";
                    using (SqlCommand commandCliente = new SqlCommand(queryCliente, sqlConnection))
                    {
                         commandCliente.Parameters.AddWithValue("@usuario_id", usuario_id);
                        using (SqlDataReader reader = await commandCliente.ExecuteReaderAsync())
                        {
                            var clientes = new List<Cliente>();
                            while (await reader.ReadAsync())
                            {
                                var cliente = new Cliente
                                {
                                    ClienteId = reader.GetInt32(0),
                                    Cedula = reader.GetString(1),
                                    Nombre = reader.GetString(2),
                                    Apellido = reader.GetString(3),
                                    Correo_electronico = reader.GetString(4),
                                    Telefono = reader.GetString(5),
                                    monto = reader.GetDecimal(6),
                                    usuario_id = usuario_id

                                };
                                clientes.Add(cliente);
                            }
                            await _database.InsertAllAsync(clientes, runInTransaction: true);
                        }
                    }

                    // Repetir para otras tablas (Cliente, Evento, Producto, TarjetaNFC, Transaccion, Usuario)
                    // ...
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine($"Error al sincronizar con SQL Server: {ex.Message}");
            }
        }

    //*Metodo para registrar un nuevo usuario   
    public async Task OnRegisterClicked(string cedula, string nombre, string apellido, string correo, string telefono, string contrasena)
    {
        using (SqlConnection conn = new SqlConnection(sqlServerConnectionString))
        {
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("INSERT INTO Usuario (Cedula, Nombre, Apellido, Correo_Electronico, Telefono, contrasena) VALUES (@Cedula, @Nombre, @Apellido, @Correo, @Telefono, @contrasena)", conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Apellido", apellido);
            cmd.Parameters.AddWithValue("@Cedula", cedula);
            cmd.Parameters.AddWithValue("@Correo", correo);
            cmd.Parameters.AddWithValue("@Telefono", telefono);
            cmd.Parameters.AddWithValue("@Contrasena", contrasena);

            await cmd.ExecuteNonQueryAsync();
        }
    }


    //*metodo para obtener el ID del usuario que ingreso a la app.
    public async Task<( int id, bool existe)> ObtenerIdUsuarioAsync(string correo_electronico)
    {
        using (SqlConnection conn = new SqlConnection(sqlServerConnectionString))
        {
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT usuario_id FROM usuario WHERE correo_electronico = @Correo_electronico", conn);
            cmd.Parameters.AddWithValue("@Correo_electronico", correo_electronico);

            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    return (reader.GetInt32(0),true); 
                }
            }
        }
        return (-1, false);
    }

//*Metodo para obtener el Id del organizador A traves del RNC 
    public async Task<( int id, bool existe)> ObtenerIdOrganizadorAsync(string RNC)
    {
        using (SqlConnection conn = new SqlConnection(sqlServerConnectionString))
        {
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("SELECT organizador_id FROM organizador WHERE RNC = @RNC", conn);
            cmd.Parameters.AddWithValue("@RNC", RNC);

            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                if (reader.Read())
                {
                    return (reader.GetInt32(0),true); 
                }
            }
        }
        return (-1, false);
    }

//*Metodo para Registrar un nuevo Organizador en la base de datos
    public async Task OnRegisterOrganizadorClicked(string nombre, string correo_Electronico, string telefono, string RNC, int usuario_id)
    {
        using (SqlConnection conn = new SqlConnection(sqlServerConnectionString))
        {
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("INSERT INTO organizador (Nombre, Correo_Electronico, Telefono, RNC, usuario_id) VALUES (@Nombre, @Correo_Electronico, @Telefono, @RNC, @id)", conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Correo_Electronico", correo_Electronico);
            cmd.Parameters.AddWithValue("@Telefono", telefono);
            cmd.Parameters.AddWithValue("@RNC", RNC);
            cmd.Parameters.AddWithValue("@id", usuario_id);
  

            await cmd.ExecuteNonQueryAsync();
        }
    }

    //*Metodo para Registrar un nuevo Evento en la base de datos
    public async Task OnRegisterEventoClicked(string nombre, string fecha, string HoraInicio, string HoraFin, string Ubicacion, int usuario_id, int organizador_id)
    {
        using (SqlConnection conn = new SqlConnection(sqlServerConnectionString))
        {
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("INSERT INTO evento (Nombre, fecha, HoraInicio, HoraFin, Ubicacion, usuario_id, organizador_id) VALUES (@Nombre, @fecha, @HoraInicio, @HoraFin, @Ubicacion, @usuario_id, @organizador_id)", conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Parameters.AddWithValue("@HoraInicio", HoraInicio);
            cmd.Parameters.AddWithValue("@HoraFin", HoraFin);
            cmd.Parameters.AddWithValue("@Ubicacion", Ubicacion);
            cmd.Parameters.AddWithValue("@usuario_id", usuario_id);
            cmd.Parameters.AddWithValue("@organizador_id", organizador_id);
  

            await cmd.ExecuteNonQueryAsync();
        }
    }

    

       

        // CRUD para Organizador
        public Task<List<Organizador>> GetOrganizadoresAsync(int usuario_id)
        {
            return _database.Table<Organizador>().ToListAsync();
        }

        public Task<Organizador> GetOrganizadorAsync(String nombre)
        {
            return _database.Table<Organizador>().Where(i => i.Nombre == nombre).FirstOrDefaultAsync();
        }

        public Task<int> SaveOrganizadorAsync(Organizador organizador)
        {

            if (organizador.OrganizadorId != 0)
            {
                return _database.UpdateAsync(organizador);
            }
            else
            {
                return _database.InsertAsync(organizador);
            }
        }

        public Task<int> DeleteOrganizadorAsync(Organizador organizador)
        {
            return _database.DeleteAsync(organizador);
        }

        // CRUD para Evento
        public Task<List<Evento>> GetEventosAsync(int usuario_id)
        {
            return _database.Table<Evento>().ToListAsync();
        }

        public Task<Evento> GetEventoAsync(int id)
        {
            return _database.Table<Evento>().Where(i => i.EventoId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveEventoAsync(Evento evento)
        {
            if (evento.EventoId != 0)
            {
                return _database.UpdateAsync(evento);
            }
            else
            {
                return _database.InsertAsync(evento);
            }
        }

        public Task<int> DeleteEventoAsync(Evento evento)
        {
            return _database.DeleteAsync(evento);
        }

        // CRUD para Usuario
        public Task<List<Usuario>> GetUsuariosAsync()
        {
            return _database.Table<Usuario>().ToListAsync();
        }

        public Task<Usuario> GetUsuarioAsync(int id)
        {
            return _database.Table<Usuario>().Where(i => i.UsuarioId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveUsuarioAsync(Usuario usuario)
        {
            if (usuario.UsuarioId != 0)
            {
                return _database.UpdateAsync(usuario);
            }
            else
            {
                return _database.InsertAsync(usuario);
            }
        }

        public Task<int> DeleteUsuarioAsync(Usuario usuario)
        {
            return _database.DeleteAsync(usuario);
        }

        // CRUD para TarjetaNFC
        public Task<List<TarjetaNFC>> GetTarjetasNFCAsync()
        {
            return _database.Table<TarjetaNFC>().ToListAsync();
        }

        public Task<TarjetaNFC> GetTarjetaNFCAsync(int id)
        {
            return _database.Table<TarjetaNFC>().Where(i => i.TarjetaId == id).FirstOrDefaultAsync();
        }

        public Task<TarjetaNFC> GetTarjetaByTagIdAsync(string tagId)
        {
            return _database.Table<TarjetaNFC>().Where(i => i.TarjetaId == int.Parse(tagId)).FirstOrDefaultAsync();
        }

        public Task<int> SaveTarjetaNFCAsync(TarjetaNFC tarjetaNFC)
        {
            if (tarjetaNFC.TarjetaId != 0)
            {
                return _database.UpdateAsync(tarjetaNFC);
            }
            else
            {
                return _database.InsertAsync(tarjetaNFC);
            }
        }

        public Task<int> DeleteTarjetaNFCAsync(TarjetaNFC tarjetaNFC)
        {
            return _database.DeleteAsync(tarjetaNFC);
        }

        public Task<int> UpdateTarjetaAsync(TarjetaNFC tarjetaNFC)
        {
            return _database.UpdateAsync(tarjetaNFC);
        }

        // CRUD para Producto
        public Task<List<Producto>> GetProductosAsync()
        {
            return _database.Table<Producto>().ToListAsync();
        }

        public Task<Producto> GetProductoAsync(int id)
        {
            return _database.Table<Producto>().Where(i => i.ProductoId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveProductoAsync(Producto producto)
        {
            if (producto.ProductoId != 0)
            {
                return _database.UpdateAsync(producto);
            }
            else
            {
                return _database.InsertAsync(producto);
            }
        }

        public Task<int> DeleteProductoAsync(Producto producto)
        {
            return _database.DeleteAsync(producto);
        }

        public Task<int> UpdateProductoAsync(Producto producto)
        {
            return _database.UpdateAsync(producto);
        }

        // CRUD para Transaccion
        public Task<List<Transaccion>> GetTransaccionesAsync()
        {
            return _database.Table<Transaccion>().ToListAsync();
        }

        public Task<Transaccion> GetTransaccionAsync(int id)
        {
            return _database.Table<Transaccion>().Where(i => i.TransaccionId == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTransaccionAsync(Transaccion transaccion)
        {
            if (transaccion.TransaccionId != 0)
            {
                return _database.UpdateAsync(transaccion);
            }
            else
            {
                return _database.InsertAsync(transaccion);
            }
        }

        public Task<int> DeleteTransaccionAsync(Transaccion transaccion)
        {
            return _database.DeleteAsync(transaccion);
        }

        public Task<int> UpdateTransaccionAsync(Transaccion transaccion)
        {
            return _database.UpdateAsync(transaccion);
        }

        // CRUD para Cliente

        // Obtener todos los clientes
        public Task<List<Cliente>> GetClientesAsync(int usuario_id)
        {
             return _database.Table<Cliente>().Where(c => c.usuario_id == usuario_id).ToListAsync();
        }

        // Obtener un cliente por ID
        public Task<Cliente> GetClienteAsync(int usuario_id)
        {
            return _database.Table<Cliente>().Where(c => c.usuario_id == usuario_id).FirstOrDefaultAsync();
        }

        // Guardar o actualizar un cliente
        public Task<int> SaveClienteAsync(Cliente cliente)
        {
            if (cliente.ClienteId != 0)
            {
                return _database.UpdateAsync(cliente);
            }
            else
            {
                return _database.InsertAsync(cliente);
            }
        }

        // Eliminar un cliente
        public Task<int> DeleteClienteAsync(Cliente cliente)
        {
            return _database.DeleteAsync(cliente);
        }
    }
}
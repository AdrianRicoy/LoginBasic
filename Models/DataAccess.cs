using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace Login.Models
{
    /// <summary>
    /// Clase para gestionar las distintas conexiones a la base de datos.
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Propiedad para obtener los datos de la conexión predeterminada a la base de datos.
        /// </summary>
        public static string StrConnection;
        /// <summary>
        /// Propiedad para realizar la conexión.
        /// </summary>
        private static NpgsqlConnection Connection = new NpgsqlConnection();
        /// <summary>
        /// Propiedad para validad si la conexión es nula o se encuentra cerrada.
        /// </summary>
        private static NpgsqlConnection NewConnection
        {
            get
            {
                if (Connection == null || Connection.State != ConnectionState.Open)
                {
                    Connection = new NpgsqlConnection();
                    Connection.ConnectionString = StrConnection;
                    Connection.Open();
                }
                return Connection;
            }
        }
        /// <summary>
        /// Constructor por defecto de la clase <see cref="DataAccess"/>.
        /// </summary>
        public DataAccess() {}
        /// <summary>
        /// Ejecuta una consulta en la base de datos.
        /// </summary>
        /// <param name="sql">Texto de la consulta SQL.</param>
        /// <returns>DataTable con el resultado de la consulta.</returns>
        protected DataTable GetQuery(string sql)
        {
            DataTable table = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            using (Connection = NewConnection)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = sql;
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }

            return table;
        }
        /// <summary>
        /// Ejecuta una consulta en la base de datos.
        /// </summary>
        /// <param name="sql">Texto de la consulta SQL.</param>
        /// <param name="table">DataTable con la definición de las columnas.</param>
        /// <returns>DataTable con el resultado de la consulta.</returns>
        protected DataTable GetQuery(string sql, DataTable table)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            using (Connection = NewConnection)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = sql;
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }

            return table;
        }
        /// <summary>
        /// Ejecuta una consulta en la base de datos.
        /// </summary>
        /// <param name="sql">Texto de la consulta SQL.</param>
        /// <param name="parameters">Lista de parámetros de la consulta.</param>
        /// <returns>DataTable con el resultado de la consulta.</returns>
        protected DataTable GetQuery(string sql, List<NpgsqlParameter> parameters)
        {
            DataTable table = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            using (Connection = NewConnection)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    foreach (NpgsqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }
            return table;
        }
        /// <summary>
        /// Ejecuta una consulta en la base de datos.
        /// </summary>
        /// <param name="sql">Texto de la consulta SQL.</param>
        /// <param name="table">DataTable con la definición de las columnas.</param>
        /// <param name="parameters">Lista de parámetros de la consulta.</param>
        /// <returns>DataTable con el resultado de la consulta.</returns>
        protected DataTable GetQuery(string sql, DataTable table, List<NpgsqlParameter> parameters)
        {
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();

            using (Connection = NewConnection)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    foreach (NpgsqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    adapter.SelectCommand = command;
                    adapter.Fill(table);
                }
            }

            return table;
        }
        /// <summary>
        /// Ejecuta una consulta en la base de datos de inserción, actualización o eliminación.
        /// </summary>
        /// <param name="sql">Texto de la consulta SQL.</param>
        /// <param name="parameters">Lista de parámetros de la consulta.</param>
        /// <returns>Número de filas afectadas.</returns>
        protected int ExecuteQuery(string sql, List<NpgsqlParameter> parameters)
        {
            int rowsAffected;

            using (Connection = NewConnection)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = sql;
                    command.Parameters.Clear();
                    foreach (NpgsqlParameter param in parameters)
                    {
                        command.Parameters.Add(param);
                    }
                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        /// <summary>
        /// Ejecuta una consulta en la base de datos de inserción, actualización o eliminación.
        /// </summary>
        /// <param name="sql">Texto de la consulta SQL.</param>
        /// <returns>Número de filas afectadas.</returns>
        protected int ExecuteQuery(string sql)
        {
            int rowsAffected;

            using (Connection = NewConnection)
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = Connection;
                    command.CommandText = sql;
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }
    }
}

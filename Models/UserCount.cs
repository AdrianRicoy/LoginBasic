using Npgsql;
using System.Data;
using System.Collections.Generic;

namespace Login.Models
{
    public class UserCount : DataAccess
    {
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public UserCount() { }
        /// <summary>
        /// Constructor sobrecargado, que crea un objeto de tipo UserCount
        /// </summary>
        /// <param name="email">Dirección Email del Usuario</param>
        /// <param name="password">Contraseña del Usuario</param>
        public UserCount(string email, string password)
        {
            NpgsqlParameter paramEmail = new NpgsqlParameter(":email", email);
            NpgsqlParameter paramPassword = new NpgsqlParameter(":pass", password);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>
            {
                paramEmail, paramPassword
            };

            const string SQL = "SELECT * FROM UserCount WHERE email = :email AND pass = :pass";

            DataTable table = GetQuery(SQL, lstParameters);

            if (table.Rows.Count <= 0) return;

            DataRow row = table.Rows[0];
            this.idUserCount = (int)row["id_usercount"];
            this.email = (string)row["email"];
            this.password = (string)row["pass"];
            this.status = (int)row["status"];
        }
        /// <summary>
        /// Identificador de la cuenta del usuario
        /// </summary>
        public int idUserCount { get; set; }
        /// <summary>
        /// La dirección Email
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// Contraseña del usuario
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Status de la cuenta, si será admin o vendedor
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// Agregar una nueva cuenta del usuario
        /// </summary>
        /// <param name="userCount">Objeto de tipo UserCount con todos los valores a agregar a la tabla usercount</param>
        /// <returns>true si se ha agregado el nuevo registro</returns>
        public bool AddUserCount(UserCount userCount)
        {
            NpgsqlParameter paramEmail = new NpgsqlParameter(":email", userCount.email);
            NpgsqlParameter paramPassword = new NpgsqlParameter(":pass", userCount.password);
            NpgsqlParameter paramStatus = new NpgsqlParameter(":status", userCount.status);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>
            {
                paramEmail, paramPassword, paramStatus
            };

            const string SQL = "INSERT INTO UserCount (email, pass, status) " +
                "VALUES (:email, :pass, :status)";

            int affected = ExecuteQuery(SQL, lstParameters);

            return affected > 0;
        }
        /// <summary>
        /// Actualza una cuenta del usuario
        /// </summary>
        /// <param name="userCount">Objeto de tipo UserCount con todos los valores a agregar a la tabla usercoumt</param>
        /// <returns></returns>
        public bool UpdateUserCount(UserCount userCount)
        {
            NpgsqlParameter paramEmail = new NpgsqlParameter(":email", userCount.email);
            NpgsqlParameter paramPassword = new NpgsqlParameter(":pass", userCount.password);
            NpgsqlParameter paramStatus = new NpgsqlParameter(":status", userCount.status);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>
            {
                paramEmail, paramPassword, paramStatus
            };

            string sql = "UPDATE UserCount SET email = :email, pass = :pass, status = :status " +
                "WHERE id_usercount = " + userCount.idUserCount;

            int affected = ExecuteQuery(sql, lstParameters);

            return affected > 0;
        }
        /// <summary>
        /// Borrar un registro de la tabla UserCount, por su id o dirección de email
        /// </summary>
        /// <param name="idUserCount">Identificador de la cuenta de usuario</param>
        /// <param name="email">Dirección Email</param>
        /// <returns>Un boolean para indicar si se ha borrado el registro o no</returns>
        public bool DeleteUserCount(int idUserCount, string email)
        {
            NpgsqlParameter paramEmail = new NpgsqlParameter(":email", email);
            NpgsqlParameter paramIdUserCount = new NpgsqlParameter(":id_usercount", idUserCount);

            List<NpgsqlParameter> lstParameters = new List<NpgsqlParameter>();
            List<string> lstConditions = new List<string>();
            string conditionsOfQuery = "";

            if (idUserCount != 0)
            {
                lstParameters.Add(paramIdUserCount);
                lstConditions.Add("id_usercount = :id_usercount");
            } 
            if(!string.IsNullOrEmpty(email))
            {
                lstParameters.Add(paramEmail);
                lstConditions.Add("email = :email");
            }

            switch(lstParameters.Count)
            {
                case 1:
                    conditionsOfQuery = string.Format("WHERE {0}", lstConditions[0]);
                    break;
                case 2:
                    conditionsOfQuery = string.Format("WHERE {0} AND {1}", lstConditions[0], lstConditions[1]);
                    break;
                default: return false;
            }

            string sql = "DELETE FROM UserCount " + conditionsOfQuery;

            int affected = ExecuteQuery(sql, lstParameters);
            return affected > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Vacunacion.Models;

namespace Vacunacion.Servicios
{
    public class RepositorioVacunas
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionVacunacion"].ToString();

        public List<Vacuna> ObtenerVacunas()
        {
            List<Vacuna> Vacunas = new List<Vacuna>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Vacuna_Obtener";
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Vacunas.Add(new Vacuna
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString(),
                        Marca = reader["Marca"].ToString()
                    });
                }
            }
            return Vacunas.ToList();
        }

        public void Crear(Vacuna vacuna)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Vacuna_Crear";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Nombre", vacuna.Nombre);
                command.Parameters.AddWithValue("@Marca", vacuna.Marca);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Vacuna ObtenerPorId(int Id)
        {
            Vacuna vacuna = new Vacuna();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Vacuna_ObtenerPorId";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    vacuna.Id = (int)reader["Id"];
                    vacuna.Nombre = reader["Nombre"].ToString();
                    vacuna.Marca = reader["Marca"].ToString();
                }
            }
            return vacuna;
        }

        public void Editar(Vacuna vacuna)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Vacuna_Editar";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", vacuna.Id);
                command.Parameters.AddWithValue("@Nombre", vacuna.Nombre);
                command.Parameters.AddWithValue("@Marca", vacuna.Marca);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool Borrar(int Id)
        {
            bool Noexiste = ExisteVacunaEnRegistro(Id);
            if (Noexiste)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_Vacuna_Eliminar";
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@Id", Id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ExisteVacunaEnRegistro(int Id)
        {
            int Cantidad;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Vacuna_ExisteVacunaEnTablaRegistro";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                Cantidad = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return Cantidad == 0;
        }
    }
}
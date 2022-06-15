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
    public class RepositorioMunicipios
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionVacunacion"].ToString();

        public List<Municipio> ObtenerMunicipios()
        {
            List<Municipio> Municipios = new List<Municipio>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Municipio_Obtener";
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Municipios.Add(new Municipio
                    {
                        Id = (int)reader["Id"],
                        Nombre = reader["Nombre"].ToString()
                    });
                }
            }
            return Municipios;
        }

        public void Crear(Municipio mun)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Municipio_Crear";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Nombre", mun.Nombre);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Municipio ObtenerPorId(int Id)
        {
            Municipio municipio = new Municipio();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Municipio_ObtenerPorId";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    municipio.Id = (int)reader["Id"];
                    municipio.Nombre = reader["Nombre"].ToString();
                }
            }
            return municipio;
        }

        public void Editar(Municipio mun)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Municipio_Editar";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", mun.Id);
                command.Parameters.AddWithValue("@Nombre", mun.Nombre);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool Borrar(int Id)
        {
            bool Noexiste = ExisteMunicipioEnRegistro(Id);
            if (Noexiste)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "sp_Municipio_Eliminar";
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

        public bool ExisteMunicipioEnRegistro(int Id)
        {
            int Cantidad;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Municipio_ExisteMunicipioEnTablaRegistro";
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
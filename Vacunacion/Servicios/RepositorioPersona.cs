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
    public class RepositorioPersona
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionVacunacion"].ToString();

        public void Crear(Persona persona)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Persona_Crear";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Correo", persona.Correo);
                command.Parameters.AddWithValue("@Edad", persona.Edad);
                command.Parameters.AddWithValue("@RFC", persona.RFC);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Persona ObtenerPorId(int Id)
        {
            Persona persona = new Persona();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Persona_ObtenerPorId";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    persona.Folio = (int)reader["Folio"];
                    persona.Nombre = reader["Nombre"].ToString();
                    persona.Correo = reader["Correo"].ToString();
                    persona.Edad = (int)reader["Edad"];
                    persona.RFC = reader["RFC"].ToString();
                }
            }
            return persona;
        }

        public int ObtenerFolioPorRFC(string RFC)
        {
            int Folio = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Persona_ObtenerFolioPorRFC";
                command.Connection = connection;

                command.Parameters.AddWithValue("@RFC", RFC);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Folio = (int)reader["Folio"];
                }
            }
            return Folio;
        }
    }
}
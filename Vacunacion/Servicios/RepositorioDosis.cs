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
    public class RepositorioDosis
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionVacunacion"].ToString();

        public List<Dosis> ObtenerDosis()
        {
            List<Dosis> Dosis = new List<Dosis>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Dosis_Obtener";
                command.Connection = connection;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Dosis.Add(new Dosis
                    {
                        Id = (int)reader["Id"],
                        Descripcion = reader["Descripcion"].ToString()
                    });
                }
            }
            return Dosis;
        }

        public Dosis ObtenerPorId(int Id)
        {
            Dosis dosis = new Dosis();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_Dosis_ObtenerPorId";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dosis.Id = (int)reader["Id"];
                    dosis.Descripcion = reader["Descripcion"].ToString();
                }
            }
            return dosis;
        }
    }
}
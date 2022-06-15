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
    public class RepositorioRegistroVacunas
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionVacunacion"].ToString();

        public List<Registro> ObtenerPorFolio(int FolioId)
        {
            List<Registro> Registros = new List<Registro>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_RegistroVacuna_ObtenerPorFolio";
                command.Connection = connection;

                command.Parameters.AddWithValue("@FolioId", FolioId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Registros.Add(new Registro
                    {
                        Id = (int)reader["RegistroId"],
                        FechaVacunacion = (DateTime)reader["FechaVacunacion"],
                        Vacuna = reader["Vacuna"].ToString(),
                        Municipio = reader["Municipio"].ToString(),
                        Dosis = reader["Dosis"].ToString()
                    });
                }
            }
            return Registros;
        }

        public void Crear(Registro registro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_RegistroVacuna_Crear";
                command.Connection = connection;

                command.Parameters.AddWithValue("@FolioId", registro.FolioId);
                command.Parameters.AddWithValue("@FechaVacunacion", registro.FechaVacunacion);
                command.Parameters.AddWithValue("@VacunaId", registro.VacunaId);
                command.Parameters.AddWithValue("@MunicipioId", registro.MunicipioId);
                command.Parameters.AddWithValue("@Dosis", registro.DosisId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Registro ObtenerPorId(int Id)
        {
            Registro registro = new Registro();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_RegistroVacuna_ObtenerRegistroPorId";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    registro.Id = (int)reader["Id"];
                    registro.FolioId = (int)reader["FolioId"];
                    registro.FechaVacunacion = (DateTime)reader["FechaVacunacion"];
                    registro.VacunaId = (int)reader["VacunaId"];
                    registro.MunicipioId = (int)reader["MunicipioId"];
                    registro.DosisId = (int)reader["DosisId"];
                    registro.Vacuna = reader["Vacuna"].ToString();
                    registro.Municipio = reader["Municipio"].ToString();
                    registro.Dosis = reader["Dosis"].ToString();
                }
            }
            return registro;
        }

        public void Editar(Registro registro)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_RegistroVacuna_Editar";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", registro.Id);
                command.Parameters.AddWithValue("@FechaVacunacion", registro.FechaVacunacion);
                command.Parameters.AddWithValue("@VacunaId", registro.VacunaId);
                command.Parameters.AddWithValue("@MunicipioId", registro.MunicipioId);
                command.Parameters.AddWithValue("@DosisId", registro.DosisId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Borrar(int Id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_RegistroVacuna_Eliminar";
                command.Connection = connection;

                command.Parameters.AddWithValue("@Id", Id);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
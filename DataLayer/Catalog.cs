using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DotNetEnv;

namespace DataLayer
{
    class Catalog
    {
        #region Metodos
        public static bool Obter(int id, ref string nome, ref DateTime dataValidade, ref float preco, ref int principioAtivo, out string erro)
        {
            Env.Load();
            bool resultado = false;
            erro = string.Empty;

            try
            {
                string conString = Env.GetString("DB_CONNECTION_STRING");

                SqlConnection sqlConnection = new SqlConnection(conString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Medicamento_Obter", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.Int);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = id;

                sqlCommand.Parameters.Add(sqlParameter);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();
                    if (!sqlDataReader.IsDBNull(1))
                    {
                        nome = sqlDataReader.GetString(1);
                    }
                    if (!sqlDataReader.IsDBNull(2))
                    {
                        dataValidade = sqlDataReader.GetDateTime(2);
                    }
                    if (!sqlDataReader.IsDBNull(3))
                    {
                        preco = sqlDataReader.GetFloat(3);
                    }
                    if (!sqlDataReader.IsDBNull(4))
                    {
                        principioAtivo = sqlDataReader.GetInt32(4);
                    }

                    resultado = true;
                }

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return resultado;
        }

        public static bool Gravar(int id, string nome, DateTime dataValidade, float preco, int principioAtivo, out string erro)
        {
            bool resultado = false;
            erro = string.Empty;

            try
            {
                SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString);

                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Medicamento_Gravar", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.Int);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = id;

                sqlCommand.Parameters.Add(sqlParameter);

                sqlParameter = new SqlParameter("Nome", System.Data.SqlDbType.NVarChar, 90);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = nome;

                sqlCommand.Parameters.Add(sqlParameter);

                sqlParameter = new SqlParameter("DataValidade", System.Data.SqlDbType.DateTime);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = dataValidade;

                sqlCommand.Parameters.Add(sqlParameter);

                sqlParameter = new SqlParameter("Preco", System.Data.SqlDbType.Float);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = preco;

                sqlCommand.Parameters.Add(sqlParameter);

                sqlParameter = new SqlParameter("PrincipioAtivo", System.Data.SqlDbType.Int);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = principioAtivo;

                sqlCommand.Parameters.Add(sqlParameter);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                resultado = true;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return resultado;
        }

        public static bool Eliminar(int id, out string erro)
        {
            bool resultado = false;
            erro = string.Empty;

            try
            {
                SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Medicamento_Eliminar", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.Int);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = id;

                sqlCommand.Parameters.Add(sqlParameter);

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();

                resultado = true;
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return resultado;
        }

        public static DataTable Listar(out string erro)
        {
            DataTable dataTable = null;
            erro = string.Empty;

            try
            {
                SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("Medicamento_Listar", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.SingleResult);

                dataTable = new DataTable();
                dataTable.Load(dataReader);

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }
            return dataTable;
        }
        #endregion
    }
}

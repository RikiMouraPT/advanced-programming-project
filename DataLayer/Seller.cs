using System;
using System.Data;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace DataLayer
{
    public class Seller
    {
        public static bool GetSeller(int sellerId, ref string name, out string error)
        {
            bool result = false;
            error = string.Empty;

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("GetSeller", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlParameter = new SqlParameter("SellerID", System.Data.SqlDbType.Int);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = sellerId;
                sqlCommand.Parameters.Add(sqlParameter);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();

                    if (!sqlDataReader.IsDBNull(1))
                        name = sqlDataReader.GetString(1);

                    result = true;
                }

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return result;
        }

        public static DataTable ListSellers(out string erro)
        {
            DataTable dataTable = null;
            erro = string.Empty;

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(conString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("ListSellers", sqlConnection);
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

    }
}

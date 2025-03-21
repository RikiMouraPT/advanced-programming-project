using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace DataLayer
{
    class Products
    {
        public static bool GetProduct(int productId, ref string name, ref string category, ref string brand, ref string model,
            ref int year, ref decimal buyPrice, ref decimal sellPrice, ref bool isSold, ref DateTime dateAdded, ref int sellerId, out string error)
        {
            bool result = false;
            error = string.Empty;

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(conString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("GetProduct", sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter sqlParameter = new SqlParameter("ProductID", System.Data.SqlDbType.Int);
                sqlParameter.Direction = System.Data.ParameterDirection.Input;
                sqlParameter.Value = productId;
                sqlCommand.Parameters.Add(sqlParameter);

                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();

                    if (!sqlDataReader.IsDBNull(1))
                        name = sqlDataReader.GetString(1);
                    if (!sqlDataReader.IsDBNull(2))
                        category = sqlDataReader.GetString(2);
                    if (!sqlDataReader.IsDBNull(3))
                        brand = sqlDataReader.GetString(3);
                    if (!sqlDataReader.IsDBNull(4))
                        model = sqlDataReader.GetString(4);
                    if (!sqlDataReader.IsDBNull(5))
                        year = sqlDataReader.GetInt32(5);
                    if (!sqlDataReader.IsDBNull(6))
                        buyPrice = sqlDataReader.GetDecimal(6);
                    if (!sqlDataReader.IsDBNull(7))
                        sellPrice = sqlDataReader.GetDecimal(7);
                    if (!sqlDataReader.IsDBNull(8))
                        isSold = sqlDataReader.GetBoolean(8);
                    if (!sqlDataReader.IsDBNull(9))
                        dateAdded = sqlDataReader.GetDateTime(9);
                    if (!sqlDataReader.IsDBNull(10))
                        sellerId = sqlDataReader.GetInt32(10);

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
        public static DataTable ListProducts(out string erro)
        {
            DataTable dataTable = null;
            erro = string.Empty;

            try
            {
                string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(conString);
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("ListProduct", sqlConnection);
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

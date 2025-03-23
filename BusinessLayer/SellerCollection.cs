using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class SellerCollection : Collection<Seller>
    {
        #region Constructors
        public SellerCollection()
        {
        }

        public SellerCollection(DataTable dataTable)
            : this()
        {
            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Seller seller = new Seller();

                    seller.SellerId = dataRow.Field<int>("SellerID");
                    seller.Name = dataRow.Field<string>("Name");

                    this.Add(seller);
                }
            }
        }
        #endregion

        #region Methods
        // Method to get sellers collection
        public static SellerCollection GetCollection()
        {
            string erro = string.Empty;

            DataTable dataTable = DataLayer.Seller.List(out erro);

            SellerCollection sellers = new SellerCollection(dataTable);

            return sellers;
        }
        #endregion
    }
}

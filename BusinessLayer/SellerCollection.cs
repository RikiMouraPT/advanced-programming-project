using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    class SellerCollection : Collection<Seller>
    {
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
    }
}

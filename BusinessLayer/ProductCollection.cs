using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;

namespace BusinessLayer
{
    public class ProductCollection : Collection<Product>
    {
        public ProductCollection()
        {
        }

        public ProductCollection(DataTable dataTable)
            :this()
        {
            if (dataTable != null)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Product product = new Product();

                    product.ProductId = dataRow.Field<int>("ProductID");
                    product.Name = dataRow.Field<string>("Name");
                    product.Category = dataRow.Field<string>("Category");
                    product.Brand = dataRow.Field<string>("Brand");
                    product.Model = dataRow.Field<string>("Model");
                    product.Year = dataRow.Field<int>("Year");
                    product.BuyPrice = dataRow.Field<double>("BuyPrice");
                    product.SellPrice = dataRow.Field<double>("SellPrice");
                    product.IsSold = dataRow.Field<bool>("isSold");
                    product.Date = dataRow.Field<DateTime>("Date");
                    product.SellerId = dataRow.Field<int>("SellerID");

                    this.Add(product);
                }
            }
        }
    }
}

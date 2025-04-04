using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Linq;

namespace BusinessLayer
{
    public class ProductCollection : Collection<Product>
    {
        #region Constructors
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
        #endregion

        #region Methods
        // Method to get product collection
        public static ProductCollection Get()
        {
            string erro = string.Empty;

            DataTable dataTable = DataLayer.Product.List(out erro);

            ProductCollection products = new ProductCollection(dataTable);

            return products;
        }

        internal double GetAmount(EnumCategory category, int year, int month)
        {
            return this.Where(p => p.Category == category.ToString() && p.Date.Year == year && p.Date.Month == month && p.IsSold).Sum(p => p.SellPrice);
        }
        #endregion
    }
}

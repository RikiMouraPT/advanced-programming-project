using System;
using System.Data;

namespace BusinessLayer
{
    public class Seller
    {
        #region Constructors

        // Default Constructor

        public Seller()
        {
            this.sellerId = 0;
            this.name = string.Empty;
        }

        // Constructor with parameters

        public Seller(int sellerId, string name)
        {
            this.sellerId = sellerId;
            this.name = name;
        }

        #endregion

        #region Properties
        /*
        [SellerID] [int] NOT NULL,
	    [Name] [nvarchar](100) NOT NULL,
        */

        private int sellerId;

        public int SellerId
        {
            get { return sellerId; }
            set { sellerId = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }



        #endregion

        #region Methods

        // Method to get all Sellers
        public static DataTable List(out string erro)
        {
            DataTable dataTable = DataLayer.Seller.ListSellers(out erro);

            return dataTable;
        }

        public static SellerCollection GetSellerCollection()
        {
            string erro = string.Empty;

            DataTable dataTable = List(out erro);

            SellerCollection sellers = new SellerCollection(dataTable);

            return sellers;
        }

        // Method to get sellers by ID

        public Seller GetSeller(int sellerId)
        {
            Seller seller = null;

            string name = string.Empty;
            string error = string.Empty;


            bool result = DataLayer.Seller.GetSeller(sellerId, ref name, out error);

            if (result)
            {
                seller = new Seller(sellerId, name);
            }

            return seller;
        }

        #endregion
    }
}

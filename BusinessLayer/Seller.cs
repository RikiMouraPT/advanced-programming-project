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
        // Method to get one seller by ID
        public Seller Get(int sellerId)
        {
            Seller seller = null;

            string name = string.Empty;
            string error = string.Empty;


            bool result = DataLayer.Seller.Get(sellerId, ref name, out error);

            if (result)
            {
                seller = new Seller(sellerId, name);
            }

            return seller;
        }
        #endregion
    }
}

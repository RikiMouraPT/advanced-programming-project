using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    class Catalog
    {
        #region Constructors

        public Catalog()
        {
        }

        public Catalog(int productID, string name, string category, string brand, string model, DateTime year, float buyPrice, float sellPrice, bool isSold, DateTime date, int sellerID)
        {
            this.productID = productID;
            this.name = name;
            this.category = category;
            this.brand = brand;
            this.model = model;
            this.year = year;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.isSold = isSold;
            this.date = date;
            this.sellerID = sellerID;
        }

        #endregion

        #region Properties
        /*
		Database
		Table
        [ProductID] [int] NOT NULL,
	    [Name] [nvarchar](100) NOT NULL,
	    [Category] [nvarchar](50) NULL,
	    [Brand] [nvarchar](50) NULL,
	    [Model] [nvarchar](50) NULL,
	    [Year] [int] NULL,
	    [BuyPrice] [decimal](10, 2) NULL,
	    [SellPrice] [decimal](10, 2) NULL,
	    [isSold] [bit] DEFAULT 0 NULL,
	    [Date] [datetime] DEFAULT GETDATE() NULL,
	    [SellerID] [int] NULL
        */
        private int productID;

		public int ProductID
		{
			get { return productID; }
			set { productID = value; }
		}

		private string name;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		private string category;

		public string Category
		{
			get { return category; }
			set { category = value; }
		}

		private string brand;

		public string Brand
		{
			get { return brand; }
			set { brand = value; }
		}

		private string model;

		public string Model
		{
			get { return model; }
			set { model = value; }
		}

		private DateTime year;

		public DateTime Year
		{
			get { return year; }
			set { year = value; }
		}

		private float buyPrice;

		public float BuyPrice
		{
			get { return buyPrice; }
			set { buyPrice = value; }
		}

		private float sellPrice;

		public float SellPrice
		{
			get { return sellPrice; }
			set { sellPrice = value; }
		}

		private bool isSold;

		public bool IsSold
		{
			get { return isSold; }
			set { isSold = value; }
		}

		private DateTime date;

		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}

		private int sellerID;

		public int SellerID
		{
			get { return sellerID; }
			set { sellerID = value; }
		}

        #endregion

        #region Methods

        //method to add new product
        public void AddProduct()
        {
            //code to add new product
        }

        //method to remove product
        public void RemoveProduct() {
            //code to remove product
        }

        //method to update product
        public void UpdateProduct()
        {
            //code to update product
        }

        //method to list all products
        public void ListProducts()
        {
            //code to list all products
        }

        #endregion
    }
}

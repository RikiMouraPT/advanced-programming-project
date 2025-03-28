﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class Product
    {
        #region Constructors

        // Default Constructor
        public Product()
        {
            productId = 0;
            Name = string.Empty;
            category = string.Empty;
            brand = string.Empty;
            model = string.Empty;
            year = 0;
            buyPrice = 0;
            sellPrice = 0;
            isSold = false;
            date = DateTime.Now;
            sellerId = 0;
        }

        // Parameterized Constructor
        public Product(int productId, string name, string category,
            string brand, string model, int year, float buyPrice,
            float sellPrice, bool isSold, DateTime date, int sellerId)
        {
            this.productId = productId;
            this.name = name;
            this.category = category;
            this.brand = brand;
            this.model = model;
            this.year = year;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.isSold = isSold;
            this.date = date;
            this.sellerId = sellerId;
        }

        #endregion

        #region Properties
        /*
        CREATE TABLE [dbo].[Products](
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
        [SellerID] [int] NULL,
         */

        private int productId;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
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

        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        private double buyPrice;

        public double BuyPrice
        {
            get { return buyPrice; }
            set { buyPrice = value; }
        }

        private double sellPrice;

        public double SellPrice
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

        // Foreign Key for Seller Table

        private int sellerId;

        public int SellerId
        {
            get { return sellerId; }
            set { sellerId = value; }
        }

        #endregion

        #region Methods
        // Method to get one product by ID
        public Product Get(int productId)
        {
            Product product = null;

            string name = string.Empty;
            string category = string.Empty;
            string brand = string.Empty;
            string model = string.Empty;
            int year = 0;
            float buyPrice = 0;
            float sellPrice = 0;
            bool isSold = false;
            DateTime dateAdded = DateTime.Now;
            int sellerId = 0;
            string error = string.Empty;

            bool result = DataLayer.Product.Get(productId, ref name, ref category, ref brand, ref model,
                ref year, ref buyPrice, ref sellPrice, ref isSold, ref dateAdded, ref sellerId, out error);

            if (result)
            {
                product = new Product(productId, name, category, brand, model, year, buyPrice, sellPrice, isSold, dateAdded, sellerId);
            }

            return product;
        }
        #endregion

        
    }
}

using System;

namespace BusinessLayer
{
    public class Seller
    {
        #region Constructors

        public Seller()
        {
        }

        public Seller(int sellerID, string name)
        {
            this.sellerID = sellerID;
            this.name = name;
        }

        #endregion

        #region Properties
        private int sellerID;

        public int SellerID
        {
            get { return sellerID; }
            set { sellerID = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }


        #endregion

        #region Methods

        //method to add a new seller
        public void AddSeller()
        {
            //code to add a new seller
        }

        //method to update a seller
        public void UpdateSeller()
        {
            //code to update a seller
        }

        //method to delete a seller

        public void DeleteSeller()
        {
            //code to delete a seller
        }

        //method to get a seller by ID
        public Seller GetSellerByID(int sellerID)
        {
            //code to get a seller by ID
            return new Seller();
        }

        #endregion
    }
}

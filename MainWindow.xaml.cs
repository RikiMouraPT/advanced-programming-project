using BusinessLayer;
using DataLayer;
using Sales_Dashboard.UserControls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sales_Dashboard
{
    public partial class MainWindow : Window
    {
        #region Properties
        private EnumCategory category;

        public EnumCategory Category
        {
            get { return category; }
            set { 
                category = value;
                this.UpdateInfoCardValues(category);
                //this.UpdateUserCardValues(category);
            }
        }

        #endregion

        #region Template


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1280;
                    this.Height = 780;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }
        #endregion

        #region Methods
        #region InfoCards
        public void UpdateInfoCardValues(EnumCategory category)
        {
            ProductCollection products = BusinessLayer.ProductCollection.GetCollection();

            switch (category)
            {
                case EnumCategory.Undefined:
                    UpdateInfoCard(products, "Undefined");
                    break;
                case EnumCategory.Car:
                    UpdateInfoCard(products, "Carro");
                    break;
                case EnumCategory.Motorcycle:
                    UpdateInfoCard(products, "Mota");
                    break;
            }
        }
        private void UpdateInfoCard(ProductCollection products, string category)
        {
            int soldCount =       products.Where(p => p.Category == category && p.IsSold).Count();
            double sumSellPrice = products.Where(p => p.Category == category && p.IsSold).Sum(p => p.SellPrice);
            double sumBuyPrice =  products.Where(p => p.Category == category && p.IsSold).Sum(p => p.BuyPrice);
            
            this.qntSalesInfoCard.SubTitle =      soldCount.ToString();
            this.salesInfoCard.SubTitle =  "$" +  sumSellPrice.ToString();
            this.profitInfoCard.SubTitle = "$" + (sumSellPrice - sumBuyPrice).ToString();
        }
        #endregion
        #region UserCards
        public void UpdateUserCardValues(EnumCategory category)
        {
            SellerCollection sellers = BusinessLayer.SellerCollection.GetCollection();

            switch (category)
            {
                case EnumCategory.Undefined:
                    break;
                case EnumCategory.Car:
                    UpdateUserCard(sellers, "Carro");
                    break;
                case EnumCategory.Motorcycle:
                    UpdateUserCard(sellers, "Mota");
                    break;
            }
        }
        private void UpdateUserCard(SellerCollection sellers, string category)
        {


        }
        #endregion
        #endregion

        #region Events
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string category = categoryComboBox.SelectedItem.ToString();

            if (category.Contains("Undefined"))
            {
                this.Category = EnumCategory.Undefined;
            }
            else if (category.Contains("Car"))
            {
                this.Category = EnumCategory.Car;
            }
            else if (category.Contains("Motorcycle"))
            {
                this.Category = EnumCategory.Motorcycle;
            }
        }
        #endregion
    }
}
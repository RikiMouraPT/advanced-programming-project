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
            }
        }

        #endregion
        public MainWindow()
        {
            InitializeComponent();
        }

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
        public ProductCollection GetCollection()
        {
            ProductCollection product = BusinessLayer.Product.GetProductCollection();
            return product;
        }
        public void UpdateInfoCardValues(EnumCategory category)
        {
            ProductCollection product = this.GetCollection();

            switch (category)
            {
                case EnumCategory.Undefined:
                    break;
                case EnumCategory.Car:
                    UpdateInfoCard(product, "Carro");
                    break;
                case EnumCategory.Motorcycle:
                    UpdateInfoCard(product, "Mota");
                    break;
            }
        }

        private void UpdateInfoCard(ProductCollection product, string category)
        {
            double sumSellPrice = product.Where(p => p.Category == category && p.IsSold).Sum(p => p.SellPrice);
            double sumBuyPrice = product.Where(p => p.Category == category && p.IsSold).Sum(p => p.BuyPrice);
            int soldCount = product.Where(p => p.Category == category && p.IsSold).ToList().Count();

            this.qntSalesInfoCard.SubTitle = soldCount.ToString();
            this.salesInfoCard.SubTitle = "$" + product.Where(p => p.Category == category).Sum(p => p.SellPrice).ToString();
            this.profitInfoCard.SubTitle = "$" + (sumSellPrice - sumBuyPrice).ToString();
        }

        private void UpdateSellerInfoCardValues()
        {
            string error = string.Empty;
            SellerCollection sellers = BusinessLayer.Seller.GetSellerCollection();

            if (sellers != null && sellers.Count > 0)
            {
                UpdateUserCard(firstUserCard, sellers[0]);
                UpdateUserCard(secondUserCard, sellers[1]);
                UpdateUserCard(thirdUserCard, sellers[2]);
            }
            else
            {
                MessageBox.Show("Erro ao carregar vendedores", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateUserCard(UserCard card, BusinessLayer.Seller seller)
        {
            card.Title = seller.Name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSellerInfoCardValues();
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
    }
}
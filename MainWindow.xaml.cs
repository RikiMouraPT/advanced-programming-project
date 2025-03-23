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
                this.UpdateUserCardValues(category);
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
                    UpdateInfoCard(products, "Car");
                    break;
                case EnumCategory.Motorcycle:
                    UpdateInfoCard(products, "Motorcycle");
                    break;
            }
        }
        private void UpdateInfoCard(ProductCollection products, string category)
        {
            if (qntSalesInfoCard == null || salesInfoCard == null || profitInfoCard == null)
                return;
            //TODO: Implementar a lógica para preencher os UserCards com os dados dos Sellers da Motorcycle+Car
            if (category == "Undefined")
            {
                this.qntSalesInfoCard.SubTitle = "Null";
                this.salesInfoCard.SubTitle = "Null";
                this.profitInfoCard.SubTitle = "Null";
            }
            else
            {
                int soldCount = products.Where(p => p.Category == category && p.IsSold).Count();
                double sumSellPrice = products.Where(p => p.Category == category && p.IsSold).Sum(p => p.SellPrice);
                double sumBuyPrice = products.Where(p => p.Category == category && p.IsSold).Sum(p => p.BuyPrice);

                this.qntSalesInfoCard.SubTitle = soldCount.ToString();
                this.salesInfoCard.SubTitle = "$" + sumSellPrice.ToString();
                this.profitInfoCard.SubTitle = "$" + (sumSellPrice - sumBuyPrice).ToString();
            }
        }
        #endregion
        #region UserCards
        public void UpdateUserCardValues(EnumCategory category)
        {
            SellerCollection sellers = BusinessLayer.SellerCollection.GetCollection();
            ProductCollection products = BusinessLayer.ProductCollection.GetCollection();

            switch (category)
            {
                case EnumCategory.Undefined:
                    UpdateUserCard(sellers, products, "Undefined");
                    break;
                case EnumCategory.Car:
                    UpdateUserCard(sellers, products, "Car");
                    break;
                case EnumCategory.Motorcycle:
                    UpdateUserCard(sellers, products, "Motorcycle");
                    break;
            }
        }
        private void UpdateUserCard(SellerCollection sellers, ProductCollection products, string category)
        {
            if (firstUserCard == null || secondUserCard == null || thirdUserCard == null)
                return;
            //TODO: Implementar a lógica para preencher os UserCards com os dados dos Sellers da Motorcycle+Car
            if (category == "Undefined")
            {
                this.firstUserCard.Title = "Null";
                this.firstUserCard.UpPrice = "Null";
                this.firstUserCard.DownPrice = "Null";

                this.secondUserCard.Title = "Null";
                this.secondUserCard.UpPrice = "Null";
                this.secondUserCard.DownPrice = "Null";

                this.thirdUserCard.Title = "Null";
                this.thirdUserCard.UpPrice = "Null";
                this.thirdUserCard.DownPrice = "Null";
                return;
            }

            //Pega os top 3 sellers da categoria no parametro
            var topSellers = products
                .Where(p => p.Category == category && p.IsSold)
                .GroupBy(p => p.SellerId)
                .OrderByDescending(g => g.Sum(p => p.SellPrice))
                .Select(g => new
                {
                    SellerName = sellers.Where(s => s.SellerId == g.Key).Select(s => s.Name).FirstOrDefault(), //FirstOrDefault() para que retorne uma string apenas e não uma lista
                    TotalSellValue = g.Sum(p => p.SellPrice),
                    TotalProfitValue = g.Sum(p => p.SellPrice - p.BuyPrice)
                })
                .Take(3)
                .ToList();

            UserCard[] userCards = { firstUserCard, secondUserCard, thirdUserCard };
            for (int i = 0; i < userCards.Length; i++)
            {
                UpdateSingleUserCard(
                    userCards[i],
                    topSellers[i].SellerName,
                    topSellers[i].TotalSellValue,
                    topSellers[i].TotalProfitValue);
            }
        }
        private void UpdateSingleUserCard(UserCard userCard, string sellerName, double upPrice, double downPrice)
        {
            userCard.Title = sellerName;
            userCard.UpPrice = upPrice.ToString("C2");
            userCard.DownPrice = downPrice.ToString("C2");
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
            this.Category = EnumCategory.Undefined;
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
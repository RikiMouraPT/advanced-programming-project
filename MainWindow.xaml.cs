using BusinessLayer;
using DataLayer;
using System;
using System.Linq;
using System.Windows;
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
                this.UpdateValues(category);
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
        public void UpdateValues(EnumCategory category)
        {
            ProductCollection product = this.GetCollection();

            if (category == EnumCategory.Carro)
            {
                var result = product.Where(p => p.Category == "Carro").ToList();
            }
            else
            {
                var result = product.Where(p => p.Category == "Mota").ToList();
                this.qntSalesInfoCard.SubTitle = result.Count.ToString();
            }

            this.salesInfoCard.SubTitle = product.Count.ToString();
            this.profitInfoCard.SubTitle = product.Count.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.qntSalesInfoCard.SubTitle = "51";
            this.salesInfoCard.SubTitle = "51";
            this.profitInfoCard.SubTitle = "51";

            this.firstUserCard.Title = "Jóse Costa";
            this.secondUserCard.Title = "Vitor Costa";
            this.thirdUserCard.Title = "Ricardo Costa";

            this.UpdateValues(EnumCategory.Mota);

        }
    }
}
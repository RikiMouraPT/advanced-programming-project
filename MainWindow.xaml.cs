using BusinessLayer;
using DataLayer;
using LiveCharts;
using LiveCharts.Wpf;
using Sales_Dashboard.UserControls;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Sales_Dashboard
{
    public partial class MainWindow : Window
    {
        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        private EnumCategory category;

        public EnumCategory Category
        {
            get { return category; }
            set { 
                category = value;
            }
        }

        private int year;

        public int Year
        {
            get { return year; }
            set { 
                year = value;
            }
        }

        private ProductCollection products;

        public ProductCollection Products
        {
            get { return products; }
            set { products = value; }
        }

        private SellerCollection sellers;

        public SellerCollection Sellers
        {
            get { return sellers; }
            set { sellers = value; }
        }

        private MonthValueCollection monthValues;

        public MonthValueCollection MonthValues
        {
            get { return monthValues; }
            set { monthValues = value; }
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
        private void FillCombos()
        {
            int endYear = DateTime.Today.Year;
            int startYear = endYear - 2;

            for (int i = startYear; i <= endYear; i++)
            {
                yearComboBox.Items.Add(i);
            }

            foreach (EnumCategory category in Enum.GetValues(typeof(EnumCategory)))
            {
                categoryComboBox.Items.Add(category );
            }

        }

        private void ChangeFilter()
        {
            if (categoryComboBox.SelectedItem != null)
            {
                this.Category = (EnumCategory)categoryComboBox.SelectedItem;
            }

            if (yearComboBox.SelectedItem != null)
            {
                this.Year = (int)yearComboBox.SelectedItem;
            }

            this.ApplyFilter();
        }

        private void ApplyFilter()
        {
            this.UpdateInfoCard(this.Products, this.Category, this.Year);
            this.UpdateUserCard(this.Sellers, this.Products, this.Category, this.Year);
            this.UpdateChart();
        }

        #region InfoCards
        private void UpdateInfoCard(ProductCollection products, EnumCategory category, int year)
        {
            if (qntSalesInfoCard == null || salesInfoCard == null || profitInfoCard == null)
                return;

            //TODO: Implementar a lógica para preencher os UserCards com os dados dos Sellers da Motorcycle+Car
            if (category == EnumCategory.Undefined)
            {
                this.qntSalesInfoCard.SubTitle = "Null";
                this.salesInfoCard.SubTitle = "Null";
                this.profitInfoCard.SubTitle = "Null";
            }
            else
            {
                int soldCount = products.Where(p => p.Category == category.ToString() && p.Date.Year == year && p.IsSold).Count();
                double sumSellPrice = products.Where(p => p.Category == category.ToString() && p.Date.Year == year && p.IsSold).Sum(p => p.SellPrice);
                double sumBuyPrice = products.Where(p => p.Category == category.ToString() && p.Date.Year == year && p.IsSold).Sum(p => p.BuyPrice);

                this.qntSalesInfoCard.SubTitle = soldCount.ToString();
                this.salesInfoCard.SubTitle = "$" + sumSellPrice.ToString();
                this.profitInfoCard.SubTitle = "$" + (sumSellPrice - sumBuyPrice).ToString();
            }
        }
        #endregion
        
        #region UserCards
 
        private void UpdateUserCard(SellerCollection sellers, ProductCollection products, EnumCategory category, int year)
        {
            if (firstUserCard == null || secondUserCard == null || thirdUserCard == null)
                return;

            //TODO: Implementar a lógica para preencher os UserCards com os dados dos Sellers da Motorcycle+Car
            if (category == EnumCategory.Undefined)
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
            else
            {
                var topSellers = products
                    .Where(p => p.Category == category.ToString() && p.Date.Year == year && p.IsSold)
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
                for (int i = 0; i < userCards.Length && i < topSellers.Count; i++)
                {
                    UpdateSingleUserCard(
                        userCards[i],
                        topSellers[i].SellerName,
                        topSellers[i].TotalSellValue,
                        topSellers[i].TotalProfitValue);
                }
            }
        }
        private void UpdateSingleUserCard(UserCard userCard, string sellerName, double upPrice, double downPrice)
        {
            userCard.Title = sellerName;
            userCard.UpPrice = upPrice.ToString("C2");
            userCard.DownPrice = downPrice.ToString("C2");
        }
        public void UpdateChart()
        {
            if (mainChart == null)
                return;

            mainChartTitle.Text = "Monthly Sales";

            var axisX = new Axis
            {
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Set", "Oct", "Nov", "Dec" },
                Separator = new LiveCharts.Wpf.Separator
                {
                    Step = 1,
                }
            };
            mainChart.AxisX = new AxesCollection { axisX };

            var axisY = new Axis
            {
                LabelFormatter = value => value.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")),
                Separator = new LiveCharts.Wpf.Separator
                {
                    Step = 5,
                }
            };
            mainChart.AxisY = new AxesCollection { axisY };


            MonthValueCollection monthValues = this.MonthValues;
            if (monthValues == null)
                return;
            ChartValues<double> chartValues = new ChartValues<double>(monthValues.Select(m => m.Amount));

            var lineSeries = new LineSeries
            {
                Values = chartValues,
            };
            mainChart.Series = new SeriesCollection { lineSeries };
        }

        #endregion
        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.FillCombos();

            this.Products = BusinessLayer.ProductCollection.Get();
            this.Sellers = BusinessLayer.SellerCollection.Get();
            this.MonthValues = BusinessLayer.MonthValueCollection.Get();

            this.ApplyFilter();
        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ChangeFilter();
        }

        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ChangeFilter();
        }
        #endregion
    }
}
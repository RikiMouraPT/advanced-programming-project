using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MonthValueCollection : Collection<MonthValue>
    {
        #region Properties

        #endregion
        #region Constructors
        MonthValueCollection()
        {

        }
        #endregion
        #region Methods
        public static MonthValueCollection Get(int year)
        {
            ProductCollection products = ProductCollection.Get();
            SellerCollection sellers = SellerCollection.Get();

            MonthValueCollection monthValues = new MonthValueCollection();

            //TODO: Implement the logic to get the month values
            for (int i = 1; i <= 12; i++)
            {
                MonthValue monthValue = new();

                monthValue.MonthNumber = i;
                monthValue.Amount = 253 + i;

                monthValues.Add(monthValue);
            }

            return monthValues;
        }
        #endregion
    }
}

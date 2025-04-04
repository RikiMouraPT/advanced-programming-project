using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
        public static MonthValueCollection Get(ProductCollection products, EnumCategory category, int year)
        {

            MonthValueCollection monthValues = new MonthValueCollection();
            if (products == null)
                return monthValues;

            for (int i = 1; i <= 12; i++)
            {
                MonthValue monthValue = new();

                monthValue.MonthNumber = i;
                monthValue.Amount = products.GetAmount(category, year, i);

                monthValues.Add(monthValue);
            }

            return monthValues;
        }
        #endregion
    }
}
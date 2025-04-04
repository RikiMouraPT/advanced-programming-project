using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MonthValue
    {
		#region Constructors

		public MonthValue()
		{

		}
		public MonthValue(int monthNumber, double amount)
			:this()
        {
            this.monthNumber = monthNumber;
            this.amount = amount;
        }

        #endregion

        #region Properties
        private int monthNumber;

		public int MonthNumber
		{
			get { return monthNumber; }
			set { monthNumber = value; }
		}

		private double amount;

		public double Amount
		{
			get { return amount; }
			set { amount = value; }
		}
		#endregion

	}
}

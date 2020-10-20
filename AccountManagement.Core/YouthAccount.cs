using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Core
{
    public class YouthAccount : Account
    {
        public YouthAccount(int accountNumber) : base(accountNumber)
        {
            Balance += 50.0;
        }

        public YouthAccount(int accountNumber, double balance) : base(accountNumber, balance + 50.0)
        {
            //
        }

        public override bool RaiseFrom(double amount)
        {
            bool isRaiseValid = false;
            if (amount > 0)
            {
                if (Balance - amount >= 0)
                {
                    Balance -= amount;
                    isRaiseValid = true;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException("Der Betrag muss größer als 0 sein!");
            }
            return isRaiseValid;
        }
    }
}
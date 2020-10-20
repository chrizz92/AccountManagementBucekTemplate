using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManagement.Core
{
    public class Account : IComparable
    {
        private const double MAXOVERDRAFT = -1000.0;

        private int _accountNumber;
        private double _balance;

        public Account(int accountNumber)
        {
            if (accountNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("Die Kontonummer muss eine Ganzzahl größer 0 sein!");
            }
            if (!IsValid(accountNumber))
            {
                throw new ArgumentException("Kontonummer ist ungültig!");
            }
            _accountNumber = accountNumber;
        }

        public Account(int accountNumber, double balance) : this(accountNumber)
        {
            PayIn(balance);
        }

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public int AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        //TODO: Konstruktoren

        /// <summary>
        /// Die Kontonummer ist gültig, wenn ihre Ziffernsumme durch 10 teilbar ist
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>

        public bool IsValid(int accountNumber)
        {
            int accountNumberSum = 0;
            bool isValid = false;

            do
            {
                accountNumberSum += accountNumber % 10;
                accountNumber /= 10;
            }
            while (accountNumber > 0);

            if (accountNumberSum % 10 == 0)
            {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Auf das Konto wird der Betrag eingezahlt
        /// </summary>
        /// <param name="amount">Betrag muss größer als 0 sein</param>

        public void PayIn(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Der Betrag muss größer als 0 sein!");
            }
        }

        /// <summary>
        /// Abhebung des Betrags vom Konto
        /// </summary>
        /// <param name="amount">Betrag muss größer als 0 sein</param>
        /// <returns>War die Abhebung möglich (Konto gedeckt)</returns>

        public virtual bool RaiseFrom(double amount)
        {
            bool isRaiseValid = false;
            if (amount > 0)
            {
                if (Balance - amount >= MAXOVERDRAFT)
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
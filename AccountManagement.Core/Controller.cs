﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace AccountManagement.Core
{
    public class Controller
    {
        private List<Account> _accounts;

        public Controller()
        {
            _accounts = new List<Account>();
        }

        public int NumberOfAccounts
        {
            get
            {
                return _accounts.Count;
            }
        }

        /// <summary>
        /// Liefert das Konto, falls die Kontonummer existiert
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Konto oder null, falls kein Konto mit der Nummer existiert</returns>
        public Account GetAccount(int accountNumber)
        {
            Account accountToReturn = null;

            foreach (Account account in _accounts)
            {
                if (account.AccountNumber == accountNumber)
                {
                    accountToReturn = account;
                    break;
                }
            }

            //for (int i = 0; i < NumberOfAccounts; i++)
            //{
            //    if (_accounts[i].AccountNumber == accountNumber)
            //    {
            //        accountToReturn = _accounts[i];
            //        break;
            //    }
            //}

            return accountToReturn;
        }

        /// <summary>
        /// Ein neues Konto wird angelegt, falls die Kontonummer
        /// noch nicht existiert.
        /// </summary>
        /// <param accountToAdd="Anzulegendes KOnto"></param>
        /// <returns>Konnte das Konto angelegt werden?</returns>
        public bool AddAccount(Account accountToAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ein neues Konto wird angelegt, falls die übergebene Kontonummer
        /// noch nicht existiert.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="isYouthAccount"></param>
        /// <returns>Konnte das Konto angelegt werden?</returns>
        public bool AddAccount(int accountNumber, bool isYouthAccount = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Auf das Konto wird, falls es existiert der Betrag eingezahlt
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns>existiert das Konto?</returns>
        public bool PayIn(int accountNumber, double amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Vom Konto wird der Betrag abgehoben, falls das Konto
        /// existiert und die Abhebung von der Höhe her möglich ist.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="amount"></param>
        /// <returns>War die Abhebung erfolgreich?</returns>
        public bool RaiseFrom(int accountNumber, double amount)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Der Betrag wird von einem Konto auf ein anderes überwiesen.
        /// Beide Konten müssen existieren und die Abhebung des Betrags
        /// vom From-Konto muss möglich sein.
        /// </summary>
        /// <param name="accountNumberFrom"></param>
        /// <param name="accountNumberTo"></param>
        /// <param name="amount"></param>
        /// <returns>Wurde die Überweisung durchgeführt</returns>
        public bool Transfer(int accountNumberFrom, int accountNumberTo,
                                double amount)
        {
            throw new NotImplementedException();
        }
    }
}
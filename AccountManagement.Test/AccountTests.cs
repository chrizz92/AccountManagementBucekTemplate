using System;
using System.Runtime.InteropServices.ComTypes;
using AccountManagement.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccountManagement.Test
{
    [TestClass]
    public class AccountTests
    {
        private const double MAXDELTA = 0.000001;

        [TestMethod]
        public void Constructor_NegativeAccountNumber_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            int accountNumber = -1;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => new Account(accountNumber),
                typeof(ArgumentOutOfRangeException),
                "Die Kontonummer muss eine Ganzzahl größer 0 sein!"));
        }

        [TestMethod]
        public void Constructor_AccountNumberZero_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            int accountNumber = 0;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => new Account(accountNumber),
                typeof(ArgumentOutOfRangeException),
                "Die Kontonummer muss eine Ganzzahl größer 0 sein!"));
        }

        [TestMethod]
        public void Constructor_AccountNumberInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 12345;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => new Account(accountNumber),
                typeof(ArgumentException),
                "Kontonummer ist ungültig!"));
        }

        [TestMethod]
        public void Constructor_AccountNumberValid_ShouldNotThrowException()
        {
            // Arrange
            int accountNumber = 1234;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(
                _ => new Account(accountNumber)));
        }

        [TestMethod]
        public void Constructor_AccountNumberValid_AccountNumberShouldBeSet()
        {
            // Arrange
            int accountNumber = 1234;
            // Act
            Account account = new Account(accountNumber);
            // Assert
            Assert.AreEqual(accountNumber, account.AccountNumber);
        }

        [TestMethod]
        public void Constructor_AccountNumbersAreValid_ShouldNotThrowException()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(_ => new Account(45326)));
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(_ => new Account(47117)));
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(_ => new Account(08156)));
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(_ => new Account(32410)));
        }

        [TestMethod]
        public void Constructor_AccountNumbersAreInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(_ => new Account(45327), typeof(ArgumentException), "Kontonummer ist ungültig!"));
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(_ => new Account(47110), typeof(ArgumentException), "Kontonummer ist ungültig!"));
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(_ => new Account(08159), typeof(ArgumentException), "Kontonummer ist ungültig!"));
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(_ => new Account(32411), typeof(ArgumentException), "Kontonummer ist ungültig!"));
        }

        [TestMethod]
        public void PayIn_AmountNegative_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Account account = new Account(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => account.PayIn(-8),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void PayIn_Zero_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Account account = new Account(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => account.PayIn(0),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void PayIn_ValidAmount_ShouldNotThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Account account = new Account(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(
                _ => account.PayIn(10)));
        }

        [TestMethod]
        public void PayIn_ValidAmount_NewBalanceShouldBeCalculated()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 10.0;
            Account account = new Account(accountNumber);
            // Act
            account.PayIn(amount);
            // Assert
            Assert.AreEqual(amount, account.Balance, MAXDELTA);
        }

        [TestMethod]
        public void RaiseFrom_AmountNegative_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Account account = new Account(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => account.RaiseFrom(-8),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }


        [TestMethod]
        public void RaiseFrom_Zero_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Account account = new Account(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => account.RaiseFrom(0),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void Raise_ValidAmount_ShouldNotThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Account account = new Account(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(
                _ => account.RaiseFrom(10)));
        }

        [TestMethod]
        public void RaiseFrom_ValidAmount_NewBalanceShouldBeCalculated()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 1000.0;
            Account account = new Account(accountNumber);
            // Act
            account.RaiseFrom(amount);
            // Assert
            Assert.AreEqual(amount * (-1), account.Balance, MAXDELTA);
        }

        [TestMethod]
        public void RaiseFrom_TooMuch_ShouldReturnFalse()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 1001.0;
            Account account = new Account(accountNumber);
            // Act
            bool ok = account.RaiseFrom(amount);
            // Assert
            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void RaiseFrom_TooMuch_ShouldNotChangeBalance()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 1001.0;
            Account account = new Account(accountNumber);
            // Act
            bool ok = account.RaiseFrom(amount);
            // Assert
            Assert.AreEqual(0,account.Balance);
        }


    }
}

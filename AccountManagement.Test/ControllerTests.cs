using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using AccountManagement.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AccountManagement.Test
{
    [TestClass]
    public class ControllerTests
    {
        private const double MAXDELTA = 0.000001;

        [TestMethod]
        public void Constructor_InitializeAccounts_ShouldReturnZeroForNumberOfAccounts()
        {
            // Arrange
            Controller controller = new Controller();
            // Act
            // Assert
            Assert.AreEqual(0,controller.NumberOfAccounts);
        }


        [TestMethod]
        public void AddAccount_NegativeAccountNumber_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = -1;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.AddAccount(accountNumber),
                typeof(ArgumentOutOfRangeException),
                "Die Kontonummer muss eine Ganzzahl größer 0 sein!"));
        }

        [TestMethod]
        public void AddAccount_AccountNumberZero_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 0;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.AddAccount(accountNumber),
                typeof(ArgumentOutOfRangeException),
                "Die Kontonummer muss eine Ganzzahl größer 0 sein!"));
        }

        [TestMethod]
        public void AddAccount_AccountNumberInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 12345;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.AddAccount(accountNumber),
                typeof(ArgumentException),
                "Kontonummer ist ungültig!"));
        }

        [TestMethod]
        public void AddAccount_AccountNumberValid_ShouldNotThrowException()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 1234;
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(
                _ => controller.AddAccount(accountNumber)));
        }

        [TestMethod]
        public void AddAccount_AccountNumberValid_NumberOfAccountsShoudBeIncreased()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 1234;
            // Act
            controller.AddAccount(accountNumber);
            // Assert
            Assert.AreEqual(1, controller.NumberOfAccounts);
        }

        [TestMethod]
        public void GetAccount_AccountNumberValid_ShouldReturnNotNull()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 1234;
            // Act
            controller.AddAccount(accountNumber);
            Account account = controller.GetAccount(accountNumber);
            // Assert
            Assert.IsNotNull(account);
        }

        [TestMethod]
        public void GetAccount_AccountNumberValid_ShouldReturnAccountWithNumber()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 1234;
            // Act
            controller.AddAccount(accountNumber);
            Account account = controller.GetAccount(accountNumber);
            // Assert
            Assert.AreEqual(accountNumber, account.AccountNumber);
        }

        [TestMethod]
        public void GetAccount_NumberValid_AccountNotAdded_ShouldReturnNull()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 1234;
            // Act
            controller.AddAccount(accountNumber);
            Account account = controller.GetAccount(2341);
            // Assert
            Assert.IsNull(account);
        }

        [TestMethod]
        public void GetAccount_NumberIsInvalid_AccountNotAdded_ShouldReturnNull()
        {
            // Arrange
            Controller controller = new Controller();
            int accountNumber = 1234;
            // Act
            controller.AddAccount(accountNumber);
            Account account = controller.GetAccount(2340);
            // Assert
            Assert.IsNull(account);
        }

        [TestMethod]
        public void PayIn_AmountNegative_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.PayIn(accountNumber, -8),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void PayIn_Zero_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.PayIn(accountNumber, 0),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void PayIn_ValidAmount_ShouldNotThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(
                _ => controller.PayIn(accountNumber, 10)));
        }

        [TestMethod]
        public void PayIn_ValidAmount_NewBalanceShouldBeCalculated()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 10.0;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            controller.PayIn(accountNumber, amount);
            Account account = controller.GetAccount(accountNumber);
            // Assert
            Assert.AreEqual(amount, account.Balance, MAXDELTA);
        }

        [TestMethod]
        public void RaiseFrom_AmountNegative_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.RaiseFrom(accountNumber, -8),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }


        [TestMethod]
        public void RaiseFrom_Zero_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.RaiseFrom(accountNumber, 0),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void Raise_ValidAmount_ShouldNotThrowArgumentException()
        {
            // Arrange
            int accountNumber = 1234;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldNotThrowException(
                _ => controller.RaiseFrom(accountNumber, 10)));
        }

        [TestMethod]
        public void RaiseFrom_ValidAmount_NewBalanceShouldBeCalculated()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 1000.0;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            controller.RaiseFrom(accountNumber, amount);
            Account account = controller.GetAccount(accountNumber);
            // Assert
            Assert.AreEqual(amount * (-1), account.Balance, MAXDELTA);
        }

        [TestMethod]
        public void RaiseFrom_TooMuch_ShouldReturnFalse()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 1001.0;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            bool ok = controller.RaiseFrom(accountNumber, amount);

            // Assert
            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void RaiseFrom_TooMuch_ShouldNotChangeBalance()
        {
            // Arrange
            int accountNumber = 1234;
            double amount = 1001.0;
            Controller controller = new Controller();
            controller.AddAccount(accountNumber);
            // Act
            bool ok = controller.RaiseFrom(accountNumber, amount);
            Account account = controller.GetAccount(accountNumber);
            // Assert
            Assert.AreEqual(0,account.Balance);
        }

        [TestMethod]
        public void Transfer_AmountNegative_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.Transfer(accountNumberFrom, accountNumberTo, -8),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void Transfer_AmountZero_ShouldThrowArgumentException()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            // Assert
            Assert.IsTrue(ExceptionUtils.ShouldThrowException(
                _ => controller.Transfer(accountNumberFrom, accountNumberTo, 0),
                typeof(ArgumentOutOfRangeException),
                "Der Betrag muss größer als 0 sein!"));
        }

        [TestMethod]
        public void Transfer_FromDoesntExist_ShouldReturnFalse()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            bool ok = controller.Transfer(-1, accountNumberTo, 100);
            // Assert
            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void Transfer_ToDoesntExist_ShouldReturnFalse()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            bool ok = controller.Transfer(accountNumberFrom, 7652, 100);
            // Assert
            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void Transfer_TooMuch_ShouldReturnFalse()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            bool ok = controller.Transfer(accountNumberFrom, accountNumberTo, 1001);
            // Assert
            Assert.IsFalse(ok);
        }

        [TestMethod]
        public void Transfer_TooMuch_BalanceShouldNotChange()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            bool ok = controller.Transfer(accountNumberFrom, accountNumberTo, 1001);
            // Assert
            Assert.AreEqual(0, controller.GetAccount(accountNumberFrom).Balance);
            Assert.AreEqual(0, controller.GetAccount(accountNumberTo).Balance);
        }

        [TestMethod]
        public void Transfer_ValidAmount_ShouldReturnTrue()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            bool ok = controller.Transfer(accountNumberFrom, accountNumberTo, 1000);
            // Assert
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void Transfer_ValidAmount_BalanceFromShouldBeCorrect()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            controller.Transfer(accountNumberFrom, accountNumberTo, 1000);
            // Assert
            Assert.AreEqual(-1000, controller.GetAccount(accountNumberFrom).Balance);
        }

        [TestMethod]
        public void Transfer_ValidAmount_BalanceToShouldBeCorrect()
        {
            // Arrange
            int accountNumberFrom = 1234;
            int accountNumberTo = 4565;
            Controller controller = new Controller();
            controller.AddAccount(accountNumberFrom);
            controller.AddAccount(accountNumberTo);
            // Act
            controller.Transfer(accountNumberFrom, accountNumberTo, 1000);
            // Assert
            Assert.AreEqual(1000, controller.GetAccount(accountNumberTo).Balance);
        }


        [TestMethod]
        public void IEnumerable_Test()
        {
            Controller controller = new Controller();

            List<Account> accounts = new List<Account>()
            {
                new YouthAccount(19,10),
                new YouthAccount(1919,40),
                new YouthAccount(191919,60),
                new YouthAccount(82, 1000),
                new Account(73,5),
                new Account(64,10),
                new Account(55,100),
                new Account(5519,200),
                new Account(5528,2000)
            };

            controller.AddAccount(accounts[6]);
            controller.AddAccount(accounts[2]);
            controller.AddAccount(accounts[8]);
            controller.AddAccount(accounts[0]);
            controller.AddAccount(accounts[3]);
            controller.AddAccount(accounts[7]);
            controller.AddAccount(accounts[5]);
            controller.AddAccount(accounts[1]);
            controller.AddAccount(accounts[4]);

            int i = 0;
            foreach (var account in (IEnumerable<Account>) controller)
            {
                Assert.AreEqual(accounts[i], account, "Reihenfolge der Iteration falsch!");
                i++;
            }
            Assert.AreEqual(9, i, "Anzahl der zurückgelieferten Konten falsch!");
        }



    }
}

using NUnit.Framework;
using System;
using Monitor = OTS_Supermarket.Models.Monitor;

namespace OTS_Supermarket.Test
{
    [TestFixture]
    public class CartTest
    {
        [Test]
        public void AddOneToCart_OneMonitorToCart_Success()
        {
            Cart cart = new Cart();
            Monitor monitor = new Monitor();

            cart.AddOneToCart(monitor);

            Assert.That(1, Is.EqualTo(cart.Monitor_counter));
        }

        [Test]
        public void AddOneToCart_OneMonitorToCart_Size()
        {
            Cart cart = new Cart();
            Monitor monitor = new Monitor();

            cart.AddOneToCart(monitor);

            Assert.That(1, Is.EqualTo(cart.Size));
        }

        [Test]
        public void AddOneToCart_OneMonitorToCartSizeAlready10_Exception()
        {
            Cart cart = new Cart();
            Monitor monitor = new Monitor();
            cart.Size = 10;

            Exception exception = Assert.Throws<Exception>(() => cart.AddOneToCart(monitor));

            Assert.That(exception.Message, Is.EqualTo("Number of items in cart must be 10 or less!"));
        }

        [TestCase(5, 6)]
        [TestCase(6, 7)]
        public void AddOneToCart_OneMonitorToCart_SuccessDataDriven(int counter, int expectedResult)
        {
            Cart cart = new Cart();
            Monitor monitor = new Monitor();
            cart.Size = counter;

            cart.AddOneToCart(monitor);

            Assert.That(expectedResult, Is.EqualTo(cart.Size));
        }

        [TestCase(5, ExpectedResult = 6)]
        [TestCase(6, ExpectedResult = 7)]
        public int AddOneToCart_OneMonitorToCart_SuccessDataDrivenWithReturnValue(int counter)
        {
            Cart cart = new Cart();
            Monitor monitor = new Monitor();
            cart.Size = counter;

            cart.AddOneToCart(monitor);

            return cart.Size;
        }

        [TestCaseSource(typeof(CartTxtParser), "GetTestCasesData", new object[] { "PICTResults.txt" })]
        public void Test(int size, int amount, int laptop, int monitor, int chair, int computer, int keyboard, string stringDate, double discount)
        {
            Cart cart = new Cart();
            cart.Size = size;
            cart.Monitor_counter = monitor;
            cart.Laptop_counter = laptop;
            cart.Computer_counter = computer;
            cart.Keyboard_counter = keyboard;
            cart.Chair_counter = chair;
            cart.Amount = amount;
            cart.Budget = 100000;

            cart.Calculate(stringDate);

            double expectedFinalBudget = 100000 - (amount - amount * discount);
            Assert.That(cart.Budget, Is.EqualTo(expectedFinalBudget).Within(0.001));
        }

        [Test]
        public void NewCart_SizeIsZero()
        {
            Cart cart = new Cart();

            Assert.That(cart.Size, Is.EqualTo(0));
        }

        [Test]
        public void NewCart_MonitorCounterIsZero()
        {
            Cart cart = new Cart();

            Assert.That(cart.Monitor_counter, Is.EqualTo(0));
        }

        [Test]
        public void AddOneToCart_TwoMonitors_MonitorCounterIsTwo()
        {
            Cart cart = new Cart();
            Monitor monitor1 = new Monitor();
            Monitor monitor2 = new Monitor();

            cart.AddOneToCart(monitor1);
            cart.AddOneToCart(monitor2);

            Assert.That(cart.Monitor_counter, Is.EqualTo(2));
        }

        [Test]
        public void AddOneToCart_TwoMonitors_SizeIsTwo()
        {
            Cart cart = new Cart();
            Monitor monitor1 = new Monitor();
            Monitor monitor2 = new Monitor();

            cart.AddOneToCart(monitor1);
            cart.AddOneToCart(monitor2);

            Assert.That(cart.Size, Is.EqualTo(2));
        }

        [Test]
        public void AddOneToCart_SizeNine_SuccessSizeBecomesTen()
        {
            Cart cart = new Cart();
            Monitor monitor = new Monitor();
            cart.Size = 9;

            cart.AddOneToCart(monitor);

            Assert.That(cart.Size, Is.EqualTo(10));
        }

        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(8, 9)]
        [TestCase(9, 10)]
        public void AddOneToCart_SizeIncreasesByOne(int size, int expectedSize)
        {
            Cart cart = new Cart();
            Monitor monitor = new Monitor();
            cart.Size = size;

            cart.AddOneToCart(monitor);

            Assert.That(cart.Size, Is.EqualTo(expectedSize));
        }

        [Test]
        public void Calculate_AmountZero_BudgetStaysSame()
        {
            Cart cart = new Cart();
            cart.Amount = 0;
            cart.Budget = 100000;

            cart.Calculate("2024-01-01");

            Assert.That(cart.Budget, Is.EqualTo(100000).Within(0.001));
        }

        [Test]
        public void Calculate_AmountWithoutDiscount_BudgetIsReduced()
        {
            Cart cart = new Cart();
            cart.Amount = 5000;
            cart.Budget = 100000;

            cart.Calculate("2024-01-01");

            Assert.That(cart.Budget, Is.LessThan(100000));
        }
    }
}
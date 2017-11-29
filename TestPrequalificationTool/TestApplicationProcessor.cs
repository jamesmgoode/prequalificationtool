using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrequalificationTool;
using PrequalificationTool.Models;
using System;

namespace TestPrequalificationTool
{
    [TestClass]
    public class TestApplicationProcessor
    {
        private readonly DateTime fixedDate = new DateTime(2017, 11, 27, 1, 1, 1);

        #region ValidateAge
        [TestMethod]
        public void TestValidateAge_Under18()
        {
            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(2000, 10, 24),
                AnnualIncome = 100000
            };

            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(fixedDate);

            var applicationProcessor = new ApplicationProcessor(cardApplication, mockTime.Object);

            bool validAge = applicationProcessor.ValidateAge();

            Assert.IsFalse(validAge);
        }

        [TestMethod]
        public void TestValidateAge_Exactly18()
        {
            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(1999, 11, 27),
                AnnualIncome = 100000
            };

            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(fixedDate);

            var applicationProcessor = new ApplicationProcessor(cardApplication, mockTime.Object);

            bool validAge = applicationProcessor.ValidateAge();

            Assert.IsTrue(validAge);
        }

        [TestMethod]
        public void TestValidateAge_Over18()
        {
            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(1969, 7, 20),
                AnnualIncome = 100000
            };

            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(fixedDate);

            var applicationProcessor = new ApplicationProcessor(cardApplication, mockTime.Object);

            bool validAge = applicationProcessor.ValidateAge();

            Assert.IsTrue(validAge);
        }
        #endregion

        #region ProcessApplication
        [TestMethod]
        public void TestProcessApplication_Under30000()
        {
            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(2000, 10, 24),
                AnnualIncome = 20000
            };

            var applicationProcessor = new ApplicationProcessor(cardApplication, new DateTimeHelper());

            var card = applicationProcessor.ProcessApplication();

            Assert.AreEqual("Vanquis", card.CardName);
        }

        [TestMethod]
        public void TestProcessApplication_Exactly30000()
        {
            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(2000, 10, 24),
                AnnualIncome = 30000
            };

            var applicationProcessor = new ApplicationProcessor(cardApplication, new DateTimeHelper());

            var card = applicationProcessor.ProcessApplication();

            Assert.AreEqual("Vanquis", card.CardName);
        }

        [TestMethod]
        public void TestProcessApplication_Over30000()
        {
            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(2000, 10, 24),
                AnnualIncome = 40000
            };

            var applicationProcessor = new ApplicationProcessor(cardApplication, new DateTimeHelper());

            var card = applicationProcessor.ProcessApplication();

            Assert.AreEqual("Barclaycard", card.CardName);
        }
        #endregion
    }
}

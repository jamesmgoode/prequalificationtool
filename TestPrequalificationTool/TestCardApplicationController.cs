using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrequalificationTool;
using PrequalificationTool.Controllers;
using PrequalificationTool.Models;

namespace TestPrequalificationTool
{
    [TestClass]
    public class TestCardApplicationController
    {
        private readonly DateTime _fixedDate = new DateTime(2017, 11, 27, 1, 1, 1);

        [TestMethod]
        public void TestCardApplication_IndexRedirectsToBlankForm()
        {
            var cardApplicationController = new CardApplicationController(null, null);

            var result = cardApplicationController.Index() as ViewResult;

            Assert.AreEqual("CardApplication", result.ViewName);
        }

        [TestMethod]
        public void TestCardApplication_BlankForm()
        {
            var cardApplicationController = new CardApplicationController(null, null);

            var result = cardApplicationController.CardApplication() as ViewResult;

            Assert.AreEqual("CardApplication", result.ViewName);
        }

        [TestMethod]
        public void TestCardApplication_ModelStateInvalid()
        {
            var cardApplicationController = new CardApplicationController(null, null);
            cardApplicationController.ModelState.AddModelError("test_error", "error to test model state");

            var result = cardApplicationController.CardApplication(new CardApplicationViewModel()) as ViewResult;

            Assert.AreEqual("CardApplication", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(CardApplicationViewModel));
        }

        [TestMethod]
        public void TestCardApplication_Under18()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(_fixedDate);

            var cardApplicationController = new CardApplicationController(null, mockTime.Object);

            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(2000, 10, 24),
                AnnualIncome = 100000
            };

            var result = cardApplicationController.CardApplication(cardApplication) as ViewResult;

            Assert.AreEqual("AgeNotValid", result.ViewName);
        }

        [TestMethod]
        public void TestCardApplication_Under30000()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(_fixedDate);

            var cardApplicationController = new CardApplicationController(null, mockTime.Object);

            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(1969, 7, 20),
                AnnualIncome = 20000
            };

            var result = cardApplicationController.CardApplication(cardApplication) as ViewResult;

            var card = result.Model as Card;

            Assert.AreEqual("CardOffer", result.ViewName);
            Assert.AreEqual("Vanquis", card.CardName);
        }

        [TestMethod]
        public void TestCardApplication_Exactly30000()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(_fixedDate);

            var cardApplicationController = new CardApplicationController(null, mockTime.Object);

            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(1969, 7, 20),
                AnnualIncome = 30000
            };

            var result = cardApplicationController.CardApplication(cardApplication) as ViewResult;

            var card = result.Model as Card;

            Assert.AreEqual("CardOffer", result.ViewName);
            Assert.AreEqual("Vanquis", card.CardName);
        }

        [TestMethod]
        public void TestCardApplication_Over30000()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(_fixedDate);

            var cardApplicationController = new CardApplicationController(null, mockTime.Object);

            var cardApplication = new CardApplicationViewModel
            {
                FirstName = "John",
                LastName = "Doe",
                Dob = new DateTime(1969, 7, 20),
                AnnualIncome = 40000
            };

            var result = cardApplicationController.CardApplication(cardApplication) as ViewResult;

            var card = result.Model as Card;

            Assert.AreEqual("CardOffer", result.ViewName);
            Assert.AreEqual("Barclaycard", card.CardName);
        }
    }
}

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
        private readonly DateTime _fixedDate = new DateTime(2017, 11, 27, 1, 1, 1);

        #region ValidateAge
        [TestMethod]
        public void TestValidateAge_Under18()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(_fixedDate);
            var applicationProcessor = new ApplicationProcessor(mockTime.Object);
            var dob = new DateTime(2000, 10, 24);

            var validAge = applicationProcessor.ValidateAge(dob);

            Assert.IsFalse(validAge);
        }

        [TestMethod]
        public void TestValidateAge_Exactly18()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(new DateTime(2017, 11, 27));
            var applicationProcessor = new ApplicationProcessor(mockTime.Object);
            var dob = new DateTime(1999, 11, 27);

            var validAge = applicationProcessor.ValidateAge(dob);

            Assert.IsTrue(validAge);
        }

        [TestMethod]
        public void TestValidateAge_18thBirthday()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(_fixedDate);
            var applicationProcessor = new ApplicationProcessor(mockTime.Object);
            var dob = new DateTime(1999, 11, 27, 1, 1, 1);

            var validAge = applicationProcessor.ValidateAge(dob);

            Assert.IsTrue(validAge);
        }

        [TestMethod]
        public void TestValidateAge_Over18()
        {
            var mockTime = new Mock<IDateTimeHelper>();
            mockTime.Setup(mt => mt.Now()).Returns(_fixedDate);
            var applicationProcessor = new ApplicationProcessor(mockTime.Object);
            var dob = new DateTime(1969, 7, 20);

            var validAge = applicationProcessor.ValidateAge(dob);

            Assert.IsTrue(validAge);
        }
        #endregion

        #region ProcessApplication
        [TestMethod]
        public void TestProcessApplication_Under30000()
        {
            var applicationProcessor = new ApplicationProcessor(null);
            const int annualIncome = 20000;

            var card = applicationProcessor.ProcessApplication(annualIncome);

            Assert.AreEqual("Vanquis Card", card.CardName);
        }

        [TestMethod]
        public void TestProcessApplication_Exactly30000()
        {
            var applicationProcessor = new ApplicationProcessor(null);
            const int annualIncome = 30000;

            var card = applicationProcessor.ProcessApplication(annualIncome);

            Assert.AreEqual("Vanquis Card", card.CardName);
        }

        [TestMethod]
        public void TestProcessApplication_Over30000()
        {
            var applicationProcessor = new ApplicationProcessor(null);
            const int annualIncome = 40000;

            var card = applicationProcessor.ProcessApplication(annualIncome);

            Assert.AreEqual("Barclaycard Credit Card", card.CardName);
        }
        #endregion
    }
}

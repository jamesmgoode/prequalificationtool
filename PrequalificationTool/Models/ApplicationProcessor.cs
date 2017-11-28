using System;

namespace PrequalificationTool.Models
{
    public class ApplicationProcessor
    {
        private const int AgeThreshold = 18;
        private const int IncomeThreshold = 30000;

        private readonly CardApplicationViewModel _cardApplication;

        public ApplicationProcessor(CardApplicationViewModel cardApplication)
        {
            _cardApplication = cardApplication;
        }

        public bool ValidateAge()
        {
            var now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var datePlus18 = _cardApplication.Dob.AddYears(AgeThreshold);
            return datePlus18 >= now;
        }

        public Card ProcessApplication()
        {
            return _cardApplication.AnnualIncome > IncomeThreshold ? CardFactory.BarclayCard() : CardFactory.VanquisCard();
        }

        public void RecordApplication()
        {
        }
    }
}

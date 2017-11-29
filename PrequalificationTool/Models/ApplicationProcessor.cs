using System;

namespace PrequalificationTool.Models
{
    public class ApplicationProcessor
    {
        private const int AgeThreshold = 18;
        private const int IncomeThreshold = 30000;

        private readonly CardApplicationViewModel _cardApplication;
        private readonly IDateTimeHelper _dateTimeHelper;

        public ApplicationProcessor(CardApplicationViewModel cardApplication, IDateTimeHelper dateTimeHelper)
        {
            _cardApplication = cardApplication;
            _dateTimeHelper = dateTimeHelper;
        }

        public bool ValidateAge()
        {
            var now = _dateTimeHelper.Now();
            now = new DateTime(now.Year, now.Month, now.Day);
            var datePlus18 = _cardApplication.Dob.AddYears(AgeThreshold);
            return now >= datePlus18;
        }

        public Card ProcessApplication()
        {
            return _cardApplication.AnnualIncome > IncomeThreshold ? CardFactory.BarclayCard() : CardFactory.VanquisCard();
        }
    }
}

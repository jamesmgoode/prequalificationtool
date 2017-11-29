using System;

namespace PrequalificationTool.Models
{
    public class ApplicationProcessor
    {
        private const int AgeThreshold = 18;
        private const int IncomeThreshold = 30000;
        
        private readonly IDateTimeHelper _dateTimeHelper;

        public ApplicationProcessor(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }

        public bool ValidateAge(DateTime dob)
        {
            var now = _dateTimeHelper.Now();
            var datePlus18 = dob.AddYears(AgeThreshold);
            return now >= datePlus18;
        }

        public Card ProcessApplication(int annualIncome)
        {
            return annualIncome > IncomeThreshold ? CardFactory.BarclayCard() : CardFactory.VanquisCard();
        }
    }
}

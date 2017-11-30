using Microsoft.AspNetCore.Mvc;
using PrequalificationTool.Models;

namespace PrequalificationTool.Controllers
{
    public class CardApplicationController : Controller
    {
        private readonly CardApplicationContext _context;
        private readonly IDateTimeHelper _dateTimeHelper;

        public CardApplicationController(CardApplicationContext context, IDateTimeHelper dateTimeHelper)
        {
            _context = context;
            _dateTimeHelper = dateTimeHelper;
        }

        public IActionResult CardApplication()
        {
            return View("CardApplication");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CardApplication([Bind("FirstName,LastName,Dob,AnnualIncome")] CardApplicationViewModel cardApplication)
        {
            if (!ModelState.IsValid)
            {
                return View("CardApplication", cardApplication);
            }

            var applicationProcessor = new ApplicationProcessor(_dateTimeHelper);

            var validAge = applicationProcessor.ValidateAge(cardApplication.Dob);
            if (!validAge)
            {
                var under18Result = new ApplicationResult
                {
                    FirstName = cardApplication.FirstName,
                    LastName = cardApplication.LastName,
                    Result = "No cards shown"
                };
                _context.ApplicationResults.Add(under18Result);
                _context.SaveChanges();

                return View("AgeNotValid");
            }

            var card = applicationProcessor.ProcessApplication(cardApplication.AnnualIncome);
            var cardResult = new ApplicationResult
            {
                FirstName = cardApplication.FirstName,
                LastName = cardApplication.LastName,
                Result = "Card shown: " + card.CardName
            };
            _context.ApplicationResults.Add(cardResult);
            _context.SaveChanges();

            return View("CardOffer", card);
        }
    }
}

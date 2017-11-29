using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            return View("CardApplication");
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
                return View("AgeNotValid");
            }

            var card = applicationProcessor.ProcessApplication(cardApplication.AnnualIncome);


            return View("CardOffer", card);
        }
    }
}

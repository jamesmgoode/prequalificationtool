using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrequalificationTool.Models;
using Microsoft.EntityFrameworkCore;

namespace PrequalificationTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbContext _dbContext;
        private readonly IDateTimeHelper _dateTimeHelper;

        public HomeController(DbContext dbContext, IDateTimeHelper dateTimeHelper)
        {
            _dbContext = dbContext;
            _dateTimeHelper = dateTimeHelper;
        }

        public IActionResult Index()
        {
            return View();
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

            var applicationProcessor = new ApplicationProcessor(cardApplication, _dateTimeHelper);

            var validAge = applicationProcessor.ValidateAge();
            if (!validAge)
            {
                return View("AgeNotValid");
            }

            var card = applicationProcessor.ProcessApplication();
            
            return View("CardOffer", card);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

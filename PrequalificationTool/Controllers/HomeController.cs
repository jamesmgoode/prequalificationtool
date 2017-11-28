﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrequalificationTool.Models;

namespace PrequalificationTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDateTimeHelper _dateTimeHelper;

        public HomeController(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CardApplication()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CardApplication([Bind("FirstName,LastName,Dob,AnnualIncome")] CardApplicationViewModel cardApplication)
        {
            var applicationProcessor = new ApplicationProcessor(cardApplication, _dateTimeHelper);

            var validAge = applicationProcessor.ValidateAge();

            var decision = applicationProcessor.ProcessApplication();


            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

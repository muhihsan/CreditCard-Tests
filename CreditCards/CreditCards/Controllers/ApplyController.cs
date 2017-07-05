﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreditCards.ViewModels;
using CreditCards.Core.Models;
using CreditCards.Core.Interfaces;

namespace CreditCards.Controllers
{
    public class ApplyController : Controller
    {
        private ICreditCardApplicationRepository _applicationRepository;

        public ApplyController(ICreditCardApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(NewCreditCardApplicationDetails applicationDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(applicationDetails);
            }

            var creditCardApplication = new CreditCardApplication
            {
                FirstName = applicationDetails.FirstName,
                LastName = applicationDetails.LastName,
                Age = applicationDetails.Age.Value,
                GrossAnnualIncome = applicationDetails.GrossAnnualIncome.Value
            };

            await _applicationRepository.AddAsync(creditCardApplication);

            return View("ApplicationComplete", creditCardApplication);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

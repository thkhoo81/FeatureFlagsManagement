﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FeatureFlagsManagement.Models;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlagsManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeatureManager _featureManager;

        public HomeController(ILogger<HomeController> logger, IFeatureManager featureManager)
        {
            _logger = logger;
            _featureManager = featureManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewData["Version"] = "3.1";

            if (await _featureManager.IsEnabledAsync(MyFeatureManagementFlags.NewFeature2.ToString()))
            {
                ViewData["Version"] = "2.2";
            }
            
            
            return View();
        }

        //[FeatureGate(MyFeatureManagementFlags.NewFeature1)]
        [Route("[controller]/[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

        
        [Route("[controller]/[action]")]
        [FeatureGate(MyFeatureManagementFlags.NewFeature3)]
        public IActionResult NewFeature3()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

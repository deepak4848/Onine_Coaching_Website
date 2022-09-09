﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YET.Models;


namespace YET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DatabaseContext _context;
        
             public HomeController(DatabaseContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

           
            return View();
        }

        public IActionResult test()
        {


            return View();
        }



        public IActionResult Aboutus()
        {
            return View();
        }
        public  IActionResult Team()
        {
            var data = _context.tbl_Teams.ToList();

            return View(data);
        }

       

        public IActionResult Contactus()
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
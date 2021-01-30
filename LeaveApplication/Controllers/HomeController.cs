using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NaijaFarmers.Models;

namespace NaijaFarmers.Controllers
{
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        public ViewResult Details()
        {
            //_employeeRepository = new MockEmployeeRepository();
            return View(_employeeRepository.GetEmployee(1));
        }
    }
}

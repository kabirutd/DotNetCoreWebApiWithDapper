using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MK.DotNetCore.Dapper.ApplicationCore.Entities;
using MK.DotNetCore.Dapper.Infrastructure.DataAccess.Interface;
using MK.DotNetCore.Dapper.WebApi.Models;

namespace MK.DotNetCore.Dapper.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeesRepository _employeeRepository;
        public HomeController(ILogger<HomeController> logger, IEmployeesRepository employeesRepository)
        {
            _logger = logger;
            _employeeRepository = employeesRepository;
        }

        public ActionResult Index()
        {
            var AllEmployees = _employeeRepository.GetEmployeesByQuery();
            return View(AllEmployees);
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var Employee = _employeeRepository.GetEmployeesById(id);
            return View(Employee);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees data)
        {
            var employeeId = _employeeRepository.AddEmployee(data);
            if (employeeId > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var Employee = _employeeRepository.GetEmployeesById(id);
            return View(Employee);
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employees data)
        {
            var employeeUpdated = _employeeRepository.UpdateEmployee(id, data);
            if (employeeUpdated)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var deleted = _employeeRepository.DeleteEmployee(id);
            if (deleted)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
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

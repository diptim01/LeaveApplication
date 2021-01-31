using LeaveApplication.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using NaijaFarmers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LeaveApplication.Controllers
{
    public class AdminDashboard : Controller
    {
        public AdminDashboard(AppDbContext context)
        {
            _context = context;
        }
        private AppDbContext _context;

        public IActionResult Index()
        {
            var allStaffs = _context.StaffInformation.ToList();

            return View(allStaffs);
        }


        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            var staff = _context.StaffInformation.Find(id);
            return View(staff);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            ViewBag.staffList = _context.StaffInformation.Select(x => new SelectListItem { Text = x.StaffId, Value = x.StaffId }).ToList();
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                StaffInformation staffInformation = new StaffInformation()
                {
                    FirstName = collection["FirstName"],
                    StaffId = collection["StaffId"],
                    LastName = collection["LastName"],
                    LeaveBalance = collection["LeaveBalance"] == StringValues.Empty ? 0 : Convert.ToInt32(collection["LeaveBalance"]),
                    LineManager = collection["LineManager"]
                };

                if(_context.StaffInformation.Any(x => x.StaffId.ToLower() == staffInformation.StaffId.ToLower()))
                {
                    return View();
                }

                _context.StaffInformation.Add(staffInformation);

                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            var staff = _context.StaffInformation.FirstOrDefault(x => x.Id == id);
            ViewBag.staffList = _context.StaffInformation.Select(x => new SelectListItem { Text = x.StaffId, Value = x.StaffId }).ToList();

            return View(staff);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var staff = _context.StaffInformation.Find(id);

                if (staff == null)
                {
                    return View();
                }

                staff.Id = id;
                staff.FirstName = collection["FirstName"];
                staff.StaffId = collection["StaffId"];
                staff.LastName = collection["LastName"];
                staff.LeaveBalance = collection["LeaveBalance"] == StringValues.Empty ? 0 : Convert.ToInt32(collection["LeaveBalance"]);
                staff.LineManager = collection["LineManager"];


                _context.StaffInformation.Update(staff);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            var staff = _context.StaffInformation.Find(id);
            return View(staff);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var staff = _context.StaffInformation.Find(id);

                _context.StaffInformation.Remove(staff);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

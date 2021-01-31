using LeaveApplication.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NaijaFarmers.DAL;
using NaijaFarmers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LeaveApplication.Controllers
{
    public class LeaveDashboard : Controller
    {
        private AppDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public LeaveDashboard(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: LeaveDashboard
        public ActionResult Index()
        {
            var staffId = _userManager.GetUserAsync(HttpContext.User).Result.UserName;
            return View(_context.LeaveInformation.Where(x => x.StaffId == staffId).ToList());
        }

        // GET: LeaveDashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveDashboard/Create
        public ActionResult Create()
        {
            ViewBag.LeaveTypes = new SelectList(Enum.GetValues(typeof(LeaveTypes)), LeaveTypes.Annual);
            return View();
        }

        // POST: LeaveDashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var staffId = _userManager.GetUserAsync(HttpContext.User).Result.UserName;

                var lineManager = _context.StaffInformation.FirstOrDefault(x => x.StaffId == staffId).LineManager;

                if (string.IsNullOrEmpty(lineManager))
                    return Content("Line Manger not found!");


                var leaveFrom = Convert.ToDateTime(collection["LeaveFrom"]);
                var leaveTo = Convert.ToDateTime(collection["LeaveTo"]);

                var leaveDays = Convert.ToInt32((leaveTo - leaveFrom).TotalDays);

                var leaveBalance = _context.StaffInformation.Where(x => x.StaffId == staffId).Select(x => x.LeaveBalance).FirstOrDefault();

                if (leaveBalance < leaveDays)
                {
                    return Content("Leave balance is low for the leave days");
                }

                LeaveInformation leave = new LeaveInformation
                {
                    LeaveStatus = "AwaitingApproval",
                    DateRequested = DateTime.Now,
                    LeaveType = collection["LeaveType"],
                    StaffId = staffId,
                    LineManager = lineManager,
                    LeaveDays = leaveDays,
                    LeaveFrom = leaveFrom,
                    LeaveTo = leaveTo,
                    InitialBalance = leaveBalance,
                    AwaitingApproval = true,
                    FinalBalance = leaveBalance - leaveDays
                };

                _context.LeaveInformation.Add(leave);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: LeaveDashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveDashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveDashboard/Delete/5
        public ActionResult Delete(int id)
        {
            var leaveInfo = _context.LeaveInformation.Find(id);
            return View(leaveInfo);
        }

        // POST: LeaveDashboard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var leaveInfo = _context.LeaveInformation.Find(id);

                _context.LeaveInformation.Remove(leaveInfo);
          
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

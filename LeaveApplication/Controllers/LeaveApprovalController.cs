using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NaijaFarmers.Models;
using Microsoft.AspNetCore.Identity;

namespace LeaveApplication.Controllers
{
    public class LeaveApprovalController : Controller
    {
        private AppDbContext _context;
        private UserManager<IdentityUser> _userManager;

        public LeaveApprovalController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var staffId = _userManager.GetUserAsync(HttpContext.User).Result.UserName;

           var approvalList = _context.LeaveInformation.Where(x => x.LineManager == staffId && (x.IsProcessed == null || x.IsProcessed == false)).ToList();
            return View(approvalList);
        }

        public ActionResult Approve(int id)
        {
            try
            {
                var leaveInfo = _context.LeaveInformation.Find(id);

                leaveInfo.LeaveStatus = "Approved";
                leaveInfo.IsProcessed = true;
                leaveInfo.AwaitingApproval = false;
                leaveInfo.DateApproved = DateTime.Now;
                leaveInfo.InitialBalance = leaveInfo.FinalBalance;
                leaveInfo.FinalBalance = 0;

                _context.LeaveInformation.Update(leaveInfo);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Reject(int id)
        {
            try
            {
                var leaveInfo = _context.LeaveInformation.Find(id);

                leaveInfo.LeaveStatus = "Rejected";
                leaveInfo.IsProcessed = true;
                leaveInfo.AwaitingApproval = false;
                leaveInfo.DateApproved = DateTime.Now;
               

                _context.LeaveInformation.Update(leaveInfo);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}

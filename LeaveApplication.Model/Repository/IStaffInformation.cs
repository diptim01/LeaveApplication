using LeaveApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveApplication.Model.Repository
{
    public interface IStaffInformation
    {
        List<StaffInformation> GetAllStaffInformation();
    }
}

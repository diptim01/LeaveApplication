using LeaveApplication.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaijaFarmers.DAL.ViewModels
{
    public class StaffInfoVM
    {
        public StaffInformation StaffInformation { get; set; }

        [NotMapped]
        public List<String> StaffIdList { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRepairShop.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int VehicleId { get; set; }
        public string UserId { get; set; }

    }
}

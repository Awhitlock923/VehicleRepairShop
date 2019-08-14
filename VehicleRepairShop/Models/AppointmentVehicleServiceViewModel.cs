using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRepairShop.Models
{
    public class AppointmentVehicleServiceViewModel
    {
        public Appointment Appointment { get; set; }
        public List<VehicleService> VehicleServices { get; set; }
        public Vehicle Vehicle { get; set; }
        public User User { get; set; }
    }
}

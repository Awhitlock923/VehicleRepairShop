using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRepairShop.Models
{
    public class AppointmentVehicleServiceLink
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int VehicleServiceId { get; set; }
        public VehicleService VehicleService { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRepairShop.Models
{
    public class User: IdentityUser
    {
        public int TypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

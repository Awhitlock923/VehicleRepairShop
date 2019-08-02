using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VehicleRepairShop.Models;

namespace VehicleRepairShop.Models
{
    public class VehicleRepairShopContext : IdentityDbContext<User>
    {
        public VehicleRepairShopContext (DbContextOptions<VehicleRepairShopContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleRepairShop.Models.Vehicle> Vehicle { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VehicleRepairShop.Models.VehicleService> VehicleService { get; set; }
        public DbSet<VehicleRepairShop.Models.Appointment> Appointment { get; set; }
    }
}

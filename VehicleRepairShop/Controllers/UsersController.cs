using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairShop.Models;

namespace VehicleRepairShop.Controllers
{
    public class UsersController: Controller
    {
        private readonly VehicleRepairShopContext _context;

        public UsersController(VehicleRepairShopContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
    }
}

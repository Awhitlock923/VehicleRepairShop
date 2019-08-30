using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRepairShop.Models;

namespace VehicleRepairShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly VehicleRepairShopContext _context;
        private readonly UserManager<User> _userManager;
        public UsersController(VehicleRepairShopContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Users
        public ActionResult Index()
        {
            _context.Users.ToList();
            var users = ConvertFromDatabase(_context.Users.ToArray());
            return View(users);
        }

        private IEnumerable<UserViewModel> ConvertFromDatabase(IEnumerable<User> users)
        {
            return users.Select(u => new UserViewModel()
            {
                EmailAddress = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Id = u.Id,
                Type = (UserType)u.TypeId
            }).ToList();
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (User == null)
                throw new Exception();
            var newUser = ConvertFromDatabase(new[] { user }).First();
            return View(newUser);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel user)
        {
            try
            {
                // TODO: Add insert logic here
                var newUser = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.EmailAddress,
                    UserName = user.EmailAddress,
                    TypeId = (int)user.Type
                };

                var result = await _userManager.CreateAsync(newUser);
                if (!result.Succeeded)
                {
                    throw new Exception();
                }

                var newRole = string.Empty;
                switch (user.Type)
                {
                    case UserType.Administrator:
                        newRole = Constants.Roles.Admin;
                        break;
                    case UserType.Technician:
                        newRole = Constants.Roles.Tech;
                        break;
                    case UserType.User:
                        newRole = Constants.Roles.User;
                        break;
                    default:
                        break;
                }
                await _userManager.AddToRoleAsync(newUser, newRole);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _context.Users
                .FirstOrDefault(m => m.Id == id);
            var newUser = ConvertFromDatabase(new[] { user }).First();
            if (newUser == null)
            {
                return NotFound();
            }

            return View(newUser);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
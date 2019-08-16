using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleRepairShop.Models;

namespace VehicleRepairShop.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly VehicleRepairShopContext _context;

        public AppointmentsController(VehicleRepairShopContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Appointment.Include(v => v.User).Include(a => a.Vehicle).ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.User)
                .Include(a => a.Vehicle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            var vehicleServices = await _context
                .AppointmentVehicleServiceLink
                .Where(l => l.AppointmentId == appointment.Id)
                .Include(l => l.VehicleService)
                .Select(l => l.VehicleService)
                .ToListAsync();
            var appointmentViewModel = new AppointmentVehicleServiceViewModel()
            {
                Appointment = appointment,
                VehicleServices = vehicleServices
            };
            return View(appointmentViewModel);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create(int vehicleId)
        {
            var vehicle = _context.Vehicle.Where(v => v.Id == vehicleId).First();
            var user = _context.Users.Where(u => u.Id == vehicle.OwnerId).First();
            var vehicleServices = await _context.VehicleService.ToListAsync();

            var appointmentViewModel = new AppointmentVehicleServiceViewModel() {
                Vehicle = vehicle,
                User = user,
                VehicleServices = vehicleServices,
                Appointment = new Appointment()
                {
                    UserId = user.Id,
                    VehicleId = vehicle.Id,
                    ScheduledDate = DateTime.Now
                }
            };
            return View(appointmentViewModel);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment, List<int> vehicleServices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();

                foreach(var service in vehicleServices)
                {
                    _context.Add(new AppointmentVehicleServiceLink()
                    {
                        AppointmentId = appointment.Id,
                        VehicleServiceId = service
                    });
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DateTime scheduledDate)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.ScheduledDate = scheduledDate;
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.Id == id);
        }
    }
}

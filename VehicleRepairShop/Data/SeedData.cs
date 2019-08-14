using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRepairShop.Models;

namespace VehicleRepairShop.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new VehicleRepairShopContext(
                serviceProvider.GetRequiredService<DbContextOptions<VehicleRepairShopContext>>()))
            {
                string UserId = null;
                var userEmail = "admin@test.com";
                var userManager = serviceProvider.GetService<UserManager<User>>();
                if (!context.Users.Any())
                {
                    var user = new User()
                    {
                        FirstName = "Admin",
                        LastName = "Super",
                        Email = userEmail,
                        UserName = userEmail
                    };

                    var result = await userManager.CreateAsync(user, "password");
                    if (!result.Succeeded)
                    {
                        throw new Exception();
                    }

                    user = await userManager.FindByEmailAsync(userEmail);
                    UserId = await userManager.GetUserIdAsync(user);
                }

                if(UserId == null)
                {
                    var user = await userManager.FindByEmailAsync(userEmail);
                    UserId = user.Id;
                }

                if (!context.Vehicle.Any())
                {
                    context.Vehicle.AddRange(
                        new Vehicle()
                        {
                            Make = "Toyota",
                            Model = "4Runner",
                            AcceptedDate = DateTime.Now,
                            IsInShop = true,
                            OwnerId = UserId
                        },
                        new Vehicle()
                        {
                            Make = "Toyota",
                            Model = "Camry",
                            AcceptedDate = DateTime.Now,
                            IsInShop = true,
                            OwnerId = UserId
                        }); ;
                }

                if (!context.VehicleService.Any())
                    {
                        context.VehicleService.AddRange(
                        new VehicleService() { Name = "Tire Rotation" },
                        new VehicleService() { Name = "Oil Change" }
                        );
                    }

                context.SaveChanges();
            }
        }

        internal static Task InitializeAsync(IServiceProvider services)
        {
            throw new NotImplementedException();
        }

        internal static object InitializeAsynv(object services)
        {
            throw new NotImplementedException();
        }
    }
}

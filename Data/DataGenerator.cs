using HotelBookingAppReact.Models;
using HotelBookingAppReact.Models.Facility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingAppReact.Data
{
    public class DataGenerator
    {
        public static async void GenerateData(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            Facility facility = new Facility
            {
                Name = "WiFi",
                Description = "Free Wifi available in the room"
            };

            context.Facilities.AddRange(
               facility,
                 new Models.Facility.Facility
                 {
                     Name = "A/C",
                     Description = "Air conditioning available in the room"
                 }
            );
            context.SaveChanges();
            context.Rooms.AddRange(
                new Models.Room.Room
                {
                    RoomNumber = 1,
                    Facilities = new List<Models.Facility.Facility> { context.Facilities.First() },
                    PricePerNight = 55,
                    NumberOfBeds = 1
                }
                );

            context.SaveChanges();


            // Creez rolul de administrator
            var role = new IdentityRole();
            role.Name = "Administrator";
            await roleManager.CreateAsync(role);

            //Dupa care creez un admin
            ApplicationUser user = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                SecurityStamp = DateTime.Now.ToString()

            };
            
            var success = await userManager.CreateAsync(user, "Admin8*");
            await userManager.AddToRoleAsync(user, "Administrator");

            System.Diagnostics.Debug.WriteLine(userManager.Users.ToList().First().ToString());
        }
    }
}

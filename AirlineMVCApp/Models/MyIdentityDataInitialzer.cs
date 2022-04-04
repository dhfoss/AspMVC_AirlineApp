using Microsoft.AspNetCore.Identity;
using System;

namespace AirlineMVCApp.Models
{
    public static class MyIdentityDataInitializer
    {


        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            //AddUserToRole(userManager, roleManager);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("bob@bob.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "bob1";
                user.Email = "bob@bob.com";
                user.FirstName = "Bob";
                user.LastName = "Smith";
                user.DateOfBirth = new DateTime(1960, 1, 1);

                IdentityResult result = userManager.CreateAsync
                (user, "asdfASDF1234!").Result;

            }

            if (userManager.FindByEmailAsync("jane@jane.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "jane1";
                user.Email = "jane@jane.com";
                user.FirstName = "Jane";
                user.LastName = "Jones";
                user.DateOfBirth = new DateTime(1970, 1, 1);

                IdentityResult result = userManager.CreateAsync
                (user, "asdfASDF1234!").Result;

            }

            if (userManager.FindByEmailAsync("sam@sam.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "sam1";
                user.Email = "sam@sam.com";
                user.FirstName = "Sam";
                user.LastName = "Weatherman";
                user.DateOfBirth = new DateTime(1980, 1, 1);

                IdentityResult result = userManager.CreateAsync
                (user, "asdfASDF1234!").Result;

            }



        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("BookingAgent").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "BookingAgent";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Pilot").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Pilot";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
                
            }

            if (!roleManager.RoleExistsAsync("FlightAttendant").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "FlightAttendant";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


        }

        public async static void AddUserToRole(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            //ApplicationUser user = new ApplicationUser();
            //user.UserName = "pilot";
            //user.Email = "pilot@pilot.com";
            //user.FirstName = "James";
            //user.LastName = "Jamison";
            //user.DateOfBirth = new DateTime(1980, 1, 1);

            //IdentityResult result = userManager.CreateAsync
            //(user, "asdfASDF1234!").Result;

            var user = await userManager.FindByEmailAsync("pilot@pilot.com");
            var role = await roleManager.FindByNameAsync("Pilot");
            var result2 = await userManager.AddToRoleAsync(user, role.Name);
        }
    }
}

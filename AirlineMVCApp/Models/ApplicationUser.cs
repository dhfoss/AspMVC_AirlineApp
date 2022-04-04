using Microsoft.AspNetCore.Identity;
using System;

namespace AirlineMVCApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual PassengerInfo PassengerInfo { get; set; }

    }
}

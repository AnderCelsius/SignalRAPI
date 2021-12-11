using Microsoft.AspNetCore.Identity;
using System;

namespace SignalRAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName 
        { 
            get { return FirstName + " " + LastName; }
        }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalRAPI.Models;
using System;

namespace SignalRAPI.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<FormStatus> FormStatuses { get; set; }
        public DbSet<RequestForm> RequestForms { get; set; }
    }

}

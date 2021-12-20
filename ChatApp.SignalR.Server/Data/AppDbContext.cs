using ChatApp.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.SignalR.Server
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<ChatMessage> ChatMessages { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Message> Messages { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

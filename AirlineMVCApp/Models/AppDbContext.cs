using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirlineMVCApp.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightTask> FlightTasks { get; set; }
        public DbSet<PassengerInfo> PassengerInfo { get; set; }
        public DbSet<BoardedPassenger> BoardedPassengers { get; set;}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BoardedPassenger>().HasNoKey();

            builder.Entity<FlightTask>().HasData(new FlightTask { FlightTaskId = 1, TaskName = "No task", TaskCompleteMessage = "No task" });
            builder.Entity<FlightTask>().HasData(new FlightTask { FlightTaskId = 2, TaskName = "Greet Passengers", TaskCompleteMessage = "This is your Captain speaking, sit back and enjoy the flight." });
            builder.Entity<FlightTask>().HasData(new FlightTask { FlightTaskId = 3, TaskName = "Start Flight", TaskCompleteMessage = "Flight has started. All systems go.  Good luck, Captain." });
            builder.Entity<FlightTask>().HasData(new FlightTask { FlightTaskId = 4, TaskName = "Ascend Flight", TaskCompleteMessage = "Flight has ascended." });
            builder.Entity<FlightTask>().HasData(new FlightTask { FlightTaskId = 5, TaskName = "Descend Flight", TaskCompleteMessage = "Another happy landing. Well done, Captain." });
            builder.Entity<FlightTask>().HasData(new FlightTask { FlightTaskId = 6, TaskName = "Tasks Complete", TaskCompleteMessage = "No more tasks for this flight." });
        
            
        }

    }
}

using Duende.IdentityServer.EntityFramework.Options;
using HotelBookingAppReact.Models;
using HotelBookingAppReact.Models.Facility;
using HotelBookingAppReact.Models.Reservation;
using HotelBookingAppReact.Models.Room;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HotelBookingAppReact.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("BookingsDb");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                 .HasMany(e => e.Reservations)
                 .WithOne(e => e.User)
                 .HasForeignKey(e => e.UserId);

            builder.Entity<Room>()
                .HasMany(e => e.Facilities)
                .WithMany(e => e.rooms);

            builder.Entity<Reservation>()
                .HasOne(e => e.Room)
                .WithMany(e => e.Reservations);
        }

        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Facility>? Facilities { get; set; }
    }
}
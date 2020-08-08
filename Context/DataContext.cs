using CopaVale.Models;
using Microsoft.EntityFrameworkCore;

namespace CopaVale.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Team> Team { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
              .HasIndex(user => user.Nickname)
                .IsUnique();

            builder.Entity<User>()
              .HasIndex(user => user.Email)
                .IsUnique();

            builder.Entity<User>()
              .HasIndex(user => user.FaceitURL)
                .IsUnique();
        }
    }


}

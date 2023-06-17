using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class GameStoreDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public GameStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameGenre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>().HasMany(x => x.Genres).WithMany(x => x.Games);
            builder.Entity<Game>().Property(x => x.Price).HasColumnType("decimal(6,2)");

            builder.Entity<Order>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Order>()
                .HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(x => x.Games)
                .WithMany(y => y.Users);

            builder.Entity<Order>().Property(x => x.OrderTotalCost).HasColumnType("decimal(6,2)");
        }
    }
}
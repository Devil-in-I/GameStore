using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Tests
{
    internal static class UnitTestHelper
    {
        public static DbContextOptions<GameStoreDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<GameStoreDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new GameStoreDbContext(options))
            {
                SeedData(context);
            }

            return options;
        }

        public static void SeedData(GameStoreDbContext context)
        {
            context.Games.AddRange(
                new Game { Id = 1, Name = "Assassin's Creed 1" },
                new Game { Id = 2, Name = "Assassin's Creed 2" },
                new Game { Id = 3, Name = "Call of duty 4: Modern Warfare" }
                );
            context.SaveChanges();
        }


    }
}

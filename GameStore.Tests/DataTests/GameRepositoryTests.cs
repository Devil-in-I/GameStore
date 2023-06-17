
namespace GameStore.Tests.DataTests
{
    [TestFixture]
    public class GameRepositoryTests
    {
        [Test]
        public async Task GetAllAsync_ReturnsAllValues()
        {
            // Arrange
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());
            var repository = new GameRepository(context);

            // Act
            var games = await repository.GetAllAsync();

            Assert.That(games, Is.EqualTo(ExpectedGames).Using(new GameEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GetByIdAsync_ReturnsSingleValue(int id)
        {
            // Arrange
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var repository = new GameRepository(context);

            // Act
            var game = await repository.GetByIdAsync(id);

            var expected = ExpectedGames.FirstOrDefault(x => x.Id == id);

            Assert.That(game, Is.EqualTo(expected).Using(new GameEqualityComparer()), message: "GetAllAsync method works incorrect");
        }

        [Test]
        public void GetByIdAsync_ThrowsArgumentNullException()
        {
            // Arrange
            using var context = new GameStoreDbContext(UnitTestHelper.GetUnitTestDbOptions());

            var repository = new GameRepository(context);

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await repository.GetByIdAsync(-1));
        }

        #region Test data
        private static IEnumerable<Game> ExpectedGames => new[]
        {
            new Game { Id = 1, Name = "Assassin's Creed 1" },
            new Game { Id = 2, Name = "Assassin's Creed 2" },
            new Game { Id = 3, Name = "Call of duty 4: Modern Warfare" }
        };
        #endregion
    }
}

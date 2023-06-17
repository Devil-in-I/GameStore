namespace Business.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public List<GameGenreModel> Genres { get; set; } = new List<GameGenreModel>();
    }
}

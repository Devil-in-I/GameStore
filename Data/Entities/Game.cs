using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class Game : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [Range(0, int.MaxValue/2)]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        // Navigation property
        public List<GameGenre>? Genres { get; set; } = new List<GameGenre>();
        public List<User> Users { get; set; } = new List<User>();

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}

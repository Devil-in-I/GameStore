using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class GameGenre : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        // Navigation property
        public List<Game>? Games { get; set; } = new List<Game>();
    }
}

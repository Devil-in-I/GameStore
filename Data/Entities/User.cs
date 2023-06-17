using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        public List<Order> Orders { get; set; } = new List<Order>();

        public List<Game> Games { get; set; } = new List<Game>();
    }
}

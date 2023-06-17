using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GameId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderCreated { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Game Game { get; set; }

        /// <summary>
        /// Cost of order, if there's a disount or something like that.
        /// </summary>
        [Required]
        public decimal OrderTotalCost { get; set; }
    }
}

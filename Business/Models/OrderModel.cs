using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class OrderModel
    {
        public int UserId { get; set; }

        public int GameId { get; set; }

        public decimal OrderTotalCost { get; set; }
    }
}

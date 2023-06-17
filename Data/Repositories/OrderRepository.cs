using Data.Data;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly GameStoreDbContext context;
        public OrderRepository(GameStoreDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

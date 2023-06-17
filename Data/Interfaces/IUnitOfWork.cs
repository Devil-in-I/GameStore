using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        public IGameRepository Game { get; }

        public IGameGenreRepository GameGenre { get; }

        public Task Save();
    }
}

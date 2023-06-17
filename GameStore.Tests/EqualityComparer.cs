using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Tests
{
    public class GameEqualityComparer : IEqualityComparer<Game>
    {
        public bool Equals([AllowNull] Game x, [AllowNull] Game y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Genres == y.Genres;
        }
        public int GetHashCode([DisallowNull] Game obj)
        {
            return obj.GetHashCode();
        }
    }
    public class GameGenreEqualityComparer : IEqualityComparer<GameGenre>
    {
        public bool Equals([AllowNull] GameGenre x, [AllowNull] GameGenre y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id
                && x.Name == y.Name
                && x.Games == y.Games;
        }
        public int GetHashCode([DisallowNull] GameGenre obj)
        {
            return obj.GetHashCode();
        }
    }
}

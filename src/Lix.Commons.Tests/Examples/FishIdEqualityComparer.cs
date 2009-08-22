using System.Collections.Generic;

namespace Lix.Commons.Tests.Examples
{
    public class FishIdEqualityComparer : IEqualityComparer<Fish>
    {
        public bool Equals(Fish x, Fish y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Fish obj)
        {
            return obj.GetHashCode();
        }
    }
}
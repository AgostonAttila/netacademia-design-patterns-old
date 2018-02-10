using System;
using System.Collections.Generic;
using System.Linq;

namespace _03Strategy
{
    public class ParosokOsszeg : IStrategiaMuvelet
    {
        public int Osszegezz(List<int> list)
        {
            return list.Where(x => (x % 2 == 0))
                       .Sum();
        }
    }
}
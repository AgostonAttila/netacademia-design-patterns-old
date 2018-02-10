using System;
using System.Collections.Generic;
using System.Linq;

namespace _03Strategy
{
    public class ParatlanokOsszeg : IStrategiaMuvelet
    {
        public int Osszegezz(List<int> list)
        {
            return list.Where(x=>(x % 2 == 1))
                       .Sum();
        }
    }
}
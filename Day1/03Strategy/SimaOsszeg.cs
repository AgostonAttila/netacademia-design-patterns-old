using System;
using System.Collections.Generic;
using System.Linq;

namespace _03Strategy
{
    public class SimaOsszeg : IStrategiaMuvelet
    {
        public int Osszegezz(List<int> list)
        {
            return list.Sum();
        }
    }
}
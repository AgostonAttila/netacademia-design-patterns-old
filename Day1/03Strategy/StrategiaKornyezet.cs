using System;
using System.Collections.Generic;

namespace _03Strategy
{
    public class StrategiaKornyezet
    {
        private List<int> list;

        private IStrategiaMuvelet strategia;

        public StrategiaKornyezet(List<int> list)
        {
            this.list = list;
        }

        public int Osszeg()
        {
            if (strategia == null)
            {
                throw new ArgumentNullException("strategia");
            }

            return strategia.Osszegezz(list);
        }

        public void Strategiabeallitas(IStrategiaMuvelet strategia)
        {
            this.strategia = strategia;
        }
    }

    public interface IStrategiaMuvelet
    {
        int Osszegezz(List<int> list);
    }

}
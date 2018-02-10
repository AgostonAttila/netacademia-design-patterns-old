using System;
using System.Collections.Generic;

namespace _03Strategy
{
    public class DelegateKornyezet
    {
        private List<int> list;

        public DelegateKornyezet(List<int> list)
        {
            this.list = list;
        }

        /// <summary>
        /// Egy példa .NET-ben a stratégia interface-ének
        /// delegate-tel való kiváltására
        /// az Action<> és Func<> előre bekészített 
        /// delegate-ek segítségével menet közben 
        /// paraméterdefinícióban tudjuk a stratégia metódus 
        /// paramétereit és visszaadott értékét definiálni
        /// </summary>
        /// <param name="strategia"></param>
        /// <returns></returns>
        public int Osszeg(Func<List<int>, int> strategia)
        {
            return strategia(list);
        }
    }
}
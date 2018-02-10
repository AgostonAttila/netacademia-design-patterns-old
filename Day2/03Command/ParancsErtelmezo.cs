using System;
using System.Collections.Generic;
using System.Linq;

namespace _03Command
{
    public class ParancsErtelmezo
    {
        private List<IParancs> parancsok;

        public ParancsErtelmezo(List<IParancs> parancsok)
        {
            this.parancsok = parancsok;
        }

        public IParancs ParancsValasztasa(string[] parameterek)
        {
            var parancsszoveg = parameterek[0];
            var parancs = parancsok.FirstOrDefault(x => x.Hivas == parancsszoveg);
            if (parancs == null)
            {
                //Null object pattern
                return new NincsIlyenParancs(parameterek[0]);
            }
            parancs.ParameterMegadas(parameterek);
            return parancs;
        }
    }
}
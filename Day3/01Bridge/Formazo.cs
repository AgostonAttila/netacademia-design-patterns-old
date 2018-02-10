using System;
using System.Linq;

namespace _01Bridge
{
    public class Formazo : IFormazo
    {
        public string Formazas(string cimke, string szoveg)
        {
            return string.Format("{0}: {1}", cimke, szoveg);
        }
    }

    public class VisszafeleFormazo : IFormazo
    {
        public string Formazas(string cimke, string szoveg)
        {
            return string.Format("{0}: {1}", cimke, new string(szoveg.Reverse().ToArray()));
        }
    }

    public class MasfeleFormazo : IFormazo
    {
        public string Formazas(string cimke, string szoveg)
        {
            return string.Format("{0}: =={1}== ", cimke,szoveg);
        }
    }

}
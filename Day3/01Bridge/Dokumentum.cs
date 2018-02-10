using System;

namespace _01Bridge
{
    /// <summary>
    /// Az osztályok számának robbanásszerű növekedését a gyökerénél próbáljuk megoldani,
    /// ha itt sikerül egy öszekapcsolható megoldást létrehozni, akkor az minden osztályra hat.
    /// 
    /// Ezt implementációval tudjuk elérni, így a felület az nem elég hatékony eszköz.
    /// </summary>
    public abstract class Dokumentum
    {

        /// <summary>
        /// Ide bekapcsolunk egy stratégiát, amit a leszármaztatott 
        /// osztályokból meg tudunk hívni, így egy helyen kell csak 
        /// cserélni az algoritmust, annak az eredménye mindenre 
        /// hatással lesz, ami innen származódik le és használja.
        /// </summary>
        protected IFormazo formazo;

        public Dokumentum(IFormazo formazo)
        {
            this.formazo = formazo;
        }
        
        public abstract void Print();
    }

    public interface IFormazo
    {
        string Formazas(string cimke, string szoveg);
    }
}
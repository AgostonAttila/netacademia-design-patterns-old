using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _02Iterator
{

    //példa abstract osztályra a .NET-ben a Stream:
    //önállóan nem példányosítható, csak a belőle leszármaztatott
    //osztályokon keresztül érhető el a logika
    //var stream = new Stream();


    /// <summary>
    /// Absztrakt osztály önállóan nem használható, nem példányosítható
    /// </summary>
    public abstract class AbstractList
    {
        /// <summary>
        /// Az absztrakt függvény szerepe hasonló a felület (interface)-ben 
        /// definiált függvényhez, a leszármaztatott osztályban implementálni kell
        /// </summary>
        /// <returns></returns>
        public abstract AbstractIterator CreateIterator();
    }
}
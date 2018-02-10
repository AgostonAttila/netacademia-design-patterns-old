using System;
using System.Collections.Generic;
using System.Linq;

namespace _03Mediator
{
    public class UzenoSzoba
    {
        List<IMegfigyelo> felhasznalok = new List<IMegfigyelo>();
        public void Regisztracio(IMegfigyelo felhasznalo)
        {
            felhasznalok.Add(felhasznalo);
            felhasznalo.UzenetKozpont = this;
        }

        public void Uzenet(string kuldo, string cimzett, string uzenet)
        {
            ///Ezt .NET eseményekkel nem tudjuk megoldani könnyen:
            ///szűrünk a feliratkozott felhasználók között valamilyen 
            ///szempont alapján
            foreach (var felhasznalo in felhasznalok.Where(x => cimzett.Contains(x.Nev))
                                             .ToList())
            {
                felhasznalo.Uzenet(kuldo, uzenet);
            } 
        }
    }
}
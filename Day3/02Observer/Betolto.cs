using System;
using System.Collections.Generic;

namespace _02Observer
{
    public class Betolto : IAllapot
    {
        public event EventHandler<int> Ertesites = delegate { };

        private int folyamatJelzo;
        public int FolyamatJelzo
        {
            get { return folyamatJelzo; }
            set
            {
                //Esetleg, ha nem módosul az érték, akkor nem kell üzenni
                //if (folyamatJelzo == value) { return; }
                //Beállítom a státuszt
                folyamatJelzo = value;
                ///Ez az üzenet szűrés nélkül MINDEN feliratkozóhoz kimegy.
                Ertesites(this, folyamatJelzo);
            }
        }

        public void Start()
        {

            ///A betöltés közben szeretnénk, ha értesülne a felhasználó az állapotról.

            //0% (elindult)
            ///Itt értesítést kell kikuldeni a feliratkozott megfigyelőknek
            FolyamatJelzo = 0;

            //20%
            FolyamatJelzo = 20;
            //FolyamatJelzo = 20;
            //FolyamatJelzo = 20;
            //FolyamatJelzo = 20;
            //FolyamatJelzo = 20;
            //FolyamatJelzo = 20;

            //50%
            FolyamatJelzo = 50;

            //80%
            FolyamatJelzo = 80;

            //100%
            FolyamatJelzo = 100;

        }

    }
}
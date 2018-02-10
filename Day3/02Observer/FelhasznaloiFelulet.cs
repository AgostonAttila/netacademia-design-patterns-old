using System;

namespace _02Observer
{
    public class FelhasznaloiFelulet : IMegfigyelo
    {

        public void Ertesites(int folyamatJelzo)
        {
            Console.WriteLine("Felhasználói felület értesült: {0}", folyamatJelzo);
        }
    }
}
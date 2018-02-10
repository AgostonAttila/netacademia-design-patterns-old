using System;

namespace _02Observer
{
    public class Naplozo : IMegfigyelo
    {
        public void Ertesites(int folyamatJelzo)
        {
            Console.WriteLine("Naplózás értesült: {0} ({1})", folyamatJelzo, DateTime.Now.Ticks);
        }
    }
}
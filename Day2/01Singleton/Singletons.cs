namespace _01Singleton
{
    public sealed class Singleton1
    {
        /// <summary>
        /// A konstruktor private, vagyis ez az osztály nem példányosítható
        /// az osztályon kívülről
        /// </summary>
        private Singleton1() { }

        /// <summary>
        /// Csak akkor hozom létre a példányt, ha valóban szükség van rá
        /// </summary>
        private static Singleton1 instance = null;

        public static Singleton1 Instance
        {
            get
            {
                //Mivel csak akkor hozom létre, ha szükség
                //van rá, így itt vizsgálom, hogy kell-e
                if (instance == null)
                { //ez a megoldás nem thread safe, ha itt találkozik két szál, akkor kétszer példányosítok.
                    //ha még nincs, létrehozom a példányt
                    instance = new Singleton1();
                }
                return instance;
            }
        }
    }

    /// <summary>
    /// Első thread safe próba
    /// </summary>
    public sealed class Singleton2
    {
        private Singleton2() { }
        private static readonly object _lock = new object();

        private static Singleton2 instance = null;

        public static Singleton2 Instance
        {
            get
            {
                ///Thread safe megoldás no1.
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new Singleton2();
                    }
                }
                return instance;
            }
        }
    }

    /// <summary>
    /// Második thread safe próba, teljesítményjavítás
    /// Nem javasolt (ha egy implementációt jegyzünk meg, ne ez legyen):
    /// - Java-ban nem implementálható így 
    /// - A korábbi CLI (Common Language Infrastructure) szabványoknak nem felel meg
    /// - könnyű elrontani
    /// - teljesítmény szempontjából mindig van egy if végrehajtás, amikor elkérjük a példányt
    /// </summary>
    public sealed class Singleton3
    {
        private Singleton3() { }
        private static readonly object _lock = new object();

        private static Singleton3 instance = null;

        public static Singleton3 Instance
        {
            get
            {
                if (instance == null)
                { //ide csak akkor kerülünk ha még senki nem hozott létre példányt
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new Singleton3();
                        }
                    }
                }
                return instance;
            }
        }
    }

    public sealed class Singleton4
    {
        //Ez elvileg ugyanaz, 
        //private static Singleton4 instance = new Singleton4();

        //mint:
        //private static Singleton4 instance = null;
        //static Singleton4()
        //{
        //    instance = new Singleton4();
        //}

        //De sajnos nem. Az első a típusinicializátor végrehajtásakor fut, a második pedig a statikus konstruktor a C# definíció szerint

        //Itt a leírás

        /// <summary>
        /// Mikor fut le a statikus konstruktor:
        /// 
        /// 1. C# szabvány szerint
        ///  - Ha az osztályból létrejön egy példány
        ///  - Bármelyik osztályszintű (static) tagját az osztálynak meghívják
        ///  
        /// 2. CLI szabvány szerint
        ///  - használja az osztály típusinicializátor fogalmát
        ///  - nem biztos, hogy van
        ///  - és az osztály dekorálva lehet BeforeFieldInit atributummal
        ///  
        /// ez az inicializátor lefut
        ///  - ha dekorálva van, legkésőbb akkor, 
        ///     amikor az értékre szükség van (de bármikor lefuthat előtte)
        ///  - ha nincs dekorálva, akkor ha 
        ///     - bármelyik mező használatakor
        ///     - bármelyik property vagy metódus meghívásakor
        /// 
        /// A mezei programozó a második esetre számít, vagyis, hogy csak akkor fut, 
        /// ha "használjuk az osztályt". De ez csak akkor van így ha nincs dekorálva.
        /// 
        /// Márpedig csak akkor nincs dekorálva, ha létezik statikus konstruktor.
        /// </summary>

        //A fentiek szerint az első ezzel egyenértékű
        private static Singleton4 instance = new Singleton4(); //Ez a futtatókörnyezet segsége miatt definíció szerint thread safe
        static Singleton4() { }
        //A többi a szokásos
        private Singleton4() { }
        public static Singleton4 Instance
        {
            get
            {
                return instance;
            }
        }

        //Ezzel az implementációval egy probléma van:
        //ha az osztálynak van más működése is, akkor 
        //azt használva példányosodik az osztályom, ami 
        //a DP szerint nem jó
    }

    /// <summary>
    /// Ez a végleges nyertes, a teljesítmény szempontjából IS a legjobb
    /// </summary>
    public sealed class Singleton5
    {
        private Singleton5() { }
        public static Singleton5 Instance
        {
            get { return Belso.instance; }
        }

        private class Belso
        {
            internal static Singleton5 instance = new Singleton5();
            static Belso() { } //azért kell, hogy ne legyen dekorálva
        }
    }

    /// <summary>
    /// Egy példa leszármaztatható singletonra
    /// </summary>
    public class Singleton6 //nem sealed
    {
        protected Singleton6() { } //a konstruktora a gyerek osztályokból hívható
        public static Singleton6 Instance
        {
            get { return Belso.instance; }
        }

        private class Belso
        {
            internal static Singleton6 instance = new Singleton6();
            static Belso() { }
        }
    }

    /// <summary>
    /// DE: az ősosztályból (Singleton6) ad ennek az 
    /// Instance property-je is példányt, nem a Singleton7-ből!
    /// </summary>
    public class Singleton7 : Singleton6
    { }

    public class AkarmiBarmi
    {
        public int MyProperty { get; set; }
        public string MyProperty2 { get; set; }
        public void Muvelet() { }
    }
}
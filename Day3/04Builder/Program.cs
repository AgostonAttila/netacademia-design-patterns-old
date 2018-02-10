using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04Builder
{
    /// <summary>
    /// Bonyolult sok részből álló objektumpéldány
    /// "felparaméterezésére" szolgál
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var computer = new Computer();
            computer.Processor = Processor.x64;
            computer.OS = OS.Windows7;
            computer.HasDVD = true;
            computer.HDD = 120;
            computer.HasSoundCard = true;
            computer.HasUSB = true;
            computer.Applications = new List<string>{ "MSSQL", "VisualStudio", "VLC" };
            //computer.Display();

            ///Hogy lehetne-ezt delegálni valahogyan?
            var builder1 = new PCBuilder();
            builder1.CreatePC();
            computer = builder1.GetPC();
            //computer.Display();

            //No jó, de ha mindent újra kell írni, akkor nem nyerünk semmit,
            //munkaszervezés1
            //var builder2 = new WindowsPCBuilder();
            //builder2.CreatePC();
            //builder2.BuildPC();
            //computer = builder2.GetPC();
            //computer.Display();

            //Megvan a gyártósor, vezéreljük
            var director = new NormalPcDirector(new WindowsPCBuilder());
            director.BuildPC();
            computer = director.GetPC();
            computer.Display();

            director = new NormalPcDirector(new LinuxPCBuilder());
            director.BuildPC();
            computer = director.GetPC();
            computer.Display();


            Console.ReadLine();
        }
    }
}

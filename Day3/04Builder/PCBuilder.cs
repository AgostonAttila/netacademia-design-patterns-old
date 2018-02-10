using System;
using System.Collections.Generic;

namespace _04Builder
{
    public class PCBuilder
    {
        private Computer computer;

        public void CreatePC()
        {
            computer = new Computer();

            BuildHardware();
            InstallOS();
            InstallApps();
        }

        private void BuildHardware()
        {
            ///hardver összeállítása
            computer.Processor = Processor.x64;
            computer.HasDVD = true;
            computer.HDD = 120;
            computer.HasSoundCard = true;
            computer.HasUSB = true;
        }

        private void InstallOS()
        {
            //szoftver összeállítása
            computer.OS = OS.Windows7;
        }

        private void InstallApps()
        {
            //alkalmazások telepítése
            computer.Applications = new List<string> { "MSSQL", "VisualStudio", "VLC" };
        }

        public Computer GetPC()
        {
            return computer;
        }
    }
}
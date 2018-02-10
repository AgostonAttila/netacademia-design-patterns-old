using System;
using System.Collections.Generic;

namespace _04Builder
{
    public class WindowsPCBuilder : AbstractBuilder
    {
        public override void BuildHardware()
        {
            computer.Processor = Processor.x64;
            computer.HasDVD = true;
            computer.HDD = 120;
            computer.HasSoundCard = true;
            computer.HasUSB = true;
        }

        public override void InstallApps()
        {
            computer.OS = OS.Windows7;
        }

        public override void InstallOS()
        {
            computer.Applications = new List<string> { "MSSQL", "VisualStudio", "VLC" };
        }
    }
}
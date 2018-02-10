using System;
using System.Collections.Generic;

namespace _04Builder
{
    public class LinuxPCBuilder : AbstractBuilder
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
            computer.Applications = new List<string> { "Firefox", "Thunderbird", "VLC" };
        }

        public override void InstallOS()
        {
            computer.OS = OS.Ubuntu16_10;
        }
    }
}
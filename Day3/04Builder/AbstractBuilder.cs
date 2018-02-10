using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _04Builder
{
    public abstract class AbstractBuilder
    {
        protected Computer computer;

        public void CreatePC()
        {
            computer = new Computer();
        }

        public abstract void BuildHardware();
        public abstract void InstallOS();
        public abstract void InstallApps();
        public Computer GetPC()
        {
            return computer;
        }
    }
}
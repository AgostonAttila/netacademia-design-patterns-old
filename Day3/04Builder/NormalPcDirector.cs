using System;

namespace _04Builder
{
    public class NormalPcDirector
    {
        private AbstractBuilder builder;

        public NormalPcDirector(AbstractBuilder builder)
        {
            this.builder = builder;
        }

        public void BuildPC()
        {
            builder.CreatePC();
            builder.BuildHardware();
            builder.InstallOS();
            builder.InstallApps();
        }

        public Computer GetPC()
        {
            return builder.GetPC();
        }
    }
}
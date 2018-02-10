using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace _04Builder
{
    public class Computer
    {
        public Computer()
        {
        }

        public List<string> Applications { get; internal set; }
        public bool HasDVD { get; internal set; }
        public bool HasSoundCard { get; internal set; }
        public bool HasUSB { get; internal set; }
        public int HDD { get; internal set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public OS OS { get; internal set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Processor Processor { get; internal set; }

        public void Display()
        {
            Console.WriteLine(JsonConvert.SerializeObject(this, Formatting.Indented));
            Console.WriteLine();
        }
    }

    public enum Processor
    {
        x86, x64, amd32, amd64
    }

    public enum OS
    {
        Windows95, WindowsXP, Windows7, Windows10,
        Ubuntu14_4, Ubuntu16_10, CentOS
    }

}
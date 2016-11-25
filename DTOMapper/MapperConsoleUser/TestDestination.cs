using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperConsoleUser
{
    class TestDestination
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public bool Married { get; set; }

        public override string ToString()
        {
            return "Destination { \n" +
                "Age: " + Age + "\n" +
                "Name: " + Name + "\n" +
                "Weight: " + Weight + "\n" +
                "Married: " + Married + "\n" +
                "}";

        }
    }
}

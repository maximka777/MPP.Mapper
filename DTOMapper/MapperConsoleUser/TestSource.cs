using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperConsoleUser
{
    class TestSource
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public bool Man { get; set; }

        public override string ToString()
        {
            return "Source { \n" +
                "Age: " + Age + "\n" +
                "Name: " + Name + "\n" +
                "Weight: " + Weight + "\n" +
                "Man: " + Man + "\n" +
                "}";

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOMapper;

namespace MapperConsoleUser
{
    class Program
    {
        static void Main(string[] args)
        {
            TestSource source = new TestSource()
            {
                Age = 5,
                Name = "Maxim",
                Man = true,
                Weight = 87.0f
            };
            Console.WriteLine(source);

            Mapper mapper = new Mapper();
            TestDestination dest = mapper.Map<TestSource, TestDestination>(source);
            Console.WriteLine(dest);

            TestDestination dest1 = mapper.Map<TestSource, TestDestination>(source);
            Console.WriteLine(dest1);

            Console.Read();
        }
    }
}

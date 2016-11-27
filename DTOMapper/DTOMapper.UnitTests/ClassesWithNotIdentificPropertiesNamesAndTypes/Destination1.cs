using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper.UnitTests.ClassesWithNotIdentificPropertiesNamesAndTypes
{
    class Destination1
    {
        public int Age1 { get; set; }
        public string Weight { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            else
            {
                Destination1 dest = obj as Destination1;
                if (obj == null)
                {
                    return false;
                }
                if ((Age1 == dest.Age1) && (Weight == dest.Weight))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

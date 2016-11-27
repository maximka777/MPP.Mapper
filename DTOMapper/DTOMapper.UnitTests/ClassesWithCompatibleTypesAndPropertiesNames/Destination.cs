using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper.UnitTests.ClassesWithCompatibleTypesAndPropertiesNames
{
    class Destination
    {
        public int Age { get; set; }
        public float Weight { get; set; }

        public override bool Equals(object obj)
        {
            if(this == obj)
            {
                return true;
            }
            else
            {
                Destination dest = obj as Destination;
                if(obj == null)
                {
                    return false;
                }
                if((Age == dest.Age) && (Weight == dest.Weight))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

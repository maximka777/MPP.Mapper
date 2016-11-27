using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper.UnitTests.ClassesWithNumberTypes
{
    class Destination2
    {
        public long Prop1 { get; set; }
        public float Prop2 { get; set; }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            else
            {
                Destination2 dest = obj as Destination2;
                if (obj == null)
                {
                    return false;
                }
                if ((Prop1 == dest.Prop1) && (Prop2 == dest.Prop2))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

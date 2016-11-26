using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper
{
    class SourceDestinationTypesPair
    {
        public Type SourceType { get; set; }
        public Type DestinationType { get; set; }

        public override bool Equals(object obj)
        {
            SourceDestinationTypesPair pair = obj as SourceDestinationTypesPair;
            if((pair.DestinationType.FullName == DestinationType.FullName) &&
                (pair.SourceType.FullName == SourceType.FullName))
            {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return 31 * DestinationType.GetHashCode() + SourceType.GetHashCode();
        }
    }
}

using DTOMapper.UnitTests.ClassesWithCompatibleTypesAndPropertiesNames;
using DTOMapper.UnitTests.ClassesWithNotIdentificPropertiesNamesAndTypes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper.UnitTests
{
    [TestFixture]
    class MapperTests
    {
        
        [Test]
        public void Map_CompatibleTypes_ReturnsRightMappedObject()
        {
            Mapper mapper = Mapper.Instance;
            Source source = new Source()
            {
                Age = 54,
                Weight = 87.0f
            };

            Destination etalon = new Destination()
            {
                Age = 54,
                Weight = 87.0f
            };

            Destination dest = mapper.Map<Source, Destination>(source);

            Assert.AreEqual(dest, etalon);
        }
        
        [Test]
        public void Map_UncompatibleProperties_ReturnsRightMappedObject()
        {
            Mapper mapper = Mapper.Instance;
            Source1 source = new Source1()
            {
                Age = 5,
                Weight = 87.0f
            };
            Destination1 etalon = new Destination1();

            Destination1 dest = mapper.Map<Source1, Destination1>(source);

            Assert.AreEqual(dest, etalon);
        }

    }
}

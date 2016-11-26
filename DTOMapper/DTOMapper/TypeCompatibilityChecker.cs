using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper
{
    class TypeCompatibilityChecker
    {
        private static readonly TypeCompatibilityChecker instance = new TypeCompatibilityChecker();

        private List<Type> signedTypes = new List<Type>()
        {
            typeof(long),
            typeof(int),
            typeof(short),
            typeof(sbyte)
        };

        private List<Type> unsignedTypes = new List<Type>()
        {
            typeof(ulong),
            typeof(uint),
            typeof(ushort),
            typeof(byte)
        };

        private List<Type> realTypes = new List<Type>()
        {
            typeof(double),
            typeof(float)
        };

        public TypeCompatibilityChecker Instance
        {
            get
            {
                return instance;
            }
        }

        public bool IsCompatible(Type source, Type destination)
        {
            bool result = false;
            if(source.FullName == destination.FullName)
            {
                result = true;
            }
            else
            {
                if ((signedTypes.Contains(source)) && (signedTypes.Contains(destination)))
                {
                    if(signedTypes.IndexOf(source) >= signedTypes.IndexOf(destination))
                    {
                        result = true;
                    }
                }
                else if ((unsignedTypes.Contains(source)) && (unsignedTypes.Contains(destination)))
                {
                    if (unsignedTypes.IndexOf(source) >= unsignedTypes.IndexOf(destination))
                    {
                        result = true;
                    }
                }
                else if ((realTypes.Contains(source)) && (realTypes.Contains(destination)))
                {
                    if (realTypes.IndexOf(source) >= realTypes.IndexOf(destination))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}

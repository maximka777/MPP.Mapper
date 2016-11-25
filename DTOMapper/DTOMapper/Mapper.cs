using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DTOMapper
{
    public class Mapper : IMapper
    {
        private Dictionary<SourceDestinationTypesPair, Expression> expressions = 
            new Dictionary<SourceDestinationTypesPair, Expression>();

        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            Type sourceType = typeof(TSource);
            Type destType = typeof(TDestination);
            SourceDestinationTypesPair key = new SourceDestinationTypesPair()
                {
                    SourceType = sourceType,
                    DestinationType = destType
                };
            TDestination result = (TDestination)destType.GetConstructor(new Type[0]).Invoke(new object[0]);
            if (expressions.ContainsKey(key))
            {
                var lambda = ((Expression<Func<TSource, TDestination>>)expressions[key]).Compile();
                result = (TDestination)lambda(source);
            }
            else
            {
                Expression<Func<TSource, TDestination>> expr = BuildNewMapLambda<TSource, TDestination>();
                expressions[key] = expr;
                result = expr.Compile()(source);
            }
            return result;
        }

        private Expression<Func<TSource, TDestination>> BuildNewMapLambda<TSource, TDestination>()
        {
            Expression<Func<TSource, TDestination>> result = null;
            
            return result;
        }
    }
}

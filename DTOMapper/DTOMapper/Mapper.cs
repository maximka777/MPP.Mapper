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
                var lambda = ((Expression<Func<TSource, TDestination, TDestination>>)expressions[key]).Compile();
                result = (TDestination)lambda(source, result);
            }
            else
            {
                Expression<Action<TSource, TDestination>> expr = 
                    BuildNewMapLambda<TSource, TDestination>();
                expressions[key] = expr;
                expr.Compile()(source, result);
            }
            return result;
        }

        private Expression<Action<TSource, TDestination>> BuildNewMapLambda<TSource, TDestination>()
        {
            Type sourceType = typeof(TSource);
            Type destinationType = typeof(TDestination);
            List<Expression> exprList = new List<Expression>();

            ParameterExpression sourceParameter = Expression.Parameter(typeof(TSource), "source");
            ParameterExpression destinationParameter = Expression.Parameter(typeof(TDestination), "destination");
            Expression assignExpression = null;

            foreach (PropertyInfo destinationProperty in destinationType.GetProperties())
            {
                if (destinationProperty.CanWrite)
                {
                    foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
                    {
                        if(CanMap(sourceProperty, destinationProperty)){
                            Expression getSourcePropertyValueExpression = Expression.Property(sourceParameter, sourceProperty);
                            Expression getDestinationPropertyExpression = Expression.Property(destinationParameter, destinationProperty);
                            assignExpression = Expression.Assign(getDestinationPropertyExpression, getSourcePropertyValueExpression);
                            exprList.Add(assignExpression);
                        }
                    }
                }
            }

            Expression body = Expression.Block(exprList);

            Expression<Action<TSource, TDestination>> result = 
                Expression.Lambda<Action<TSource, TDestination>>(body, sourceParameter, destinationParameter);

            return result;
        }

        private bool CanMap(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            return (sourceProperty.Name == destinationProperty.Name) 
                && (sourceProperty.PropertyType == destinationProperty.PropertyType);
        }
    }
}

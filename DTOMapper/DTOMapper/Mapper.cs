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
        private static readonly Mapper instance = new Mapper();

        public static Mapper Instance
        {
            get
            {
                return instance;
            }
        }

        private Dictionary<SourceDestinationTypesPair, Delegate> expressions = 
            new Dictionary<SourceDestinationTypesPair, Delegate>();
        private TypeCompatibilityChecker checker = new TypeCompatibilityChecker();

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
                var lambda = expressions[key];
                ((Action<TSource, TDestination>)lambda)(source, result);
            }
            else
            {
                Expression<Action<TSource, TDestination>> expr = 
                    BuildNewMapLambda<TSource, TDestination>();
                var lambda = expr.Compile();
                expressions[key] = lambda;
                lambda(source, result);
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
            Expression assignPropertiesExpression = null;

            foreach (PropertyInfo destinationProperty in destinationType.GetProperties())
            {
                if (destinationProperty.CanWrite)
                {
                    foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
                    {
                        if(CanMap(sourceProperty, destinationProperty)){
                            Expression getSourcePropertyValueExpression = Expression.Property(sourceParameter, sourceProperty);
                            Expression getDestinationPropertyExpression = Expression.Property(destinationParameter, destinationProperty);
                            assignPropertiesExpression = Expression.Assign(getDestinationPropertyExpression, Expression.Convert(getSourcePropertyValueExpression, destinationProperty.PropertyType));
                            exprList.Add(assignPropertiesExpression);
                        }
                    }
                }
            }

            Expression body = null;
            if (exprList.Count != 0)
            {
                body = Expression.Block(exprList);
            }
            else
            {
                body = Expression.Empty();
            }
            Expression<Action<TSource, TDestination>> result = 
                Expression.Lambda<Action<TSource, TDestination>>(body, sourceParameter, destinationParameter);

            return result;
        }

        private bool CanMap(PropertyInfo sourceProperty, PropertyInfo destinationProperty)
        {
            return (sourceProperty.Name == destinationProperty.Name) 
                && (checker.IsCompatible(sourceProperty.PropertyType, destinationProperty.PropertyType));
        }
    }
}

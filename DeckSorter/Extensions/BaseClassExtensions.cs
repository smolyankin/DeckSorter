using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeckSorter.Extensions
{
    /// <summary>
    /// Работа с базовыми классами
    /// </summary>
    public static class BaseClassExtensions
    {
        
        /// <summary>
        /// Преобразование произвольного класса к другому (копирование свойств)
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TDestination Transform<TDestination>(this object obj) where TDestination : new()
        {
            var tDerived = new TDestination();
            foreach (var propDerived in typeof(TDestination).GetProperties())
            {
                var propBase = obj.GetType().GetProperty(propDerived.Name);
                if (propBase != null)
                {
                    object value = propBase.GetValue(obj, null);

                    if (value != null && propBase.PropertyType.IsArray)
                    {
                        MethodInfo convertMethod = typeof(BaseClassExtensions).GetMethod("ConvertArray",
                            BindingFlags.NonPublic | BindingFlags.Static);
                        var targetType = propDerived.PropertyType.GenericTypeArguments.First();
                        MethodInfo generic = convertMethod.MakeGenericMethod(targetType);
                        value = generic.Invoke(null, new[] { value });
                    }
                    propDerived.SetValue(tDerived, value, null);
                }
            }
            return tDerived;
        }

        /// <summary>
        /// Преобразование произвольного класса к другому (копирование свойств)
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static List<TDestination> Transform<TSource, TDestination>(this List<TSource> objs)
            where TDestination : new()
            where TSource : new()
        {
            return objs.Select(obj => Transform<TDestination>(obj)).ToList();
        }
    }
}
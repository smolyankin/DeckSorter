using System;
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
        /// Преобразование базового класса к производному (копированием свойств)
        /// </summary>
        /// <param name="tBase">Базовый объект</param>
        /// <typeparam name="TBase">Базовый класс</typeparam>
        /// <typeparam name="TDerived">Производный класс</typeparam>
        /// <returns>Производный объект</returns>
        public static TDerived ToDerived<TBase, TDerived>(this TBase tBase) where TDerived : TBase, new()
        {
            var tDerived = new TDerived();
            foreach (var propBase in typeof(TBase).GetProperties())
            {
                var propDerived = typeof(TDerived).GetProperty(propBase.Name);
                propDerived.SetValue(tDerived, propBase.GetValue(tBase, null), null);
            }
            return tDerived;
        }

        /// <summary>
        /// копировать одноименным свойствам значения текущего объекта
        /// </summary>
        /// <typeparam name="TDestination">объект в котором нужно обновить</typeparam>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="self"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TBase ToCopy<TBase, TDestination>(this TBase self, TDestination obj)
            where TDestination : class, new()
            where TBase : new()
        {
            foreach (var pS in self.GetType().GetProperties())
            {
                foreach (var pT in obj.GetType().GetProperties())
                {
                    if (pT.Name != pS.Name) continue;
                    (pT.GetSetMethod()).Invoke(obj, new[]
                        { pS.GetGetMethod().Invoke( self, null ) });
                }
            }
            return self;
        }

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

        private static List<T> ConvertArray<T>(Array input)
        {
            return input.Cast<T>().ToList(); // Using LINQ for simplicity
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

        /// <summary>
        /// Клонировать объект
        /// </summary>
        /// <param name="obj"></param>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        public static TDestination Clone<TDestination>(this TDestination obj) where TDestination : new()
        {
            var tDerived = new TDestination();
            foreach (var propDerived in typeof(TDestination).GetProperties())
            {
                var propBase = obj.GetType().GetProperty(propDerived.Name);
                if (propBase != null)
                {
                    propDerived.SetValue(tDerived, propBase.GetValue(obj, null), null);
                }
            }
            return tDerived;
        }
    }
}
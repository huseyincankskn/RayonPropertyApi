using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the value of a property in a object through relection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(this object source, string property)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var sourceType = source.GetType();
            var sourceProperties = sourceType.GetProperties();

            var propertyValue = (from s in sourceProperties
                                 where s.Name.Equals(property)
                                 select s.GetValue(source, null)).FirstOrDefault();

            return propertyValue != null ? (T)propertyValue : default;
        }

        /// <summary>
        /// Loads the custom attributes from the type
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="typeWithAttributes">The calling assembly to search.</param>
        /// <returns>The custom attribute of type T, if found.</returns>
        public static T GetAttribute<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            return GetAttributes<T>(typeWithAttributes).FirstOrDefault();
        }

        /// <summary>
        /// Loads the custom attributes from the type
        /// </summary>
        /// <typeparam name="T">The type of the custom attribute to find.</typeparam>
        /// <param name="typeWithAttributes">The calling assembly to search.</param>
        /// <returns>An enumeration of attributes of type T that were found.</returns>
        public static IEnumerable<T> GetAttributes<T>(this Type typeWithAttributes)
            where T : Attribute
        {
            // Try to find the configuration attribute for the default logger if it exists
            Attribute[] configAttributes = Attribute.GetCustomAttributes(typeWithAttributes,
                typeof(T), false);

            foreach (T attribute in configAttributes.OfType<T>())
            {
                yield return attribute;
            }
        }

        public static bool IsDerived<T>(this Type type)
        {
            Type baseType = typeof(T);

            if (baseType.FullName == type.FullName)
            {
                return true;
            }

            if (type.IsClass)
            {
                return baseType.IsClass
                    ? type.IsSubclassOf(baseType)
                    : baseType.IsInterface && IsImplemented(type, baseType);
            }
            else if (type.IsInterface && baseType.IsInterface)
            {
                return IsImplemented(type, baseType);
            }
            return false;
        }

        public static bool IsImplemented(Type type, Type baseType)
        {
            Type[] faces = type.GetInterfaces();
            foreach (Type face in faces)
            {
                if (baseType.Name.Equals(face.Name))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Return true if the type is a System.Nullable wrapper of a value type
        /// </summary>
        /// <param name="type">The type to check</param>
        /// <returns>True if the type is a System.Nullable wrapper</returns>
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType
                   && (type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        public static void SetProperties(this object target, List<string> properties, object source)
        {
            if (source != null)
            {
                var sourceProperties = source.GetType().GetProperties();
                foreach (string property in properties)
                {
                    var value = (from s in sourceProperties
                                 where s.Name.Equals(property)
                                 select s.GetValue(source, null)).FirstOrDefault();

                    target.GetType().GetProperty(property)?.SetValue(target, value, null);
                }
            }
        }

        public static void SetProperties(this object target, List<string> properties, params object[] values)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                var property = target.GetType().GetProperty(properties[i]);
                if (property != null)
                {
                    object value = values[i];
                    property.SetValue(target, value, null);
                }
            }
        }

        public static void SetSingleProperty(this object target, List<string> properties, params object[] values)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                var property = target.GetType().GetProperty(properties[i]);
                if (property != null)
                {
                    object value = values[i];
                    property.SetValue(target, value, null);
                }
            }
        }

        public static List<Type> BaseTypes(this Type type)
        {
            List<Type> baseTypes = new List<Type>();
            FillBaseClasses(type, ref baseTypes);

            return baseTypes;
        }

        private static void FillBaseClasses(Type item, ref List<Type> baseTypes)
        {
            if (item != null && item.BaseType != null && item.BaseType.Name != "Object")
            {
                baseTypes.Add(item.BaseType);
                baseTypes.AddRange(item.GetInterfaces());

                FillBaseClasses(item.BaseType, ref baseTypes);
            }
        }
    }
}
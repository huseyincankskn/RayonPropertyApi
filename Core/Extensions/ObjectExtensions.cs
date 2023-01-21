using System.Reflection;

namespace Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void TrimAllProps<T>(this T item)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in properties)
            {
                if (p.PropertyType != typeof(string) || !p.CanWrite || !p.CanRead) { continue; }
                var value = p.GetValue(item) as string;

                if (!string.IsNullOrWhiteSpace(value))
                    p.SetValue(item, value.Trim());

                var trimValue = p.GetValue(item) as string;
                if (string.IsNullOrWhiteSpace(trimValue))
                    p.SetValue(item, null);
            }
        }
    }
}
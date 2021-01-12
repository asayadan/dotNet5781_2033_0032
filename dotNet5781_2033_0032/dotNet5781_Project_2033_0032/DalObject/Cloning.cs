using System.Reflection;

namespace DL
{
    static class Cloning
    {
        internal static T Clone<T>(this T original) where T : new()
        {
            T copyToObject = new T();

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                propertyInfo.SetValue(copyToObject, propertyInfo.GetValue(original, null), null);

            return copyToObject;
        }
    }
}

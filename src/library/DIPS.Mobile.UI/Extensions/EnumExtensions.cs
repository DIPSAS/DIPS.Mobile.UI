namespace DIPS.Mobile.UI.Extensions
{
    public static class Enum
    {
        public static IEnumerable<T> ToList<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
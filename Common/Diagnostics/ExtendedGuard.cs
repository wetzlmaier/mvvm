namespace Common.Diagnostics
{
    public static class ExtendedGuard
    {
        public static void Contains<T>(ICollection<T> collection, T item, string name)
        {
            if (!collection.Contains(item))
                throw new ArgumentException("Collection does not contain '{name}'");
        }
        public static void NotContains<T>(ICollection<T> collection, T item, string name)
        {
            if (collection.Contains(item))
                throw new ArgumentException("Collection already contains '{name}'");
        }
    }
}

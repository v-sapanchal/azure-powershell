namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource.Extensions
{
    /// <summary>
    /// The object extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Casts the specified object to type T.
        /// </summary>
        /// <typeparam name="T">The type to cast to</typeparam>
        /// <param name="obj">The input object</param>
        public static T Cast<T>(this object obj)
        {
            return (T)obj;
        }

        /// <summary>
        /// Wraps the object in an array of length 1.
        /// </summary>
        /// <typeparam name="T">Type of object to wrap.</typeparam>
        /// <param name="obj">Object to wrap in array.</param>
        public static T[] AsArray<T>(this T obj)
        {
            return new T[] { obj };
        }
    }
}

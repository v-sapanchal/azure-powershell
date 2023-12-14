using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    /// <summary>
    /// The insensitive version of dictionary.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    [JsonPreserveCaseDictionary]
    public class InsensitiveDictionary<TValue> : Dictionary<string, TValue>
    {
        /// <summary>
        /// The empty dictionary.
        /// </summary>
        public static readonly InsensitiveDictionary<TValue> Empty = new InsensitiveDictionary<TValue>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InsensitiveDictionary{TValue}"/> class.
        /// </summary>
        public InsensitiveDictionary()
            : base(StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsensitiveDictionary{TValue}"/> class.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
        public InsensitiveDictionary(int capacity)
            : base(capacity, StringComparer.InvariantCultureIgnoreCase)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsensitiveDictionary{TValue}"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public InsensitiveDictionary(IDictionary<string, TValue> dictionary)
            : base(dictionary, StringComparer.InvariantCultureIgnoreCase)
        {
        }
    }
}

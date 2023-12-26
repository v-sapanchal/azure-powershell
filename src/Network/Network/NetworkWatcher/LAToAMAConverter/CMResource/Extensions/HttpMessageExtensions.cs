using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource.Extensions
{
    /// <summary>
    /// Class containing HTTP message extension methods.
    /// </summary>
    public static class HttpMessageExtensions
    {
        /// <summary>
        /// Reads the JSON content from the http response message.
        /// </summary>
        /// <typeparam name="T">The type of object contained in the JSON.</typeparam>
        /// <param name="message">The response message to be read.</param>
        /// <param name="rewindContentStream">Rewind content stream if set to true.</param>
        /// <returns>An object of type T instantiated from the response message's body.</returns>
        public static async Task<T> ReadContentAsJsonAsync<T>(this HttpResponseMessage message, bool rewindContentStream = false)
        {
            using (var stream = await message.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(continueOnCapturedContext: false))
            {
                var streamPosition = stream.Position;
                try
                {
                    return FromJson<T>(stream);
                }
                finally
                {
                    if (stream.CanSeek && streamPosition != stream.Position && rewindContentStream)
                    {
                        stream.Seek(streamPosition, SeekOrigin.Begin);
                    }
                }
            }
        }

        /// <summary>
        /// Deserialize object from a JSON stream.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="stream">A <see cref="Stream"/> that contains a JSON representation of object</param>
        public static T FromJson<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return JsonExtensions.JsonObjectTypeSerializer.Deserialize<T>(jsonReader);
            }
        }

        /// <summary>
        /// Reads the JSON content from the http response message.
        /// </summary>
        /// <param name="message">The response message to be read.</param>
        /// <param name="rewindContentStream">Rewind content stream if set to true.</param>
        /// <returns>An object of type T instantiated from the response message's body.</returns>
        public static async Task<string> ReadContentAsStringAsync(this HttpResponseMessage message, bool rewindContentStream = false)
        {
            using (var stream = await message.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(continueOnCapturedContext: false))
            using (var streamReader = new StreamReader(stream))
            {
                var streamPosition = stream.Position;
                try
                {

                    return streamReader.ReadToEnd();
                }
                finally
                {
                    if (stream.CanSeek && streamPosition != stream.Position && rewindContentStream)
                    {
                        stream.Seek(streamPosition, SeekOrigin.Begin);
                    }
                }
            }
        }
    }
}

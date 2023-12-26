using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Hyak.Common;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Globalization;
using System.Xml;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using Formatting = Newtonsoft.Json.Formatting;
using Microsoft.Azure.Commands.ResourceManager.Common;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter
{
    public class NetworkWatcherUtility
    {
        public static string GetSubscription(string resourceUri)
        {
            return GetResourceValue(resourceUri, "/subscriptions");
        }

        public static string GetResourceValue(string resourceUri, string resourceName)
        {
            if (string.IsNullOrEmpty(resourceUri))
            {
                return null;
            }

            if (!resourceName.StartsWith("/"))
            {
                resourceName = "/" + resourceName;
            }

            if (!resourceUri.StartsWith("/"))
            {
                resourceUri = "/" + resourceUri;
            }

            string text = "/resourceGroups" + resourceName;
            if (resourceUri.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                string text2 = resourceUri.ToLowerInvariant().Split(new string[1] { text.ToLowerInvariant() }, StringSplitOptions.None).Last();
                int num = text2.IndexOf(resourceName, StringComparison.InvariantCultureIgnoreCase);
                if (num != -1)
                {
                    return text2.Substring(num + resourceName.Length).Split('/')[1];
                }
            }

            int num2 = resourceUri.IndexOf(resourceName, StringComparison.InvariantCultureIgnoreCase);
            if (num2 != -1)
            {
                string[] array = resourceUri.Substring(num2 + resourceName.Length).Split('/');
                if (array.Length > 1)
                {
                    return array[1];
                }
            }

            return null;
        }
    }
}

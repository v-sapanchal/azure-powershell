using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.Network.NetworkWatcher.LAToAMAConverter.CMResource
{
    internal static class TagsHelper
    {
        internal static Hashtable GetTagsHashtable(InsensitiveDictionary<string> tags)
        {
            return tags == null
                ? null
                : TagsConversionHelper.CreateTagHashtable(tags);
        }
    }
}

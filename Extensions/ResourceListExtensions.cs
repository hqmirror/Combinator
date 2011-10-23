﻿using System.Collections.Generic;
using System.Linq;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;
using Piedone.Combinator.Services;

namespace Piedone.Combinator.Extensions
{
    [OrchardFeature("Piedone.Combinator")]
    internal static class ResourceListExtensions
    {
        public static int GetResourceListHashCode<T>(this IList<T> resources) where T: ResourceRequiredContext
        {
            var key = "";

            resources.ToList().ForEach(resource => key += resource.Resource.GetFullPath() + "__");

            return key.GetHashCode();
        }

        public static IList<T> SetLocation<T>(this IList<T> resources, ResourceLocation location) where T : ResourceRequiredContext
        {
            resources.ToList().ForEach(resource => resource.Settings.Location = location);
            return resources;
        }

        public static IList<T> DeleteCombinedResources<T>(this IList<T> resources) where T : ResourceRequiredContext
        {
            for (int i = 0; i < resources.Count; i++)
            {
                if (resources[i].Resource.GetFullPath().Contains(CacheFileService.cacheFolderName))
                {
                    resources.RemoveAt(i);
                    i--;
                }
            }
            return resources;
        }
    }
}
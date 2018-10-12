using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace restore.Configure
{
    public class RootLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            return new[] { "/Root/{1}/{0}.cshtml", "/Root/Shared/{0}.cshtml" };
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}

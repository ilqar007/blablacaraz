using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BlaBlaCarAz.Localization
{
    public class LocService
    {
        private readonly IStringLocalizer<SharedResource> _localizer;

        public LocService(IStringLocalizer<SharedResource> localizer)
        {
            //var type = typeof(SharedResource);
            //var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            //_localizer = factory.Create("SharedResource", assemblyName.Name);
            _localizer = localizer;
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }
    }
}

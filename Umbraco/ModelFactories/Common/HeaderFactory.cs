using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Website.Models.Common;
using Header = Website.Models.Common.Header;

namespace Website.ModelFactories.Common
{
    public class HeaderFactory
    {
        public static Header Build(IPublishedContent content)
        {
            return new Header()
            {
                
            };
        }
    }
}
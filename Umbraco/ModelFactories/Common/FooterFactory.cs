using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Website.Models.Common;
using Footer = Website.Models.Common.Footer;

namespace Website.ModelFactories.Common
{
    public class FooterFactory
    {
        public static Footer Build(IPublishedContent content)
        {
            return new Footer()
            {
                
            };
        }
    }
}
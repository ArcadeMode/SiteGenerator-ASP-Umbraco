using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Website.Models.Aboutpage;
using CTA = Website.Models.Aboutpage.CTA;

namespace Website.ModelFactories.Aboutpage
{
    public class CTAFactory
    {
        public static CTA Build(IPublishedContent content)
        {
            return new CTA()
            {
                
            };
        }
    }
}
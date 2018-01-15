using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Website.Models.Contactpage;
using Content = Website.Models.Contactpage.Content;

namespace Website.ModelFactories.Contactpage
{
    public class ContentFactory
    {
        public static Content Build(IPublishedContent content)
        {
            return new Content()
            {
                
            };
        }
    }
}
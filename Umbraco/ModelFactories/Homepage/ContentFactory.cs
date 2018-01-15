using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Website.Models.Homepage;
using Content = Website.Models.Homepage.Content;

namespace Website.ModelFactories.Homepage
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
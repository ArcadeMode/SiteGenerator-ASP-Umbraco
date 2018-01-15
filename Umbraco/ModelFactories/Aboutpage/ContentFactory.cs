using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using SiteGenerator.Interfaces;
using Content = Website.Models.Aboutpage.Content;

namespace Website.ModelFactories.Aboutpage
{
    public class ContentFactory : IModelFactory
    {
        public static Content Build(object ipublishedcontent)
        {
            IPublishedContent content = (IPublishedContent)ipublishedcontent;
            return new Content()
            {
                
            };
        }
    }
}
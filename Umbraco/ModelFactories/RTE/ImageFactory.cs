using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Website.Models.RTE;
using Image = Website.Models.RTE.Image;

namespace Website.ModelFactories.RTE
{
    public class ImageFactory
    {
        public static Image Build(IPublishedContent content)
        {
            return new Image()
            {
                
            };
        }
    }
}
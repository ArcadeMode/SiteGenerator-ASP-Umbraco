using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.Templates
{
    class ModelFactory
    {

        public const string Template =
@"using System;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using {model_name} = {model_fullname};

namespace {modelfactory_namespace}
{
    public class {modelfactory_name}
    {
        public static {model_name} Build(IPublishedContent content)
        {
            return new {model_name}()
            {
                
            };
        }
    }
}";

        public string Namespace;
        public string Name;
        public string FullName;
        public string Path;

        public ModelFactory(string targetNamespace, string partialGroup, string partialAlias, string baseFSPath)
        {
            Namespace = string.Format("{0}.ModelFactories.{1}", targetNamespace, partialGroup);
            Name = string.Format("{0}Factory", partialAlias);
            FullName = string.Format("{0}.{1}", Namespace, Name);
            Path = string.Format("{0}\\ModelFactories\\{1}\\{2}.cs", baseFSPath, partialGroup, Name);
        }
    }
}

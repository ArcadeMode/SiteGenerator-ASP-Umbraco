using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.Templates
{
    class Model
    {
        public const string Template =
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace {model_namespace}
{
    public class {model_name}
    {
        
    }
}";

        public string Namespace;
        public string Name;
        public string FullName;
        public string Path;

        public Model(string targetNamespace, string partialGroup, string partialAlias, string baseFSPath)
        {
            Namespace = string.Format("{0}.Models.{1}", targetNamespace, partialGroup);
            Name = partialAlias;
            FullName = string.Format("{0}.{1}", Namespace, partialAlias);
            Path = string.Format("{0}\\Models\\{1}\\{2}.cs", baseFSPath, partialGroup, partialAlias);
        }
    }
}

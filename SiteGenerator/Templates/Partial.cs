using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.Templates
{
    class Partial
    {
        public const string Template =
@"@model {model_fullname}
<!-- generated code -->
<div>{model_fullname}</div>
";

        public string Name;
        public string Path;

        public Partial(string partialGroup, string partialAlias, string baseFSPath)
        {
            Name = partialAlias;
            Path = string.Format("{0}\\Views\\Partials\\{1}\\{2}.cshtml", baseFSPath, partialGroup, partialAlias);
        }
    }
}

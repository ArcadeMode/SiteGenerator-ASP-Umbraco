using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator.Templates
{
    class MacroPartial
    {
        public const string Template =
@"@using {constants_namespace};
@using {modelfactory_namespace};
@inherits Umbraco.Web.Macros.PartialViewMacroPage
@Html.Partial(Macros.{macropartial_name}.PartialPath, {modelfactory_name}.Build(Model.Content))
";

        public string Path;
        public string Name;

        public MacroPartial(string partialGroup, string partialAlias, string baseFSPath)
        {
            Name = partialGroup + partialAlias;
            Path = string.Format("{0}\\Views\\MacroPartials\\{1}\\{2}.cshtml", baseFSPath, partialGroup, partialAlias);
        }
    }
}

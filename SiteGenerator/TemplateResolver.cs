using SiteGenerator.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteGenerator.Templates.Constants;
using System.Xml;

namespace SiteGenerator
{
    class TemplateResolver
    {
        public static string ResolveTemplate(Model model)
        {
            return Model.Template
                .Replace("{model_namespace}", model.Namespace)
                .Replace("{model_name}", model.Name);
        }

        public static string ResolveTemplate(ModelFactory modelFactory, Model model)
        {
            return ModelFactory.Template
                .Replace("{modelfactory_namespace}", modelFactory.Namespace)
                .Replace("{modelfactory_name}", modelFactory.Name)
                .Replace("{model_namespace}", model.Namespace)
                .Replace("{model_name}", model.Name)
                .Replace("{model_fullname}", model.FullName);
        }

        public static string ResolveTemplate(Partial partial, Model model)
        {
            return Partial.Template
                .Replace("{model_fullname}", model.FullName);
        }

        public static string ResolveTemplate(MacroPartial macroPartial, Macros macros, ModelFactory modelFactory)
        {
            return MacroPartial.Template
                .Replace("{constants_namespace}", macros.Namespace)
                .Replace("{modelfactory_namespace}", modelFactory.Namespace)
                .Replace("{macropartial_name}", macroPartial.Name)
                .Replace("{modelfactory_name}", modelFactory.Name);
        }

        public static string ResolveTemplate(Template template, Macros macros, XmlNode xmltemplate)
        {
            StringBuilder sb = new StringBuilder();
            foreach(XmlNode partialRef in xmltemplate.ChildNodes)
            {
                sb.AppendLine(Template.PartialLineTemplate.Replace("{partial_name}", partialRef.Attributes["alias"]?.Value));
            }
            return Template._Template
                .Replace("{constants_namespace}", macros.Namespace)
                .Replace("{partials}", sb.ToString());
        }

        public static string ResolveTemplate(Macros macros, XmlNode xmlpartials)
        {
            StringBuilder sb = new StringBuilder();
            foreach(XmlNode partial in xmlpartials.ChildNodes)
            {
                string partialAlias = partial.Attributes["alias"]?.Value;
                string partialGroup = partial.Attributes["group"]?.Value;

                string umbracoAlias = partialGroup + partialAlias;

                string macroDefinition = Macros.MacroDefinitionTemplate
                    .Replace("{partial_alias}", umbracoAlias)
                    .Replace("{partial_path}", string.Format("~/Views/Partials/{0}/{1}.cshtml", partialGroup, partialAlias));
                sb.AppendLine(macroDefinition);
            }
            return Macros.Template
                .Replace("{constants_namespace}", macros.Namespace)
                .Replace("{macro_definitions}", sb.ToString());
        }

    }
}

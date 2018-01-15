using SiteGenerator.Templates;
using SiteGenerator.Templates.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SiteGenerator
{
    public class Runner
    {
        public static void GenerateBoilerplate(string baseFSPath, string targetNamespace, XmlDocument structure)
        {
            XmlNode partialsNode = structure.DocumentElement.SelectSingleNode("/website/partials");

            Macros macros = new Macros(targetNamespace, baseFSPath);
            FileGenerator.GenerateFile(macros.Path, TemplateResolver.ResolveTemplate(macros, partialsNode), false);
            
            foreach(XmlNode partialNode in partialsNode.ChildNodes)
            {
                string partialAlias = partialNode.Attributes["alias"]?.Value;
                string partialGroup = partialNode.Attributes["group"]?.Value;

                Model model = new Model(targetNamespace, partialGroup, partialAlias, baseFSPath);
                FileGenerator.GenerateFile(model.Path, TemplateResolver.ResolveTemplate(model), true);

                ModelFactory modelFactory = new ModelFactory(targetNamespace, partialGroup, partialAlias, baseFSPath);
                FileGenerator.GenerateFile(modelFactory.Path, TemplateResolver.ResolveTemplate(modelFactory, model), true);

                Partial partial = new Partial(partialGroup, partialAlias, baseFSPath);
                FileGenerator.GenerateFile(partial.Path, TemplateResolver.ResolveTemplate(partial, model), true);

                MacroPartial macroPartial = new MacroPartial(partialGroup, partialAlias, baseFSPath);
                FileGenerator.GenerateFile(macroPartial.Path, TemplateResolver.ResolveTemplate(macroPartial, macros, modelFactory), true);
            }

            XmlNode templatesNode = structure.DocumentElement.SelectSingleNode("/website/templates");
            foreach(XmlNode templateNode in templatesNode.ChildNodes)
            {
                Template template = new Template(targetNamespace, templateNode.Attributes["alias"]?.Value, baseFSPath);
                FileGenerator.GenerateFile(template.Path, TemplateResolver.ResolveTemplate(template, macros, templateNode), false);
            }
        }
    }
}

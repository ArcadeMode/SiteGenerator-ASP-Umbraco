using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Umbraco.Web;
using umbraco;
using File = System.IO.File;
using System.Xml;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Archetype.Models;
using Newtonsoft.Json;
using ITemplate = Umbraco.Core.Models.ITemplate;
using System.Reflection;

namespace Website.Handlers
{
    public class SiteGeneratorHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            XmlDocument structure = LoadXml();
            
            CreateDocumentTypes(structure.SelectSingleNode("/website/templates"));
            CreateMacros(structure.SelectSingleNode("/website/partials"));

            SiteGenerator.Runner.GenerateBoilerplate(GetRootFolder(), Assembly.GetExecutingAssembly().GetName().Name, structure);
            context.Response.Write(GetSiteGeneratorFolder());
        }

        private void CreateMacros(XmlNode partials)
        {
            var mService = ApplicationContext.Current.Services.MacroService;
            foreach(XmlNode partial in partials.ChildNodes)
            {
                string macroName = partial.Attributes["name"]?.Value;
                string macroGroup = partial.Attributes["group"]?.Value;
                string macroAlias = partial.Attributes["alias"]?.Value;

                string umbracoAlias = macroGroup + macroAlias;

                bool allowInRTE = partial.Attributes["allowInRTE"]?.Value == "1";
                IMacro macro = mService.GetByAlias(umbracoAlias) != null ? mService.GetByAlias(umbracoAlias) : new Macro(); //get existing or create new
                macro.Alias = umbracoAlias;
                macro.Name = macroName;
                macro.UseInEditor = allowInRTE;
                macro.ScriptPath = string.Format("~/Views/MacroPartials/{0}/{1}.cshtml", macroGroup, macroAlias);
                mService.Save(macro);
            }
        }

        private void CreateDocumentTypes(XmlNode templates)
        {
            var ctService = ApplicationContext.Current.Services.ContentTypeService;
            var fService = ApplicationContext.Current.Services.FileService;
            ITemplate baseLayout = fService.GetTemplate("baseLayout");

            foreach (XmlNode template in templates.ChildNodes)
            {
                string templateName = template.Attributes["name"]?.Value;
                string templateAlias = template.Attributes["alias"]?.Value;
                bool allowAsRoot = template.Attributes["allowAsRoot"]?.Value == "1";
                
                ITemplate umbracoTemplate = fService.GetTemplate(templateAlias) != null 
                    ? fService.GetTemplate(templateAlias)
                    : fService.CreateTemplateWithIdentity(templateAlias, string.Empty, baseLayout);

                ContentType current = (ContentType)ctService.GetAllContentTypes().FirstOrDefault(x => x.Alias == templateAlias);
                ContentType ct = current != null ? current : new ContentType(-1);
                ct.Name = templateName;
                ct.Alias = templateAlias;
                ct.AllowedTemplates = umbracoTemplate.AsEnumerableOfOne();
                ct.AllowedAsRoot = allowAsRoot;
                ct.AllowedContentTypes = parseAllowedContentTypes(template.Attributes["children"]?.Value);
                //ct.DefaultTemplate = ct.AllowedTemplates.First();
                ctService.Save(ct);
                
            }
        }

        private IEnumerable<ContentTypeSort> parseAllowedContentTypes(string attributeValue)
        {
            if(attributeValue != null)
            {
                var ctService = ApplicationContext.Current.Services.ContentTypeService;
                IEnumerable<string> aliasses = attributeValue.Split(',').AsEnumerable<string>();

                return aliasses.Select(x =>
                {
                    var allowedCT = ctService.GetAllContentTypes().First(ct => ct.Alias == x);
                    return new ContentTypeSort(allowedCT.Id, aliasses.IndexOf(x));
                });
            }
            return Enumerable.Empty<ContentTypeSort>();
        }

        private static XmlDocument LoadXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            try { doc.Load(string.Format("{0}\\structure.xml", GetSiteGeneratorFolder())); }
            catch (System.IO.FileNotFoundException)
            {
                throw;
            }
            return doc;
        }

        private static string GetSiteGeneratorFolder()
        {
            return HttpContext.Current.Server.MapPath("/SiteGenerator");
        }

        private static string GetRootFolder()
        {
            return HttpContext.Current.Server.MapPath(null);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
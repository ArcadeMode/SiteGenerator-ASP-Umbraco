using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteGenerator
{
    class FileGenerator
    {
        public static void GenerateFile(string path, string content, bool skipIfExists)
        {
            string dirPath = path.Remove(path.LastIndexOf("\\"), (path.Length - path.LastIndexOf("\\")));
            if ((!skipIfExists && System.IO.File.Exists(path)) || !System.IO.File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(dirPath);
                System.IO.File.WriteAllBytes(path, Encoding.ASCII.GetBytes(content));
            }
        }
    }
}

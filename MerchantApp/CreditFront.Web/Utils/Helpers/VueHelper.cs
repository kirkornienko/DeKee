using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CreditFront.Web.Utils.Helpers
{
    public class VueHelper
    {
        public static IHtmlString LoadFile(string path)
        {
            var content = new HtmlString("");
            if(!string.IsNullOrEmpty(path))
            {
                string filepath = System.Web.Hosting.HostingEnvironment.MapPath(path);
                if(File.Exists(filepath))
                {
                    var fileContent = File.ReadAllText(filepath);
                    if(!string.IsNullOrWhiteSpace(fileContent))
                    {
                        content = new HtmlString(fileContent);
                    }
                }
            }
            return content;
        }
    }
}

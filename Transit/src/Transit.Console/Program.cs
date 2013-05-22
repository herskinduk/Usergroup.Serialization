using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var deserializerUtil = new ItemUtil(new SyncItemDeserializer());

            var items = deserializerUtil.Deserialize(@"C:\inetpub\wwwroot\Usergroup\Website\App_Data\serialization\master\sitecore");

            var templates = deserializerUtil.BuildTemplates(items);

            //"".StartsWith("", StringComparison.OrdinalIgnoreCase )
        
        }
    }
}

using Sitecore.Data;
using Sitecore.Data.Serialization;
using Sitecore.Data.Serialization.ObjectModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Transit
{
    public class ItemUtil
    {
        protected readonly IItemDeserializer _deserializer;

        public ItemUtil(IItemDeserializer deserializer)
        {
            _deserializer = deserializer;
        }

        public IList<IItem> Deserialize(string path)
        {
            List<IItem> items = new List<IItem>();

            LoadSyncItemTree(path, true, items);

            return items;
        }

        public IEnumerable<Template> BuildTemplates(IList<IItem> items)
        {
            return items
                .Where(i => new ID(i.TemplateID) == Sitecore.TemplateIDs.Template)
                .Select(i => new Template(i, items))
                .ToList();

        }

        public string ClassName(string name)
        {
            return TitleCase(name);
        }

        public string PrivateMemberName(string name)
        {
            return "_m" + TitleCase(name);
        }


        public string MemberName(string name, string className)
        {
            if (TitleCase(name) == className)
            {
                return TitleCase(name) + "X";
            }
            else
            {
                return TitleCase(name);
            }
            
        }

        public string InterfaceName(string name)
        {
            return "I" + TitleCase(name);
        }

        public string TitleCase(string name)
        {
            name = Regex.Replace(name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            name = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name);
            name = Regex.Replace(name, @"[^a-zA-Z0-9]", String.Empty);
            name = Regex.Replace(name, @"(^[0-9])", "Z$1");

            return name;
        }

        public string RelativeNamespace(Template template, int startLevel)
        {
            var sb = new StringBuilder();
            var pathList = new List<string>(template.Path.Split('/'));

            try
            {
                return string.Join(".", pathList.Take(pathList.Count - 1).Skip(startLevel).Select(p => TitleCase(p)));
            }
            catch
            {
            }

            return "";
        }

        protected void LoadSyncItemTree(string path, bool recursive, List<IItem> items)
        {
            LoadSyncItemTreeLevel(path, recursive, items, false);
        }

        protected void LoadSyncItemTreeLevel(string path, bool recursive, List<IItem> items, bool includeLevel)
        {
            //System.IO.Directory.GetCurrentDirectory();

            var files = includeLevel ? System.IO.Directory.GetFiles(path) : new string[] {path + ".item"};

            foreach (var file in files)
            {
                if (file.EndsWith(".item"))
                {
                    var item = _deserializer.Deserialize(file);
                    if (item != null)
                    {
                        items.Add(item);

                        if (recursive)
                        {
                            var folderName = file.Substring(0, file.Length - 5);

                            if (System.IO.Directory.Exists(folderName) && !CommonUtils.IsDirectoryHidden(folderName))
                            {
                                LoadSyncItemTreeLevel(folderName, recursive, items, true);
                            }
                        }
                    }
                }
            }
        }
    }
}

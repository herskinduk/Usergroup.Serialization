using Sitecore.Data;
using Sitecore.Data.Serialization.ObjectModel;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit
{
    public class Template
    {
        private readonly IItem _item;
        private readonly IList<IItem> _items;
        private IList<IItem> _inheritedTemplates;
        private IList<TemplateField> _combinedFields;
        private IList<TemplateField> _localFields;

        public Template(IItem item, IList<IItem> items)
        {
            Assert.IsTrue(new ID(item.TemplateID).Equals(Sitecore.TemplateIDs.Template), "item must be template");

            _item = item;
            _items = items;
        }

        public string Path
        {
            get
            {
                return _item.ItemPath;
            }
        }

        public string Name
        {
            get
            {
                return _item.Name;
            }
        }
        public string Id
        {
            get
            {
                return _item.ID;
            }
        }
        public string ParentId
        {
            get
            {
                return _item.ParentID;
            }
        }

        IEnumerable<IItem> CombinedTemplateItems
        {
            get
            {
                if (_inheritedTemplates == null)
                {
                    _inheritedTemplates = new List<IItem>();
                    GetBaseTemplates(_item, _inheritedTemplates);
                }

                return _inheritedTemplates;
            }
        }

        public IEnumerable<TemplateField> CombinedFields
        {
            get
            {
                if (_combinedFields == null)
                {
                    var sections = _items.Where(template => CombinedTemplateItems.Select(i => i.ID).Contains(template.ParentID)).ToList();
                    var fields = _items.Where(field => sections.Select(i=> i.ID).Contains(field.ParentID)).ToList();
                    _combinedFields = fields.Select(syncField => new TemplateField(syncField)).OrderBy(i => i.Name).ToList();
                }
                return _combinedFields;
            }
        }

        public IEnumerable<TemplateField> LocalFields
        {
            get
            {
                if (_localFields == null)
                {
                    var sections = _items.Where(template => template.ParentID == _item.ID).ToList();
                    var fields = _items.Where(field => sections.Select(i => i.ID).Contains(field.ParentID)).ToList();
                    _localFields = fields.Select(syncField => new TemplateField(syncField)).OrderBy(i => i.Name).ToList();
                }
                return _localFields;
            }
        }

        private void GetBaseTemplates(IItem item, IList<IItem> inheritedTemplates)
        {
            if (item != null && !inheritedTemplates.Where(i => i.ID == item.ID).Any())
            {
                inheritedTemplates.Add(item);
                var baseField = item.SharedFields.Where(i => new ID(i.FieldID) == Sitecore.FieldIDs.BaseTemplate).FirstOrDefault();
                if (baseField != null)
                {
                    foreach (var value in baseField.FieldValue.Split('|'))
                    {
                        var baseItem = _items.Where(i => i.ID == value).FirstOrDefault();
                        GetBaseTemplates(baseItem, inheritedTemplates);
                    }
                }
            }
        }
    }
}

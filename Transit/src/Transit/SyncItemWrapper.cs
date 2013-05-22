using Sitecore.Data.Serialization.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit
{
    public class SyncItemWrapper : IItem
    {
        protected readonly SyncItem _item;
        public SyncItemWrapper(SyncItem item)
        {
            _item = item;
        }



        public string BranchId
        {
            get { return _item.BranchId; }
        }

        public string DatabaseName
        {
            get { return _item.DatabaseName; }
        }

        public string ID
        {
            get { return _item.ID; }
        }

        public string ItemPath
        {
            get { return _item.ItemPath; }
        }

        public string MasterID
        {
            get { return _item.MasterID; }
        }

        public string Name
        {
            get { return _item.Name; }
        }

        public string ParentID
        {
            get { return _item.ParentID; }
        }

        public IList<IField> SharedFields
        {
            get { return _item.SharedFields.Select(f => new SyncFieldWrapper(f)).ToList<IField>(); }
        }

        public string TemplateID
        {
            get { return _item.TemplateID; }
        }

        public string TemplateName
        {
            get { return _item.TemplateName; }
        }
    }
}

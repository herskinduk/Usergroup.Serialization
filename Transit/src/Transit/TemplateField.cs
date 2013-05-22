using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transit
{
    public class TemplateField
    {
        private IItem fieldItem;

        public TemplateField(IItem fieldItem)
        {
            // TODO: Complete member initialization
            this.fieldItem = fieldItem;
        }

        public string FieldID
        {
            get
            {
                return fieldItem.ID;
            }
        }

        public string TypeName
        {
            get
            {
                return fieldItem.SharedFields
                    .Where(f => f.FieldKey == "type")
                    .Select(i => i.FieldValue)
                    .FirstOrDefault();
            }
        }

        public string TypeKey
        {
            get
            {
                return TypeName.ToLower();
            }
        }

        public string Name
        {
            get
            {
                return fieldItem.Name;
            }
        }

        public string Key
        {
            get
            {
                return Name.ToLower();
            }
        }
    }
}

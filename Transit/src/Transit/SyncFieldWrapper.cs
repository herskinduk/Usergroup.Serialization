using Sitecore.Data.Serialization.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit
{
    public class SyncFieldWrapper : IField
    {
        protected readonly SyncField _field;

        public SyncFieldWrapper(SyncField field)
        {
            _field = field;
        }

        public string FieldID
        {
            get
            {
                return _field.FieldID; 
            }
        }
        public string FieldKey
        {
            get
            {
                return _field.FieldKey;
            }
        }
        public string FieldName
        {
            get
            {
                return _field.FieldName;
            }
        }
        public string FieldValue
        {
            get
            {
                return _field.FieldValue;
            }
        }
    }
}

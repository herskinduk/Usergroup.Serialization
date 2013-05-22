using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit
{
    public interface IField
    {
       // string TemplateID { get;  }
        string FieldID { get;  }
        string FieldKey { get;  }
        string FieldName { get;  }
        string FieldValue { get;  }

    }
}

using Sitecore.Data.Serialization.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transit
{
    public interface IItem
    {
         string BranchId { get;  }
         string DatabaseName { get;  }
         string ID { get;  }
         string ItemPath { get;  }
         string MasterID { get;  }
         string Name { get;  }
         string ParentID { get;  }
         IList<IField> SharedFields { get; }
         string TemplateID { get;  }
         string TemplateName { get;  }
        // IList<SyncVersion> Versions { get; }
    }
}

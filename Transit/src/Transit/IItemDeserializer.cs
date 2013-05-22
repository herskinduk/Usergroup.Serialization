using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transit
{
    public interface IItemDeserializer
    {
        IItem Deserialize(string path);
    }
}

using Sitecore.Data.Serialization.ObjectModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transit
{
    public class SyncItemDeserializer : IItemDeserializer
    {


        protected SyncItem LoadSyncItem(string path)
        {
            using (TextReader reader = (TextReader)new StreamReader((Stream)File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                return SyncItem.ReadItem(new Tokenizer(reader));
            }
        }

        public IItem Deserialize(string path)
        {
            return new SyncItemWrapper(LoadSyncItem(path));
        }
    }
}

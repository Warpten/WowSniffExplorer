using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IObjectData : IUpdateFieldStorage
    {
        public IUpdateField GUID { get; }
        public IUpdateField Data { get; }
        public IUpdateField<ObjectType> Type { get; }
        public IUpdateField<uint> Entry { get; }
        public IUpdateField Scale { get; }
        public IUpdateField Padding { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IGameObjectData : IUpdateFieldStorage
    {
        public GuidUpdateField CreatedBy { get; }
        public IUpdateField DisplayID { get; }
        public IUpdateField Flags { get; }
        public IUpdateField[] ParentRotation { get; }
        public IUpdateField Dynamic { get; }
        public IUpdateField Faction { get; }
        public IUpdateField Level { get; }
        public IUpdateField Bytes { get; }
    }
}

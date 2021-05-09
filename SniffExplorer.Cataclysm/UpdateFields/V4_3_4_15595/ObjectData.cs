using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IObjectData))]
    public enum ObjectData
    {
        [DescriptorValue(typeof(IObjectGUID))]
        GUID,
        
        [DescriptorValue(typeof(IObjectGUID))]
        Data,
        
        [DescriptorValue(typeof(ObjectType))]
        Type,
        
        [DescriptorValue(typeof(uint))]
        Entry,
        
        [DescriptorValue(typeof(float))]
        Scale,
        
        [DescriptorValue(typeof(uint))]
        Padding
    }
}

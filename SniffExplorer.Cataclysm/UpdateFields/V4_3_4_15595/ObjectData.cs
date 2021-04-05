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
        [DescriptorValue(ValueType = typeof(IObjectGUID))] GUID,
        [DescriptorValue(ValueType = typeof(IObjectGUID))] Data,
        [DescriptorValue(ValueType = typeof(ObjectType))]  Type,
        [DescriptorValue(ValueType = typeof(uint))]        Entry,
        [DescriptorValue(ValueType = typeof(float))]       Scale,
        [DescriptorValue(ValueType = typeof(uint))]        Padding
    }
}

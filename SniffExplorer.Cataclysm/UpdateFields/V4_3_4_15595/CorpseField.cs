using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(ICorpseData))]
    public enum CorpseField
    {
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      Owner,
        [DescriptorValue(ValueType = typeof(IObjectGUID))]      Party,
        [DescriptorValue(ValueType = typeof(uint))]             DisplayId,
        [DescriptorValue(ValueType = typeof(uint), Arity = 19)] Items,
        [DescriptorValue(ValueType = typeof(uint))]             Bytes1,
        [DescriptorValue(ValueType = typeof(uint))]             Bytes2,
        [DescriptorValue(ValueType = typeof(uint))]             Flags,
        [DescriptorValue(ValueType = typeof(uint))]             DynamicFlags
    }
}
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IDynamicObjectData))]
    public enum DynamicObjectData
    {
        [DescriptorValue(ValueType = typeof(IObjectGUID))] Caster,
        [DescriptorValue(ValueType = typeof(int))]         Bytes,
        [DescriptorValue(ValueType = typeof(int))]         SpellID,
        [DescriptorValue(ValueType = typeof(float))]       Radius,
        [DescriptorValue(ValueType = typeof(int))]         CastTime
    }
}
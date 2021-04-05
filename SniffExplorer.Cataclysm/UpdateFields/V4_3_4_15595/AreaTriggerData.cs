using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IAreaTriggerData))]
    public enum AreaTriggerData
    {
        [DescriptorValue(ValueType = typeof(uint))]             SpellID,
        [DescriptorValue(ValueType = typeof(uint))]             SpellVisualID,
        [DescriptorValue(ValueType = typeof(uint))]             Duration,
        [DescriptorValue(ValueType = typeof(float), Arity = 3)] FinalPosition
    }
}
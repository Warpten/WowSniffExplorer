using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IAreaTriggerData))]
    public enum AreaTriggerData
    {
        [DescriptorValue(typeof(uint))]
        SpellID,
        
        [DescriptorValue(typeof(uint))]
        SpellVisualID,
        
        [DescriptorValue(typeof(uint))]
        Duration,
        
        [DescriptorValue(typeof(float), 3)]
        FinalPosition
    }
}
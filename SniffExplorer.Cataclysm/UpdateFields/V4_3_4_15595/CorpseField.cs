using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(ICorpseData))]
    public enum CorpseField
    {
        [DescriptorValue(typeof(IObjectGUID))]
        Owner,
        
        [DescriptorValue(typeof(IObjectGUID))]
        Party,
        
        [DescriptorValue(typeof(uint))]
        DisplayId,
        
        [DescriptorValue(typeof(uint), 19)]
        Items,
        
        [DescriptorValue(typeof(uint))]
        Bytes1,
        
        [DescriptorValue(typeof(uint))]
        Bytes2,
        
        [DescriptorValue(typeof(uint))]
        Flags,
        
        [DescriptorValue(typeof(uint))]
        DynamicFlags
    }
}
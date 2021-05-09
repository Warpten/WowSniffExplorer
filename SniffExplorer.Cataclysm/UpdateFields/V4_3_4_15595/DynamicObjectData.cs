using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IDynamicObjectData))]
    public enum DynamicObjectData
    {
        [DescriptorValue(typeof(IObjectGUID))]
        Caster,
        
        [DescriptorValue(typeof(int))]
        Bytes,
        
        [DescriptorValue(typeof(int))]
        SpellID,
        
        [DescriptorValue(typeof(float))]
        Radius,
        
        [DescriptorValue(typeof(int))]
        CastTime
    }
}
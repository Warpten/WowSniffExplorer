using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IContainerData))]
    public enum ContainerData
    {
        [DescriptorValue(typeof(uint))]
        NumSlots,
        
        [DescriptorValue(typeof(uint))]
        AlignPad,
        
        [DescriptorValue(typeof(IObjectGUID),  36)]
        Slots
    }
}
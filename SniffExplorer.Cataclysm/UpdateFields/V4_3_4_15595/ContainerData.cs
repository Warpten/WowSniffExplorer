using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IContainerData))]
    public enum ContainerData
    {
        [DescriptorValue(ValueType = typeof(uint))]                    NumSlots,
        [DescriptorValue(ValueType = typeof(uint))]                    AlignPad,
        [DescriptorValue(ValueType = typeof(IObjectGUID), Arity = 36)] Slots
    }
}
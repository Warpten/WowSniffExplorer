using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IGameObjectData))]
    public enum GameObjectData
    {
        [DescriptorValue(ValueType = typeof(IObjectGUID))] CreatedBy,
        [DescriptorValue(ValueType = typeof(int))]         DisplayID,
        [DescriptorValue(ValueType = typeof(uint))]        Flags,
        [DescriptorValue(ValueType = typeof(float), Arity = 4)] ParentRotation,
        [DescriptorValue(ValueType = typeof(ushort[]))]    Dynamic,
        [DescriptorValue(ValueType = typeof(int))]         Faction,
        [DescriptorValue(ValueType = typeof(int))]         Level,
        [DescriptorValue(ValueType = typeof(byte[]))]      Bytes
    }
}
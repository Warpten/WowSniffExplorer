using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IGameObjectData))]
    public enum GameObjectData
    {
        [DescriptorValue(typeof(IObjectGUID))]
        CreatedBy,
        
        [DescriptorValue(typeof(int))]
        DisplayID,
        
        [DescriptorValue(typeof(uint))]
        Flags,
        
        [DescriptorValue(typeof(float), 4)]
        ParentRotation,
        
        [DescriptorValue(typeof(ushort[]))]
        Dynamic,
        
        [DescriptorValue(typeof(int))]
        Faction,
        
        [DescriptorValue(typeof(int))]
        Level,
        
        [DescriptorValue(typeof(byte[]))]
        Bytes
    }
}
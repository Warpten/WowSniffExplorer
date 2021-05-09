using SniffExplorer.Cataclysm.UpdateFields.Types;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595
{
    [Descriptor(ClientBuild = 15595, RealmType = RealmExpansionType.Retail, InterfaceType = typeof(IPlayerData))]
    public enum PlayerField
    {
        [DescriptorValue(typeof(IObjectGUID))]
        DuelArbiter,
        
        [DescriptorValue(typeof(uint))]
        FLAGS,
        
        [DescriptorValue(typeof(uint))]
        GuildRank,
        
        [DescriptorValue(typeof(uint))]
        GuildDeleteDate,
        
        [DescriptorValue(typeof(uint))]
        GuildLevel,
        
        [DescriptorValue(typeof(byte[]))]
        Bytes0,
        
        [DescriptorValue(typeof(byte[]))]
        Bytes1,
        
        [DescriptorValue(typeof(byte[]))]
        Bytes2,
        
        [DescriptorValue(typeof(uint))]
        DuelTeam,
        
        [DescriptorValue(typeof(uint))]
        GuildTimestamp,
        
        [DescriptorValue(typeof(QuestData), 50)]
        QuestLog,
        
        [DescriptorValue(typeof(VisibleItem), 19)]
        VisibleItems,
        
        [DescriptorValue(typeof(uint))]
        ChosenTitle,
        
        [DescriptorValue(typeof(uint))]
        FakeInebriation,
        
        [DescriptorValue(typeof(uint))]
        _ // Padding
    }
}
using SniffExplorer.Cataclysm.Attributes;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Events;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    public enum ChatMessageType : uint
    {
        System               = 0,
        Say                  = 1,
        Party                = 2,
        Raid                 = 3,
        Guild                = 4,
        Officer              = 5,
        Yell                 = 6,
        Whisper              = 7,
        WhisperForeign       = 8,
        WhisperInform        = 9,
        Emote                = 10,
        TextEmote            = 11,
        MonsterSay           = 12,
        MonsterParty         = 13,
        MonsterYell          = 14,
        MonsterWhisper       = 15,
        MonsterEmote         = 16,
        Channel              = 17,
        ChannelJoin          = 18,
        ChannelLeave         = 19,
        ChannelList          = 20,
        ChannelNotice        = 21,
        ChannelNoticeUser    = 22,
        Afk                  = 23,
        Dnd                  = 24,
        Ignored              = 25,
        Skill                = 26,
        Loot                 = 27,
        Money                = 28,
        Opening              = 29,
        Tradeskills          = 30,
        PetInfo              = 31,
        CombatMiscInfo       = 32,
        CombatXPGain         = 33,
        CombatHonorGain      = 34,
        CombatFactionChange  = 35,
        BattlegroundNeutral  = 36,
        BattlegroundAlliance = 37,
        BattlegroundHorde    = 38,
        RaidLeader           = 39,
        RaidWarning          = 40,
        RaidBossEmote        = 41,
        RaidBossWhisper      = 42,
        Filtered             = 43,
        Battleground         = 44,
        BattlegroundLeader   = 45,
        Restricted           = 46,
        BattleNet            = 47,
        Achievement          = 48,
        GuildAchievement     = 49,
        ArenaPoints          = 50,
        PartyLeader          = 51,
        Addon                = uint.MaxValue
    }

    public sealed class ChatHandlers
    {
        private readonly ParsingContext _context;

        public ChatHandlers(ParsingContext context)
        {
            _context = context;
        }

        [Parser(PacketDirection.ServerToClient, Opcode.SMSG_MESSAGECHAT)]
        [Parser(PacketDirection.ServerToClient, Opcode.SMSG_GM_MESSAGECHAT)]
        public void HandleMessageChat(ParsingContext context, Packet packet)
        {
            var chatMessageType = (ChatMessageType) packet.ReadUInt8();
            var language = packet.ReadUInt32();
            var senderGUID = packet.ReadGUID();
            var messageFlags = packet.ReadUInt32();

            var sourceGUID = default(IObjectGUID?);
            var targetGUID = default(IObjectGUID?);
            var sourceName = string.Empty;
            var targetName = string.Empty;

            switch (chatMessageType)
            {
                case ChatMessageType.MonsterSay:
                case ChatMessageType.MonsterYell:
                case ChatMessageType.RaidBossEmote:
                case ChatMessageType.RaidBossWhisper:
                case ChatMessageType.MonsterEmote:
                case ChatMessageType.MonsterWhisper:
                case ChatMessageType.MonsterParty:
                case ChatMessageType.BattleNet:
                {
                    sourceName = packet.ReadWoWString(packet.ReadUInt32());

                    targetGUID = packet.ReadGUID();
                    switch (targetGUID.Type)
                    {
                        case ObjectGuidType.Creature:
                        case ObjectGuidType.Vehicle:
                        case ObjectGuidType.GameObject:
                        case ObjectGuidType.Transport:
                            targetName = packet.ReadWoWString(packet.ReadUInt32());
                            break;
                    }

                    break;
                }
                case ChatMessageType.BattlegroundNeutral:
                case ChatMessageType.BattlegroundAlliance:
                case ChatMessageType.BattlegroundHorde:
                {
                    targetGUID = packet.ReadGUID();
                    switch (targetGUID.Type)
                    {
                        case ObjectGuidType.Creature:
                        case ObjectGuidType.Vehicle:
                        case ObjectGuidType.Pet:
                        case ObjectGuidType.GameObject:
                        case ObjectGuidType.Transport:
                            sourceName = packet.ReadWoWString(packet.ReadUInt32());
                            break;
                    }

                    break;
                }
                default:
                {
                    if (packet.Opcode == Opcode.SMSG_GM_MESSAGECHAT)
                    {
                        var gamemasterName = packet.ReadWoWString(packet.ReadUInt32());
                    }


                    if (chatMessageType == ChatMessageType.Channel)
                    {
                        var channel = packet.ReadCString(0x80);
                    }

                    sourceGUID = packet.ReadGUID();
                    break;
                }
            }
            
            if (language == uint.MaxValue) // Addon
                packet.ReadCString(0x11); // Addon prefix

            var text = packet.ReadWoWString(packet.ReadUInt32());
            var chatTag = packet.ReadUInt8();

            if (chatMessageType == ChatMessageType.RaidBossEmote || chatMessageType == ChatMessageType.RaidBossWhisper)
            {
                var displayTime = packet.ReadSingle();
                var hideChatLog = packet.ReadUInt8();
            }

            if (chatMessageType == ChatMessageType.Achievement || chatMessageType == ChatMessageType.GuildAchievement)
            {
                var achievementID = packet.ReadUInt32();
            }

            // Do not log addon messages
            if (language == uint.MaxValue)
                return;

            // Add names to cache to help instead of displaying stupid entries.
            context.NameCache.Register(senderGUID, sourceName);

            if (targetGUID != null)
                context.NameCache.Register(targetGUID, targetName);

            var @event = new MessageChatEvent
            {
                MessageType = chatMessageType.ToString(),
                Sender = senderGUID,
                Source = sourceGUID!,
                Target = targetGUID!,
                Text = text
            };

            context.RegisterEvent(packet.Moment, @event);
        }

        [Parser(PacketDirection.ServerToClient, Opcode.SMSG_PLAY_SOUND)]
        [Parser(PacketDirection.ServerToClient, Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public void HandlePlaySound(ParsingContext context, Packet packet)
        {
            var soundKitID = packet.ReadUInt32();
            var source = packet.ReadPackedGUID();

            var target = default(IObjectGUID?);
            if (packet.Opcode == Opcode.SMSG_PLAY_OBJECT_SOUND)
                target = packet.ReadPackedGUID();

            var @event = new PlaySoundEvent {
                SoundKitID = soundKitID,
                Source = source,
                Target = target
            };

            context.RegisterEvent(packet.Moment, @event);
        }
    }
}

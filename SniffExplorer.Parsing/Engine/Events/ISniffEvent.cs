using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Events
{
    public enum SniffEventType
    {
        None,
        PlaySound,
        MessageChat
    }

    public interface ISniffEvent
    {
        public SniffEventType Type { get; }
    }

    public class PlaySoundEvent : ISniffEvent
    {
        public SniffEventType Type { get; } = SniffEventType.PlaySound;

        public uint SoundKitID { get; init; }
        public IObjectGUID Source { get; init; }
        public IObjectGUID? Target { get; init; }
    }

    public class MessageChatEvent : ISniffEvent
    {
        public SniffEventType Type { get; } = SniffEventType.MessageChat;

        public IObjectGUID Sender { get; init; }
        public IObjectGUID Source { get; init; }
        public IObjectGUID Target { get; init; }

        public string Text { get; init; }

        public string MessageType { get; init; }
    }
}

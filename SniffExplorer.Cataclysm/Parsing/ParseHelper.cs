using System;
using SniffExplorer.Cataclysm.Attributes;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Helpers;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using Object = SniffExplorer.Parsing.Engine.Tracking.Entities.Object;

namespace SniffExplorer.Cataclysm.Parsing
{
    public class ParseHelper : BaseParseHelper<ParseHelper, ParserAttribute>
    {
        public ParseHelper(ParsingContext context) : base(context)
        {
        }

        public override EntityTypeID ResolveTypeID(byte value)
        {
            // This needs version-specific handling (again)
            return value switch
            {
                0  => EntityTypeID.Object,
                1  => EntityTypeID.Item,
                2  => EntityTypeID.Container,
                3  => EntityTypeID.Creature,
                4  => EntityTypeID.Player,
                5  => EntityTypeID.GameObject,
                6  => EntityTypeID.DynamicObject,
                7  => EntityTypeID.Corpse,
                8  => EntityTypeID.AreaTrigger,
                9  => EntityTypeID.SceneObject,
                10 => EntityTypeID.Conversation,
                _  => EntityTypeID.Unknown
            };
        }

        public override IEntity CreateEntity(IObjectGUID objectGUID, EntityTypeID typeID, bool isSelf)
        {
            return typeID switch
            {
                EntityTypeID.Object        => new Object(objectGUID, Context),
                EntityTypeID.ActivePlayer  => new Player(objectGUID, Context, true),
                EntityTypeID.Player        => new Player(objectGUID, Context, isSelf),
                EntityTypeID.Corpse        => new Corpse(objectGUID, Context),
                EntityTypeID.Creature      => new Creature(objectGUID, Context),
                EntityTypeID.DynamicObject => new DynamicObject(objectGUID, Context),
                EntityTypeID.GameObject    => new GameObject(objectGUID, Context),
                EntityTypeID.Item          => new Item(objectGUID, Context),
                EntityTypeID.Container     => new Container(objectGUID, Context),
                EntityTypeID.AreaTrigger   => new AreaTrigger(objectGUID, Context),
                _ => objectGUID.Type switch
                {
                    ObjectGuidType.Player        => new Player(objectGUID, Context, isSelf),
                    ObjectGuidType.Pet           => new Creature(objectGUID, Context),
                    ObjectGuidType.Vehicle       => new Creature(objectGUID, Context),
                    ObjectGuidType.Creature      => new Creature(objectGUID, Context),
                    ObjectGuidType.Transport     => new GameObject(objectGUID, Context),
                    ObjectGuidType.GameObject    => new GameObject(objectGUID, Context),
                    ObjectGuidType.Corpse        => new Corpse(objectGUID, Context),
                    ObjectGuidType.DynamicObject => new DynamicObject(objectGUID, Context),
                    ObjectGuidType.AreaTrigger   => new AreaTrigger(objectGUID, Context),
                    // TODO: This is a bad situation: we don't know what the item is exactly;
                    // TODO: Ideally, we would create an Item OR a Container depending on some value,
                    // TODO: but Cataclysm decided to be extra rude and give Items and Containers
                    // TODO: the same HighGuidType. We thus decide, for now, to return a Container,
                    // TODO: because parsing of a Container won't happen if the bitmask isn't set for it.
                    ObjectGuidType.Item => new Container(objectGUID, Context),
                    _                   => throw new InvalidOperationException()
                }
            };
        }
    }
}

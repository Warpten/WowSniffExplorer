﻿// AUTOGENERATED FILE - DO NOT EDIT
// This file was generated by UpdateFieldResolverGenerator on 4/6/2021 1:30:38 AM.

using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595.Retail
{
    [SniffExplorer.Shared.Attributes.Descriptors.GeneratedDescriptorAttribute(ClientBuild = 15595, RealmType = SniffExplorer.Shared.Enums.RealmExpansionType.Retail)]
    public class ICorpseDataImpl : SniffExplorer.Parsing.Engine.Tracking.UpdateFields.ICorpseData
    {
        public int BitCount { get; }

        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Owner { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Party { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField DisplayId { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField[] Items { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Bytes1 { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Bytes2 { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Flags { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField DynamicFlags { get; }

        public ICorpseDataImpl(ParsingContext context)
        {
            Owner = new GuidUpdateField(0, context);
            Party = new GuidUpdateField(Owner.BitEnd, context);
            DisplayId = new PrimitiveUpdateField<uint>(Party.BitEnd, context);

            Items = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField[19];
            Items[0] = new PrimitiveUpdateField<uint>(DisplayId.BitEnd, context);
            for (var itr0 = 1; itr0 < 19; ++itr0)
                Items[itr0] = new PrimitiveUpdateField<uint>(Items[itr0 - 1].BitEnd, context);

            Bytes1 = new PrimitiveUpdateField<uint>(Items[18].BitEnd, context);
            Bytes2 = new PrimitiveUpdateField<uint>(Bytes1.BitEnd, context);
            Flags = new PrimitiveUpdateField<uint>(Bytes2.BitEnd, context);
            DynamicFlags = new PrimitiveUpdateField<uint>(Flags.BitEnd, context);

            BitCount = DynamicFlags.BitEnd;
        }

        public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            Owner.ReadValue(packet, updateMask);
            Party.ReadValue(packet, updateMask);
            DisplayId.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 19; ++itr0)
                Items[itr0].ReadValue(packet, updateMask);

            Bytes1.ReadValue(packet, updateMask);
            Bytes2.ReadValue(packet, updateMask);
            Flags.ReadValue(packet, updateMask);
            DynamicFlags.ReadValue(packet, updateMask);
        }
    }
}

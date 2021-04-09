﻿// AUTOGENERATED FILE - DO NOT EDIT
// This file was generated by UpdateFieldResolverGenerator on 4/9/2021 11:13:30 PM.

using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595.Retail
{
    [SniffExplorer.Shared.Attributes.Descriptors.GeneratedDescriptorAttribute(ClientBuild = 15595, RealmType = SniffExplorer.Shared.Enums.RealmExpansionType.Retail)]
    public class IObjectDataImpl : SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IObjectData
    {
        public int BitCount { get; }

        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField GUID { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Data { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types.ObjectType> Type { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField<uint> Entry { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Scale { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Padding { get; }

        public IObjectDataImpl(ParsingContext context)
        {
            GUID = new GuidUpdateField(0, context);
            Data = new GuidUpdateField(GUID.BitEnd, context);
            Type = new StructuredUpdateField<SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types.ObjectType>(Data.BitEnd, 1, context, SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types.ObjectType.FromRawData);
            Entry = new PrimitiveUpdateField<uint>(Type.BitEnd, context);
            Scale = new PrimitiveUpdateField<float>(Entry.BitEnd, context);
            Padding = new PrimitiveUpdateField<uint>(Scale.BitEnd, context);

            BitCount = Padding.BitEnd;
        }

        public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            if (!updateMask.Any()) return;

            GUID.ReadValue(packet, updateMask);
            Data.ReadValue(packet, updateMask);
            Type.ReadValue(packet, updateMask);
            Entry.ReadValue(packet, updateMask);
            Scale.ReadValue(packet, updateMask);
            Padding.ReadValue(packet, updateMask);
        }
    }
}

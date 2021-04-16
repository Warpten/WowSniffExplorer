﻿// AUTOGENERATED FILE - DO NOT EDIT
// This file was generated by UpdateFieldResolverGenerator on 4/16/2021 10:27:41 PM.

using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Implementations;

namespace SniffExplorer.Cataclysm.UpdateFields.V4_3_4_15595.Retail
{
    [SniffExplorer.Shared.Attributes.Descriptors.GeneratedDescriptorAttribute(ClientBuild = 15595, RealmType = SniffExplorer.Shared.Enums.RealmExpansionType.Retail)]
    public class IAreaTriggerDataImpl : SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IAreaTriggerData
    {
        public int BitCount { get; }

        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField SpellID { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField SpellVisualID { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField Duration { get; }
        public SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField[] FinalPosition { get; }

        public IAreaTriggerDataImpl(ParsingContext context)
        {
            SpellID = new PrimitiveUpdateField<uint>(0, context);
            SpellVisualID = new PrimitiveUpdateField<uint>(SpellID.BitEnd, context);
            Duration = new PrimitiveUpdateField<uint>(SpellVisualID.BitEnd, context);

            FinalPosition = new SniffExplorer.Parsing.Engine.Tracking.UpdateFields.IUpdateField[3];
            FinalPosition[0] = new PrimitiveUpdateField<float>(Duration.BitEnd, context);
            for (var itr0 = 1; itr0 < 3; ++itr0)
                FinalPosition[itr0] = new PrimitiveUpdateField<float>(FinalPosition[itr0 - 1].BitEnd, context);


            BitCount = FinalPosition[2].BitEnd;
        }

        public void ProcessValuesUpdate(Packet packet, UpdateMask updateMask)
        {
            if (!updateMask.Any()) return;

            SpellID.ReadValue(packet, updateMask);
            SpellVisualID.ReadValue(packet, updateMask);
            Duration.ReadValue(packet, updateMask);

            for (var itr0 = 0; itr0 < 3; ++itr0)
                FinalPosition[itr0].ReadValue(packet, updateMask);

        }
    }
}

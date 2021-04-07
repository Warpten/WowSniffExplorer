using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Versions
{
    public class ClientBuild
    {
        public Expansion Expansion { get; }
        public RealmExpansionType ExpansionType { get; }

        public uint Value { get; }

        private readonly string _stringRepresentation;

        private ClientBuild(string stringRepresentation, uint build,
            Expansion expansion,
            RealmExpansionType expansionType = RealmExpansionType.Retail)
        {
            Value = build;

            Expansion = expansion;
            ExpansionType = expansionType;

            _stringRepresentation = stringRepresentation;
        }

        public override string ToString() => _stringRepresentation;

        public static ClientBuild? FromBuild(uint value) => Values.FirstOrDefault(v => v.Value == value);

        public static IEnumerable<ClientBuild> Values { get; private set; }

        static ClientBuild()
        {
            Values = typeof(ClientBuild).GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(fieldInfo => fieldInfo.FieldType == typeof(ClientBuild) && fieldInfo.IsInitOnly)
                .Select(fieldInfo => fieldInfo.GetValue(null))
                .Cast<ClientBuild>()
                .ToList();
        }

        public static bool operator >=(ClientBuild left, ClientBuild right)
        {
            if (left.Expansion != right.Expansion)
                return left.Expansion >= right.Expansion;

            return left.Value >= right.Value;
        }

        public static bool operator <=(ClientBuild left, ClientBuild right)
            => !(right > left);

        public static bool operator >(ClientBuild left, ClientBuild right)
        {
            if (left.Expansion != right.Expansion)
                return left.Expansion > right.Expansion;

            return left.Value > right.Value;
        }

        public static bool operator <(ClientBuild left, ClientBuild right)
            => !(left >= right);
        
        // ReSharper disable InconsistentNaming
        public static readonly ClientBuild V1_12_1_5875 = new("1.12.1.5875", 5875, Expansion.Vanilla);
        
        public static readonly ClientBuild V2_0_1_6180 = new("2.0.1.6180", 6180, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_0_3_6299 = new("2.0.3.6299", 6299, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_0_6_6337 = new("2.0.6.6337", 6337, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_1_0_6692 = new("2.1.0.6692", 6692, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_1_1_6739 = new("2.1.1.6739", 6739, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_1_2_6803 = new("2.1.2.6803", 6803, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_1_3_6898 = new("2.1.3.6898", 6898, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_2_0_7272 = new("2.2.0.7272", 7272, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_2_2_7318 = new("2.2.2.7318", 7318, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_2_3_7359 = new("2.2.3.7359", 7359, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_3_0_7561 = new("2.3.0.7561", 7561, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_3_2_7741 = new("2.3.2.7741", 7741, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_3_3_7799 = new("2.3.3.7799", 7799, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_4_0_8089 = new("2.4.0.8089", 8089, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_4_1_8125 = new("2.4.1.8125", 8125, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_4_2_8209 = new("2.4.2.8209", 8209, Expansion.TheBurningCrusade);
        public static readonly ClientBuild V2_4_3_8606 = new("2.4.3.8606", 8606, Expansion.TheBurningCrusade);

        public static readonly ClientBuild V3_0_2_9056 = new("3.0.2.9056", 9056, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_0_3_9183 = new("3.0.3.9183", 9183, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_0_8_9464 = new("3.0.8.9464", 9464, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_0_8a_9506 = new("3.0.8a.9506", 9506, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_0_9_9551 = new("3.0.9.9551", 9551, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_1_0_9767 = new("3.1.0.9767", 9767, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_1_1_9806 = new("3.1.1.9806", 9806, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_1_1a_9835 = new("3.1.1a.9835", 9835, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_1_2_9901 = new("3.1.2.9901", 9901, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_1_3_9947 = new("3.1.3.9947", 9947, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_2_0_10192 = new("3.2.0.10192", 10192, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_2_0a_10314 = new("3.2.0a.10314", 10314, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_2_2_10482 = new("3.2.2.10482", 10482, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_2_2a_10505 = new("3.2.2a.10505", 10505, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_3_0_10958 = new("3.3.0.10958", 10958, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_3_0a_11159 = new("3.3.0a.11159", 11159, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_3_3_11685 = new("3.3.3.11685", 11685, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_3_3a_11723 = new("3.3.3a.11723", 11723, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_3_5_12213 = new("3.3.5.12213", 12213, Expansion.WrathOfTheLichKing);
        public static readonly ClientBuild V3_3_5a_12340 = new("3.3.5a.12340", 12340, Expansion.WrathOfTheLichKing);

        public static readonly ClientBuild V4_0_1_13164 = new("4.0.1.13164", 13164, Expansion.Cataclysm);
        public static readonly ClientBuild V4_0_1a_13205 = new("4.0.1a.13205", 13205, Expansion.Cataclysm);
        public static readonly ClientBuild V4_0_3_13329 = new("4.0.3.13329", 13329, Expansion.Cataclysm);
        public static readonly ClientBuild V4_0_6_13596 = new("4.0.6.13596", 13596, Expansion.Cataclysm);
        public static readonly ClientBuild V4_0_6a_13623 = new("4.0.6a.13623", 13623, Expansion.Cataclysm);
        public static readonly ClientBuild V4_1_0_13914 = new("4.1.0.13914", 13914, Expansion.Cataclysm);
        public static readonly ClientBuild V4_1_0a_14007 = new("4.1.0a.14007", 14007, Expansion.Cataclysm);
        public static readonly ClientBuild V4_2_0_14333 = new("4.2.0.14333", 14333, Expansion.Cataclysm);
        public static readonly ClientBuild V4_2_0a_14480 = new("4.2.0a.14480", 14480, Expansion.Cataclysm);
        public static readonly ClientBuild V4_2_2_14545 = new("4.2.2.14545", 14545, Expansion.Cataclysm);
        public static readonly ClientBuild V4_3_0_15005 = new("4.3.0.15005", 15005, Expansion.Cataclysm);
        public static readonly ClientBuild V4_3_0a_15050 = new("4.3.0a.15050", 15050, Expansion.Cataclysm);
        public static readonly ClientBuild V4_3_2_15211 = new("4.3.2.15211", 15211, Expansion.Cataclysm);
        public static readonly ClientBuild V4_3_3_15354 = new("4.3.3.15354", 15354, Expansion.Cataclysm);
        public static readonly ClientBuild V4_3_4_15595 = new("4.3.4.15595", 15595, Expansion.Cataclysm);

        public static readonly ClientBuild V5_0_4_16016 = new("5.0.4.16016", 16016, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_0_5_16048 = new("5.0.5.16048", 16048, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_0_5a_16057 = new("5.0.5a.16057", 16057, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_0_5b_16135 = new("5.0.5b.16135", 16135, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_1_0_16309 = new("5.1.0.16309", 16309, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_1_0a_16357 = new("5.1.0a.16357", 16357, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16650 = new("5.2.0.16650", 16650, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16669 = new("5.2.0.16669", 16669, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16683 = new("5.2.0.16683", 16683, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16685 = new("5.2.0.16685", 16685, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16701 = new("5.2.0.16701", 16701, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16709 = new("5.2.0.16709", 16709, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16716 = new("5.2.0.16716", 16716, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16733 = new("5.2.0.16733", 16733, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16769 = new("5.2.0.16769", 16769, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_2_0_16826 = new("5.2.0.16826", 16826, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_3_0_16981 = new("5.3.0.16981", 16981, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_3_0_16983 = new("5.3.0.16983", 16983, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_3_0_16992 = new("5.3.0.16992", 16992, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_3_0_17055 = new("5.3.0.17055", 17055, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_3_0_17116 = new("5.3.0.17116", 17116, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_3_0_17128 = new("5.3.0.17128", 17128, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_0_17359 = new("5.4.0.17359", 17359, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_0_17371 = new("5.4.0.17371", 17371, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_0_17399 = new("5.4.0.17399", 17399, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_1_17538 = new("5.4.1.17538", 17538, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_2_17658 = new("5.4.2.17658", 17658, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_2_17688 = new("5.4.2.17688", 17688, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_7_17898 = new("5.4.7.17898", 17898, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_7_17930 = new("5.4.7.17930", 17930, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_7_17956 = new("5.4.7.17956", 17956, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_7_18019 = new("5.4.7.18019", 18019, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_8_18291 = new("5.4.8.18291", 18291, Expansion.MistsOfPandaria);
        public static readonly ClientBuild V5_4_8_18414 = new("5.4.8.18414", 18414, Expansion.MistsOfPandaria);

        public static readonly ClientBuild V6_0_2_19033 = new("6.0.2.19033", 19033, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_0_2_19034 = new("6.0.2.19034", 19034, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_0_3_19103 = new("6.0.3.19103", 19103, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_0_3_19116 = new("6.0.3.19116", 19116, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_0_3_19243 = new("6.0.3.19243", 19243, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_0_3_19342 = new("6.0.3.19342", 19342, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_1_0_19678 = new("6.1.0.19678", 19678, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_1_0_19702 = new("6.1.0.19702", 19702, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_1_2_19802 = new("6.1.2.19802", 19802, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_1_2_19831 = new("6.1.2.19831", 19831, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_1_2_19865 = new("6.1.2.19865", 19865, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_0_20173 = new("6.2.0.20173", 20173, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_0_20182 = new("6.2.0.20182", 20182, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_0_20201 = new("6.2.0.20201", 20201, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_0_20216 = new("6.2.0.20216", 20216, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_0_20253 = new("6.2.0.20253", 20253, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_0_20338 = new("6.2.0.20338", 20338, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_2_20444 = new("6.2.2.20444", 20444, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_2a_20490 = new("6.2.2a.20490", 20490, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_2a_20574 = new("6.2.2a.20574", 20574, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_3_20726 = new("6.2.3.20726", 20726, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_3_20779 = new("6.2.3.20779", 20779, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_3_20886 = new("6.2.3.20886", 20886, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21315 = new("6.2.4.21315", 21315, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21336 = new("6.2.4.21336", 21336, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21343 = new("6.2.4.21343", 21343, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21345 = new("6.2.4.21345", 21345, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21348 = new("6.2.4.21348", 21348, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21355 = new("6.2.4.21355", 21355, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21463 = new("6.2.4.21463", 21463, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21676 = new("6.2.4.21676", 21676, Expansion.WarlordsOfDraenor);
        public static readonly ClientBuild V6_2_4_21742 = new("6.2.4.21742", 21742, Expansion.WarlordsOfDraenor);

        public static readonly ClientBuild V7_0_3_22248 = new("7.0.3.22248", 22248, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22267 = new("7.0.3.22267", 22267, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22277 = new("7.0.3.22277", 22277, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22280 = new("7.0.3.22280", 22280, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22289 = new("7.0.3.22289", 22289, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22293 = new("7.0.3.22293", 22293, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22345 = new("7.0.3.22345", 22345, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22396 = new("7.0.3.22396", 22396, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22410 = new("7.0.3.22410", 22410, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22423 = new("7.0.3.22423", 22423, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22445 = new("7.0.3.22445", 22445, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22498 = new("7.0.3.22498", 22498, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22522 = new("7.0.3.22522", 22522, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22566 = new("7.0.3.22566", 22566, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22594 = new("7.0.3.22594", 22594, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22624 = new("7.0.3.22624", 22624, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22747 = new("7.0.3.22747", 22747, Expansion.Legion);
        public static readonly ClientBuild V7_0_3_22810 = new("7.0.3.22810", 22810, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_22900 = new("7.1.0.22900", 22900, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_22908 = new("7.1.0.22908", 22908, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_22950 = new("7.1.0.22950", 22950, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_22989 = new("7.1.0.22989", 22989, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_22995 = new("7.1.0.22995", 22995, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_22996 = new("7.1.0.22996", 22996, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_23171 = new("7.1.0.23171", 23171, Expansion.Legion);
        public static readonly ClientBuild V7_1_0_23222 = new("7.1.0.23222", 23222, Expansion.Legion);
        public static readonly ClientBuild V7_1_5_23360 = new("7.1.5.23360", 23360, Expansion.Legion);
        public static readonly ClientBuild V7_1_5_23420 = new("7.1.5.23420", 23420, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23706 = new("7.2.0.23706", 23706, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23826 = new("7.2.0.23826", 23826, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23835 = new("7.2.0.23835", 23835, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23836 = new("7.2.0.23836", 23836, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23846 = new("7.2.0.23846", 23846, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23852 = new("7.2.0.23852", 23852, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23857 = new("7.2.0.23857", 23857, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23877 = new("7.2.0.23877", 23877, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23911 = new("7.2.0.23911", 23911, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_23937 = new("7.2.0.23937", 23937, Expansion.Legion);
        public static readonly ClientBuild V7_2_0_24015 = new("7.2.0.24015", 24015, Expansion.Legion);
        public static readonly ClientBuild V7_2_5_24330 = new("7.2.5.24330", 24330, Expansion.Legion);
        public static readonly ClientBuild V7_2_5_24367 = new("7.2.5.24367", 24367, Expansion.Legion);
        public static readonly ClientBuild V7_2_5_24414 = new("7.2.5.24414", 24414, Expansion.Legion);
        public static readonly ClientBuild V7_2_5_24415 = new("7.2.5.24415", 24415, Expansion.Legion);
        public static readonly ClientBuild V7_2_5_24430 = new("7.2.5.24430", 24430, Expansion.Legion);
        public static readonly ClientBuild V7_2_5_24461 = new("7.2.5.24461", 24461, Expansion.Legion);
        public static readonly ClientBuild V7_2_5_24742 = new("7.2.5.24742", 24742, Expansion.Legion);
        public static readonly ClientBuild V7_3_0_24920 = new("7.3.0.24920", 24920, Expansion.Legion);
        public static readonly ClientBuild V7_3_0_24931 = new("7.3.0.24931", 24931, Expansion.Legion);
        public static readonly ClientBuild V7_3_0_24956 = new("7.3.0.24956", 24956, Expansion.Legion);
        public static readonly ClientBuild V7_3_0_24970 = new("7.3.0.24970", 24970, Expansion.Legion);
        public static readonly ClientBuild V7_3_0_24974 = new("7.3.0.24974", 24974, Expansion.Legion);
        public static readonly ClientBuild V7_3_0_25021 = new("7.3.0.25021", 25021, Expansion.Legion);
        public static readonly ClientBuild V7_3_0_25195 = new("7.3.0.25195", 25195, Expansion.Legion);
        public static readonly ClientBuild V7_3_2_25383 = new("7.3.2.25383", 25383, Expansion.Legion);
        public static readonly ClientBuild V7_3_2_25442 = new("7.3.2.25442", 25442, Expansion.Legion);
        public static readonly ClientBuild V7_3_2_25455 = new("7.3.2.25455", 25455, Expansion.Legion);
        public static readonly ClientBuild V7_3_2_25477 = new("7.3.2.25477", 25477, Expansion.Legion);
        public static readonly ClientBuild V7_3_2_25480 = new("7.3.2.25480", 25480, Expansion.Legion);
        public static readonly ClientBuild V7_3_2_25497 = new("7.3.2.25497", 25497, Expansion.Legion);
        public static readonly ClientBuild V7_3_2_25549 = new("7.3.2.25549", 25549, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25848 = new("7.3.5.25848", 25848, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25860 = new("7.3.5.25860", 25860, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25864 = new("7.3.5.25864", 25864, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25875 = new("7.3.5.25875", 25875, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25881 = new("7.3.5.25881", 25881, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25901 = new("7.3.5.25901", 25901, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25928 = new("7.3.5.25928", 25928, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25937 = new("7.3.5.25937", 25937, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25944 = new("7.3.5.25944", 25944, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25946 = new("7.3.5.25946", 25946, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25950 = new("7.3.5.25950", 25950, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25961 = new("7.3.5.25961", 25961, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_25996 = new("7.3.5.25996", 25996, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_26124 = new("7.3.5.26124", 26124, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_26365 = new("7.3.5.26365", 26365, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_26654 = new("7.3.5.26654", 26654, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_26755 = new("7.3.5.26755", 26755, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_26822 = new("7.3.5.26822", 26822, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_26899 = new("7.3.5.26899", 26899, Expansion.Legion);
        public static readonly ClientBuild V7_3_5_26972 = new("7.3.5.26972", 26972, Expansion.Legion);

        public static readonly ClientBuild V8_0_1_27101 = new("8.0.1.27101", 27101, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27144 = new("8.0.1.27144", 27144, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27165 = new("8.0.1.27165", 27165, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27178 = new("8.0.1.27178", 27178, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27219 = new("8.0.1.27219", 27219, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27291 = new("8.0.1.27291", 27291, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27326 = new("8.0.1.27326", 27326, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27355 = new("8.0.1.27355", 27355, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27356 = new("8.0.1.27356", 27356, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27366 = new("8.0.1.27366", 27366, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27377 = new("8.0.1.27377", 27377, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27404 = new("8.0.1.27404", 27404, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27481 = new("8.0.1.27481", 27481, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27547 = new("8.0.1.27547", 27547, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27602 = new("8.0.1.27602", 27602, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27791 = new("8.0.1.27791", 27791, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27843 = new("8.0.1.27843", 27843, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_27980 = new("8.0.1.27980", 27980, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_0_1_28153 = new("8.0.1.28153", 28153, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_28724 = new("8.1.0.28724", 28724, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_28768 = new("8.1.0.28768", 28768, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_28807 = new("8.1.0.28807", 28807, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_28822 = new("8.1.0.28822", 28822, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_28833 = new("8.1.0.28833", 28833, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29088 = new("8.1.0.29088", 29088, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29139 = new("8.1.0.29139", 29139, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29235 = new("8.1.0.29235", 29235, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29285 = new("8.1.0.29285", 29285, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29297 = new("8.1.0.29297", 29297, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29482 = new("8.1.0.29482", 29482, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29600 = new("8.1.0.29600", 29600, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_0_29621 = new("8.1.0.29621", 29621, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29683 = new("8.1.5.29683", 29683, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29701 = new("8.1.5.29701", 29701, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29704 = new("8.1.5.29704", 29704, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29705 = new("8.1.5.29705", 29705, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29718 = new("8.1.5.29718", 29718, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29732 = new("8.1.5.29732", 29732, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29737 = new("8.1.5.29737", 29737, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29814 = new("8.1.5.29814", 29814, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29869 = new("8.1.5.29869", 29869, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29896 = new("8.1.5.29896", 29896, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_29981 = new("8.1.5.29981", 29981, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_30477 = new("8.1.5.30477", 30477, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_1_5_30706 = new("8.1.5.30706", 30706, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_30898 = new("8.2.0.30898", 30898, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_30918 = new("8.2.0.30918", 30918, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_30920 = new("8.2.0.30920", 30920, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_30948 = new("8.2.0.30948", 30948, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_30993 = new("8.2.0.30993", 30993, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_31229 = new("8.2.0.31229", 31229, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_31429 = new("8.2.0.31429", 31429, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_0_31478 = new("8.2.0.31478", 31478, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_31921 = new("8.2.5.31921", 31921, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_31958 = new("8.2.5.31958", 31958, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_31960 = new("8.2.5.31960", 31960, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_31961 = new("8.2.5.31961", 31961, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_31984 = new("8.2.5.31984", 31984, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32028 = new("8.2.5.32028", 32028, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32144 = new("8.2.5.32144", 32144, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32185 = new("8.2.5.32185", 32185, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32265 = new("8.2.5.32265", 32265, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32294 = new("8.2.5.32294", 32294, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32305 = new("8.2.5.32305", 32305, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32494 = new("8.2.5.32494", 32494, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32580 = new("8.2.5.32580", 32580, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32638 = new("8.2.5.32638", 32638, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32722 = new("8.2.5.32722", 32722, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32750 = new("8.2.5.32750", 32750, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_2_5_32978 = new("8.2.5.32978", 32978, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33062 = new("8.3.0.33062", 33062, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33073 = new("8.3.0.33073", 33073, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33084 = new("8.3.0.33084", 33084, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33095 = new("8.3.0.33095", 33095, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33115 = new("8.3.0.33115", 33115, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33169 = new("8.3.0.33169", 33169, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33237 = new("8.3.0.33237", 33237, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33369 = new("8.3.0.33369", 33369, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33528 = new("8.3.0.33528", 33528, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33724 = new("8.3.0.33724", 33724, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33775 = new("8.3.0.33775", 33775, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_33941 = new("8.3.0.33941", 33941, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_34220 = new("8.3.0.34220", 34220, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_34601 = new("8.3.0.34601", 34601, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_34769 = new("8.3.0.34769", 34769, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_0_34963 = new("8.3.0.34963", 34963, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_7_35249 = new("8.3.7.35249", 35249, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_7_35284 = new("8.3.7.35284", 35284, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_7_35435 = new("8.3.7.35435", 35435, Expansion.BattleForAzeroth);
        public static readonly ClientBuild V8_3_7_35662 = new("8.3.7.35662", 35662, Expansion.BattleForAzeroth);

        public static readonly ClientBuild V9_0_1_36216 = new("9.0.1.36216", 36216, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36228 = new("9.0.1.36228", 36228, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36230 = new("9.0.1.36230", 36230, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36247 = new("9.0.1.36247", 36247, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36272 = new("9.0.1.36272", 36272, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36322 = new("9.0.1.36322", 36322, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36372 = new("9.0.1.36372", 36372, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36492 = new("9.0.1.36492", 36492, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_1_36577 = new("9.0.1.36577", 36577, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36639 = new("9.0.2.36639", 36639, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36665 = new("9.0.2.36665", 36665, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36671 = new("9.0.2.36671", 36671, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36710 = new("9.0.2.36710", 36710, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36734 = new("9.0.2.36734", 36734, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36751 = new("9.0.2.36751", 36751, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36753 = new("9.0.2.36753", 36753, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36839 = new("9.0.2.36839", 36839, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_36949 = new("9.0.2.36949", 36949, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_37106 = new("9.0.2.37106", 37106, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_37142 = new("9.0.2.37142", 37142, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_37176 = new("9.0.2.37176", 37176, Expansion.Shadowlands);
        public static readonly ClientBuild V9_0_2_37474 = new("9.0.2.37474", 37474, Expansion.Shadowlands);

        public static readonly ClientBuild V1_12_2_31446 = new("1.12.2.31446", 31446, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_31650 = new("1.13.2.31650", 31650, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_31687 = new("1.13.2.31687", 31687, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_31727 = new("1.13.2.31727", 31727, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_31830 = new("1.13.2.31830", 31830, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_31882 = new("1.13.2.31882", 31882, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_32089 = new("1.13.2.32089", 32089, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_32421 = new("1.13.2.32421", 32421, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_2_32600 = new("1.13.2.32600", 32600, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_3_32790 = new("1.13.3.32790", 32790, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_3_32836 = new("1.13.3.32836", 32836, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_3_32887 = new("1.13.3.32887", 32887, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_3_33155 = new("1.13.3.33155", 33155, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_3_33302 = new("1.13.3.33302", 33302, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_3_33526 = new("1.13.3.33526", 33526, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_4_33598 = new("1.13.4.33598", 33598, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_4_33645 = new("1.13.4.33645", 33645, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_4_33728 = new("1.13.4.33728", 33728, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_4_33920 = new("1.13.4.33920", 33920, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_4_34219 = new("1.13.4.34219", 34219, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_4_34266 = new("1.13.4.34266", 34266, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_4_34600 = new("1.13.4.34600", 34600, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_5_34713 = new("1.13.5.34713", 34713, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_5_34911 = new("1.13.5.34911", 34911, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_5_35100 = new("1.13.5.35100", 35100, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_5_35186 = new("1.13.5.35186", 35186, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_5_36035 = new("1.13.5.36035", 36035, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_5_36325 = new("1.13.5.36325", 36325, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_6_36714 = new("1.13.6.36714", 36714, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_6_36935 = new("1.13.6.36935", 36935, Expansion.Vanilla, RealmExpansionType.Classic);
        public static readonly ClientBuild V1_13_6_37497 = new("1.13.6.37497", 37497, Expansion.Vanilla, RealmExpansionType.Classic);
        // ReSharper restore InconsistentNaming
    }
}

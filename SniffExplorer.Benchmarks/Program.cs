using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace SniffExplorer.Benchmarks
{
    public class ArrayClearBenchmark
    {
        [Params(1, 5, 10, 50, 100, 500, 1000, 5000, 10000, 20000)]
        public int Size { get; set; }

        private byte[] _data;

        [GlobalSetup]
        public void GlobalSteup()
        {
            _data = new byte[Size];
        }

        [Benchmark(Description = "Array.Clear")]
        public byte[] Benchmark__Array_Clear()
        {
            Array.Clear(_data, 0, _data.Length);
            return _data;
        }

        [Benchmark(Description = "Loop")]
        public byte[] Benchmark__ZeroLoop()
        {
            for (var i = 0; i < _data.Length; ++i)
                _data[i] = 0;
            return _data;
        }

        internal class RawArrayData
        {
            public uint Length;
            public uint Padding;
            public byte Data;
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<ArrayClearBenchmark>();
        }
    }
}

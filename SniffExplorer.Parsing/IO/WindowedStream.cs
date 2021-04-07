using System;
using System.IO;

namespace SniffExplorer.Parsing.IO
{
    public class WindowedStream : Stream
    {
        public Stream BaseStream { get; }

        private long _cursor;
        private long _origin;
        private long _length;

        public WindowedStream(Stream impl, long length)
        {
            BaseStream = impl;
            _cursor = 0;
            _origin = impl.Position;
            _length = length;
        }

        public override bool CanRead => BaseStream.CanRead;
        public override bool CanSeek => BaseStream.CanSeek;
        public override bool CanWrite => BaseStream.CanWrite;

        public override long Length => _length;

        public override long Position
        {
            get => BaseStream.Position - _origin;
            set => BaseStream.Position = _origin + value;
        }

        public override void Flush()
            => BaseStream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            var availableSize = _length - _cursor;
            count = (int) Math.Min(availableSize, (uint) count);

            var readCount = BaseStream.Read(buffer, offset, count);
            _cursor += (uint) count;
            return readCount;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Current:
                    if (offset + _cursor < _length)
                        return BaseStream.Seek(offset, origin);
                    throw new ArgumentOutOfRangeException(nameof(offset));
                case SeekOrigin.Begin:
                    if (offset < _length)
                        return BaseStream.Seek(_origin + offset, SeekOrigin.Begin);
                    throw new ArgumentOutOfRangeException(nameof(offset));
                case SeekOrigin.End:
                    break;
            }

            return BaseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var availableSize = _length - _cursor;
            count = (int)Math.Min(availableSize, (uint)count);

            BaseStream.Write(buffer, offset, count);
        }
    }
}

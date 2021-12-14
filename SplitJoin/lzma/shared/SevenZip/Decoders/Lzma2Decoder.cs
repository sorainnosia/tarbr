﻿using System.Collections.Immutable;

namespace ManagedLzma.SevenZip.Reader
{
    internal sealed class Lzma2ArchiveDecoder : DecoderNode
    {
        private sealed class OutputStream : ReaderNode
        {
            private Lzma2ArchiveDecoder mOwner;
            public OutputStream(Lzma2ArchiveDecoder owner) { mOwner = owner; }
            public override void Dispose() { mOwner = null; }
            public override void Skip(int count) => mOwner.Skip(count);
            public override int Read(byte[] buffer, int offset, int count) => mOwner.Read(buffer, offset, count);
        }

        private LZMA2.Decoder mDecoder;
        private ReaderNode mInput;
        private OutputStream mOutput;
        private byte[] mBuffer;
        private int mOffset;
        private int mEnding;
        private long mLength;
        private long mPosition;

        public Lzma2ArchiveDecoder(ImmutableArray<byte> settings, long length)
        {
            ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(!settings.IsDefault && settings.Length == 1 && length >= 0);

            mDecoder = new LZMA2.Decoder(new LZMA2.DecoderSettings(settings[0]));
            mOutput = new OutputStream(this);
            mBuffer = new byte[4 << 10]; // TODO: We shouldn't have to use a buffer here. Let the input stream submit directly into the decoder.
            mLength = length;
        }

        public override void Dispose()
        {
            mDecoder?.Dispose();
            mDecoder = null;
            mOutput?.Dispose();
            mOutput = null;
            mBuffer = null;
        }

        public override void SetInputStream(int index, ReaderNode stream, long length)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("index");

            if (stream == null)
                throw new ArgumentNullException("stream");

            mInput = stream;
        }

        public override ReaderNode GetOutputStream(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException("index");

            return mOutput;
        }

        private void EnsureOutputData()
        {
            while (mDecoder.AvailableOutputLength == 0 && !mDecoder.IsOutputComplete)
            {
                if (mOffset == mEnding)
                {
                    mOffset = 0;
                    mEnding = 0;

                    var fetched = mInput.Read(mBuffer, 0, mBuffer.Length);
                    ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(0 <= fetched && fetched <= mBuffer.Length);

                    if (fetched == 0)
                    {
                        mDecoder.Decode(null, 0, 0, null, true);
                        ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(mDecoder.AvailableOutputLength > 0 || mDecoder.IsOutputComplete);
                        continue;
                    }

                    mEnding = fetched;
                }

                var written = mDecoder.Decode(mBuffer, mOffset, mEnding - mOffset, (int)Math.Min(Int32.MaxValue, mLength - mPosition), false);
                ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(0 <= written && written <= mEnding - mOffset);
                mOffset += written;
            }
        }

        private void Skip(int count)
        {
            ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(count > 0);

            while (count > 0)
            {
                EnsureOutputData();

                var skipped = mDecoder.SkipOutputData(count);
                ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(0 < skipped && skipped <= count);
                count -= skipped;
                mPosition += skipped;
            }
        }

        private int Read(byte[] buffer, int offset, int count)
        {
            ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(buffer != null);
            ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(0 <= offset && offset < buffer.Length);
            ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(0 < count && count <= buffer.Length - offset);

            EnsureOutputData();

            var fetched = mDecoder.ReadOutputData(buffer, offset, count);
            ManagedLzma.LZMA.Master.SevenZip.Utils.Assert(0 <= fetched && fetched <= count);
            mPosition += fetched;
            return fetched;
        }
    }
}

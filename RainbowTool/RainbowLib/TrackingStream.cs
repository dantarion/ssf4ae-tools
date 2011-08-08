using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace RainbowLib
{
    /* This class exists as a Stream wrapper that can track down where and why reads are being made.
     * It is what I used to see which parts of the file my code wasn't reading */
    public class TrackingStream : Stream
    {

        public struct StreamRead
        {
            public string Name;
            public long Offset;
            public long Length;
            public bool Ignore;
        }
        public TrackingStream(Stream bs)
        {
            baseStream = bs;
        }
        public void SetLabel(string s)
        {
            currentLabel = s;
        }
        public String Report()
        {

            long off = 0;
            StringBuilder sb = new StringBuilder(10240000);
            long overlaps = 0;
            string last = "";
            foreach (StreamRead read in reads.OrderBy(x=>x.Offset))
            {
                string thiss = string.Format("{0,15} - {1:X08} - {2:X08} {3:X04} - {4}\n", read.Name, read.Offset, read.Length + read.Offset, read.Ignore,read.Length);
                if (read.Offset > off)
                {
                    sb.Append(last);
                    sb.AppendFormat("Hole {0} Bytes Big\n", read.Offset - off);
                    sb.Append(thiss);

                }
                if (read.Offset < off)
                {
                     sb.Append(last);
                    sb.AppendFormat("Overlap {0} Bytes Big\n", off-read.Offset);
                    overlaps += off - read.Offset;
                    sb.Append(thiss);
                }
                last = thiss;
                if(read.Ignore)
                {
                    baseStream.Position = read.Offset;
                    for (long i = 0; i < read.Length; i += 16)
                    {
                        sb.AppendFormat("{0:X02}{1:X02}{2:X02}{3:X02} {4:X02}{5:X02}{6:X02}{7:X02}\n",
                            baseStream.ReadByte(), baseStream.ReadByte(), baseStream.ReadByte(), baseStream.ReadByte(), 
                            baseStream.ReadByte(), baseStream.ReadByte(), baseStream.ReadByte(), baseStream.ReadByte());
                    }
                }
                off = read.Offset + read.Length;
            }
            sb.AppendFormat("Read {0} bytes and Ignored {1} bytes out of {2} bytes!\n Total = {3},overlaps {4}", readBytes, ignoredBytes, Length,readBytes+ignoredBytes,overlaps);
            return sb.ToString();
        }
        private long readBytes = 0;
        private long ignoredBytes = 0;
        private List<StreamRead> reads = new List<StreamRead>();
        private string currentLabel;
        private Stream baseStream;
        public override bool CanRead
        {
            get { return baseStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return baseStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return baseStream.CanWrite; }
        }

        public override void Flush()
        {
            baseStream.Flush();
        }

        public override long Length
        {
            get { return baseStream.Length; }
        }

        public override long Position
        {
            get
            {
                return baseStream.Position;
            }
            set
            {
                baseStream.Position = value;
                    
            }
        }
        public void IgnoreBytes(long count)
        {
            var tmp = new StreamRead();
            tmp.Name = currentLabel;
            tmp.Offset = Position;
            tmp.Length = count;
            tmp.Ignore = true;
            reads.Add(tmp);
            ignoredBytes += count;
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            var tmp = new StreamRead();
            tmp.Name = currentLabel;
            tmp.Offset = Position;
            tmp.Length = count;
            tmp.Ignore = false;
            reads.Add(tmp);
            readBytes += count;
            return baseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            baseStream.Write(buffer, offset, count);
        }
    }
}

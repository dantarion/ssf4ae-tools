using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AEDataLibrary
{
    public class BACData
    {
        public const Int64 BAC_HEADER_A = 11540466583552547;
        public const Int32 BAC_HEADER_B = 65537;
        public const int BAC_SUB_HIT_SIZE = 80;
        public const int BAC_HIT_SIZE = 960;
        public const byte ZERO_BYTE = 0;
        public const int BAC_HITBOX_SIZE = 672;

        public List<Byte> hitboxBytes;

        public struct BACAnim
        {
            public string name;
            public bool bEmpty;
            public BACAnimData data;

            // byte 28, 32, 40, 44, 52, 58, 64, 68, 76, 80 is some kind of pointer

            // bytes up until 36 are gauranteed

            // 00-01 unknown
            // 02-03 unknown
            // 04-05 "length"/"speed" #1
            // 05-06 "length"/"speed" #2
            // 28-31 pointer

            // "HURT" is 32 bytes
        }

        public struct BACAnimData
        {
            //public List<byte> data;
            public List<byte> header;
            public List<BACChunk> chunks;
            public List<byte> data;

            /*public List<byte> data
            {
                get
                {
                    List<Byte> output = new List<byte>();
                    
                    if (header == null)
                    {
                        //AELogger.Log(output.Count.ToString());
                    }
                    for (int i = 0; i < header.Count; i++)
                    {
                        output.Add(header[i]);
                    }
                    output.Add(ZERO_BYTE);
                    output.Add(ZERO_BYTE);
                    output.Add(BitConverter.GetBytes((UInt16)chunks.Count)[0]);
                    output.Add(BitConverter.GetBytes((UInt16)chunks.Count)[1]);
                    output.Add(24);
                    output.Add(ZERO_BYTE);
                    output.Add(ZERO_BYTE);
                    output.Add(ZERO_BYTE);

                    UInt32 currentPointer = (UInt32)chunks.Count * 12;

                    for (int i = 0; i < chunks.Count; i++)
                    {
                        output.Add((byte)(int)chunks[i].type);
                        output.Add(ZERO_BYTE);
                        output.Add(BitConverter.GetBytes((UInt16)chunks[i].subchunks.Count)[0]);
                        output.Add(BitConverter.GetBytes((UInt16)chunks[i].subchunks.Count)[1]);
                        output.Add(BitConverter.GetBytes(currentPointer)[0]);
                        output.Add(BitConverter.GetBytes(currentPointer)[1]);
                        output.Add(BitConverter.GetBytes(currentPointer)[2]);
                        output.Add(BitConverter.GetBytes(currentPointer)[3]);
                        currentPointer += (UInt32)chunks[i].subchunks.Count * 4;
                        output.Add(BitConverter.GetBytes(currentPointer)[0]);
                        output.Add(BitConverter.GetBytes(currentPointer)[1]);
                        output.Add(BitConverter.GetBytes(currentPointer)[2]);
                        output.Add(BitConverter.GetBytes(currentPointer)[3]);
                        currentPointer += (UInt32)(chunks[i].subchunks[0].data.Count * (chunks[i].subchunks.Count-1));
                        currentPointer -= 12;
                    }
                    for (int i = 0; i < chunks.Count; i++)
                    {
                        for (int j = 0; j < chunks[i].subchunks.Count; j++)
                        {
                            output.Add(BitConverter.GetBytes(chunks[i].subchunks[j].start)[0]);
                            output.Add(BitConverter.GetBytes(chunks[i].subchunks[j].start)[1]);
                            output.Add(BitConverter.GetBytes(chunks[i].subchunks[j].end)[0]);
                            output.Add(BitConverter.GetBytes(chunks[i].subchunks[j].end)[1]);
                        }
                        for (int j = 0; j < chunks[i].subchunks.Count; j++)
                        {
                            output.AddRange(chunks[i].subchunks[j].data);
                        }
                    }
                    
                    return output;
                }
            }*/

            public int Count
            {
                get
                {
                    if (data != null)
                    {
                        return data.Count;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public struct BACChunk
        {
            public BACChunkType type;
            public List<BACSubChunk> subchunks;
        }

        public struct BACSubChunk
        {
            public UInt16 start;
            public UInt16 end;

            public List<Byte> data;
        }

        public struct BACHit
        {
            public const int STANDING = 0;
            public const int CROUCHING = 1;
            public const int JUMPING = 2;

            public const int HIT = 0;
            public const int BLOCKING = 3;
            public const int COUNTERHIT = 6;
            public const int UNKNOWN = 9;

            public const int SUBHIT_COUNT = 12;

            public Boolean bEmpty;
            public List<BACSubHit> subhits;
        }

        public struct BACSubHit
        {
            public List<byte> data;
        }

        public enum BACChunkType
        {
            ANIM = 1,
            UNK02 = 2,
            UNK03 = 3,
            UNK04 = 4,
            MOMENTUM = 5,
            CANCELLING = 6,
            HIT = 7,
            UNK08 = 8,
            HURT = 9,
            GFX = 10,
            UNK0B = 11,
            SOUND = 12,
            UNK0D = 13,
            UNK0E = 14,
            UNK0F = 15,
            UNK10 = 16,
        }

        public List<BACAnim> AnimList;
        public List<BACAnim> AnimList_b;
        public List<BACHit>  HitList;

        public int LongestAnim;
        public int ShortestAnim;

        public BACData(BinaryReader bReader)
        {
            //AELogger.Log(bReader.ReadInt64() + " " + bReader.ReadInt64(), true, true);
            Int64 header = bReader.ReadInt64();

            if (header != BAC_HEADER_A)
            {
                AELogger.Log(header.ToString() + " is not expected BAC header... error.");
                return;
            }
            header = bReader.ReadInt32();
            if (header != BAC_HEADER_B)
            {
                AELogger.Log(header.ToString() + " is not expected BAC header... error.");
                return;
            }

            bReader.BaseStream.Seek(12, SeekOrigin.Begin);
            UInt16 size_anim_list = bReader.ReadUInt16();
            AELogger.Log(size_anim_list + " size of anim list");

            bReader.BaseStream.Seek(14, SeekOrigin.Begin);
            UInt16 size_anim_b_list = bReader.ReadUInt16();
            AELogger.Log(size_anim_b_list + " size of anim b list");

            bReader.BaseStream.Seek(16, SeekOrigin.Begin);
            UInt16 size_hit_list = bReader.ReadUInt16();
            AELogger.Log(size_hit_list + " size of hit list");

            bReader.BaseStream.Seek(20, SeekOrigin.Begin);
            UInt32 addr_anim_table = bReader.ReadUInt32();
            AELogger.Log(addr_anim_table.ToString("X") + " address of animation table");

            bReader.BaseStream.Seek(24, SeekOrigin.Begin);
            UInt32 addr_anim_table_b = bReader.ReadUInt32();
            AELogger.Log(addr_anim_table_b.ToString("X") + " address of animation table b?");

            bReader.BaseStream.Seek(28, SeekOrigin.Begin);
            UInt32 addr_anim_name_table = bReader.ReadUInt32();
            AELogger.Log(addr_anim_name_table.ToString("X") + " address of animation name table");

            bReader.BaseStream.Seek(32, SeekOrigin.Begin);
            UInt32 addr_anim_name_table_b = bReader.ReadUInt32();
            AELogger.Log(addr_anim_name_table_b.ToString("X") + " address of animation name table b");

            bReader.BaseStream.Seek(36, SeekOrigin.Begin);
            UInt32 addr_hit_table = bReader.ReadUInt32();
            AELogger.Log(addr_hit_table.ToString("X") + " address of hit table");

            //672
            bReader.BaseStream.Seek(40, SeekOrigin.Begin);
            hitboxBytes = AEDataTools.SlurpBytes(bReader, BAC_HITBOX_SIZE);

            AnimList = new List<BACAnim>(size_anim_list);
            LongestAnim = 0;
            ShortestAnim = 4263493;
            for (UInt32 i = 0; i < size_anim_list; i++)
            {
                BACAnim b = new BACAnim();
                bReader.BaseStream.Seek(i * sizeof(UInt32) + addr_anim_table, SeekOrigin.Begin);
                UInt32 aAddr = bReader.ReadUInt32();
                UInt32 bAddr = bReader.ReadUInt32();
                while (bAddr == 0)
                {
                    bAddr = bReader.ReadUInt32();
                }

                if (aAddr != 0)
                {
                    bReader.BaseStream.Seek(aAddr, SeekOrigin.Begin);
                    b.bEmpty = false;
                    b.data = animFromBytes( AEDataTools.SlurpBytes(bReader, (int)(bAddr - aAddr)));
                    if ((int)(bAddr - aAddr) > LongestAnim)
                    {
                        LongestAnim = (int)(bAddr - aAddr);
                    }
                    if ((int)(bAddr - aAddr) < ShortestAnim)
                    {
                        ShortestAnim = (int)(bAddr - aAddr);
                    }

                    bReader.BaseStream.Seek(addr_anim_name_table + (sizeof(UInt32) * i), SeekOrigin.Begin);
                    UInt32 nameAddr = bReader.ReadUInt32();
                    bReader.BaseStream.Seek(nameAddr, SeekOrigin.Begin);
                    b.name = AEDataTools.SlurpString(bReader);
                    AELogger.Log(i.ToString("X4") + ". " + b.name);
                    AnimList.Add(b);
                }
                else
                {
                    b.bEmpty = true;
                    AELogger.Log(i.ToString("X4") + ". empty one");
                    AnimList.Add(b);
                }
            }
            bReader.BaseStream.Seek(addr_hit_table, SeekOrigin.Begin);
            UInt32 addr_hit_table_start = bReader.ReadUInt32();

            AnimList_b = new List<BACAnim>(size_anim_b_list);
            for (UInt32 i = 0; i < size_anim_b_list; i++)
            {
                BACAnim b = new BACAnim();
                bReader.BaseStream.Seek(i * sizeof(UInt32) + addr_anim_table_b, SeekOrigin.Begin);
                UInt32 aAddr = bReader.ReadUInt32();
                UInt32 bAddr = bReader.ReadUInt32();
                while (bAddr == 0)
                {
                    bAddr = bReader.ReadUInt32();
                }
                bAddr = Math.Min(bAddr, addr_hit_table_start);

                if (aAddr != 0)
                {
                    bReader.BaseStream.Seek(aAddr, SeekOrigin.Begin);
                    b.bEmpty = false;
                    b.data = animFromBytes(AEDataTools.SlurpBytes(bReader, (int)(bAddr - aAddr)));

                    bReader.BaseStream.Seek(addr_anim_name_table_b + (sizeof(UInt32) * (i)), SeekOrigin.Begin);
                    UInt32 nameAddr = bReader.ReadUInt32();
                    bReader.BaseStream.Seek(nameAddr, SeekOrigin.Begin);
                    b.name = AEDataTools.SlurpString(bReader);
                    AELogger.Log(i.ToString("X4") + "_b. " + b.name);
                    AnimList_b.Add(b);
                }
                else
                {
                    b.bEmpty = true;
                    AELogger.Log(i.ToString("X4") + "_b. empty one");
                    AnimList_b.Add(b);
                }
            }

            bReader.BaseStream.Seek(addr_anim_table, SeekOrigin.Begin);
            UInt32 addr_hit_table_end = bReader.ReadUInt32();

            HitList = new List<BACHit>(size_hit_list);
            for (UInt32 i = 0; i < size_hit_list; i++)
            {
                BACHit b = new BACHit();
                bReader.BaseStream.Seek(i * sizeof(UInt32) + addr_hit_table, SeekOrigin.Begin);
                UInt32 aAddr = bReader.ReadUInt32();

                if (aAddr != 0)
                {
                    bReader.BaseStream.Seek(aAddr, SeekOrigin.Begin);
                    b.bEmpty = false;
                    b.subhits = new List<BACSubHit>(BACHit.SUBHIT_COUNT);
                    for (int j = 0; j < BACHit.SUBHIT_COUNT; j++)
                    {
                        BACSubHit bs = new BACSubHit();
                        bs.data = AEDataTools.SlurpBytes(bReader, BAC_SUB_HIT_SIZE);
                        b.subhits.Add(bs);
                    }

                    HitList.Add(b);
                }
                else
                {
                    b.bEmpty = true;
                    HitList.Add(b);
                }
            }
            // hits
            // 80 per hit
            // standing, crouching, jumping hit
            // s c j block
            // s c j counterhit
            // 3 more?
            // 80 * 12 = 960


            /*
            bReader.BaseStream.Seek(, SeekOrigin.Begin);
            UInt32  = bReader.ReadUInt32();
            AELogger.Log(+"");
            */

            AELogger.Log(AnimList.Count + " is anim list size");
            AELogger.Log(AnimList_b.Count + " is anim list b size");
            AELogger.Log(HitList.Count + " is hit list size");
        }

        public BACAnimData animFromBytes(List<Byte> data)
        {
            BACAnimData a = new BACAnimData();
            a.data = data;
            return a;

            /*BACAnimData a = new BACAnimData();
            a.header = new List<byte>();
            for (int i = 0; i < 16; i++)
            {
                a.header.Add(data[i]);
            }
            
            int size_chunk_list = AEDataTools.UInt16FromData(data, 18);
            int ptr_chunk_list = (int)AEDataTools.UInt32FromData(data, 20);
            a.chunks = new List<BACChunk>(size_chunk_list);
            for (int i = 0; i < size_chunk_list; i++)
            {
                BACChunk c = new BACChunk();
                c.type = (BACChunkType)AEDataTools.UInt16FromData(data, ptr_chunk_list + (i * 12));
                int size_subchunk_list = (int)AEDataTools.UInt16FromData(data,ptr_chunk_list + (i * 12) + 2);
                c.subchunks = new List<BACSubChunk>(size_subchunk_list);
                int cur_chunk_start = (int)AEDataTools.UInt32FromData(data, ptr_chunk_list + (i * 12) + 4) + ptr_chunk_list + (i * 12);
                int cur_chunk_end = (int)AEDataTools.UInt32FromData(data, ptr_chunk_list + (i * 12) + 8) + ptr_chunk_list + (i * 12);
                int cur_chunk_data_end;
                if (i < size_subchunk_list - 1)
                {
                    cur_chunk_data_end = (int)AEDataTools.UInt32FromData(data, ptr_chunk_list + ((i + 1) * 12) + 4) + ptr_chunk_list + ((i + 1) * 12);
                }
                else
                {
                    cur_chunk_data_end = data.Count;
                }
                for (int j = 0; j < size_subchunk_list; j++)
                {
                    BACSubChunk s = new BACSubChunk();
                    AELogger.Log(cur_chunk_start + " " + cur_chunk_end + c.type);
                    s.start = AEDataTools.UInt16FromData(data, cur_chunk_start + j * 4);
                    s.end = AEDataTools.UInt16FromData(data, cur_chunk_start + j * 4 + 2);
                    s.data = data.GetRange(cur_chunk_end, Math.Min(data.Count - cur_chunk_end, (cur_chunk_data_end - cur_chunk_end) / size_subchunk_list));
                    c.subchunks.Add(s);
                }
                
                a.chunks.Add(c);
            }
            return a;*/
        }

        public bool WriteOutput(String filename)
        {
            Stream t = new FileStream(filename, FileMode.Create);
            BinaryWriter b = new BinaryWriter(t);
            b.Write(BAC_HEADER_A);
            b.Write(BAC_HEADER_B);
            b.Write((UInt16)AnimList.Count);
            b.Write((UInt16)AnimList_b.Count);
            b.Write((UInt16)HitList.Count);
            b.Write(ZERO_BYTE);
            b.Write(ZERO_BYTE);
            UInt32 currentPointer = (UInt32)BAC_HITBOX_SIZE + 40;
            b.Write(currentPointer);
            currentPointer += (UInt32)AnimList.Count * sizeof(UInt32);
            b.Write(currentPointer);
            currentPointer += (UInt32)AnimList_b.Count * sizeof(UInt32);
            b.Write(currentPointer);
            currentPointer += (UInt32)AnimList.Count * sizeof(UInt32);
            b.Write(currentPointer);
            currentPointer += (UInt32)AnimList_b.Count * sizeof(UInt32);
            b.Write(currentPointer);

            for (int i = 0; i < BAC_HITBOX_SIZE; i++)
            {
                b.Write(hitboxBytes[i]);
            }

            currentPointer += (UInt32)HitList.Count * sizeof(UInt32);
            for (int i = 0; i < AnimList.Count; i++)
            {
                if (AnimList[i].bEmpty)
                {
                    b.Write((UInt32)0);
                }
                else
                {
                    b.Write(currentPointer);
                    currentPointer += (UInt32)AnimList[i].data.Count;
                }
            }

            for (int i = 0; i < AnimList_b.Count; i++)
            {
                if (AnimList_b[i].bEmpty)
                {
                    b.Write((UInt32)0);
                }
                else
                {
                    b.Write(currentPointer);
                    currentPointer += (UInt32)AnimList_b[i].data.Count;
                }
            }

            AELogger.Log(currentPointer.ToString("X"));
            UInt32 hitStartPointer = currentPointer;

            // names are off by b7c0
            for (int i = 0; i < HitList.Count; i++)
            {
                if (!HitList[i].bEmpty)
                {
                    currentPointer += (UInt32)BAC_HIT_SIZE;
                }
            }

            // anim name table
            for (int i = 0; i < AnimList.Count; i++)
            {
                if (AnimList[i].bEmpty)
                {
                    b.Write((UInt32)0);
                }
                else
                {
                    b.Write(currentPointer);
                    currentPointer += (UInt32)AnimList[i].name.Length + 1;
                }
            }

            for (int i = 0; i < AnimList_b.Count; i++)
            {
                if (AnimList_b[i].bEmpty)
                {
                    b.Write((UInt32)0);
                }
                else
                {
                    b.Write(currentPointer);
                    currentPointer += (UInt32)AnimList_b[i].name.Length + 1;
                }
            }

            // hit table
            currentPointer = hitStartPointer;
            for (int i = 0; i < HitList.Count; i++)
            {
                if (HitList[i].bEmpty)
                {
                    b.Write((UInt32)0);
                }
                else
                {
                    b.Write(currentPointer);
                    currentPointer += (UInt32)BAC_HIT_SIZE;
                }
            }

            for (int i = 0; i < AnimList.Count; i++)
            {
                WriteAnim(AnimList[i], b);
            }

            for (int i = 0; i < AnimList_b.Count; i++)
            {
                WriteAnim(AnimList_b[i], b);
            }

            for (int i = 0; i < HitList.Count; i++)
            {
                WriteHit(HitList[i], b);
            }

            for (int i = 0; i < AnimList.Count; i++)
            {
                if (!AnimList[i].bEmpty)
                {
                    b.Write(Encoding.Default.GetBytes(AnimList[i].name));
                    b.Write(ZERO_BYTE);
                }
            }

            for (int i = 0; i < AnimList_b.Count; i++)
            {
                if (!AnimList_b[i].bEmpty)
                {
                    b.Write(Encoding.Default.GetBytes(AnimList_b[i].name));
                    b.Write(ZERO_BYTE);
                }
            }

            b.Close();
            t.Close();

            return true;
        }

        public void WriteAnim(BACAnim anim, BinaryWriter b)
        {
            if (anim.bEmpty)
            {
                return;
            }
            else
            {
                for (int i = 0; i < anim.data.Count; i++)
                {
                    b.Write(anim.data.data[i]);
                }
            }
        }

        public void WriteHit(BACHit hit, BinaryWriter b)
        {
            if (hit.bEmpty)
            {
                return;
            }
            else
            {
                for (int i = 0; i < hit.subhits.Count; i++)
                {
                    for (int j = 0; j < hit.subhits[i].data.Count; j++)
                    {
                        b.Write(hit.subhits[i].data[j]);
                    }
                }
            }
        }

        public List<String> GetAnimList()
        {
            List<String> output = new List<String>();
            for (int i = 0; i < AnimList.Count; i++)
            {
                if (AnimList[i].bEmpty)
                {
                    output.Add(i.ToString("X2") + ". *empty*");
                }
                else
                {
                    output.Add(i.ToString("X2") + ". " + AnimList[i].name);
                }
            }

            for (int i = 0; i < AnimList_b.Count; i++)
            {
                if (AnimList_b[i].bEmpty)
                {
                    output.Add(i.ToString("X2") + "-proj. *empty*");
                }
                else
                {
                    output.Add(i.ToString("X2") + "-proj. " + AnimList_b[i].name);
                }
            }
            return output;
        }

        public BACAnim GetAnimAt(int i)
        {
            if (i >= AnimList.Count)
            {
                return AnimList_b[i - AnimList.Count];
            }
            else
            {
                return AnimList[i];
            }
        }

        public void ExportAnimAt(int i, string filename)
        {
            Stream t = new FileStream(filename, FileMode.Create);
            BinaryWriter b = new BinaryWriter(t);
            WriteAnim(GetAnimAt(i), b);
            b.Close();
            t.Close();
        }

        public void RenameAnimAt(int i, string a)
        {
            BACAnim anim;
            if (i >= AnimList.Count)
            {
                anim = AnimList_b[i - AnimList.Count];
                anim.name = a;
                AnimList_b[i - AnimList.Count] = anim;
            }
            else
            {
                anim = AnimList[i];
                anim.name = a;
                AnimList[i] = anim;
            }
        }

        public void ImportAnimAt(BinaryReader b, string name, int i)
        {
            BACAnim a = new BACAnim();
            a.bEmpty = false;
            a.data = animFromBytes(AEDataTools.SlurpBytes(b, (int)b.BaseStream.Length));
            a.name = name;
            if (i >= AnimList.Count)
            {
                AnimList_b[i - AnimList.Count] = a;
            }
            else
            {
                AnimList[i] = a;
            }
        }

        public void RemoveAnimAt(int i)
        {
            BACAnim anim;
            if (i >= AnimList.Count)
            {
                anim = AnimList_b[i - AnimList.Count];
                anim.bEmpty = true;
                AnimList_b[i - AnimList.Count] = anim;
            }
            else
            {
                anim = AnimList[i];
                anim.bEmpty = true;
                AnimList[i] = anim;
            }
        }

        public void ExtendAnimList(int i)
        {
            BACAnim anim = new BACAnim();
            anim.bEmpty = true;
            if (i >= AnimList.Count)
            {
                AnimList_b.Add(anim);
            }
            else
            {
                AnimList.Add(anim);
            }
        }
    }
}

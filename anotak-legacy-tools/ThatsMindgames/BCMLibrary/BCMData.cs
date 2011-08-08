using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AEDataLibrary
{
    public class BCMData
    {
        public const Int64 BCM_HEADER_A = 11540466751455779;
        public const Int64 BCM_HEADER_B = 65537;
        public const int BCM_MOTION_SIZE = 196;
        public const int BCM_INPUT_SIZE = 84;
        public const int BCM_UNKNOWN_SIZE = 16;
        public const byte ZERO_BYTE = 0;

        public struct BCMMotion
        {
            public string name;
            public List<byte> data;
        }

        public struct BCMInput
        {
            public string name;
            public List<byte> data;

            public List<Boolean> Flags
            {
                get
                {
                    List<Boolean> a = AEDataTools.GetBits(data[0]);
                    a.AddRange(AEDataTools.GetBits(data[1]));
                    a.AddRange(AEDataTools.GetBits(data[2]));
                    a.AddRange(AEDataTools.GetBits(data[3]));
                    return a;
                }
                set
                {
                    data[0] = AEDataTools.GetByteFromBits(value, 0);
                    data[1] = AEDataTools.GetByteFromBits(value, 8);
                    data[2] = AEDataTools.GetByteFromBits(value, 16);
                    data[3] = AEDataTools.GetByteFromBits(value, 24);
                }
            }
            public UInt16 ButtonsRequiredUInt16
            {
                set
                {
                    data[0] = BitConverter.GetBytes(value)[1];
                    data[1] = BitConverter.GetBytes(value)[0];
                }
            }
            public byte CloseFar
            {
                get { return data[4]; }
                set { data[4] = value; }
            }
            public float RestrictionDistance
            {
                get
                {
                    byte[] output = new byte[4];
                    output[0] = data[20];
                    output[1] = data[21];
                    output[2] = data[22];
                    output[3] = data[23];
                    return BitConverter.ToSingle(output, 0);
                }
                set
                {
                    data[20] = BitConverter.GetBytes(value)[0];
                    data[21] = BitConverter.GetBytes(value)[1];
                    data[22] = BitConverter.GetBytes(value)[2];
                    data[23] = BitConverter.GetBytes(value)[3];
                }
            }
            public Int16 SuperMeterRequired
            {
                get
                {
                    byte[] output = new byte[2];
                    output[0] = data[24];
                    output[1] = data[25];
                    return BitConverter.ToInt16(output,0);
                }
                set
                {
                    data[24] = BitConverter.GetBytes(value)[0];
                    data[25] = BitConverter.GetBytes(value)[1];
                }
            }
            public Int16 SuperMeterUsed
            {
                get
                {
                    byte[] output = new byte[2];
                    output[0] = data[26];
                    output[1] = data[27];
                    return BitConverter.ToInt16(output, 0);
                }
                set
                {
                    data[26] = BitConverter.GetBytes(value)[0];
                    data[27] = BitConverter.GetBytes(value)[1];
                }
            }
            public Int16 UltraMeterRequired
            {
                get
                {
                    byte[] output = new byte[2];
                    output[0] = data[28];
                    output[1] = data[29];
                    return BitConverter.ToInt16(output, 0);
                }
                set
                {
                    data[28] = BitConverter.GetBytes(value)[0];
                    data[29] = BitConverter.GetBytes(value)[1];
                }
            }
            public Int16 UltraMeterUsed
            {
                get
                {
                    byte[] output = new byte[2];
                    output[0] = data[30];
                    output[1] = data[31];
                    return BitConverter.ToInt16(output, 0);
                }
                set
                {
                    data[30] = BitConverter.GetBytes(value)[0];
                    data[31] = BitConverter.GetBytes(value)[1];
                }
            }

            public byte Motion
            {
                get
                {
                    return data[32];
                }
                set
                {
                    data[32] = value;

                    if (value == 255)
                    {
                        data[33] = 255;
                        data[34] = 255;
                        data[35] = 255;
                    }
                    else
                    {
                        data[33] = 0;
                        data[34] = 0;
                        data[35] = 0;
                    }
                }
            }

            public UInt32 Animation
            {
                get
                {
                    return AEDataTools.UInt32FromData(data, 36);
                }
                set
                {
                    data[36] = BitConverter.GetBytes(value)[0];
                    data[37] = BitConverter.GetBytes(value)[1];
                    data[38] = BitConverter.GetBytes(value)[2];
                    data[39] = BitConverter.GetBytes(value)[3];
                }
            }

            public float UnknownFloat44
            {
                get
                {
                    byte[] output = new byte[4];
                    output[0] = data[44];
                    output[1] = data[45];
                    output[2] = data[46];
                    output[3] = data[47];
                    return BitConverter.ToSingle(output, 0);
                }
                set
                {
                    data[44] = BitConverter.GetBytes(value)[0];
                    data[45] = BitConverter.GetBytes(value)[1];
                    data[46] = BitConverter.GetBytes(value)[2];
                    data[47] = BitConverter.GetBytes(value)[3];
                }
            }
            public float UnknownFloat48
            {
                get
                {
                    byte[] output = new byte[4];
                    output[0] = data[48];
                    output[1] = data[49];
                    output[2] = data[50];
                    output[3] = data[51];
                    return BitConverter.ToSingle(output, 0);
                }
                set
                {
                    data[48] = BitConverter.GetBytes(value)[0];
                    data[49] = BitConverter.GetBytes(value)[1];
                    data[50] = BitConverter.GetBytes(value)[2];
                    data[51] = BitConverter.GetBytes(value)[3];
                }
            }
        }

        public struct BCMCancel
        {
            public string name;
            public List<UInt16> list;
        }

        public struct BCMUnknown
        {
            public List<Byte> data;
            public String name;
        }

        public List<BCMMotion> MotionList;
        public List<BCMInput> InputList;
        public List<BCMCancel> CancelList;
        public List<BCMUnknown> UnknownList;

        public BCMData(BinaryReader bReader)
        {
            Int64 header = bReader.ReadInt64();

            if (header != BCM_HEADER_A)
            {
                Log(header.ToString() + " is not expected BCM header... error.");
                return;
            }
            header = bReader.ReadInt64();
            if (header != BCM_HEADER_B)
            {
                Log(header.ToString() + " is not expected BCM header... error.");
                return;
            }

            bReader.BaseStream.Seek(16, SeekOrigin.Begin);
            UInt16 size_unknown_list = bReader.ReadUInt16();
            Log(size_unknown_list + " size of unknown list");

            bReader.BaseStream.Seek(18, SeekOrigin.Begin);
            UInt16 size_motion_list = bReader.ReadUInt16();
            Log(size_motion_list + " size of motion list");

            bReader.BaseStream.Seek(20, SeekOrigin.Begin);
            UInt16 size_input_list = bReader.ReadUInt16();
            Log(size_input_list + " size of input list");

            bReader.BaseStream.Seek(22, SeekOrigin.Begin);
            UInt16 size_cancel_list = bReader.ReadUInt16();
            Log(size_cancel_list + " size of cancel list of lists");


            bReader.BaseStream.Seek(24, SeekOrigin.Begin);
            UInt32 addr_unknown_list = bReader.ReadUInt32();
            Log(addr_unknown_list + " pointer to unknown list");

            bReader.BaseStream.Seek(28, SeekOrigin.Begin);
            UInt32 addr_unknown_name_table = bReader.ReadUInt32();
            Log(addr_unknown_name_table + " pointer to unknown name table");

            bReader.BaseStream.Seek(32, SeekOrigin.Begin);
            UInt32 addr_motion_list = bReader.ReadUInt32();
            Log(addr_motion_list + " pointer to motion list");

            bReader.BaseStream.Seek(36, SeekOrigin.Begin);
            UInt32 addr_motion_name_table = bReader.ReadUInt32();
            Log(addr_motion_name_table + " pointer to motion name table");

            bReader.BaseStream.Seek(40, SeekOrigin.Begin);
            UInt32 addr_input_list = bReader.ReadUInt32();
            Log(addr_input_list + " pointer to inputs");

            bReader.BaseStream.Seek(44, SeekOrigin.Begin);
            UInt32 addr_input_name_table = bReader.ReadUInt32();
            Log(addr_input_name_table + " pointer to input name table");

            bReader.BaseStream.Seek(48, SeekOrigin.Begin);
            UInt32 addr_cancel_list = bReader.ReadUInt32();
            Log(addr_cancel_list + " pointer to cancel table");

            bReader.BaseStream.Seek(52, SeekOrigin.Begin);
            UInt32 addr_cancel_name_table = bReader.ReadUInt32();
            Log(addr_cancel_name_table + " pointer to cancel name table");

            // unknownlist
            UnknownList = new List<BCMUnknown>(size_unknown_list);
            for (int i = 0; i < size_unknown_list; i++)
            {
                BCMUnknown b = new BCMUnknown();
                bReader.BaseStream.Seek(addr_unknown_list + (BCM_UNKNOWN_SIZE * i), SeekOrigin.Begin);
                b.data = AEDataTools.SlurpBytes(bReader, BCM_UNKNOWN_SIZE);
                bReader.BaseStream.Seek(addr_unknown_name_table + (sizeof(UInt32) * i), SeekOrigin.Begin);
                UInt32 nameAddr = bReader.ReadUInt32();
                bReader.BaseStream.Seek(nameAddr, SeekOrigin.Begin);
                b.name = AEDataTools.SlurpString(bReader);
                Log(i + ". " + b.name);
                UnknownList.Add(b);
            }

            // motionlist
            MotionList = new List<BCMMotion>(size_motion_list);
            for (int i = 0; i < size_motion_list; i++)
            {
                BCMMotion b = new BCMMotion();
                bReader.BaseStream.Seek(addr_motion_list + (BCM_MOTION_SIZE * i), SeekOrigin.Begin);
                b.data = AEDataTools.SlurpBytes(bReader, BCM_MOTION_SIZE);
                //Log(b.data[0] + " and " + b.data[BCM_MOTION_SIZE-1]);
                bReader.BaseStream.Seek(addr_motion_name_table + (sizeof(UInt32) * i), SeekOrigin.Begin);
                UInt32 nameAddr = bReader.ReadUInt32();
                bReader.BaseStream.Seek(nameAddr, SeekOrigin.Begin);
                b.name = AEDataTools.SlurpString(bReader);
                Log(i + ". " + b.name);
                MotionList.Add(b);
            }

            // inputlist

            InputList = new List<BCMInput>(size_input_list);
            for (int i = 0; i < size_input_list; i++)
            {
                BCMInput b = new BCMInput();
                bReader.BaseStream.Seek(addr_input_list + (BCM_INPUT_SIZE * i), SeekOrigin.Begin);
                b.data = AEDataTools.SlurpBytes(bReader, BCM_INPUT_SIZE);
                bReader.BaseStream.Seek(addr_input_name_table + (sizeof(UInt32) * i), SeekOrigin.Begin);
                UInt32 nameAddr = bReader.ReadUInt32();
                bReader.BaseStream.Seek(nameAddr, SeekOrigin.Begin);
                b.name = AEDataTools.SlurpString(bReader);
                Log(i + ". " + b.name);
                InputList.Add(b);
            }

            // cancellist

            CancelList = new List<BCMCancel>(size_cancel_list);

            for (int i = 0; i < size_cancel_list; i++)
            {
                BCMCancel b = new BCMCancel();
                bReader.BaseStream.Seek(addr_cancel_list + (8 * i), SeekOrigin.Begin);
                UInt32 cSize = bReader.ReadUInt32();
                UInt32 cAddr = bReader.ReadUInt32();
                bReader.BaseStream.Seek(addr_cancel_list + (8 * i) + cAddr, SeekOrigin.Begin);
                b.list = AEDataTools.SlurpUInt16(bReader, (int)(cSize * 2));

                bReader.BaseStream.Seek(addr_cancel_name_table + (sizeof(UInt32) * i), SeekOrigin.Begin);
                UInt32 nameAddr = bReader.ReadUInt32();
                bReader.BaseStream.Seek(nameAddr, SeekOrigin.Begin);
                b.name = AEDataTools.SlurpString(bReader);
                Log(i + ". " + b.name);
                CancelList.Add(b);
            }
        }

        public bool WriteOutput(String filename)
        {
            Stream t = new FileStream(filename, FileMode.Create);
            BinaryWriter b = new BinaryWriter(t);

            b.Write(BCM_HEADER_A);
            b.Write(BCM_HEADER_B);
            b.Write((UInt16)UnknownList.Count);
            b.Write((UInt16)MotionList.Count);
            b.Write((UInt16)InputList.Count);
            b.Write((UInt16)CancelList.Count);

            

            
            /* 56 bytes header
             * (UInt16)(MotionList.Count * BCM_MOTION_SIZE)
             * (UInt16)(MotionList.Count * sizeof(UInt32))
             * (UInt16)(InputList.Count * BCM_INPUT_SIZE)
             * (UInt16)(InputList.Count * sizeof(UInt32))
             * (UInt16)(CancelList.Count * sizeof(UInt16) * 2)
             * (UInt16)(CancelList.Count * sizeof(UInt32))
             */
            UInt32 currentPointer = 56;
            if (UnknownList.Count == 0)
            {
                for (int i = 0; i < 8; i++)
                    b.Write(ZERO_BYTE);
            }
            else
            {
                b.Write(currentPointer);
                currentPointer += (UInt32)(UnknownList.Count * BCM_UNKNOWN_SIZE);
                b.Write(currentPointer);
                currentPointer += (UInt32)(UnknownList.Count * sizeof(UInt32));
            }
            b.Write(currentPointer);
            currentPointer += (UInt32)(MotionList.Count * BCM_MOTION_SIZE);
            b.Write(currentPointer);
            currentPointer += (UInt32)(MotionList.Count * sizeof(UInt32));
            b.Write(currentPointer);
            currentPointer += (UInt32)(InputList.Count * BCM_INPUT_SIZE);
            b.Write(currentPointer);
            currentPointer += (UInt32)(InputList.Count * sizeof(UInt32));
            b.Write(currentPointer);
            currentPointer += (UInt32)(CancelList.Count * sizeof(UInt32) * 2);
            b.Write(currentPointer);


            for (int i = 0; i < UnknownList.Count; i++)
            {
                WriteUnknown(UnknownList[i], b);
            }

            // okay here it gets interesting
            // we have to point to past unconstructed cancel list output so we get the length of that now
            UInt32 cancel_list_total_count = 0;
            for (int i = 0; i < CancelList.Count; i++)
            {
                cancel_list_total_count += (UInt32)CancelList[i].list.Count * 2;
            }
            currentPointer += (UInt16)(CancelList.Count * sizeof(UInt32));
            currentPointer += cancel_list_total_count;

            for (int i = 0; i < UnknownList.Count; i++)
            {
                b.Write(currentPointer);
                currentPointer += (UInt32)UnknownList[i].name.Length + 1;
            }

            for (int i = 0; i < MotionList.Count; i++)
            {
                WriteMotion(MotionList[i], b);
            }

            for (int i = 0; i < MotionList.Count; i++)
            {
                b.Write(currentPointer);
                currentPointer += (UInt32)MotionList[i].name.Length + 1;
            }

            for (int i = 0; i < InputList.Count; i++)
            {
                WriteInput(InputList[i], b);
            }

            for (int i = 0; i < InputList.Count; i++)
            {
                b.Write(currentPointer);
                currentPointer += (UInt32)InputList[i].name.Length + 1;
            }

            UInt32 cancelPointer = (UInt32)CancelList.Count * 12;
            for (int i = 0; i < CancelList.Count; i++)
            {
                b.Write((UInt32)CancelList[i].list.Count);
                b.Write(cancelPointer);
                cancelPointer += (UInt32)CancelList[i].list.Count * 2 - 8;
            }

            for (int i = 0; i < CancelList.Count; i++)
            {
                b.Write(currentPointer);
                currentPointer += (UInt32)CancelList[i].name.Length + 1;
            }

            for (int i = 0; i < CancelList.Count; i++)
            {
                for (int j = 0; j < CancelList[i].list.Count; j++)
                {
                    b.Write(CancelList[i].list[j]);
                }
            }

            for (int i = 0; i < UnknownList.Count; i++)
            {
                b.Write(Encoding.Default.GetBytes(UnknownList[i].name));
                b.Write(ZERO_BYTE);
            }

            for (int i = 0; i < MotionList.Count; i++)
            {
                b.Write(Encoding.Default.GetBytes(MotionList[i].name));
                b.Write(ZERO_BYTE);
            }

            for (int i = 0; i < InputList.Count; i++)
            {
                b.Write(Encoding.Default.GetBytes(InputList[i].name));
                b.Write(ZERO_BYTE);
            }

            for (int i = 0; i < CancelList.Count; i++)
            {
                b.Write(Encoding.Default.GetBytes(CancelList[i].name));
                b.Write(ZERO_BYTE);
            }

            b.Close();
            t.Close();

            return true;
        }

        public void WriteMotion(BCMMotion motion, BinaryWriter b)
        {
            for (int i = 0; i < motion.data.Count; i++)
            {
                b.Write(motion.data[i]);
            }
        }

        public void WriteUnknown(BCMUnknown unknown, BinaryWriter b)
        {
            for (int i = 0; i < unknown.data.Count; i++)
            {
                b.Write(unknown.data[i]);
            }
        }

        public void WriteInput(BCMInput input, BinaryWriter b)
        {
            for (int i = 0; i < input.data.Count; i++)
            {
                b.Write(input.data[i]);
            }
        }
        

        public List<String> GetInputList()
        {
            List<String> output = new List<String>();
            for (int i = 0; i < InputList.Count; i++)
            {
                output.Add(i.ToString("X2") + ". " + InputList[i].name);
            }
            return output;
        }

        public List<String> GetCancelList()
        {
            List<String> output = new List<String>();
            for (int i = 0; i < CancelList.Count; i++)
            {
                output.Add(i.ToString("X2") + ". " + CancelList[i].name);
            }
            return output;
        }

        public List<String> GetCurrentList(int current)
        {
            List<String> output = new List<String>();
            if (current >= CancelList.Count || current < 0)
            {
                return output;
            }
            for (int i = 0; i < CancelList[current].list.Count; i++)
            {
                output.Add(CancelList[current].list[i].ToString("X2") + ". " + InputList[CancelList[current].list[i]].name);
            }
            return output;
        }

        public static void Log(String output)
        {
            AELogger.Log("BCMData : " + output);
        }

        public void AddCancelGroup(String name)
        {
            BCMCancel c = new BCMCancel();
            c.list = new List<ushort>();
            c.name = name;
            CancelList.Add(c);
        }

        public void AddCancel(int list, int move)
        {
            BCMCancel c = CancelList[list];
            c.list.Add((ushort)move);
        }

        public void RemoveCancel(int list, int move)
        {
            BCMCancel c = CancelList[list];
            c.list.RemoveAt(move);
        }

        public void RenameCancel(int list, String newname)
        {
            BCMCancel c = CancelList[list];
            c.name = newname;
            CancelList[list] = c;
        }

        public void RemoveCancelGroup(int list)
        {
            CancelList.RemoveAt(list);
        }
        public void RemoveInput(int list)
        {
            InputList.RemoveAt(list);
        }

        public void RenameInput(int list, String newname)
        {
            BCMInput c = InputList[list];
            c.name = newname;
            InputList[list] = c;
        }

        public void DuplicateInput(int index, String newname)
        {
            BCMInput o = new BCMInput();
            o.data = new List<byte>();
            o.data.AddRange(InputList[index].data);
            o.name = newname;
            InputList.Add(o);
        }

        
        /*
        public static UInt16 GetUInt16(BinaryReader b)
        {
            byte[] input = new byte[2];
            input[0] = b.ReadByte();
            input[1] = b.ReadByte();
            return BitConverter.ToUInt16(input,0);
        }

        public static UInt32 GetUInt32(BinaryReader b)
        {
            byte[] input = new byte[4];
            input[0] = b.ReadByte();
            input[1] = b.ReadByte();
            input[2] = b.ReadByte();
            input[3] = b.ReadByte();
            return BitConverter.ToUInt32(input, 0);
        }
        */
    }
}

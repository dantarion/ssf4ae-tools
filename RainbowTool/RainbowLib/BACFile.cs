using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using RainbowLib.BAC;
namespace RainbowLib
{
    public class BACFile
    {

        private ObservableCollection<ObservableCollection<float>> _UnknownFloatData = new ObservableCollection<ObservableCollection<float>>();
        public ObservableCollection<ObservableCollection<float>> UnknownFloatData
        {
            get { return _UnknownFloatData; }
        }
        private ObservableCollection<Script> _Scripts = new ObservableCollection<Script>();
        public ObservableCollection<Script> Scripts
        {
            get { return _Scripts; }
            internal set { _Scripts = value; }
        }
        private ObservableCollection<Script> _VFXScripts = new ObservableCollection<Script>();
        public ObservableCollection<Script> VFXScripts
        {
            get { return _VFXScripts; }
            internal set { _VFXScripts = value; }
        }
        private ObservableCollection<HitBoxDataset> _HitboxTable = new ObservableCollection<HitBoxDataset>();
        public ObservableCollection<HitBoxDataset> HitboxTable
        {
            get { return _HitboxTable; }
        }
        #region Reading Functions
        public static BACFile FromFilename(string name, BCMFile bcm)
        {
            using (var fs = File.OpenRead(name))
            using (var tracker = new TrackingStream(fs))
            using (var inFile = new BinaryReader(tracker))
            {
                AELogger.Log(AELogger.O_SEPARATOR, false);
                tracker.SetLabel("Header");

                if (new String(inFile.ReadChars(4)) != "#BAC")
                    throw new IOException("Not a valid BAC file");
                tracker.IgnoreBytes(8);
                inFile.BaseStream.Seek(12);

                ushort ScriptCount = inFile.ReadUInt16();
                ushort VFXScriptCount = inFile.ReadUInt16();
                uint HitboxTableSize = inFile.ReadUInt32();
                int ScriptOffset = inFile.ReadInt32();
                int VFXScriptOffset = inFile.ReadInt32();
                int ScriptNameOffset = inFile.ReadInt32();
                int VFXScriptNameOffset = inFile.ReadInt32();
                int HitboxTableOffset = inFile.ReadInt32();
                
                AELogger.Log("Header done, reading floats");

                var bac = new BACFile();
                for (int i = 0; i < 0x1c; i++)
                {
                    var list = new ObservableCollection<float>();
                    for (int j = 0; j < 6; j++)
                    {
                        list.Add(inFile.ReadSingle());
                    }
                    bac.UnknownFloatData.Add(list);
                }
                AELogger.Log("floats done, reading scripts");

                for (int i = 0; i < ScriptCount; i++)
                    bac.Scripts.Add(new Script(i));
                for (int i = 0; i < VFXScriptCount; i++)
                    bac.VFXScripts.Add(new Script(i));
                for (int i = 0; i < HitboxTableSize; i++)
                    bac.HitboxTable.Add(new HitBoxDataset(i));

                //Link BCM
                if (bcm != null)
                {
                    foreach (BCM.Move move in bcm.Moves)
                        if (move.ScriptIndex != -1)
                            move.Script = bac.Scripts[move.ScriptIndex];
                }
                //Read Scripts
                readScripts(inFile, bac.Scripts, bcm, ScriptCount, ScriptOffset, ScriptNameOffset, bac);
                readScripts(inFile, bac.VFXScripts, bcm, VFXScriptCount, VFXScriptOffset, VFXScriptNameOffset, bac);
                AELogger.Log("scripts done, reading hitboxtable");
                //Read Hitbox Table
                for (int i = 0; i < HitboxTableSize; i++)
                {
                    tracker.SetLabel("HitboxTable#" + i.ToString());
                    inFile.BaseStream.Seek(HitboxTableOffset + i*4);
                    inFile.BaseStream.Seek(inFile.ReadUInt32());
                    if (inFile.BaseStream.Position == 0)
                    {
                        continue;
                    }
                    var dataset = bac.HitboxTable[i];
                    for (int j = 0; j < 12; j++)
                    {
                        HitBoxData data = new HitBoxData(j);
                        dataset.Data.Add(data);
                        data.Damage = inFile.ReadInt16();
                        data.Stun = inFile.ReadInt16();
                        data.Effect = (HitBoxData.HitBoxEffect)inFile.ReadUInt16();
                        Util.LogUnkEnum(data.Effect, "Hitbox #" + dataset.Index, "hitboxdataset", j);

                        var index = inFile.ReadInt16() + HitBoxData.getIndexOffset(data.Effect);
                        if (index > -1)
                            data.OnHit = bac.Scripts.Where(x => x.Index == index).First();
                        else data.OnHit = new Script(index);
                        data.AttackerHitstop = inFile.ReadUInt16();
                        data.AttackerShaking = inFile.ReadUInt16();
                        data.VictimHitstop = inFile.ReadUInt16();
                        data.VictimShaking = inFile.ReadUInt16();
                        data.HitGFX = inFile.ReadInt16();
                        data.Unknown1 = inFile.ReadInt32();
                        data.Unknown2_1 = inFile.ReadInt16();
                        data.Unknown2_2 = inFile.ReadInt16();
                        data.HitSound = inFile.ReadUInt16();
                        data.Unknown4 = inFile.ReadInt16();
                        data.PainSound = inFile.ReadInt16();
                        data.HitSFX = inFile.ReadInt16();
                        data.HitSFX2 = inFile.ReadInt16();
                        data.VictimSFX = inFile.ReadInt16();

                        data.ArcadeScore = inFile.ReadUInt16();
                        data.AtkMeter = inFile.ReadUInt16();
                        data.VctmMeter = inFile.ReadUInt16();
                        data.JuggleStart = inFile.ReadInt16();
                        data.AnimTime = inFile.ReadInt16();
                        data.ForceUnknown2 = inFile.ReadInt32();
                        data.ForceX = inFile.ReadSingle();
                        data.ForceY = inFile.ReadSingle();

                        data.ForceUnknown3 = inFile.ReadSingle();
                        data.ForceXAcceleration = inFile.ReadSingle();
                        data.ForceYAcceleration = inFile.ReadSingle();
                        data.ForceUnknown4 = inFile.ReadSingle();
                        data.ForceUnknown5 = inFile.ReadSingle();
                    }
                }
                AELogger.Log("hitbox done");
                List<HitBoxDataset> nullHitboxData = bac.HitboxTable.Where(x => x.Data.Count() == 0).ToList();
                foreach (HitBoxDataset tmp in nullHitboxData)
                    bac.HitboxTable.Remove(tmp);
                FilterScripts(bac.Scripts);
                FilterScripts(bac.VFXScripts);
                AELogger.Log(AELogger.O_SEPARATOR, false);
                
                return bac;
            }
        }
        private static void readScripts(BinaryReader inFile, ObservableCollection<Script> list, BCMFile bcm, int Count, int Offset, int NameOffset, BACFile bac)
        {
            for (int i = 0; i < Count; i++)
            {
                (inFile.BaseStream as TrackingStream).SetLabel("Script#" + i.ToString());

                inFile.BaseStream.Seek(NameOffset + (i) * 4);
                var stringOffset = inFile.ReadUInt32();

                inFile.BaseStream.Seek(Offset + (i) * 4);
                var dataOffset = inFile.ReadUInt32();
                if (dataOffset == 0)
                    //This is a null entry in the table, we remove it later after reading all the Scripts
                    continue;

                //Read in the Name
                if (stringOffset != 0)
                {
                    inFile.BaseStream.Seek(stringOffset);
                    list[i].Name = inFile.ReadCString();
                }
                else
                    list[i].Name = "NULL";
                //Read in the script!
                inFile.BaseStream.Seek(dataOffset);
                readScript(inFile, list[i], bcm, bac);
            }
        }

        private static void FilterScripts(ObservableCollection<Script> list)
        {
            //Remove scripts that had 0 offset
            var filter = list.Where(x => x.Name == null).ToList();
            foreach (Script s in filter)
            {
                list.Remove(s);
            }
            //This nullscript is used so that in the UI, 
            //the user can select "NONE" when asked to pick a script for certain things
            list.Insert(0, Script.NullScript);
        }
        private static void readScript(BinaryReader inFile, Script script, BCMFile bcm, BACFile bac)
        {

            script.FirstHitboxFrame = inFile.ReadUInt16();
            script.LastHitboxFrame = inFile.ReadUInt16();
            script.IASAFrame = inFile.ReadUInt16();
            script.TotalFrames = inFile.ReadUInt16();

            script.UnknownFlags1 = inFile.ReadUInt32();
            script.UnknownFlags2 = inFile.ReadUInt32();
            script.UnknownFlags3 = inFile.ReadUInt16();
            ushort CommandListCount = inFile.ReadUInt16();
            int HeaderSize = inFile.ReadInt32();
            var baseoff = inFile.BaseStream.Position;
            for (int j = 0; j < CommandListCount; j++)
            {
                inFile.BaseStream.Seek(baseoff + 12 * j);
                ushort type = inFile.ReadUInt16();
                var cl = CommandListFactory.ByType((CommandListType)type);
                cl.Type = (CommandListType)type;
                int cnt = inFile.ReadUInt16();
                for (int o = 0; o < cnt; o++)
                {
                    cl.Add(cl.GenerateCommand());
                }
                int frameoff = inFile.ReadInt32();
                int dataoff = inFile.ReadInt32();
                inFile.BaseStream.Seek(baseoff + frameoff + 12 * j);
                foreach (BaseCommand cmd in cl)
                {
                    cmd.StartFrame = inFile.ReadUInt16();
                    cmd.EndFrame = inFile.ReadUInt16();
                }
                inFile.BaseStream.Seek(baseoff + dataoff + 12 * j);
                foreach (BaseCommand cmd in cl)
                {
                    switch ((CommandListType)cl.Type)
                    {
                        case CommandListType.FLOW:
                            (cmd as FlowCommand).Type = (FlowCommand.FlowType)inFile.ReadInt32();
                            Util.LogUnkEnum((cmd as FlowCommand).Type, "script", script.Name);
                            var index = inFile.ReadInt16();
                            if (index != -1)
                                (cmd as FlowCommand).TargetScript = bac.Scripts[index];
                            else
                                (cmd as FlowCommand).TargetScript = Script.NullScript;
                            (cmd as FlowCommand).TargetFrame = inFile.ReadInt16();
                            break;
                        case CommandListType.ANIMATION:
                            var ani = cmd as AnimationCommand;
                            ani.Animation = inFile.ReadInt16();
                            ani.Type = (AnimationCommand.AnimationType)inFile.ReadByte();
                            Util.LogUnkEnum(ani.Type, "script", script.Name);
                            ani.Flags = (AnimationCommand.AnimationFlags)inFile.ReadByte();
                            Util.LogUnkEnum(ani.Flags, "script", script.Name);
                            ani.FromFrame = inFile.ReadInt16();
                            ani.ToFrame = inFile.ReadInt16();
                            break;
                        case CommandListType.TRANSITION:
                            var transition = (TransitionCommand)cmd;
                            transition.Flag1 = inFile.ReadUInt16();
                            transition.Flag2 = inFile.ReadUInt16();
                            transition.Float1 = inFile.ReadSingle();
                            transition.Float2 = inFile.ReadSingle();
                            transition.Float3 = inFile.ReadSingle();
                            transition.Float4 = inFile.ReadSingle();
                            transition.Float5 = inFile.ReadSingle();
                            transition.Float6 = inFile.ReadSingle();
                            break;
                        case CommandListType.STATE:
                            var unk3 = cmd as StateCommand;
                            unk3.Flags = (StateCommand.StateFlags)inFile.ReadUInt32();
                            Util.LogUnkEnumFlags(unk3.Flags, "script", script.Name);
                            unk3.UnknownFlags2 = inFile.ReadUInt32();
                            break;
                        case CommandListType.SPEED:
                            (cmd as SpeedCommand).Multiplier = inFile.ReadSingle();
                            break;
                        case CommandListType.CANCELS:
                            (cmd as CancelCommand).Type = (CancelCommand.CancelType)inFile.ReadUInt32();
                            Util.LogUnkEnumFlags((cmd as CancelCommand).Type, "script", script.Name);
                            var d = inFile.ReadInt32();
                            if (d != -1 && d < bcm.CancelLists.Count)
                                (cmd as CancelCommand).CancelList = bcm.CancelLists[d];
                            break;
                        case CommandListType.HURTBOX:
                            var hurt = cmd as HurtboxCommand;
                            hurt.X = inFile.ReadSingle();
                            hurt.Y = inFile.ReadSingle();
                            hurt.Rotation = inFile.ReadSingle();
                            hurt.Width = inFile.ReadSingle();
                            hurt.Height = inFile.ReadSingle();
                            hurt.FloatUnknown = inFile.ReadSingle();
                            hurt.Unknown1 = inFile.ReadUInt32();
                            hurt.Unknown2 = inFile.ReadByte();
                            hurt.Unknown3 = inFile.ReadByte();
                            hurt.Unknown4 = inFile.ReadByte();
                            hurt.Unknown5 = inFile.ReadSByte();
                            break;
                        case CommandListType.PHYSICS:
                            var physics = cmd as PhysicsCommand;
                            physics.XVel = inFile.ReadSingle();
                            physics.YVel = inFile.ReadSingle();
                            physics.Unk01 = inFile.ReadUInt32();
                            physics.PhysicsFlags = (PhysicsCommand.PFlags)inFile.ReadUInt32();
                            Util.LogUnkEnumFlags(physics.PhysicsFlags, "script", script.Name);
                            physics.XAccel = inFile.ReadSingle();
                            physics.YAccel = inFile.ReadSingle();
                            physics.Unk02 = inFile.ReadUInt64();
                            //cmd.Raw = inFile.ReadBytes(0x20);
                            break;
                        case CommandListType.ETC:
                            var etc = cmd as EtcCommand;
                            etc.Type = (EtcCommand.EtcCommandType)inFile.ReadUInt16();
                            Util.LogUnkEnum(etc.Type, "script", script.Name);
                            etc.ShortParam = inFile.ReadUInt16();
                            var arr = new int[7];
                            for(int tmp = 0; tmp < 7; tmp++)
                                arr[tmp] = inFile.ReadInt32();
                            etc.RawParams = arr;
                            //cmd.Raw = inFile.ReadBytes(30);
                            break;
                        case CommandListType.HITBOX:
                            var hit = cmd as HitboxCommand;
                            hit.X = inFile.ReadSingle();
                            hit.Y = inFile.ReadSingle();
                            hit.Rotation = inFile.ReadSingle();
                            hit.Width = inFile.ReadSingle();
                            hit.Height = inFile.ReadSingle();
                            hit.FloatUnknown = inFile.ReadSingle();
                            hit.ID = inFile.ReadSByte();
                            hit.Juggle = inFile.ReadSByte();
                            hit.Type = (HitboxCommand.HitboxType)inFile.ReadByte();
                            Util.LogUnkEnum(hit.Type, "script", script.Name);
                            hit.HitLevel = (HitboxCommand.HitLevelType)inFile.ReadByte();
                            Util.LogUnkEnum(hit.HitLevel, "script", script.Name);
                            hit.HitFlags = (HitboxCommand.Flags)inFile.ReadInt16();
                            Util.LogUnkEnumFlags(hit.HitFlags, "script", script.Name);
                            hit.Unknown1 = inFile.ReadByte();
                            hit.UnknownByte1 = inFile.ReadSByte();
                            hit.Hits = inFile.ReadSByte();
                            hit.JugglePotential = inFile.ReadSByte();
                            hit.JuggleIncrement = inFile.ReadSByte();
                            hit.JuggleIncrementLimit = inFile.ReadSByte();
                            hit.HitboxEffect = inFile.ReadInt32();
                            var index2 = inFile.ReadInt32();
                            hit.HitboxDataSet = bac.HitboxTable[index2];
                            if (!hit.HitboxDataSet.Usage.Contains(script) && hit.Type != HitboxCommand.HitboxType.PROXIMITY)
                                hit.HitboxDataSet.Usage.Add(script);
                            break;
                        case CommandListType.INVINC:
                            var invinc = cmd as InvincCommand;
                            invinc.InvincFlags = (InvincCommand.InFlags)inFile.ReadUInt16();
                            Util.LogUnkEnumFlags(invinc.InvincFlags, "script", script.Name);
                            invinc.Unk01 = inFile.ReadUInt16();
                            invinc.Location = inFile.ReadUInt16();
                            invinc.Unk02 = inFile.ReadUInt16();
                            invinc.Unk03 = inFile.ReadUInt16();
                            invinc.Unk04 = inFile.ReadUInt16();
                            invinc.Unk05 = inFile.ReadUInt16();
                            invinc.Unk06 = inFile.ReadUInt16();
                            break;
                        case CommandListType.DAMAGEANIM:
                            var damageAnim = (DamageAnimCommand)cmd;
                            damageAnim.Type = inFile.ReadInt32();
                            damageAnim.Anim = inFile.ReadInt32();
                            damageAnim.Unknown2 = inFile.ReadInt32();
                            damageAnim.Unknown3 = inFile.ReadInt32();
                            break;
                        case CommandListType.SFX:
                            var sfx = (SfxCommand)cmd;
                            sfx.Type = (SfxType)inFile.ReadUInt16();
                            sfx.Sound = inFile.ReadInt16();
                            sfx.Unknown1 = inFile.ReadUInt32();
                            sfx.Unknown2 = inFile.ReadUInt32();
                            sfx.Unknown3 = inFile.ReadUInt32();
                            break;
                        default:
                            cmd.Raw = inFile.ReadBytes(8);
                            break;
                    }
                }
                script.CommandLists.Add(cl);

            }
            for (int i = 0; i < 13; i++)
            {
                if (script.CommandLists.Count <= i || script.CommandLists[i].Type != (CommandListType)i)
                {
                    var c = CommandListFactory.ByType((CommandListType)i);
                    c.Type = (CommandListType)i;
                    script.CommandLists.Insert(i, c);
                }
            }
        }
        #endregion
        #region Writing Functions
        public static void ToFilename(string name, BACFile bac, BCMFile bcm)
        {
            bac.Scripts.Remove(Script.NullScript);
            bac.VFXScripts.Remove(Script.NullScript);

            bac.Scripts = new ObservableCollection<Script>(bac.Scripts.OrderBy(x => x.Index));
            bac.VFXScripts = new ObservableCollection<Script>(bac.VFXScripts.OrderBy(x => x.Index));

            using (var ms = new MemoryStream())
            using (var outFile = new BinaryWriter(ms))
            {
                outFile.Write(1128350243);
                outFile.Write(2686974);
                outFile.Write(65537);

                outFile.Write((short)(bac.Scripts.Last().Index + 1));
                if (bac.VFXScripts.Count > 0)
                    outFile.Write((short)(bac.VFXScripts.Last().Index + 1));
                else
                    outFile.Write((short)0);
                outFile.Write(bac.HitboxTable.Last().Index + 1);

                int OffsetOffset = (int)outFile.BaseStream.Position;

                outFile.Write(0);
                outFile.Write(0);
                outFile.Write(0);
                outFile.Write(0);
                outFile.Write(0);
                foreach (ObservableCollection<float> list in bac.UnknownFloatData)
                    foreach (float entry in list)
                        outFile.Write(entry);

                /* Scripts */
                int ScriptOffset = (int)outFile.BaseStream.Position;
                for (int i = 0; i < bac.Scripts.Last().Index + 1; i++)
                    outFile.Write(0);
                /* VFX Scripts */
                int VFXScriptOffset = (int)outFile.BaseStream.Position;
                if (bac.VFXScripts.Count != 0)
                    for (int i = 0; i < bac.VFXScripts.Last().Index + 1; i++)
                        outFile.Write(0);
                else
                    VFXScriptOffset = 0;

                /* Script Name Offsets */
                int ScriptNameOffset = (int)outFile.BaseStream.Position;
                for (int i = 0; i < bac.Scripts.Last().Index + 1; i++)
                    outFile.Write(0);

                /* VFX Script Name Offsets */
                int VFXScriptNameOffset = (int)outFile.BaseStream.Position;
                if (bac.VFXScripts.Count == 0)
                    VFXScriptNameOffset = 0;
                else
                    for (int i = 0; i < bac.VFXScripts.Last().Index + 1; i++)
                        outFile.Write(0);
                /* Hitbox Table */
                int HitboxTableOffset = (int)outFile.BaseStream.Position;
                for (int i = 0; i < bac.HitboxTable.Last().Index + 1; i++)
                    outFile.Write(0);
                outFile.BaseStream.Seek(0, SeekOrigin.End);

                foreach (Script script in bac.Scripts)
                {
                    int tmp = (int)outFile.BaseStream.Position;
                    writeScript(bac, bcm, outFile, script);
                    outFile.BaseStream.Seek(ScriptOffset + script.Index*4);
                    outFile.Write(tmp);
                    outFile.Seek(0, SeekOrigin.End);
                }
                foreach (Script script in bac.VFXScripts)
                {
                    int tmp = (int)outFile.BaseStream.Position;
                    writeScript(bac, bcm, outFile, script);
                    outFile.BaseStream.Seek(VFXScriptOffset + script.Index*4);
                    outFile.Write(tmp);
                    outFile.Seek(0, SeekOrigin.End);
                }
                /* Hitbox Table */
                foreach (HitBoxDataset dataset in bac.HitboxTable)
                {
                    int tmp = (int)outFile.BaseStream.Position;
                    outFile.BaseStream.Seek(HitboxTableOffset + dataset.Index*4);
                    outFile.Write(tmp);
                    outFile.Seek(0, SeekOrigin.End);
                    foreach (HitBoxData data in dataset.Data)
                    {
                        outFile.Write(data.Damage);
                        outFile.Write(data.Stun);
                        outFile.Write((ushort)data.Effect);
                        if (data.OnHit != null)
                            outFile.Write((short)(data.OnHit.Index - HitBoxData.getIndexOffset(data.Effect)));
                        else
                            outFile.Write((short)-1);
                        //0x8
                        outFile.Write(data.AttackerHitstop);
                        outFile.Write(data.AttackerShaking);
                        outFile.Write(data.VictimHitstop);
                        outFile.Write(data.VictimShaking);
                        //010
                        outFile.Write(data.HitGFX);
                        outFile.Write(data.Unknown1);
                        outFile.Write(data.Unknown2_1);
                        outFile.Write(data.Unknown2_2);
                        //20
                        outFile.Write(data.HitSound);
                        outFile.Write(data.Unknown4);
                        outFile.Write(data.PainSound);
                        outFile.Write(data.HitSFX);
                        outFile.Write(data.HitSFX2);
                        outFile.Write(data.VictimSFX);

                        outFile.Write(data.ArcadeScore);
                        outFile.Write(data.AtkMeter);
                        outFile.Write(data.VctmMeter);
                        outFile.Write(data.JuggleStart);
                        outFile.Write(data.AnimTime);
                        outFile.Write(data.ForceUnknown2);
                        outFile.Write(data.ForceX);
                        outFile.Write(data.ForceY);

                        outFile.Write(data.ForceUnknown3);
                        outFile.Write(data.ForceXAcceleration);
                        outFile.Write(data.ForceYAcceleration);
                        outFile.Write(data.ForceUnknown4);
                        outFile.Write(data.ForceUnknown5);
                    }
                }
                /* Script Names+Offsets */

                List<string> strings = new List<string>();
                int index = 0;
                foreach (Script tmp in bac.Scripts)
                {
                    while (index != tmp.Index)
                    {
                        strings.Add(null);
                        index++;
                    }
                    index++;
                    if (tmp.Name != "null")
                        strings.Add(tmp.Name);
                    else
                        strings.Add(null);
                }
                Util.writeStringTable(outFile, ScriptNameOffset, strings);

                strings.Clear();
                index = 0;
                foreach (Script tmp in bac.VFXScripts)
                {
                    while (index != tmp.Index)
                    {
                        strings.Add(null);
                        index++;
                    }
                    index++;
                    strings.Add(tmp.Name);
                }
                Util.writeStringTable(outFile, VFXScriptNameOffset, strings);

                outFile.BaseStream.Seek(0x14);
                outFile.Write(ScriptOffset);
                outFile.Write(VFXScriptOffset);
                outFile.Write(ScriptNameOffset);
                outFile.Write(VFXScriptNameOffset);
                outFile.Write(HitboxTableOffset);

                using (var fs = File.Create(name))
                {
                    ms.WriteTo(fs);
                }
            }
        }

        private static void writeScript(BACFile bac, BCMFile bcm, BinaryWriter outFile, Script script)
        {
            outFile.Write(script.FirstHitboxFrame);
            outFile.Write(script.LastHitboxFrame);
            outFile.Write(script.IASAFrame);
            outFile.Write(script.TotalFrames);

            outFile.Write(script.UnknownFlags1);
            outFile.Write(script.UnknownFlags2);
            outFile.Write(script.UnknownFlags3);
            outFile.Write((ushort)script.CommandLists.Where(x => x.Count > 0).Count());
            outFile.Write(0x18);

            int baseOff = (int)outFile.BaseStream.Position;
            var clists = script.CommandLists.Where(x => x.Count > 0).ToList();
            for (int i = 0; i < clists.Count; i++)
            {
                dynamic cl = clists[i];
                outFile.Write((ushort)cl.Type);
                outFile.Write((ushort)cl.Count);
                outFile.Write(0);
                outFile.Write(0);
            }
            for (int i = 0; i < clists.Count; i++)
            {
                dynamic cl = clists[i];
                int frameoff = (int)outFile.BaseStream.Position - baseOff - 12 * i;
                outFile.Seek(0, SeekOrigin.End);
                foreach (BaseCommand cmd in cl)
                {
                    outFile.Write((ushort)cmd.StartFrame);
                    outFile.Write((ushort)cmd.EndFrame);
                }
                int dataoff = (int)outFile.BaseStream.Position - baseOff - 12 * i;
                outFile.BaseStream.Seek(baseOff + 12 * i + 4);
                outFile.Write(frameoff);
                outFile.Write(dataoff);
                outFile.Seek(0, SeekOrigin.End);
                foreach (BaseCommand cmd in cl)
                {
                    if (cmd is FlowCommand)
                    {
                        var flow = cmd as FlowCommand;
                        outFile.Write((int)flow.Type);
                        if (flow.TargetScript != null && flow.TargetScript != Script.NullScript)
                            outFile.Write((short)flow.TargetScript.Index);
                        else
                            outFile.Write((short)-1);
                        outFile.Write(flow.TargetFrame);
                    }
                    else if (cmd is AnimationCommand)
                    {
                        var animation = cmd as AnimationCommand;
                        outFile.Write(animation.Animation);
                        outFile.Write((byte)animation.Type);
                        outFile.Write((byte)animation.Flags);
                        outFile.Write(animation.FromFrame);
                        outFile.Write(animation.ToFrame);
                    }
                    else if (cmd is TransitionCommand)
                    {
                        var transition = (TransitionCommand)cmd;
                        outFile.Write(transition.Flag1);
                        outFile.Write(transition.Flag2);
                        outFile.Write(transition.Float1);
                        outFile.Write(transition.Float2);
                        outFile.Write(transition.Float3);
                        outFile.Write(transition.Float4);
                        outFile.Write(transition.Float5);
                        outFile.Write(transition.Float6);
                    }
                    else if (cmd is StateCommand)
                    {
                        var state = cmd as StateCommand;
                        outFile.Write((uint)state.Flags);
                        outFile.Write((uint)state.UnknownFlags2);
                    }
                    else if (cmd is SpeedCommand)
                    {
                        outFile.Write((cmd as SpeedCommand).Multiplier);
                    }
                    else if (cmd is PhysicsCommand)
                    {
                        var physics = cmd as PhysicsCommand;
                        outFile.Write(physics.XVel);
                        outFile.Write(physics.YVel);
                        outFile.Write((uint)physics.Unk01);
                        outFile.Write((uint)physics.PhysicsFlags);
                        outFile.Write(physics.XAccel);
                        outFile.Write(physics.YAccel);
                        outFile.Write((ulong)physics.Unk02);
                    }
                    else if (cmd is InvincCommand)
                    {
                        /*
                         * case CommandListType.INVINC:
                            var invinc = cmd as InvincCommand;
                            invinc.InvincFlags = (InvincCommand.InFlags)inFile.ReadUInt16();
                            invinc.Unk01 = inFile.ReadUInt16();
                            invinc.Location = inFile.ReadUInt16();
                            invinc.Unk02 = inFile.ReadUInt16();
                            invinc.Unk03 = inFile.ReadUInt16();
                            invinc.Unk04 = inFile.ReadUInt16();
                            invinc.Unk05 = inFile.ReadUInt16();
                            invinc.Unk06 = inFile.ReadUInt16();
                            break;
                         */
                        var invinc = cmd as InvincCommand;
                        outFile.Write((ushort)invinc.InvincFlags);
                        outFile.Write(invinc.Unk01);
                        outFile.Write(invinc.Location);
                        outFile.Write(invinc.Unk02);
                        outFile.Write(invinc.Unk03);
                        outFile.Write(invinc.Unk04);
                        outFile.Write(invinc.Unk05);
                        outFile.Write(invinc.Unk06);
                    }
                    else if (cmd is CancelCommand)
                    {
                        var cancel = cmd as CancelCommand;
                        outFile.Write((int)cancel.Type);
                        if (cancel.CancelList != null)
                            outFile.Write((int)bcm.CancelLists.IndexOf(cancel.CancelList));
                        else
                            outFile.Write(-1);
                    }
                    else if (cmd is HurtboxCommand)
                    {
                        var hurt = cmd as HurtboxCommand;
                        outFile.Write(hurt.X);
                        outFile.Write(hurt.Y);
                        outFile.Write(hurt.Rotation);
                        outFile.Write(hurt.Width);
                        outFile.Write(hurt.Height);
                        outFile.Write(hurt.FloatUnknown);
                        outFile.Write(hurt.Unknown1);
                        outFile.Write(hurt.Unknown2);
                        outFile.Write(hurt.Unknown3);
                        outFile.Write(hurt.Unknown4);
                        outFile.Write(hurt.Unknown5);
                    }
                    else if (cmd is EtcCommand)
                    {
                        var etc = cmd as EtcCommand;
                        outFile.Write((ushort)etc.Type);
                        outFile.Write((ushort)etc.ShortParam);
                        foreach (int s in etc.RawParams)
                            outFile.Write(s);
                    }
                    else if (cmd is HitboxCommand)
                    {
                        var hit = cmd as HitboxCommand;
                        outFile.Write(hit.X);
                        outFile.Write(hit.Y);
                        outFile.Write(hit.Rotation);
                        outFile.Write(hit.Width);
                        outFile.Write(hit.Height);
                        outFile.Write(hit.FloatUnknown);
                        outFile.Write(hit.ID);
                        outFile.Write(hit.Juggle);
                        outFile.Write((byte)hit.Type);
                        outFile.Write((byte)hit.HitLevel);
                        outFile.Write((ushort)hit.HitFlags);
                        outFile.Write(hit.Unknown1);
                        outFile.Write(hit.UnknownByte1);
                        outFile.Write(hit.Hits);
                        outFile.Write(hit.JugglePotential);
                        outFile.Write(hit.JuggleIncrement);
                        outFile.Write(hit.JuggleIncrementLimit);
                        outFile.Write(hit.HitboxEffect);
                        outFile.Write((int)hit.HitboxDataSet.Index);
                    }
                    else if (cmd is DamageAnimCommand)
                    {
                        var damageAnim = (DamageAnimCommand)cmd;
                        outFile.Write(damageAnim.Type);
                        outFile.Write(damageAnim.Anim);
                        outFile.Write(damageAnim.Unknown2);
                        outFile.Write(damageAnim.Unknown3);
                    }
                    else if (cmd is SfxCommand)
                    {
                        var sfx = (SfxCommand)cmd;
                        outFile.Write((UInt16)sfx.Type);
                        outFile.Write(sfx.Sound);
                        outFile.Write(sfx.Unknown1);
                        outFile.Write(sfx.Unknown2);
                        outFile.Write(sfx.Unknown3);
                    }
                    else
                    {
                        outFile.Write(cmd.Raw);
                    }
                }

            }
        }
        #endregion
        private BACFile()
        {

        }
    }
}

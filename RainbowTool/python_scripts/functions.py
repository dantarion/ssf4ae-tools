import struct, os
def read_cstring(f):
    s = ""
    c = f.read(1)
    while ord(c) != 0:
        s = s+c
        c = f.read(1)
    return s
def ripBVS(filename):
	with open(filename,"rb") as infile:
		infile.seek(0xC)
		header = struct.unpack("6H3I",infile.read(0x18))
		VFX_NAMES = {}
		for i in range(0,header[0]):
			infile.seek(0x24+i*0x30)
			name = read_cstring(infile)
			infile.seek(0x24+i*0x30+0x20)
			index = struct.unpack("H",infile.read(2))[0]
			VFX_NAMES[index] = name
		VFX_NAMES2 = {}
		for i in range(0,header[1]):
			infile.seek(header[6]+i*0x30)
			name = read_cstring(infile)
			infile.seek(header[6]+i*0x30+0x20)
			index = struct.unpack("H",infile.read(2))[0]
			VFX_NAMES2[index] = name
	return [VFX_NAMES,VFX_NAMES2]
def ripEMA(filename):
	f = open(filename,"rb")
	f.seek(16,0)
	animation_count = struct.unpack("H",f.read(0x2))[0] 
	NAMES = []
	for i in range(0,animation_count):
		f.seek(0x20+i*4,0)
		off = struct.unpack("I",f.read(0x4))[0]
		f.seek(off+12)
		off = struct.unpack("I",f.read(0x4))[0]
		f.seek(off-6,1)
		count = ord(f.read(1))
		NAMES.append(f.read(count))
	f.close()
	return NAMES
def ripEMABones(filename):
    NAMES = {}
    f = open(filename,"rb")
    f.seek(12)
    skeletonOff = struct.unpack("I",f.read(0x4))[0]
    #print hex(skeletonOff)
    f.seek(skeletonOff)
    boneCount = struct.unpack("H",f.read(0x2))[0]
    f.seek(skeletonOff+0xC)
    boneNameOff = struct.unpack("I",f.read(0x4))[0]
    for i in range(0,boneCount):
        f.seek(skeletonOff+boneNameOff+i*4)
        strOff = struct.unpack("I",f.read(0x4))[0]
        f.seek(skeletonOff+strOff)
        NAMES[i] = read_cstring(f)
    f.close()
    return NAMES
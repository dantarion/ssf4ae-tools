from functions import *
import os,struct
with open("C:\\Program Files (x86)\\Capcom\\Super Street Fighter IV\\resource\\battle\\chara\\BOS\\BOS.clg") as infile:
    for i in range(0,0x18):
        print i,
        infile.seek(0x40+i*0x70)
        print read_cstring(infile)
        infile.seek(0x40+i*0x70+0x20)
        for b in range(0,0x50):
            print "%02X" % ord(infile.read(1)),
            if (b+1) % 16 == 0:
                print
        print

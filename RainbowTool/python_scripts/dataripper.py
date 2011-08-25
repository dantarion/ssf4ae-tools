from functions import *
import os, struct

INSTALL_DIR = "C:\\Program Files (x86)\\Capcom\\Super Street Fighter IV\\"
def ripBVS2File(bvs,log):
    data = ripBVS(bvs)
    log.write("VFX = {}\n")
    for key in data[0].keys():
        log.write("VFX[0x%04X] = \"%s\" #%d\n"%(key,data[0][key],key))
    log.write("VFX2 = {}\n")
    for key in data[1].keys():
        log.write("VFX2[0x%04X] = \"%s\" #%d\n"%(key,data[1][key],key))
def ripEMS2File(bvs,name,log):
    log.write(name+" = []\n")
    for i,vfx in enumerate(ripEMA(bvs)):
        log.write("%s.append(\"%s\")# %02d\n"%(name,vfx,i))  
#Global Data
with open("out/global.py","w") as log:
    ripBVS2File(INSTALL_DIR+"resource\\battle\\vfx\\CMN.vfx.bvs",log)

#Character Data
BASE = INSTALL_DIR+"resource\\battle\\chara\\"
AE_BASE = INSTALL_DIR+"dlc\\03_character_free\\battle\\chara\\"
#Build paths!
chars = []
dirs = []
for entry in os.listdir(BASE):
    if os.path.isdir(BASE+entry):
        chars.append(entry)
        dirs.append(BASE+entry)
for entry in os.listdir(AE_BASE):
    if os.path.isdir(AE_BASE+entry):
        chars.append(entry)
        dirs.append(AE_BASE+entry)
        
for i,char in enumerate(chars):
    print char
    log = open("out/"+char+".py","w")
    ripBVS2File(dirs[i]+"/"+char+".vfx.bvs",log)
    ripEMS2File(dirs[i]+"/"+char+".obj.ema","OBJ",log)
    ripEMS2File(dirs[i]+"/"+char+".fce.ema","FCE",log)
    ripEMS2File(dirs[i]+"/"+char+".cam.ema","CAM",log)
    ripEMS2File(dirs[i]+"/"+char+".uc1.ema","UC1",log)
    ripEMS2File(dirs[i]+"/"+char+".uc2.ema","UC2",log)
    log.close()
print "ALL DONE YEAH"

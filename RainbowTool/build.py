import zipfile
import os

with zipfile.ZipFile('ono-latest.zip','w',zipfile.ZIP_DEFLATED) as myzip:
    myzip.write("OnoEdit\\bin\\Debug\\OnoEdit.exe""","OnoEdit.exe")
    myzip.write("OnoEdit\\bin\\Debug\\RainbowLib.dll","RainbowLib.dll")
    myzip.write("RainbowScript\\bin\\Debug\\RainbowScript.exe","RainbowScript.exe")
    myzip.write("RainbowLib\\lib\\IronPython.dll","IronPython.dll")
    myzip.write("RainbowLib\\lib\\Microsoft.Scripting.dll","Microsoft.Scripting.dll")
    for dirpath, dirnames, filenames in os.walk("RainbowLib\\lib\\"):
        if '.svn' in dirnames:
            dirnames.remove(".svn")
        print filenames
        for filename in filenames:
            myzip.write(os.path.join(dirpath,filename),filename)
    for dirpath, dirnames, filenames in os.walk("python_scripts/out"):
        if '.svn' in dirnames:
            dirnames.remove(".svn")
        print filenames
        for filename in filenames:
            myzip.write(os.path.join(dirpath,filename),"data/"+filename)

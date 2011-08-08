Anotak's SSF4 AE modding tools
------------------------------
Okay, if you don't make a backup, it isn't my fault if your shit breaks. If it crashes, send me the Logfile.txt and tell me what you tried to do. If you break your SSF4 AE installation, it isn't my fault.
for the exes you need .NET framework.

This isn't gauranteed to work 100%, but i try my best.
Tools:
	GraveStormboner.exe - this lets you edit individual inputs in the BCM file.
	BCMAnalyze.exe - this is for figuring shit out in the BCM file
	ThatsMindgames.exe - this is for editing the cancel lists in the BCM file

GraveStormboner.exe
-------------------
This opens up BCM files and lets you edit which buttons or motion applies to the input, or add new inputs. inputs are on the left, the flags (such as which button to use) are in the middle. you can change whether or not a move requires being close or far. right now the animation to use is just a hex value, i haven't written the code to load BAC into memory yet and read the actual names.

BCMAnalyze.exe
--------------
this is a commandline tool, it takes 1-3 inputs and will analyze the input section of BCM. the first argument should be the file to  use, the 2nd and 3rd are option arguments for target bytes. this is for helping understand the file format.

ThatsMindgames.exe
------------------
this is for adding to or removing from the cancel lists in the BCM file. these cancel lists are referenced in the animations in the BAC files.

YomiLayer7.exe
--------------
importing, removing, exporting animations.

Credits
-------
Programming:
Anotak

File format info (SSF4 AE):
Anotak
Dantarion
Polarity
Zeipher

File format info (sf4):
Anotak
Polarity
Zeipher
Illitirit
Gojira

Minor file help (sf4):
Piecemontee
SSJ George Bush
Providenceangle
Error1

Special thanks:
#sf4-modding on synirc
Zandwich
Banana Ken
Geoff the Hero
ahfb
Dandy_J
sonichurricane.com

Testing:
Zeipher
Polarity

Bug reports:
Zeipher
Polarity
Bebopfan
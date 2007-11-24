The Windows application requires the .NET 2.0 framework to run.
The kernel plugin has been tested on 3.71 M33, but it might work with other versions.

To use the kernel plugin:
Copy FFTSaveHook.prx to your seplugins directory, and edit game.txt to tell 
it to load. Enable it in the recovery menu. 

The next time you load a game in War of the Lions, the plugin will check
ms0:/decryptedSaves for a decrypted copy of the save, and load it if it's 
there. If it's not there it will decrypt whatever is in ms0:/psp/savedata/ 
for the game currently being run. 

The next time you save a game in War of the Lions, the plugin will dump a 
decrypted copy into ms0:/decryptedSaves.

The plugin will unload itself if the game currently being run is not
War of the Lions. Saving and loading in games other than FFT are not affected.
It does this by looking at the game number:
	ULUS10297 - US
	ULES00850 - Europe
	ULJM05194 - Japan (Untested)


Once you have a decrypted FFTA.SYS file, you can load it into Lion Editor 
and begin making changes.

Future features planned:
	Inventory editor
	Brave story editor (feats, artefacts, wonders, events, personae, kills, casualties, timer)
	Import saves from Playstation version
	
If you find any bugs, please fill out an issue report at 
http://code.google.com/p/lioneditor/issues/list

Attach a copy of your FFTA.SYS if you think it will help me reproduce the 
problem. Also indicate what version of the game you are using 
(US/Europe/Japan).

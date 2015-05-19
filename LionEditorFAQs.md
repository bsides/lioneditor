### Do I really need custom firmware? ###
Yes. The game expects the saves to be encrypted. LionEditor expects the saves to be decrypted. It is not currently possible to decrypt the savegame information without a PSP. The solution is the FFTSaveHook.prx kernel plugin, which intercepts the game's access to the Memory Stick and injects or extracts the decrypted data as necessary.

### Is it possible to convert saves between regions? ###
Yes. You will need a copy of War of the Lions from both regions (the target and destination).
For example, to convert from European to US:
  1. Enable the FFTSaveHook kernel plugin
  1. Load the game in the European version
  1. Shut down the PSP
  1. Find the file `lioneditor.bin` on your Memory Stick in `/PSP/SAVEDATA/ULES00850FFT0000` (this is the location where saves are stored for the European version of War of the Lions)
  1. Copy the file to `/PSP/SAVEDATA/ULUS10297FFT0000` (this is the location where saves are stored for the US version of War of the Lions)
  1. Start the US version of the game
  1. The European saves should appear in the load screen

The saves for each region are stored in the following locations on the memory stick
  * US: /PSP/SAVEDATA/ULUS10297FFT0000
  * Europe: /PSP/SAVEDATA/ULES00850FFT0000
  * Japan: /PSP/SAVEDATA/ULJM05194FFT0000
  * Australia: Unknown. Please email me if you know.

Be sure to save a copy of your saves (including the FFTA.SYS file) before you try to modify anything.
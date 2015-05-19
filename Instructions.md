# Introduction #

Some people were having trouble with the readme and instructions I provided, so I'll try to explain the process in more detail here.


# PSP #
## Summary ##
Save games on the PSP memory stick are **[encrypted](http://en.wikipedia.org/wiki/Encryption)**. It is currently not possible to decrypt the saves with anything other than a PSP. FFTSaveHook.prx is a **kernel plugin** that decrypts and reencrypts FFT: War of the Lions saves automatically.

## Kernel plugin requirements ##
  * PSP or PSP Slim Lite
  * Custom Firmware 3.71 M33 or higher (The process of installing custom firmware is beyond the scope of this document. Check out [MaxConsole](http://maxconsole.com) and [PSPUpdates](http://pspupdates.qj.net))

## Kernel plugin installation and use ##
  1. Plug your Memory Stick into your computer or hook up a USB cable between your PSP and computer
  1. Run the `LionEditor.exe` program
  1. Click the "Install plugin to Memory Stick" button at the top right of the window.
  1. Select the drive letter of your PSP or Memory Stick
  1. Put your memory stick back in your PSP
  1. Boot the PSP into the custom firmware's **Recovery Mode** by holding **the R Button** as you turn on the PSP
  1. Select _Plugins_
  1. Highlight `FFTSaveHook.prx [GAME]` and push **the X Button**.
  1. Ensure that it now says `FFTSaveHook.prx [GAME] (Enabled)`
  1. Exit the recovery menu
  1. Put your Final Fantasy Tactics: War of the Lions UMD in your PSP
  1. Start the game
  1. Load your saved game
    * When this is done a new file is created on your Memory Stick `ms0:/PSP/SAVEDATA/ULUS10297FFT0000/lioneditor.bin` (For the US version)
  1. Turn off your PSP and hook it up to the computer again

# PC #
## PC Application requirements ##
  * [.NET 2.0 Framework](http://www.microsoft.com/downloads/details.aspx?familyid=0856eacb-4362-4b0d-8edd-aab15c5e04f5&displaylang=en)

## PC Application instructions ##
  1. Start the program `LionEditor.exe`
  1. Click the Folder Icon or select the Open option from the File menu
  1. Browse to the location of the decrypted savegame `lioneditor.bin`
  1. Click Open
  1. Modify the file using the GUI
  1. Click the Disk Icon or select the Save option from the File menu to save your changes
  1. Put your memory stick back in your PSP
  1. Start up War of the Lions and load your modified save
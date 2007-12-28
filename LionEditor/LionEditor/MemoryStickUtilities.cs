/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of LionEditor.

    LionEditor is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    LionEditor is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with LionEditor.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using LionEditor.Properties;

namespace LionEditor
{
    public static class MemoryStickUtilities
    {
        private const string pluginPath = "ms0:/seplugins/FFTSaveHook.prx";

        public enum InstallResult
        {
            Success,
            NotMemoryStickRoot,
            Failure,
        }

        public static string GetSavePath(string msRoot, Region region)
        {
            if (!IsMemoryStickRoot(msRoot))
            {
                return string.Empty;
            }

            switch (region)
            {
                case Region.Europe:
                    return msRoot + "/PSP/SAVEDATA/ULES00850FFT0000/lioneditor.bin";
                case Region.Japan:
                    return msRoot + "/PSP/SAVEDATA/ULJM05194FFT0000/lioneditor.bin";
                case Region.US:
                    return msRoot + "/PSP/SAVEDATA/ULUS10297FFT0000/lioneditor.bin";
            }

            return string.Empty;
        }

        public static InstallResult InstallPlugin(string msRoot)
        {
            if (!IsMemoryStickRoot(msRoot))
            {
                return InstallResult.NotMemoryStickRoot;
            }

            StreamReader reader = null;
            StreamWriter writer = null;
            try
            {

                if (!Directory.Exists(msRoot + "/seplugins"))
                {
                    Directory.CreateDirectory(msRoot + "/seplugins");
                }

                bool saveHookFound = false;

                if (File.Exists(msRoot + "/seplugins/GAME.TXT"))
                {
                    reader = new StreamReader(msRoot + "/seplugins/GAME.TXT");

                    while (!reader.EndOfStream && !saveHookFound)
                    {
                        string line = reader.ReadLine();
                        saveHookFound = (line == pluginPath);
                    }

                    reader.Close();
                    reader = null;
                }

                if (!saveHookFound)
                {
                    writer = new StreamWriter(msRoot + "/seplugins/GAME.TXT", true);
                    writer.WriteLine(writer.NewLine + pluginPath);
                    writer.Close();
                    writer = null;
                }

                File.WriteAllBytes(msRoot + "/seplugins/FFTSaveHook.prx", Resources.FFTSaveHook);

                return InstallResult.Success;
            }
            catch (Exception)
            {
                return InstallResult.Failure;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public static bool IsMemoryStickRoot(string path)
        {
            return 
                Directory.Exists(path) && 
                Directory.Exists(path + "/PSP") && 
                Directory.Exists(path + "/PSP/SAVEDATA");
        }


    }
}

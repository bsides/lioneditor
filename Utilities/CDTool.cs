/*
    Copyright 2007, Joe Davidson <joedavidson@gmail.com>

    This file is part of FFTPatcher.

    FFTPatcher is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    FFTPatcher is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with FFTPatcher.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using FFTPatcher.Datatypes;
using FFTPatcher.Properties;

namespace FFTPatcher
{
    public static class CDTool
    {
        public class PatchedByteArray
        {
            public string Filename { get; private set; }
            public long Offset { get; private set; }
            public byte[] Bytes { get; private set; }

            public PatchedByteArray( string filename, long offset, byte[] bytes )
            {
                Filename = filename;
                Offset = offset;
                Bytes = bytes;
            }
        }

        private const string patchFunction = 
@"function patchFile(filename, p)
	f=cdutil:findpath(filename)
    file = cdfile(cdutil, f)
    buff = Buffer(true)
	
    buff:copyfrom(file)
	
	for k,v in pairs(p) do
	    copyBytesToLocation(buff, k, b64.decode(v))
	end
	
	cdutil:writefile(buff, f.Size, f.Sector)
end

function copyBytesToLocation(buffer, location, bytes)
  for i=1,#bytes,1 do
    buffer[location+i-1]=bytes[i]
  end
end

function doPatch()
  for k,v in pairs(patches) do
    print(""Patching file "" .. k)
    patchFile(k,v)
  end
end

";
        private const string binModule =
@"bin = { }
function bin.imod(a, b)
    return a - math.floor(a / b) * b
end
function bin.rshift(a, b)
    if (a < 0) then
	a = 4294967296 + a
    end
    if (b < 0) then
	b = 4294967296 + b
    end
    a = bin.imod(a, 4294967296)
    b = bin.imod(b, 4294967296)
    return math.floor(a / (2 ^ b))
end
function bin.lshift(a, b)
    if (a < 0) then
	a = 4294967296 + a
    end
    if (b < 0) then
	b = 4294967296 + b
    end
    a = bin.imod(a, 4294967296)
    b = bin.imod(b, 4294967296)
    return math.floor(a * (2 ^ b))
end
function bin.band(a, b)
    local i, v, r, b1, b2
    if (a < 0) then
	a = 4294967296 + a
    end
    if (b < 0) then
	b = 4294967296 + b
    end
    a = bin.imod(a, 4294967296)
    b = bin.imod(b, 4294967296)
    r = 0
    for i = 31, 0, -1 do
	v = 2 ^ i
	b1 = a >= v
	b2 = b >= v
	if (b1) and (b2) then
	    r = r + v
	end
	if (b1) then
	    a = a - v
	end
	if (b2) then
	    b = b - v
	end
    end
    return r
end
function bin.bor(a, b)
    local i, v, r, b1, b2
    if (a < 0) then
	a = 4294967296 + a
    end
    if (b < 0) then
	b = 4294967296 + b
    end
    a = bin.imod(a, 4294967296)
    b = bin.imod(b, 4294967296)
    r = 0
    for i = 31, 0, -1 do
	v = 2 ^ i
	b1 = a >= v
	b2 = b >= v
	if (b1) or (b2) then
	    r = r + v
	end
	if (b1) then
	    a = a - v
	end
	if (b2) then
	    b = b - v
	end
    end
    return r
end

";
        private const string b64Module=
@"b64 = { }
local cb64 = ""ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/""

local function stri(s)
    return s == ""="" and -1 or (string.find(cb64, s) - 1)
end
function b64.decodeblock(s1, s2, s3, s4)
    local b1, b2, b3, len
    len = s3 == ""="" and 1 or s4 == ""="" and 2 or 3
    s3 = s3 == ""="" and ""A"" or s3
    s4 = s4 == ""="" and ""A"" or s4
    s1 = stri(s1)
    s2 = stri(s2)
    s3 = stri(s3)
    s4 = stri(s4)
    b1 = bin.bor(bin.lshift(s1, 2), bin.rshift(s2, 4))
    b2 = bin.bor(bin.band(bin.lshift(s2, 4), 255), bin.rshift(s3, 2))
    b3 = bin.bor(bin.band(bin.lshift(s3, 6), 240), s4)
    return {b1, b2, b3}, len    
end
function b64.decode(str_in)
    local s_len = string.len(str_in) / 4
    local out, len, i, j, t_out, s1, s2, s3, s4, t_len = {}, 0
    for i = 1, s_len do
	s1 = string.sub(str_in, i * 4 - 3, i * 4 - 3)
	s2 = string.sub(str_in, i * 4 - 2, i * 4 - 2)
	s3 = string.sub(str_in, i * 4 - 1, i * 4 - 1)
	s4 = string.sub(str_in, i * 4 - 0, i * 4 - 0)
	t_out, t_len = b64.decodeblock(s1, s2, s3, s4)
	for j = 1, t_len do
	    out[i * 3 - 3 + j] = t_out[j]
	end
	len = len + t_len
    end
    return out, len
end
";
        public static event EventHandler<ProgressEventArgs> ProgressChanged;
        public static event DataReceivedEventHandler ConsoleOutputChanged;
        public static event EventHandler OperationComplete;

        private static void FireConsoleOutputChangedEvent( object sender, DataReceivedEventArgs e )
        {
            if( ConsoleOutputChanged != null )
            {
                ConsoleOutputChanged( null, e );
            }
        }

        private static void FireOperationCompleteEvent( object sender, EventArgs e )
        {
            if( OperationComplete != null )
            {
                OperationComplete( null, EventArgs.Empty );
            }

            if( currentProcess != null )
            {
                currentProcess.Exited -= FireOperationCompleteEvent;
                currentProcess.OutputDataReceived -= FireConsoleOutputChangedEvent;
            }
        }

        private static void FireProgressChangedEvent( int done, int total )
        {
            if( ProgressChanged != null )
            {
                ProgressChanged( null, new ProgressEventArgs( done, total ) );
            }
        }

        private static string GetPathOfCdTool()
        {
            using( OpenFileDialog ofd = new OpenFileDialog() )
            {
                ofd.Filter = "CD-Tool executables|cd-tool.exe";
          
                bool cancelled = false;

                string configuredPath = Settings.Default.cdToolPath;
                if( !File.Exists( configuredPath ) )
                {
                    MessageBox.Show( "Please locate cd-tool.exe" );
                    while( !File.Exists( configuredPath ) || cancelled )
                    {
                        switch( ofd.ShowDialog() )
                        {
                            case DialogResult.OK:
                                configuredPath = ofd.FileName;
                                break;
                            default:
                                cancelled = true;
                                break;
                        }
                    }
                }

                if( cancelled )
                {
                    return null;
                }
                else
                {
                    Settings.Default.cdToolPath = configuredPath;
                    return configuredPath;
                }
            }
        }

        public static void PatchISO( string imageFilename, params PatchedByteArray[] patches )
        {
            string cdToolPath = GetPathOfCdTool();
            if( cdToolPath == null )
            {
                return;
            }

            string isoFilename = null;
            using( SaveFileDialog sfd = new SaveFileDialog() )
            {
                sfd.Filter = "Final Fantasy Tactics images|*.bin";
                if( sfd.ShowDialog() == DialogResult.OK )
                {
                    isoFilename = sfd.FileName;
                }
                else
                {
                    return;
                }
            }

            Dictionary<string, IList<string>> patchDictionary = new Dictionary<string, IList<string>>( patches.Length );
            foreach( PatchedByteArray p in patches )
            {
                string s = string.Format( "[{0}]=\"{1}\"", p.Offset, Convert.ToBase64String( p.Bytes ) );
                if( !patchDictionary.ContainsKey( p.Filename ) )
                {
                    patchDictionary.Add( p.Filename, new List<string>() );
                }

                patchDictionary[p.Filename].Add( s );
            }

            StringBuilder output = new StringBuilder();
            output.AppendLine( binModule );
            output.AppendLine( b64Module );
            output.AppendLine( patchFunction );
            output.AppendLine( @"patches = {" );
            string[] patchStrings = new string[patchDictionary.Count];

            int i = 0;
            foreach( KeyValuePair<string, IList<string>> kvp in patchDictionary )
            {
                patchStrings[i++] = string.Format( "[\"{0}\"]={{ {1} }}", kvp.Key, string.Join( ", ", kvp.Value.ToArray() ) );
            }

            output.AppendLine( string.Join( ", \n", patchStrings ) );
            output.AppendLine( "}" );

            using( StreamWriter sw = new StreamWriter( "fftpatcher.lua", false, Encoding.ASCII ) )
            {
                sw.WriteLine( output.ToString() );
            }

            currentProcess = new Process();
            currentProcess.StartInfo = new ProcessStartInfo( 
                    cdToolPath, 
                    string.Format( "-f {0} -w -e \"doPatch()\" fftpatcher.lua", isoFilename ) );
            currentProcess.OutputDataReceived += FireConsoleOutputChangedEvent;
            currentProcess.EnableRaisingEvents = true;
            currentProcess.Exited += FireOperationCompleteEvent;

            currentProcess.Start();
        }

        private static Process currentProcess;

        public static void PatchISOWithFFTPatchAsync( string imageFilename,
            AllAbilities abilities,
            AllItems items,
            AllItemAttributes itemAttributes,
            AllJobs jobs,
            JobLevels jobLevels,
            AllSkillSets skillsets,
            AllMonsterSkills monsters,
            AllActionMenus actionMenus,
            AllStatusAttributes statuses,
            AllInflictStatuses inflictStatuses,
            AllPoachProbabilities poach,
            FFTFont font,
            AllENTDs entds)
        {
            const string scus = "/SCUS_942.21;1";
            const string battle = "/BATTLE.BIN;1";
            List<PatchedByteArray> patches = new List<PatchedByteArray>();
            if( abilities.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x4F3F0, abilities.ToByteArray() ) );
                FireProgressChangedEvent( 1, 18 );
                patches.Add( new PatchedByteArray( battle, 0x14F3F0, abilities.ToEffectsByteArray() ) );
                FireProgressChangedEvent( 2, 18 );
            }
            if( items.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x536B8, items.ToFirstByteArray() ) );
                FireProgressChangedEvent( 3, 18 );
            }
            if( itemAttributes.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x54AC4, itemAttributes.ToFirstByteArray() ) );
                FireProgressChangedEvent( 4, 18 );
            }
            if( jobs.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x518B8, jobs.ToByteArray( Context.US_PSX ) ) );
                FireProgressChangedEvent( 5, 18 );
            }
            if( jobLevels.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x568C4, jobLevels.ToByteArray( Context.US_PSX ) ) );
                FireProgressChangedEvent( 6, 18 );
            }
            if( skillsets.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x55294, skillsets.ToByteArray( Context.US_PSX ) ) );
                FireProgressChangedEvent( 7, 18 );
            }
            if( monsters.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x563C4, monsters.ToByteArray( Context.US_PSX ) ) );
                FireProgressChangedEvent( 8, 18 );
            }
            if( actionMenus.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x564B4, actionMenus.ToByteArray( Context.US_PSX ) ) );
                FireProgressChangedEvent( 9, 18 );
            }
            if( statuses.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x565E4, statuses.ToByteArray( Context.US_PSX ) ) );
                FireProgressChangedEvent( 10, 18 );
            }
            if( inflictStatuses.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x547C4, inflictStatuses.ToByteArray() ) );
                FireProgressChangedEvent( 11, 18 );
            }
            if( poach.HasChanged )
            {
                patches.Add( new PatchedByteArray( scus, 0x56864, poach.ToByteArray( Context.US_PSX ) ) );
                FireProgressChangedEvent( 12, 18 );
            }
            if( entds.HasChanged )
            {
                patches.Add( new PatchedByteArray( "/BATTLE/ENTD1.ENT;1", 0, entds.ENTDs[0].ToByteArray() ) );
                FireProgressChangedEvent( 13, 18 );
                patches.Add( new PatchedByteArray( "/BATTLE/ENTD2.ENT;1", 0, entds.ENTDs[1].ToByteArray() ) );
                FireProgressChangedEvent( 14, 18 );
                patches.Add( new PatchedByteArray( "/BATTLE/ENTD3.ENT;1", 0, entds.ENTDs[2].ToByteArray() ) );
                FireProgressChangedEvent( 15, 18 );
                patches.Add( new PatchedByteArray( "/BATTLE/ENTD4.ENT;1", 0, entds.ENTDs[3].ToByteArray() ) );
                FireProgressChangedEvent( 16, 18 );
            }

            patches.Add( new PatchedByteArray( "/EVENT/FONT.BIN;1", 0, font.ToByteArray() ) );
            FireProgressChangedEvent( 17, 18 );
            patches.Add( new PatchedByteArray( battle, 0xFF0FC, font.ToWidthsByteArray() ) );
            FireProgressChangedEvent( 18, 18 );

            PatchISO( imageFilename, patches.ToArray() );
        }
    }
}

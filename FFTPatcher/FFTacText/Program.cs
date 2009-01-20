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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using FFTPatcher.TextEditor.Files;

namespace FFTPatcher.TextEditor
{
    static class Program
    {

		#region Methods (2) 


        private static bool HandleArgs( string[] args )
        {
            if( args == null )
            {
                return false;
            }

            if( args.Length >= 4 && args[0] == @"--load" )
            {
                string filename = args[1];
                string type = args[2];
                string output = args[3];
                int? start = null;
                if( args.Length > 4 )
                {
                    int locStart;
                    string s = args[4].TrimStart( '0', 'x' );
                    if( s == string.Empty )
                        s = "0";
                    if( Int32.TryParse( s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out locStart ) )
                    {
                        start = locStart;
                    }
                }
                int? length = null;
                if( args.Length > 5 )
                {
                    string s = args[5].TrimStart( '0', 'x' );
                    if( s == string.Empty )
                        s = "0";
                    int loclength;
                    if( Int32.TryParse( s, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out loclength ) )
                    {
                        length = loclength;
                    }
                }

                Type t =
                    Type.GetType( type, false, true ) ??
                    Type.GetType( "FFTPatcher.TextEditor.Files." + type, false, true ) ??
                    Type.GetType( "FFTPatcher.TextEditor.Files.PSX." + type, false, true ) ??
                    Type.GetType( "FFTPatcher.TextEditor.Files.PSP." + type, false, true );
                if( t != null )
                {
                    IList<byte> bytes;
                    using( FileStream stream = new FileStream( filename, FileMode.Open ) )
                    {
                        bytes = new byte[stream.Length];
                        stream.Read( (byte[])bytes, 0, bytes.Count );
                    }

                    ConstructorInfo ci = t.GetConstructor( new Type[] { typeof( IList<byte> ) } );
                    if( start.HasValue && length.HasValue )
                    {
                        bytes = bytes.Sub( start.Value, start.Value + length.Value - 1 );
                    }
                    else if( start.HasValue )
                    {
                        bytes = bytes.Sub( start.Value );
                    }

                    object o = ci.Invoke( new object[] { bytes } );
                    if( o is IStringSectioned )
                    {
                        using( XmlTextWriter writer = new XmlTextWriter( output, System.Text.Encoding.UTF8 ) )
                        {
                            writer.WriteStartElement( "dongs" );
                            (o as IStringSectioned).WriteXml( writer, true );
                            writer.WriteEndElement();
                            return true;
                        }
                    }
                    else if( o is IPartitionedFile )
                    {
                        using( XmlTextWriter writer = new XmlTextWriter( output, System.Text.Encoding.UTF8 ) )
                        {
                            writer.WriteStartElement( "dongs" );
                            (o as IPartitionedFile).WriteXml( writer, true );
                            writer.WriteEndElement();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public static Set<string> groups;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main( string[] args )
        {
            var f = new FFTPatcher.Datatypes.FFTFont( FFTPatcher.PSPResources.FontBin, FFTPatcher.PSPResources.FontWidthsBin );

            List<int> widths = new List<int>( 2200 );
            f.Glyphs.ForEach( g => widths.Add( g.Width ) );

            groups = TextUtilities.GetGroups( TextUtilities.PSPMap, FFTPatcher.PSPResources.CharacterSet, widths );
           
           
            if( !HandleArgs( args ) )
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault( false );
                Application.Run( new MainForm() );
            }
        }


		#endregion Methods 

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FFTPatcher.Datatypes
{
    public static class FFTPack
    {
        public static byte[] GetFile( FileStream stream, int index )
        {
            byte[] bytes = new byte[4];
            stream.Seek( index * 4 + 4, SeekOrigin.Begin );
            stream.Read( bytes, 0, 4 );
            UInt32 start = bytes.ToUInt32();

            UInt32 end;
            if( index == 3970 )
            {
                end = (UInt32)stream.Length;
            }
            else
            {
                stream.Read( bytes, 0, 4 );
                end = bytes.ToUInt32();
            }

            UInt32 length = end - start;

            byte[] result = new byte[length];

            stream.Seek( start, SeekOrigin.Begin );
            stream.Read( result, 0, (int)length );

            return result;
        }

        public static void PatchFile( string filename, int index, byte[] bytes )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( filename, FileMode.Open );
                stream.Seek( (index - 1) * 4 + 8, SeekOrigin.Begin );
                byte[] pointer = new byte[4];
                stream.Read( pointer, 0, 4 );

                stream.Seek( pointer.ToUInt32(), SeekOrigin.Begin );

                stream.Write( bytes, 0, bytes.Length );
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                    stream = null;
                }
            }
        }

        private static void SaveToFile( byte[] bytes, string path )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( path, FileMode.Create );
                stream.Write( bytes, 0, bytes.Length );
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                }
            }
        }

        private static void MakeDirectories( string path, params string[] dirs )
        {
            foreach( string dir in dirs )
            {
                Directory.CreateDirectory( Path.Combine( path, dir ) );
            }
        }

        public static void DumpToDirectory( FileStream stream, string path )
        {
            MakeDirectories( path, "BATTLE", "EFFECT", "EVENT", "MAP", "MENU", "SOUND", "WORLD", "SAVEIMAGE", "unknown" );
            for( int i = 1; i <= 3970; i++ )
            {
                byte[] bytes = GetFile( stream, i );
                string filename = string.Empty;

                if( Resources.FFTPackFiles.ContainsKey( i ) )
                {
                    filename = Resources.FFTPackFiles[i];
                }
                else if( bytes.Length == 0 )
                {
                    filename = string.Format( "unknown/fftpack.{0}.dummy", i );
                }
                else
                {
                    filename = string.Format( "unknown/fftpack.{0}", i );
                }

                filename = Path.Combine( path, filename );

                SaveToFile( bytes, filename );
            }
        }

        private static byte[] ReadFile( string path, int index )
        {
            FileStream stream = null;

            try
            {
                if( Resources.FFTPackFiles.ContainsKey(index) )
                {
                    stream = new FileStream( Path.Combine( path, Resources.FFTPackFiles[index] ), FileMode.Open );
                }
                else if( File.Exists( Path.Combine( path, string.Format( "unknown/fftpack.{0}.dummy", index ) ) ) )
                {
                    return new byte[0];
                }
                else if( File.Exists( Path.Combine( path, string.Format( "unknown/fftpack.{0}", index ) ) ) )
                {
                    stream = new FileStream( Path.Combine( path, string.Format( "unknown/fftpack.{0}", index ) ), FileMode.Open );
                }
                else
                {
                    throw new Exception();
                }

                byte[] result = new byte[stream.Length];
                stream.Read( result, 0, (int)stream.Length );

                return result;
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Close();
                    stream = null;
                }
            }
        }

        public static void MergeDumpedFiles( string path, string filename )
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream( filename, FileMode.Create );
                stream.WriteByte( 0x80 );
                stream.WriteByte( 0x0F );
                stream.WriteByte( 0x00 );
                stream.WriteByte( 0x00 );
                stream.WriteByte( 0x10 );
                stream.WriteByte( 0x00 );
                stream.WriteByte( 0x00 );
                stream.WriteByte( 0x00 );

                stream.Seek( 15888, SeekOrigin.Begin );
                UInt32 end = (UInt32)stream.Position;
                UInt32 start = 8;

                for( int i = 1; i <= 3970; i++ )
                {
                    stream.Seek( start, SeekOrigin.Begin );
                    stream.Write( end.ToBytes(), 0, 4 );
                    start += 4;

                    stream.Seek( end, SeekOrigin.Begin );
                    
                    byte[] bytes = ReadFile( path, i );
                    if( bytes.Length != 0 )
                    {
                        stream.Write( bytes, 0, bytes.Length );
                    }

                    end = (UInt32)stream.Position;
                }
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( stream != null )
                {
                    stream.Flush();
                    stream.Close();
                    stream = null;
                }
            }
        }
    }
}

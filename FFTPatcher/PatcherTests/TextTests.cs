using NUnit.Framework;
using FFTPatcher.TextEditor.Files.PSX;
using System.IO;
using FFTPatcher;
using System;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;
using System.Reflection;

namespace PatcherTests
{
    [TestFixture]
    public class TextTests
    {
        [Test]
        public void ShouldNotCompressExcludedEntries()
        {
            FFTPatcher.TextEditor.Files.PSP.WORLDLZW w = new FFTPatcher.TextEditor.Files.PSP.WORLDLZW( Resources.pspWORLDLZW );
            IList<byte> uncompressed = w.ToUncompressedBytes();
            FieldInfo fi = w.GetType().GetField( "exclusions", BindingFlags.Static | BindingFlags.NonPublic );
            List<int> newExclusions = new List<int>();

            int count = 0;
            foreach( IList<string> s in w.Sections )
            {
                foreach( string st in s )
                {
                    newExclusions.Add( count );
                    count++;
                }
            }
            fi.SetValue( null, newExclusions.ToArray() );

            byte[] compressed = w.ToByteArray();

            //for( int i = 0; i < uncompressed.Count; i++ )
            //{
            //    Assert.That( uncompressed[i], Is.EqualTo( compressed[i] ), "{0}", i );
            //}

            File.WriteAllBytes( "uncompressed.bin", uncompressed.ToArray() );
            File.WriteAllBytes( "compressed.bin", compressed );
        }

        //[Test]
        //public void ShouldSerializeAndDeserialize()
        //{
        //    WLDMESBIN file = new WLDMESBIN( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN );
        //    byte[] newBytes = file.ToByteArray();
        //    WLDMESBIN file2 = new WLDMESBIN( newBytes );
        //}

        //[Test]
        //public void ShouldConvertBase64()
        //{
        //    string base64 = Utilities.GetPrettyBase64( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN );
        //    byte[] bytes = Convert.FromBase64String( base64 );
        //    Assert.That( bytes, Is.EquivalentTo( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN ) );
        //}

        //[Test]
        //public void ShouldCompress()
        //{
        //    FileStream stream = new FileStream( "wldmes.bin.gz", FileMode.Create );
        //    IList<byte> compressed = GZip.Compress( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN );
        //    stream.Write( compressed.ToArray(), 0, compressed.Count );
        //    stream.Flush();
        //    stream.Close();

        //    stream = new FileStream( "wldmes.bin", FileMode.Create );
        //    byte[] decompressed = GZip.Decompress( compressed );
        //    stream.Write( decompressed, 0, decompressed.Length );
        //    stream.Flush();
        //    stream.Close();

        //    Assert.That( decompressed, Is.EquivalentTo( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN ) );
        //}
    }
}

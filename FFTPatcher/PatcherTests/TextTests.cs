using NUnit.Framework;
using FFTPatcher.TextEditor.Files.PSX;
using System.IO;
using FFTPatcher;
using System;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;

namespace PatcherTests
{
    [TestFixture]
    public class TextTests
    {
        [Test]
        public void ShouldSerializeAndDeserialize()
        {
            WLDMES file = new WLDMES( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN );
            byte[] newBytes = file.ToByteArray();
            WLDMES file2 = new WLDMES( newBytes );
        }

        [Test]
        public void ShouldConvertBase64()
        {
            string base64 = Utilities.GetPrettyBase64( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN );
            byte[] bytes = Convert.FromBase64String( base64 );
            Assert.That( bytes, Is.EquivalentTo( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN ) );
        }

        [Test]
        public void ShouldCompress()
        {
            FileStream stream = new FileStream( "wldmes.bin.gz", FileMode.Create );
            IList<byte> compressed = GZip.Compress( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN );
            stream.Write( compressed.ToArray(), 0, compressed.Count );
            stream.Flush();
            stream.Close();

            stream = new FileStream( "wldmes.bin", FileMode.Create );
            byte[] decompressed = GZip.Decompress( compressed );
            stream.Write( decompressed, 0, decompressed.Length );
            stream.Flush();
            stream.Close();

            Assert.That( decompressed, Is.EquivalentTo( FFTPatcher.TextEditor.PSXResources.WLDMES_BIN ) );
        }
    }
}

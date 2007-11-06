using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace LionEditor
{
    [TestFixture]
    public class LionEditorTests
    {
        [Test]
        public void ShouldWhatever()
        {
            FileStream stream = new FileStream( "testCharacter.hex", FileMode.Open );
            byte[] bytes = new byte[256];
            stream.Read( bytes, 0, 256 );
            Character c = new Character( bytes );
        }
    }
}

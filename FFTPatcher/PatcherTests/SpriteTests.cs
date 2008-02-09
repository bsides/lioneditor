using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using FFTPatcher.SpriteEditor;
using NUnit.Framework.SyntaxHelpers;

namespace PatcherTests
{
    [TestFixture]
    public class SpriteTests
    {
        [Test]
        public void ShouldSaveSprites()
        {
            PropertyInfo[] properties = typeof( Resources ).GetProperties( BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty );
            foreach( PropertyInfo p in properties )
            {
                string name = p.Name;

                if( p.PropertyType == typeof( byte[] ) )
                {
                    Console.Out.WriteLine( name );
                    byte[] bytes = p.GetValue( null, null ) as byte[];
                    Sprite s = new Sprite( bytes );
                    Sprite s2 = new Sprite( s.ToByteArray() );
                    Assert.That( AreEqual( s, s2 ), "Sprite failed: {0}", name );
                }
            }
        }

        public bool AreEqual( Sprite left, Sprite right )
        {
            for( int i = 0; i < left.Palettes.Length; i++ )
            {
                for( int j = 0; j < left.Palettes[i].Colors.Length; j++ )
                {
                    if( left.Palettes[i].Colors[j].ToArgb() != right.Palettes[i].Colors[j].ToArgb() )
                    {
                        Console.Out.WriteLine( "Palette {0} Color {1} differs", i, j );
                        return false;
                    }
                }
            }

            for( long i = 0; i < left.Pixels.LongLength; i++ )
            {
                if( left.Pixels[i] != right.Pixels[i] )
                {
                    Console.Out.WriteLine( "Pixel {0} ({1} {2})", i, left.Pixels[i], right.Pixels[i] );
                    return false;
                }
            }

            return true;
        }
    }
}

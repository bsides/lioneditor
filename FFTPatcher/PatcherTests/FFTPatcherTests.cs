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

using System.IO;
using System.Reflection;
using FFTPatcher.Datatypes;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;
using FFTPatcher;

namespace PatcherTests
{
    [TestFixture]
    public class FFTPatcherTests
    {
        [Test, Explicit]
        public void ShouldExpandFile()
        {
            FileStream stream = new FileStream( @"M:\dev\LionEditor\fftpack\fftpack.bin", FileMode.Open );
            //FFTPack.DumpToDirectory( stream, @"M:\dev\LionEditor\fftpack\test" );
            FFTPack.MergeDumpedFiles( @"M:\dev\LionEditor\fftpack\test", @"M:\dev\LionEditor\fftpack\fftpack2.bin" );
        }

        [Test]
        public void ShouldNotMangleAbilities()
        {
            FileStream stream = new FileStream( "Abilities.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllAbilities all = new AllAbilities( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleJobs()
        {
            FileStream stream = new FileStream( "Jobs.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllJobs all = new AllJobs( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleSkillSets()
        {
            FileStream stream = new FileStream( "SkillSetsBin.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllSkillSets all = new AllSkillSets( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleFont()
        {
            FileStream stream = new FileStream( "font.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            FFTFont font = new FFTFont( new SubArray<byte>( bytes ), new SubArray<byte>( new byte[0x899] ) );
            byte[] outputBytes = font.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleMonsterSkills()
        {
            FileStream stream = new FileStream( "MonsterSkills.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllMonsterSkills all = new AllMonsterSkills( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleActionEvents()
        {
            MethodInfo mi = typeof( FFTPatch ).GetMethod( "set_Context", BindingFlags.Static | BindingFlags.NonPublic );
            mi.Invoke( null, new object[] { Context.US_PSP } );

            FileStream stream = new FileStream( "ActionEvents.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllActionMenus all = new AllActionMenus( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleStatusAttributes()
        {
            FileStream stream = new FileStream( "StatusAttributes.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllStatusAttributes all = new AllStatusAttributes( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotManglePoachProbabilities()
        {
            FileStream stream = new FileStream( "PoachProbabilities.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllPoachProbabilities all = new AllPoachProbabilities( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleJobLevels()
        {
            FileStream stream = new FileStream( "JobLevels.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            JobLevels all = new JobLevels( new SubArray<byte>( bytes ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleItems()
        {
            FileStream stream = new FileStream( "OldItems.bin", FileMode.Open );
            byte[] oldBytes = new byte[stream.Length];
            stream.Read( oldBytes, 0, (int)stream.Length );
            stream.Close();

            stream = new FileStream( "NewItems.bin", FileMode.Open );
            byte[] newBytes = new byte[stream.Length];
            stream.Read( newBytes, 0, (int)stream.Length );
            stream.Close();

            AllItems all = new AllItems(
                new SubArray<byte>( oldBytes ),
                new SubArray<byte>( newBytes ));
            byte[] oldOutput = all.ToFirstByteArray();
            byte[] newOutput = all.ToSecondByteArray();

            Assert.That( oldOutput, Is.EqualTo( oldBytes ) );
            Assert.That( newOutput, Is.EqualTo( newBytes ) );
        }

        [Test]
        public void ShouldNotMangleENTD()
        {
            FileStream stream = new FileStream( "ENTD3.ENT", FileMode.Open );
            MethodInfo mi = typeof( FFTPatch ).GetMethod( "set_Context", BindingFlags.Static | BindingFlags.NonPublic );
            mi.Invoke( null, new object[] { Context.US_PSP } );
            
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            ENTD ent = new ENTD( 0x00, new SubArray<byte>( bytes ), null );
            byte[] output = ent.ToByteArray();

            Assert.That( output, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldGUnzip()
        {
            Type t = Type.GetType( "FFTPatcher.Properties.Resources,FFTPatcher", false );
            PropertyInfo p = t.GetProperty( "ENTD1_ENT", BindingFlags.Static | BindingFlags.NonPublic );
            byte[] b = p.GetValue( null, null ) as byte[];

            byte[] result = GZip.Decompress( b );

            byte[] expected = t.GetProperty( "ENTD1", BindingFlags.Static | BindingFlags.NonPublic ).GetValue( null, null ) as byte[];
            Assert.That( result, Is.EquivalentTo( expected ) );

        }

        [Test]
        public void ShouldHaveCorrectJobRequirements()
        {
            FileStream stream = new FileStream( "JobLevels.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            JobLevels all = new JobLevels( new SubArray<byte>( bytes ) );

            Assert.That( all.Archer.Squire, Is.EqualTo( 2 ) );

            Assert.That( all.Arithmetician.Mystic, Is.EqualTo( 4 ) );
            Assert.That( all.Arithmetician.TimeMage, Is.EqualTo( 4 ) );
            Assert.That( all.Arithmetician.BlackMage, Is.EqualTo( 5 ) );
            Assert.That( all.Arithmetician.WhiteMage, Is.EqualTo( 5 ) );
            Assert.That( all.Arithmetician.Chemist, Is.EqualTo( 2 ) );

            Assert.That( all.Bard.Orator, Is.EqualTo( 5 ) );
            Assert.That( all.Bard.Summoner, Is.EqualTo( 5 ) );
            Assert.That( all.Bard.Mystic, Is.EqualTo( 3 ) );
            Assert.That( all.Bard.TimeMage, Is.EqualTo( 3 ) );
            Assert.That( all.Bard.WhiteMage, Is.EqualTo( 3 ) );
            Assert.That( all.Bard.BlackMage, Is.EqualTo( 3 ) );
            Assert.That( all.Bard.Chemist, Is.EqualTo( 2 ) );

            Assert.That( all.BlackMage.Chemist, Is.EqualTo( 2 ) );

            Assert.That( all.Dancer.Dragoon, Is.EqualTo( 5 ) );
            Assert.That( all.Dancer.Geomancer, Is.EqualTo( 5 ) );
            Assert.That( all.Dancer.Monk, Is.EqualTo( 4 ) );
            Assert.That( all.Dancer.Thief, Is.EqualTo( 4 ) );
            Assert.That( all.Dancer.Archer, Is.EqualTo( 3 ) );
            Assert.That( all.Dancer.Knight, Is.EqualTo( 3 ) );
            Assert.That( all.Dancer.Squire, Is.EqualTo( 2 ) );

            Assert.That( all.DarkKnight.Samurai, Is.EqualTo( 8 ) );
            Assert.That( all.DarkKnight.Dragoon, Is.EqualTo( 8 ) );
            Assert.That( all.DarkKnight.Geomancer, Is.EqualTo( 8 ) );
            Assert.That( all.DarkKnight.Ninja, Is.EqualTo( 8 ) );

            Assert.That( all.Dragoon.Thief, Is.EqualTo( 4 ) );
            Assert.That( all.Dragoon.Archer, Is.EqualTo( 3 ) );
            Assert.That( all.Dragoon.Squire, Is.EqualTo( 2 ) );

            Assert.That( all.Geomancer.Monk, Is.EqualTo( 4 ) );
            Assert.That( all.Geomancer.Knight, Is.EqualTo( 3 ) );
            Assert.That( all.Geomancer.Squire, Is.EqualTo( 2 ) );

            Assert.That( all.Knight.Squire, Is.EqualTo( 2 ) );

            Assert.That( all.Mime.Squire, Is.EqualTo( 8 ) );
            Assert.That( all.Mime.Chemist, Is.EqualTo( 8 ) );
            Assert.That( all.Mime.Knight, Is.EqualTo( 3 ) );
            Assert.That( all.Mime.Archer, Is.EqualTo( 3 ) );
            Assert.That( all.Mime.Monk, Is.EqualTo( 4 ) );
            Assert.That( all.Mime.Thief, Is.EqualTo( 4 ) );
            Assert.That( all.Mime.WhiteMage, Is.EqualTo( 3 ) );
            Assert.That( all.Mime.Orator, Is.EqualTo( 5 ) );
            Assert.That( all.Mime.Mystic, Is.EqualTo( 3 ) );
            Assert.That( all.Mime.BlackMage, Is.EqualTo( 3 ) );
            Assert.That( all.Mime.TimeMage, Is.EqualTo( 3 ) );
            Assert.That( all.Mime.Summoner, Is.EqualTo( 5 ) );
            Assert.That( all.Mime.Dragoon, Is.EqualTo( 5 ) );
            Assert.That( all.Mime.Geomancer, Is.EqualTo( 5 ) );

            Assert.That( all.Monk.Knight, Is.EqualTo( 3 ) );
            Assert.That( all.Monk.Squire, Is.EqualTo( 2 ) );

            Assert.That( all.Mystic.Chemist, Is.EqualTo( 2 ) );
            Assert.That( all.Mystic.WhiteMage, Is.EqualTo( 3 ) );

            Assert.That( all.Ninja.Squire, Is.EqualTo( 2 ) );
            Assert.That( all.Ninja.Archer, Is.EqualTo( 4 ) );
            Assert.That( all.Ninja.Geomancer, Is.EqualTo( 2 ) );
            Assert.That( all.Ninja.Monk, Is.EqualTo( 4 ) );

            Assert.That( all.OnionKnight.Squire, Is.EqualTo( 6 ) );
            Assert.That( all.OnionKnight.Chemist, Is.EqualTo( 6 ) );

            Assert.That( all.Orator.Mystic, Is.EqualTo( 3 ) );
            Assert.That( all.Orator.WhiteMage, Is.EqualTo( 3 ) );
            Assert.That( all.Orator.Chemist, Is.EqualTo( 2 ) );

            Assert.That( all.Samurai.Knight, Is.EqualTo( 4 ) );
            Assert.That( all.Samurai.Squire, Is.EqualTo( 2 ) );
            Assert.That( all.Samurai.Dragoon, Is.EqualTo( 2 ) );
            Assert.That( all.Samurai.Thief, Is.EqualTo( 4 ) );

            Assert.That( all.Summoner.TimeMage, Is.EqualTo( 3 ) );
            Assert.That( all.Summoner.BlackMage, Is.EqualTo( 3 ) );
            Assert.That( all.Summoner.Chemist, Is.EqualTo( 2 ) );

            Assert.That( all.Thief.Archer, Is.EqualTo( 3 ) );
            Assert.That( all.Thief.Squire, Is.EqualTo( 2 ) );

            Assert.That( all.TimeMage.BlackMage, Is.EqualTo( 3 ) );
            Assert.That( all.TimeMage.Chemist, Is.EqualTo( 2 ) );

            Assert.That( all.WhiteMage.Chemist, Is.EqualTo( 2 ) );
        }
    }
}

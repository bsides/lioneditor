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
        public void ShouldBuildCharacterFromByteArray()
        {
            FileStream stream = new FileStream( "testCharacter.hex", FileMode.Open );
            byte[] bytes = new byte[256];
            stream.Read( bytes, 0, 256 );
            stream.Close();

            Character c = new Character( bytes );

            Assert.AreEqual( 0x80, c.SpriteSet );
            Assert.AreEqual( 1, c.Index );
            Assert.AreEqual( "Dark Knight", c.Job.Name );
            Assert.AreEqual( 3, c.Job.Move );
            Assert.AreEqual( 3, c.Job.Jump );
            Assert.AreEqual( 0x00, c.UnknownOffset03 );
            Assert.IsTrue( (c.Gender == Gender.Male) );
            Assert.AreEqual( 0x90, c.UnknownOffset05 );
            Assert.AreEqual( Zodiac.Gemini, c.ZodiacSign );
            Assert.AreEqual( "Steal", c.SecondaryAction.Name );
            Assert.AreEqual( "Shirahadori", c.ReactAbility.Name );
            Assert.AreEqual( "Vehemence", c.SupportAbility.Name );
            Assert.AreEqual( "Move +2", c.MovementAbility.Name );
            Assert.AreEqual( "Crystal Helm", c.Head.Name );
            Assert.AreEqual( "Mirror Mail", c.Body.Name );
            Assert.AreEqual( "Japa Mala", c.Accessory.Name );
            Assert.AreEqual( "Runeblade", c.RightHand.Name );
            Assert.AreEqual( "<Nothing>", c.RightShield.Name );
            Assert.AreEqual( "<Nothing>", c.LeftHand.Name );
            Assert.AreEqual( "Crystal Shield", c.LeftShield.Name );
            Assert.AreEqual( 50, c.Experience );
            Assert.AreEqual( 77, c.Level );
            Assert.AreEqual( 67, c.Brave );
            Assert.AreEqual( 61, c.Faith );
            Assert.AreEqual( 177, c.Job.ActualHP(c.RawHP) );
            Assert.AreEqual( 73, c.Job.ActualMP(c.RawMP) );
            Assert.AreEqual( 10, c.Job.ActualSP(c.RawSP) );
            Assert.AreEqual( 17, c.Job.ActualPA(c.RawPA) );
            Assert.AreEqual( 7, c.Job.ActualMA(c.RawMA) );
            Assert.AreEqual( new byte[] { 0xFF, 0x4F, 0x0E }, c.JobsUnlocked );
            for( int i = 0; i < 22; i++ )
            {
                for( int j = 0; j < 3; j++ )
                {
                    Assert.AreEqual( bytes[0x32 + i * 3 + j], c.SkillsUnlocked[i, j] );
                }
            }
            Assert.AreEqual( new byte[] { 0x88, 0x85, 0x51, 0x81, 0x18, 0x21, 0x88, 0x88, 0x11, 0x01, 0x73, 0x08 }, c.JobLevels );
            Assert.AreEqual( new ushort[] { 0x7DA, 0x7F, 0x1B3, 0x36D, 958, 178, 76, 176, 101, 168, 283, 194, 327, 233, 1599, 76, 113, 123, 0, 140, 89, 143, 0 }, c.JP );
            Assert.AreEqual( new ushort[] { 3680, 3557, 3485, 1577, 1558, 178, 5926, 176, 101, 3628, 283, 194, 3197, 7683, 9599, 4036, 113, 123, 0, 140, 2789, 400, 0 }, c.TotalJP );
            Assert.AreEqual( "Bran", c.Name );
        }

        [Test]
        public void ShouldBuildByteArrayFromCharacter()
        {
            FileStream stream = new FileStream( "testCharacter.hex", FileMode.Open );
            byte[] bytes = new byte[256];
            stream.Read( bytes, 0, 256 );
            stream.Close();
            Character c = new Character( bytes );
            byte[] newBytes = c.ToByteArray();

            Assert.AreEqual( bytes, newBytes );
        }

        [Test, Ignore("This is failing due to bad data for Dark Knight")]
        public void ShouldBuildSavegameFromByteArray()
        {
            FileStream stream = new FileStream( "FFTA.SYS", FileMode.Open );
            byte[] bytes = new byte[0x2A3C];
            stream.Read( bytes, 0, 0x2A3C );
            stream.Close();

            Savegame s = new Savegame( bytes );
            //2098:34:46

            Assert.AreEqual( "Ramza", s.SaveName );
            Assert.AreEqual( 2, s.Casualties );
            //Assert.AreEqual( 1, s.Day ); // 10 Cancer = 1 July
            //Assert.AreEqual( 7, s.Month );
            Assert.AreEqual( 10, s.SaveScreenDay );
            Assert.AreEqual( 4, s.SaveScreenMonth );
            Assert.AreEqual( 154, s.Kills );
            //Assert.AreEqual( (uint)((uint)2098 * (uint)3600 + (uint)34 * (uint)60 + (uint)46), s.Timer );
            Assert.AreEqual( 598499, s.WarFunds );

            Assert.AreEqual( "Japa Mala", s.Characters[0].Accessory.Name );
            Assert.AreEqual( "Excalibur", s.Characters[0].RightHand.Name );
            Assert.AreEqual( "Crystal Shield", s.Characters[0].LeftShield.Name );
            Assert.AreEqual( "Crystal Helm", s.Characters[0].Head.Name );
            Assert.AreEqual( "Mirror Mail", s.Characters[0].Body.Name );
            Assert.AreEqual( 77, s.Characters[0].Brave );
            Assert.AreEqual( 97, s.Characters[0].Experience );
            Assert.AreEqual( 70, s.Characters[0].Faith );
            Assert.IsTrue( (s.Characters[0].Gender == Gender.Male) );
            Assert.AreEqual( "Dark Knight", s.Characters[0].Job.Name );
            Assert.AreEqual( 77, s.Characters[0].Level );
            Assert.AreEqual( "Move +2", s.Characters[0].MovementAbility.Name );
            Assert.AreEqual( "Shirahadori", s.Characters[0].ReactAbility.Name );
            Assert.AreEqual( "Vehemence", s.Characters[0].SupportAbility.Name );
            Assert.AreEqual( "Martial Arts", s.Characters[0].SecondaryAction.Name );
            Assert.AreEqual( 182, s.Characters[0].Job.ActualHP( s.Characters[0].RawHP ) );
            Assert.AreEqual( 93, s.Characters[0].Job.ActualMP( s.Characters[0].RawMP ) );
            Assert.AreEqual( 3, s.Characters[0].Job.Move );
            Assert.AreEqual( 3, s.Characters[0].Job.Jump );
            Assert.AreEqual( 10, s.Characters[0].Job.ActualSP( s.Characters[0].RawSP ) );
            Assert.AreEqual( 18, s.Characters[0].Job.ActualPA( s.Characters[0].RawPA ) );
            Assert.AreEqual( 10, s.Characters[0].Job.ActualMA( s.Characters[0].RawMA ) );
        }

        [Test]
        public void ShouldBuildByteArrayFromSavegame()
        {
            FileStream stream = new FileStream( "FFTA.SYS", FileMode.Open );
            byte[] bytes = new byte[0x2A3C];
            stream.Read( bytes, 0, 0x2A3C );
            stream.Close();

            Savegame s = new Savegame( bytes );
            byte[] newBytes = s.ToByteArray();

            Assert.AreEqual( bytes, newBytes );
        }
    }
}

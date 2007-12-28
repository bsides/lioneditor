using System.Collections.Generic;
using System.IO;
using FFTPatcher.Datatypes;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace PatcherTests
{
    [TestFixture]
    public class FFTPatcherTests
    {
        [Test]
        public void ShouldNotMangleAbilities()
        {
            FileStream stream = new FileStream( "Abilities.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllAbilities all = new AllAbilities( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
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

            AllJobs all = new AllJobs( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
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

            AllSkillSets all = new AllSkillSets( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleMonsterSkills()
        {
            FileStream stream = new FileStream( "MonsterSkills.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllMonsterSkills all = new AllMonsterSkills( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
            byte[] outputBytes = all.ToByteArray();
            Assert.That( outputBytes, Is.EqualTo( bytes ) );
        }

        [Test]
        public void ShouldNotMangleActionEvents()
        {
            FileStream stream = new FileStream( "ActionEvents.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            AllActionMenus all = new AllActionMenus( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
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

            AllStatusAttributes all = new AllStatusAttributes( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
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

            AllPoachProbabilities all = new AllPoachProbabilities( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
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

            JobLevels all = new JobLevels( new SubArray<byte>( new List<byte>( bytes ), 0 ) );
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
                new SubArray<byte>( new List<byte>( oldBytes ), 0 ),
                new SubArray<byte>( new List<byte>( newBytes ), 0 ));
            byte[] oldOutput = all.ToFirstByteArray();
            byte[] newOutput = all.ToSecondByteArray();

            Assert.That( oldOutput, Is.EqualTo( oldBytes ) );
            Assert.That( newOutput, Is.EqualTo( newBytes ) );
        }

        [Test]
        public void ShouldHaveCorrectJobRequirements()
        {
            FileStream stream = new FileStream( "JobLevels.bin", FileMode.Open );
            byte[] bytes = new byte[stream.Length];
            stream.Read( bytes, 0, (int)stream.Length );
            stream.Close();

            JobLevels all = new JobLevels( new SubArray<byte>( new List<byte>( bytes ), 0 ) );

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

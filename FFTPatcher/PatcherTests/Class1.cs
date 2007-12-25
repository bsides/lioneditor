using System.Collections.Generic;
using System.IO;
using FFTPatcher.Datatypes;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace PatcherTests
{
    [TestFixture]
    public class Class1
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
    }
}

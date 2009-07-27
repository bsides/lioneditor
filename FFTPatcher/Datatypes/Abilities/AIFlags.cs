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

using System.Collections.Generic;
using PatcherLib.Utilities;

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents an <see cref="Ability"/>'s AI behavior.
    /// </summary>
    public class AIFlags : BaseDataType, ISupportDigest, ISupportDefault<AIFlags>
    {
		#region Instance Variables (25) 

        public bool AddStatus;
        public bool Blank;
        public bool CancelStatus;
        public bool DefenseUp;
        public bool DirectAttack;
        public bool HP;
        public bool IgnoreRange;
        public bool LineAttack;
        public bool MagicDefenseUp;
        public bool MP;
        public bool RandomHits;
        public bool Reflectable;
        public bool Silence;
        public bool Stats;
        public bool TargetAllies;
        public bool TargetEnemies;
        public bool TripleAttack;
        public bool TripleBracelet;
        public bool UndeadReverse;
        public bool Unequip;
        public bool Unknown1;
        public bool Unknown2;
        public bool Unknown3;
        public bool VerticalIncrease;

		#endregion Instance Variables 

		#region Public Properties (3) 

        public AIFlags Default { get; set; }

        public IList<string> DigestableProperties
        {
            get { return digestableProperties; }
        }

        public bool HasChanged
        {
            get { return !PatcherLib.Utilities.Utilities.CompareArrays( ToByteArray(), Default.ToByteArray() ); }
        }

		#endregion Public Properties 

		#region Constructors (2) 

        internal AIFlags()
        {
        }

        public AIFlags( IList<byte> bytes )
            : this( bytes, null )
        {
        }

        public AIFlags( IList<byte> bytes, AIFlags defaults )
        {
            Default = defaults;

            PatcherLib.Utilities.Utilities.CopyByteToBooleans( bytes[0],
                ref HP, ref MP, ref CancelStatus, ref AddStatus, ref Stats, ref Unequip, ref TargetEnemies, ref TargetAllies );

            PatcherLib.Utilities.Utilities.CopyByteToBooleans( bytes[1],
                ref IgnoreRange, ref Reflectable, ref UndeadReverse, ref Unknown1, ref RandomHits, ref Unknown2, ref Unknown3, ref Silence );
            Silence = !Silence;

            PatcherLib.Utilities.Utilities.CopyByteToBooleans( bytes[2],
                ref Blank, ref DirectAttack, ref LineAttack, ref VerticalIncrease, ref TripleAttack, ref TripleBracelet, ref MagicDefenseUp, ref DefenseUp );
            VerticalIncrease = !VerticalIncrease;
        }

		#endregion Constructors 

		#region Public Methods (4) 

        public static void Copy( AIFlags source, AIFlags destination )
        {
            destination.AddStatus = source.AddStatus;
            destination.Blank = source.Blank;
            destination.CancelStatus = source.CancelStatus;
            destination.DefenseUp = source.DefenseUp;
            destination.DirectAttack = source.DirectAttack;
            destination.HP = source.HP;
            destination.IgnoreRange = source.IgnoreRange;
            destination.LineAttack = source.LineAttack;
            destination.MagicDefenseUp = source.MagicDefenseUp;
            destination.MP = source.MP;
            destination.RandomHits = source.RandomHits;
            destination.Reflectable = source.Reflectable;
            destination.Silence = source.Silence;
            destination.Stats = source.Stats;
            destination.TargetAllies = source.TargetAllies;
            destination.TargetEnemies = source.TargetEnemies;
            destination.TripleAttack = source.TripleAttack;
            destination.TripleBracelet = source.TripleBracelet;
            destination.UndeadReverse = source.UndeadReverse;
            destination.Unequip = source.Unequip;
            destination.Unknown1 = source.Unknown1;
            destination.Unknown2 = source.Unknown2;
            destination.Unknown3 = source.Unknown3;
            destination.VerticalIncrease = source.VerticalIncrease;
        }

        public void CopyTo( AIFlags destination )
        {
            Copy( this, destination );
        }

        private static readonly string[] digestableProperties = new string[] {
            "HP","MP","CancelStatus","AddStatus","Stats","Unequip","TargetEnemies","TargetAllies",
            "IgnoreRange","Reflectable","UndeadReverse","Unknown1","RandomHits","Unknown2","Unknown3",
            "Silence","Blank","DirectAttack","LineAttack","VerticalIncrease","TripleAttack",
            "TripleBracelet","MagicDefenseUp","DefenseUp" };

        public bool[] ToBoolArray()
        {
            return new bool[24] { 
                HP, MP, CancelStatus, AddStatus, Stats, Unequip, TargetEnemies, TargetAllies,
                IgnoreRange, Reflectable, UndeadReverse, Unknown1, RandomHits, Unknown2, Unknown3, Silence,
                Blank, DirectAttack, LineAttack, VerticalIncrease, TripleAttack, TripleBracelet, MagicDefenseUp, DefenseUp };
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[3];
            result[0] = PatcherLib.Utilities.Utilities.ByteFromBooleans( HP, MP, CancelStatus, AddStatus, Stats, Unequip, TargetEnemies, TargetAllies );
            result[1] = PatcherLib.Utilities.Utilities.ByteFromBooleans( IgnoreRange, Reflectable, UndeadReverse, Unknown1, RandomHits, Unknown2, Unknown3, !Silence );
            result[2] = PatcherLib.Utilities.Utilities.ByteFromBooleans( Blank, DirectAttack, LineAttack, !VerticalIncrease, TripleAttack, TripleBracelet, MagicDefenseUp, DefenseUp );
            return result;
        }

		#endregion Public Methods 
    
        protected override void ReadXml( System.Xml.XmlReader reader )
        {
            reader.ReadStartElement();
            for ( int i = 0; i < DigestableProperties.Count; i++ )
            {
                PatcherLib.ReflectionHelpers.SetFieldOrProperty(
                    this, DigestableProperties[i], reader.ReadElementContentAsBoolean() );
            }
            reader.ReadEndElement();
        }

        protected override void WriteXml( System.Xml.XmlWriter writer )
        {
            bool[] bools = ToBoolArray();
            System.Diagnostics.Debug.Assert( bools.Length == digestableProperties.Length );
            for ( int i = 0; i < bools.Length; i++ )
            {
                writer.WriteValueElement( digestableProperties[i], bools[i] );
            }
        }
    }
}

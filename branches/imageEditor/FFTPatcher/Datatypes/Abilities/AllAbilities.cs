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

using System;
using System.Collections.Generic;
using System.Xml;

namespace FFTPatcher.Datatypes
{
    public class AllAbilityEffects : PatchableFile
    {
		#region Instance Variables (1) 

        private AllAbilities owner;

		#endregion Instance Variables 

		#region Public Properties (1) 

        public override bool HasChanged
        {
            get { return owner.Abilities.Exists( ability => ability.Effect != null && ability.Default != null && ability.Default.Effect != null && ability.Effect.Value != ability.Default.Effect.Value ); }
        }

		#endregion Public Properties 

		#region Constructors (1) 

        public AllAbilityEffects( AllAbilities owner )
        {
            this.owner = owner;
        }

		#endregion Constructors 

		#region Public Methods (1) 

        public override IList<PatchedByteArray> GetPatches( Context context )
        {
            byte[] effects = owner.ToEffectsByteArray();
            List<PatchedByteArray> result = new List<PatchedByteArray>( 2 );
            if( context == Context.US_PSX )
            {
                result.Add( new PatchedByteArray( PsxIso.BATTLE_BIN, 0x14F3F0, effects ) );
            }
            else if( context == Context.US_PSP )
            {
                result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, 0x3177B4, effects ) );
                result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, 0x3177B4, effects ) );
            }

            return result;
        }

		#endregion Public Methods 
    }

    /// <summary>
    /// Represents all of the Abilities in this file.
    /// </summary>
    public class AllAbilities : PatchableFile, IXmlDigest
    {

        #region Static Fields (2)

        private static Ability[] pspEventAbilites;
        private static Ability[] psxEventAbilites;

        #endregion Static Fields

        #region Static Properties (7)


        public static Ability[] DummyAbilities
        {
            get
            {
                return FFTPatch.Context == Context.US_PSP ? PSPAbilities : PSXAbilities;
            }
        }

        public static Ability[] EventAbilities
        {
            get { return FFTPatch.Context == Context.US_PSP ? pspEventAbilites : psxEventAbilites; }
        }

        /// <summary>
        /// Gets the names of all abilities, based on the current Context.
        /// </summary>
        public static string[] Names
        {
            get
            {
                return FFTPatch.Context == Context.US_PSP ? PSPNames : PSXNames;
            }
        }

        public static Ability[] PSPAbilities { get; private set; }

        public static string[] PSPNames { get; private set; }

        public static Ability[] PSXAbilities { get; private set; }

        public static string[] PSXNames { get; private set; }


        #endregion Static Properties

        #region Properties (3)


        public Ability[] Abilities { get; private set; }

        public Ability[] DefaultAbilities { get; private set; }

        public AllAbilityEffects AllEffects { get; private set;}

        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value></value>
        public override bool HasChanged
        {
            get
            {
                foreach( Ability a in Abilities )
                {
                    if( a.HasChanged )
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        #endregion Properties

        #region Constructors (2)

        static AllAbilities()
        {
            PSPAbilities = new Ability[512];
            PSXAbilities = new Ability[512];
            psxEventAbilites = new Ability[512];
            pspEventAbilites = new Ability[512];

            PSPNames = Utilities.GetStringsFromNumberedXmlNodes(
                PSPResources.Abilities,
                "/Abilities/Ability[@value='{0}']/@name",
                512 );
            PSXNames = Utilities.GetStringsFromNumberedXmlNodes(
                PSXResources.Abilities,
                "/Abilities/Ability[@value='{0}']/@name",
                512 );
            for( int i = 0; i < 512; i++ )
            {
                PSPAbilities[i] = new Ability( PSPNames[i], (UInt16)i );
                PSXAbilities[i] = new Ability( PSXNames[i], (UInt16)i );
                pspEventAbilites[i] = new Ability( PSPNames[i], (UInt16)i );
                psxEventAbilites[i] = new Ability( PSXNames[i], (UInt16)i );
            }

            pspEventAbilites[510] = new Ability( "<Random>", 510 );
            pspEventAbilites[511] = new Ability( "Nothing", 511 );
            psxEventAbilites[510] = new Ability( "<Random>", 510 );
            psxEventAbilites[511] = new Ability( "Nothing", 511 );

        }

        public AllAbilities( IList<byte> bytes, IList<byte> effectsBytes )
        {
            AllEffects = new AllAbilityEffects( this );
            byte[] defaultBytes = FFTPatch.Context == Context.US_PSP ? PSPResources.AbilitiesBin : PSXResources.AbilitiesBin;
            Dictionary<UInt16, Effect> effects = FFTPatch.Context == Context.US_PSP ? Effect.PSPEffects : Effect.PSXEffects;
            byte[] defaultEffects = FFTPatch.Context == Context.US_PSP ? PSPResources.AbilityEffectsBin : PSXResources.AbilityEffectsBin;

            Abilities = new Ability[512];
            DefaultAbilities = new Ability[512];
            for( UInt16 i = 0; i < 512; i++ )
            {
                IList<byte> defaultFirst = defaultBytes.Sub( i * 8, i * 8 + 7 );
                IList<byte> first = bytes.Sub( i * 8, i * 8 + 7 );
                IList<byte> second;
                IList<byte> defaultSecond;
                Effect effect = null;
                Effect defaultEffect = null;

                if( i <= 0x16F )
                {
                    second = bytes.Sub( 0x1000 + 14 * i, 0x1000 + 14 * i + 13 );
                    defaultSecond = defaultBytes.Sub( 0x1000 + 14 * i, 0x1000 + 14 * i + 13 );
                    effect = effects[Utilities.BytesToUShort( effectsBytes[i * 2], effectsBytes[i * 2 + 1] )];
                    defaultEffect = effects[Utilities.BytesToUShort( defaultEffects[i * 2], defaultEffects[i * 2 + 1] )];
                }
                else if( i <= 0x17D )
                {
                    second = bytes.Sub( 0x2420 + i - 0x170, 0x2420 + i - 0x170 );
                    defaultSecond = defaultBytes.Sub( 0x2420 + i - 0x170, 0x2420 + i - 0x170 );
                }
                else if( i <= 0x189 )
                {
                    second = bytes.Sub( 0x2430 + i - 0x17E, 0x2430 + i - 0x17E );
                    defaultSecond = defaultBytes.Sub( 0x2430 + i - 0x17E, 0x2430 + i - 0x17E );
                }
                else if( i <= 0x195 )
                {
                    second = bytes.Sub( 0x243C + (i - 0x18A) * 2, 0x243C + (i - 0x18A) * 2 + 1 );
                    defaultSecond = defaultBytes.Sub( 0x243C + (i - 0x18A) * 2, 0x243C + (i - 0x18A) * 2 + 1 );
                }
                else if( i <= 0x19D )
                {
                    second = bytes.Sub( 0x2454 + (i - 0x196) * 2, 0x2454 + (i - 0x196) * 2 + 1 );
                    defaultSecond = defaultBytes.Sub( 0x2454 + (i - 0x196) * 2, 0x2454 + (i - 0x196) * 2 + 1 );
                }
                else if( i <= 0x1A5 )
                {
                    second = bytes.Sub( 0x2464 + i - 0x19E, 0x2464 + i - 0x19E );
                    defaultSecond = defaultBytes.Sub( 0x2464 + i - 0x19E, 0x2464 + i - 0x19E );
                }
                else
                {
                    second = bytes.Sub( 0x246C + i - 0x1A6, 0x246C + i - 0x1A6 );
                    defaultSecond = defaultBytes.Sub( 0x246C + i - 0x1A6, 0x246C + i - 0x1A6 );
                }

                Abilities[i] = new Ability( Names[i], i, first, second, new Ability( Names[i], i, defaultFirst, defaultSecond ) );
                if( i <= 0x16F )
                {
                    Abilities[i].Effect = effect;
                    Abilities[i].Default.Effect = defaultEffect;
                }
            }
        }

        #endregion Constructors

        #region Methods (5)


        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                List<string> result = new List<string>();
                result.AddRange( Codes.GenerateCodes( Context.US_PSP, PSPResources.AbilitiesBin, this.ToByteArray(), 0x2754C0 ) );
                result.AddRange( Codes.GenerateCodes( Context.US_PSP, PSPResources.AbilityEffectsBin, this.ToEffectsByteArray(), 0x31B760 ) );
                return result;
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.AbilitiesBin, this.ToByteArray(), 0x05EBF0 );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> bytes = new List<byte>();
            for( UInt16 i = 0; i < 512; i++ )
            {
                bytes.AddRange( Abilities[i].ToByteArray() );
            }
            for( UInt16 i = 0; i < 512; i++ )
            {
                bytes.AddRange( Abilities[i].ToSecondByteArray() );
            }

            bytes.Insert( 0x242E, 0x00 );
            bytes.Insert( 0x242E, 0x00 );
            return bytes.ToArray();
        }

        public byte[] ToByteArray( Context context )
        {
            return ToByteArray();
        }

        public byte[] ToEffectsByteArray()
        {
            List<byte> result = new List<byte>( 0x2E0 );
            foreach( Ability a in Abilities )
            {
                if( a.IsNormal )
                {
                    result.AddRange( a.Effect.Value.ToBytes() );
                }
            }

            return result.ToArray();
        }

        public void WriteXml( XmlWriter writer )
        {
            if( HasChanged )
            {
                writer.WriteStartElement( this.GetType().Name );
                writer.WriteAttributeString( "changed", HasChanged.ToString() );
                foreach( Ability a in Abilities )
                {
                    a.WriteXml( writer );
                }
                writer.WriteEndElement();
            }
        }


        #endregion Methods


        public override IList<PatchedByteArray> GetPatches( Context context )
        {
            var result = new List<PatchedByteArray>( 4 );
            byte[] bytes = ToByteArray( context );

            if ( context == Context.US_PSX )
            {
                result.Add( new PatchedByteArray( PsxIso.SCUS_942_21, 0x4F3F0, bytes ) );
            }
            else if ( context == Context.US_PSP )
            {
                result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.BOOT_BIN, 0x271514, bytes ) );
                result.Add( new PatchedByteArray( PspIso.Files.PSP_GAME.SYSDIR.EBOOT_BIN, 0x271514, bytes ) );
            }

            return result;
        }
    }
}

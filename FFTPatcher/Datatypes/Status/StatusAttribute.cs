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
using PatcherLib;
using PatcherLib.Datatypes;
using PatcherLib.Utilities;

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents attributes of a specific Status ailment/effect.
    /// </summary>
    public class StatusAttribute : ISupportDigest
    {
		#region Instance Variables (17) 

        public bool Blank;
        public bool CanReact;
        private static readonly string[] digestableProperties = new string[] {
            "Blank1", "Blank2", "Order", "CT", "FreezeCT", "Unknown1", "Unknown2", "Unknown3", "Unknown4",
            "Unknown5", "Unknown6", "KO", "CanReact", "Blank", "IgnoreAttack", "Unknown7", "Unknown8",
            "Unknown9", "Unknown10", "Unknown11", "Cancels", "CantStackOn" };
        public bool FreezeCT;
        public bool IgnoreAttack;
        public bool KO;
        public bool Unknown1;
        public bool Unknown10;
        public bool Unknown11;
        public bool Unknown2;
        public bool Unknown3;
        public bool Unknown4;
        public bool Unknown5;
        public bool Unknown6;
        public bool Unknown7;
        public bool Unknown8;
        public bool Unknown9;

		#endregion Instance Variables 

		#region Public Properties (11) 

        [Hex]
        public byte Blank1 { get; set; }

        [Hex]
        public byte Blank2 { get; set; }

        public Statuses Cancels { get; private set; }

        public Statuses CantStackOn { get; private set; }

        public byte CT { get; set; }

        public StatusAttribute Default { get; private set; }

        public IList<string> DigestableProperties
        {
            get { return digestableProperties; }
        }

        public bool HasChanged
        {
            get
            {
                return Default != null &&
                    (Cancels.HasChanged ||
                    CantStackOn.HasChanged ||
                    Blank1 != Default.Blank1 ||
                    Blank2 != Default.Blank2 ||
                    CT != Default.CT ||
                    Order != Default.Order ||
                    Blank != Default.Blank ||
                    CanReact != Default.CanReact ||
                    FreezeCT != Default.FreezeCT ||
                    IgnoreAttack != Default.IgnoreAttack ||
                    KO != Default.KO ||
                    Unknown1 != Default.Unknown1 ||
                    Unknown2 != Default.Unknown2 ||
                    Unknown3 != Default.Unknown3 ||
                    Unknown4 != Default.Unknown4 ||
                    Unknown5 != Default.Unknown5 ||
                    Unknown6 != Default.Unknown6 ||
                    Unknown7 != Default.Unknown7 ||
                    Unknown8 != Default.Unknown8 ||
                    Unknown9 != Default.Unknown9 ||
                    Unknown10 != Default.Unknown10 ||
                    Unknown11 != Default.Unknown11);
            }
        }

        public string Name { get; private set; }

        [Hex]
        public byte Order { get; set; }

        public byte Value { get; private set; }

		#endregion Public Properties 

		#region Constructors (2) 

        public StatusAttribute( string name, byte value, IList<byte> bytes )
            : this( name, value, bytes, null )
        {
        }

        public StatusAttribute( string name, byte value, IList<byte> bytes, StatusAttribute defaults )
        {
            Default = defaults;
            Name = name;
            Value = value;

            Blank1 = bytes[0];
            Blank2 = bytes[1];
            Order = bytes[2];
            CT = bytes[3];

            PatcherLib.Utilities.Utilities.CopyByteToBooleans( bytes[4], ref FreezeCT, ref Unknown1, ref Unknown2, ref Unknown3, ref Unknown4, ref Unknown5, ref Unknown6, ref KO );
            PatcherLib.Utilities.Utilities.CopyByteToBooleans( bytes[5], ref CanReact, ref Blank, ref IgnoreAttack, ref Unknown7, ref Unknown8, ref Unknown9, ref Unknown10, ref Unknown11 );
            CanReact = !CanReact;
            Cancels = new Statuses( bytes.Sub( 6, 10 ), defaults == null ? null : defaults.Cancels );
            CantStackOn = new Statuses( bytes.Sub( 11, 15 ), defaults == null ? null : defaults.CantStackOn );
        }

		#endregion Constructors 

		#region Public Methods (5) 

        public static void Copy( StatusAttribute source, StatusAttribute destination )
        {
            source.Cancels.CopyTo( destination.Cancels );
            source.CantStackOn.CopyTo( destination.CantStackOn );
            destination.Blank1 = source.Blank1;
            destination.Blank2 = source.Blank2;
            destination.Order = source.Order;
            destination.CT = source.CT;
            destination.FreezeCT = source.FreezeCT;
            destination.KO = source.KO;
            destination.CanReact = source.CanReact;
            destination.Blank2 = source.Blank2;
            destination.IgnoreAttack = source.IgnoreAttack;
            destination.Unknown1 = source.Unknown1;
            destination.Unknown2 = source.Unknown2;
            destination.Unknown3 = source.Unknown3;
            destination.Unknown4 = source.Unknown4;
            destination.Unknown5 = source.Unknown5;
            destination.Unknown6 = source.Unknown6;
            destination.Unknown7 = source.Unknown7;
            destination.Unknown8 = source.Unknown8;
            destination.Unknown9 = source.Unknown9;
            destination.Unknown10 = source.Unknown10;
            destination.Unknown11 = source.Unknown11;
        }

        public void CopyTo( StatusAttribute destination )
        {
            Copy( this, destination );
        }

        public bool[] ToBoolArray()
        {
            return new bool[16] {
                FreezeCT, Unknown1, Unknown2, Unknown3, Unknown4, Unknown5, Unknown6, KO,
                CanReact, Blank, IgnoreAttack, Unknown7, Unknown8, Unknown9, Unknown10, Unknown11 };
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 16 );
            result.Add( Blank1 );
            result.Add( Blank2 );
            result.Add( Order );
            result.Add( CT );
            result.Add( PatcherLib.Utilities.Utilities.ByteFromBooleans( FreezeCT, Unknown1, Unknown2, Unknown3, Unknown4, Unknown5, Unknown6, KO ) );
            result.Add( PatcherLib.Utilities.Utilities.ByteFromBooleans( !CanReact, Blank, IgnoreAttack, Unknown7, Unknown8, Unknown9, Unknown10, Unknown11 ) );
            result.AddRange( Cancels.ToByteArray() );
            result.AddRange( CantStackOn.ToByteArray() );

            return result.ToArray();
        }

        public override string ToString()
        {
            return (HasChanged ? "*" : "") + Name;
        }

		#endregion Public Methods 
    }

    public class AllStatusAttributes : PatchableFile, IXmlDigest
    {
		#region Public Properties (2) 

        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value></value>
        public override bool HasChanged
        {
            get
            {
                foreach( StatusAttribute s in StatusAttributes )
                {
                    if ( s.Default != null && !PatcherLib.Utilities.Utilities.CompareArrays( s.ToByteArray(), s.Default.ToByteArray() ) )
                        return true;
                }

                return false;
            }
        }

        public StatusAttribute[] StatusAttributes { get; private set; }

		#endregion Public Properties 

		#region Constructors (1) 

        public AllStatusAttributes( IList<byte> bytes )
        {
            StatusAttributes = new StatusAttribute[40];
            byte[] defaultBytes = FFTPatch.Context == Context.US_PSP ? PSPResources.StatusAttributesBin : PSXResources.StatusAttributesBin;

            string[] names = FFTPatch.Context == Context.US_PSP ? PSPResources.Statuses : PSXResources.Statuses;
            for( int i = 0; i < 40; i++ )
            {
                StatusAttributes[i] =
                    new StatusAttribute( names[i], (byte)i, bytes.Sub( 16 * i, 16 * i + 15 ),
                        new StatusAttribute( names[i], (byte)i, defaultBytes.Sub( 16 * i, 16 * i + 15 ) ) );
            }
        }

		#endregion Constructors 

		#region Public Methods (5) 

        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Codes.GenerateCodes( Context.US_PSP, PSPResources.StatusAttributesBin, this.ToByteArray(), 0x27AD50 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.StatusAttributesBin, this.ToByteArray(), 0x065DE4 );
            }
        }

        public override IList<PatchedByteArray> GetPatches( Context context )
        {
            var result = new List<PatchedByteArray>( 2 );

            var bytes = ToByteArray( context );
            if ( context == Context.US_PSX )
            {
                result.Add(PatcherLib.Iso.PsxIso.StatusAttributes.GetPatchedByteArray(bytes));
            }
            else if ( context == Context.US_PSP )
            {
                PatcherLib.Iso.PspIso.StatusAttributes.ForEach(kl => result.Add(kl.GetPatchedByteArray(bytes)));
            }

            return result;
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 640 );
            foreach( StatusAttribute attr in StatusAttributes )
            {
                result.AddRange( attr.ToByteArray() );
            }

            return result.ToArray();
        }

        public byte[] ToByteArray( Context context )
        {
            return ToByteArray();
        }

        public void WriteXml( System.Xml.XmlWriter writer )
        {
            if( HasChanged )
            {
                writer.WriteStartElement( this.GetType().Name );
                writer.WriteAttributeString( "changed", HasChanged.ToString() );
                foreach( StatusAttribute attr in StatusAttributes )
                {
                    if( attr.HasChanged )
                    {
                        writer.WriteStartElement( attr.GetType().Name );
                        writer.WriteAttributeString( "name", attr.Name );
                        writer.WriteAttributeString( "value", attr.Value.ToString( "X2" ) );
                        DigestGenerator.WriteXmlDigest( attr, writer, false, true );
                    }
                }

                writer.WriteEndElement();
            }
        }

		#endregion Public Methods 
    }
}

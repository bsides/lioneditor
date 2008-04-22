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

namespace FFTPatcher.Datatypes
{
    /// <summary>
    /// Represents attributes of a specific Status ailment/effect.
    /// </summary>
    public class StatusAttribute
    {

		#region Fields (16) 

 // inverted
        public bool Blank;
        public bool CanReact;
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

		#endregion Fields 

		#region Properties (9) 


        public byte Blank1 { get; set; }

        public byte Blank2 { get; set; }

        public Statuses Cancels { get; private set; }

        public Statuses CantStackOn { get; private set; }

        public byte CT { get; set; }

        public StatusAttribute Default { get; private set; }

        public string Name { get; private set; }

        public byte Order { get; set; }

        public byte Value { get; private set; }


		#endregion Properties 

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

            Utilities.CopyByteToBooleans( bytes[4], ref FreezeCT, ref Unknown1, ref Unknown2, ref Unknown3, ref Unknown4, ref Unknown5, ref Unknown6, ref KO );
            Utilities.CopyByteToBooleans( bytes[5], ref CanReact, ref Blank, ref IgnoreAttack, ref Unknown7, ref Unknown8, ref Unknown9, ref Unknown10, ref Unknown11 );
            CanReact = !CanReact;
            Cancels = new Statuses( bytes.Sub( 6, 10 ), defaults == null ? null : defaults.Cancels );
            CantStackOn = new Statuses( bytes.Sub( 11, 15 ), defaults == null ? null : defaults.CantStackOn );
        }

		#endregion Constructors 

		#region Methods (3) 


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
            result.Add( Utilities.ByteFromBooleans( FreezeCT, Unknown1, Unknown2, Unknown3, Unknown4, Unknown5, Unknown6, KO ) );
            result.Add( Utilities.ByteFromBooleans( !CanReact, Blank, IgnoreAttack, Unknown7, Unknown8, Unknown9, Unknown10, Unknown11 ) );
            result.AddRange( Cancels.ToByteArray() );
            result.AddRange( CantStackOn.ToByteArray() );

            return result.ToArray();
        }



        public override string ToString()
        {
            return Name;
        }


		#endregion Methods 

    }

    public class AllStatusAttributes : IChangeable
    {

		#region Properties (2) 


        /// <summary>
        /// Gets a value indicating whether this instance has changed.
        /// </summary>
        /// <value></value>
        public bool HasChanged
        {
            get
            {
                foreach( StatusAttribute s in StatusAttributes )
                {
                    if( s.Default != null && !Utilities.CompareArrays( s.ToByteArray(), s.Default.ToByteArray() ) )
                        return true;
                }

                return false;
            }
        }

        public StatusAttribute[] StatusAttributes { get; private set; }


		#endregion Properties 

		#region Constructors (1) 

        public AllStatusAttributes( IList<byte> bytes )
        {
            StatusAttributes = new StatusAttribute[40];
            byte[] defaultBytes = FFTPatch.Context == Context.US_PSP ? Resources.StatusAttributesBin : PSXResources.StatusAttributesBin;

            string[] names = FFTPatch.Context == Context.US_PSP ? Resources.Statuses : PSXResources.Statuses;
            for( int i = 0; i < 40; i++ )
            {
                StatusAttributes[i] =
                    new StatusAttribute( names[i], (byte)i, bytes.Sub( 16 * i, 16 * i + 15 ),
                        new StatusAttribute( names[i], (byte)i, defaultBytes.Sub( 16 * i, 16 * i + 15 ) ) );
            }
        }

		#endregion Constructors 

		#region Methods (3) 


        public List<string> GenerateCodes()
        {
            if( FFTPatch.Context == Context.US_PSP )
            {
                return Codes.GenerateCodes( Context.US_PSP, Resources.StatusAttributesBin, this.ToByteArray(), 0x27AD50 );
            }
            else
            {
                return Codes.GenerateCodes( Context.US_PSX, PSXResources.StatusAttributesBin, this.ToByteArray(), 0x065DE4 );
            }
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


		#endregion Methods 

    }
}

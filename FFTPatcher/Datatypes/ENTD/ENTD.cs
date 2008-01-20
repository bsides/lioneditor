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
    /// Represents all Events in the game.
    /// </summary>
    public class ENTD
    {
        public Event[] Events { get; private set; }

        public ENTD Default { get; private set; }

        public ENTD( int start, SubArray<byte> bytes, ENTD defaults )
        {
            Default = defaults;
            Events = new Event[0x80];
            for( int i = 0; i < 0x80; i++ )
            {
                Events[i] = new Event(
                    i + start,
                    new SubArray<byte>( bytes, i * 16 * 40, (i + 1) * 16 * 40 - 1 ),
                    defaults == null ? null : defaults.Events[i] );
            }
        }

        public byte[] ToByteArray()
        {
            List<byte> result = new List<byte>( 16 * 40 * 0x80 );
            foreach( Event e in Events )
            {
                result.AddRange( e.ToByteArray() );
            }

            return result.ToArray();
        }
    }

    public class AllENTDs
    {
        public ENTD[] ENTDs { get; private set; }
        public List<Event> Events { get; private set; }
        public List<Event> PSPEvent { get; private set; }

        public AllENTDs( IList<byte> entd1, IList<byte> entd2, IList<byte> entd3, IList<byte> entd4 )
        {
            ENTDs = new ENTD[4];
            ENTDs[0] = new ENTD( 
                0,
                new SubArray<byte>( entd1 ), 
                new ENTD( 0, new SubArray<byte>( Resources.ENTD1 ), null ) );
            ENTDs[1] = new ENTD( 
                0x80,
                new SubArray<byte>( entd2 ), 
                new ENTD( 0x80, new SubArray<byte>( Resources.ENTD2 ), null ) );
            ENTDs[2] = new ENTD( 
                0x100,
                new SubArray<byte>( entd3 ), 
                new ENTD( 0x100, new SubArray<byte>( Resources.ENTD3 ), null ) );
            ENTDs[3] = new ENTD( 
                0x180,
                new SubArray<byte>( entd4 ), 
                new ENTD( 0x180, new SubArray<byte>( Resources.ENTD4 ), null ) );

            Events = new List<Event>( 0x200 );
            foreach( ENTD e in ENTDs )
            {
                Events.AddRange( e.Events );
            }

            if( FFTPatch.Context == Context.US_PSP )
            {
                PSPEvent = new List<Event>( 77 );
                for( int i = 0; i < 77; i++ )
                {
                    PSPEvent.Add( new Event( 0x200 + i, new SubArray<byte>( Resources.ENTD5, i * 16 * 40, (i + 1) * 16 * 40 - 1 ),
                        new Event( 0x200 + i, new SubArray<byte>( Resources.ENTD5, i * 16 * 40, (i + 1) * 16 * 40 - 1 ), null ) ) );
                }

                Events.AddRange( PSPEvent );
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace LionEditor
{
    public class Savefile
    {
        const uint saveFileSize = 0x2A3C;

        byte[] offset0x00 = new byte[257];
        string saveName;
        byte[] saveNameRaw = new byte[15];

        public string SaveName
        {
            get { return Character.DecodeName( saveNameRaw, 0 ); }
            set { Character.EncodeName( value, saveNameRaw, 0 ); }
        }

        byte[] offset0x110 = new byte[4];
        byte saveScreenMonth;
        byte saveScreenDay;
        byte saveScreenMapPosition;
        byte[] offset0x117 = new byte[17];
        uint timer;
        byte[] artifactDates = new byte[53];
        byte[] wondersDates = new byte[17];
        byte[] featsDates = new byte[108];
        byte[] offset0x1DE = new byte[610];
        byte numPropositions;
        byte[] prop1 = new byte[9];
        byte[] prop2 = new byte[9];
        byte[] prop3 = new byte[9];
        byte[] prop4 = new byte[9];
        byte[] prop5 = new byte[9];
        byte[] prop6 = new byte[9];
        byte[] prop7 = new byte[9];
        byte[] prop8 = new byte[9];
        byte[] offset0x489 = new byte[3];
        Character[] characters = new Character[24];
        Character[] guests = new Character[4];
        byte[] inventory = new byte[316];
        byte[] furshop = new byte[316];
        byte[] offset0x2304 = new byte[304];
        uint warFunds;
        byte[] offset0x2438 = new byte[4];
        uint month;
        uint day;
        byte[] offset0x2444 = new byte[4];
        byte mapPosition;
        byte[] offset0x2449 = new byte[191];
        uint kills;
        uint casualties;
        byte[] offset0x2510 = new byte[200];
        byte[] artefacts = new byte[6];
        byte[] wonders = new byte[2];
        byte[] feats = new byte[48];
        byte[] offset0x2610 = new byte[372];
        byte[] options = new byte[4];
        byte[] offset0x2788 = new byte[692];

        public Savefile( byte[] file )
        {
            CopyArray( file, offset0x00, 0, 257 );
            CopyArray( file, saveNameRaw, 0x101, 15 );

            CopyArray( file, offset0x110, 0x110, 4 );
            saveScreenMonth = file[0x114];
            saveScreenDay = file[0x115];
            saveScreenMapPosition = file[0x116];
            CopyArray( file, offset0x117, 0x117, 17 );
            timer = (uint)((uint)file[0x128] + ((uint)file[0x129] << 8) + ((uint)file[0x12A] << 16) + ((uint)file[0x12B] << 24));
            CopyArray( file, artifactDates, 0x12C, 53 );
            CopyArray( file, wondersDates, 0x161, 17 );
            CopyArray( file, featsDates, 0x172, 108 );
            CopyArray( file, offset0x1DE, 0x1DE, 610 );
            numPropositions = file[0x440];
            CopyArray( file, prop1, 0x441, 9 );
            CopyArray( file, prop2, 0x44A, 9 );
            CopyArray( file, prop3, 0x453, 9 );
            CopyArray( file, prop4, 0x45C, 9 );
            CopyArray( file, prop5, 0x465, 9 );
            CopyArray( file, prop6, 0x46E, 9 );
            CopyArray( file, prop7, 0x477, 9 );
            CopyArray( file, prop8, 0x480, 9 );
            CopyArray( file, offset0x489, 0x489, 3 );
            for( int i = 0; i < 24; i++ )
            {
                byte[] charBytes = new byte[256];
                CopyArray( file, charBytes, 0x48C + i * 0x100, 0x100 );
                try
                {
                    characters[i] = new Character( charBytes );
                }
                catch( Exception e )
                {
                    if( !(e is BadCharacterDataException) )
                    {
                        throw;
                    }
                }
            }
            for( int i = 0; i < 4; i++ )
            {
                byte[] guestBytes = new byte[256];
                CopyArray( file, guestBytes, 0x1C8C + i * 0x100, 0x100 );
                try
                {
                    guests[i] = new Character( guestBytes );
                }
                catch( Exception e )
                {
                    if( !(e is BadCharacterDataException) )
                    {
                        throw;
                    }
                }
            }
            CopyArray( file, inventory, 0x208C, 316 );
            CopyArray( file, furshop, 0x21C8, 316 );
            CopyArray( file, offset0x2304, 0x2304, 304 );
            warFunds = (uint)((uint)file[0x2434] + ((uint)file[0x2435] << 8) + ((uint)file[0x2436] << 16) + ((uint)file[0x2437] << 24));
            CopyArray( file, offset0x2438, 0x2438, 4 );
            month = file[0x243C];
            day = file[0x2440];
            CopyArray( file, offset0x2444, 0x2444, 4 );
            mapPosition = file[0x2448];
            CopyArray( file, offset0x2449, 0x2449, 191 );
            kills = (uint)((uint)file[0x2508] + ((uint)file[0x2509] << 8) + ((uint)file[0x250A] << 16) + ((uint)file[0x250B] << 25));
            casualties = (uint)((uint)file[0x250C] + ((uint)file[0x250D] << 8) + ((uint)file[0x250E] << 16) + ((uint)file[0x250F] << 24));
            CopyArray( file, offset0x2510, 0x2510, 200 );
            CopyArray( file, artefacts, 0x25D8, 6 );
            CopyArray( file, wonders, 0x25DE, 2 );
            CopyArray( file, feats, 0x25E0, 48 );
            CopyArray( file, offset0x2610, 0x2610, 372 );
            CopyArray( file, options, 0x2784, 4 );
            CopyArray( file, offset0x2788, 0x2788, 692 );
        }

        public byte[] ToByteArray()
        {
            byte[] result = new byte[saveFileSize];
            CopyArray( offset0x00, result, 0, 0, 257 );
            CopyArray( saveNameRaw, result, 0, 0x101, 15 );
            CopyArray( offset0x110, result, 0, 0x110, 4 );
            result[0x114] = saveScreenMonth;
            result[0x115] = saveScreenDay;
            result[0x116] = saveScreenMapPosition;
            CopyArray( offset0x117, result, 0, 0x117, 17 );
            result[0x128] = (byte)(timer & 0xFF);
            result[0x129] = (byte)((timer >> 8) & 0xFF);
            result[0x12A] = (byte)((timer >> 16) & 0xFF);
            result[0x12B] = (byte)((timer >> 24) & 0xFF);
            CopyArray( artifactDates, result, 0, 0x12C, 53 );
            CopyArray( wondersDates, result, 0, 0x161, 17 );
            CopyArray( featsDates, result, 0, 0x172, 108 );
            CopyArray( offset0x1DE, result, 0, 0x1DE, 610 );
            result[0x440] = numPropositions;
            CopyArray( prop1, result, 0, 0x441, 9 );
            CopyArray( prop2, result, 0, 0x44A, 9 );
            CopyArray( prop3, result, 0, 0x453, 9 );
            CopyArray( prop4, result, 0, 0x45C, 9 );
            CopyArray( prop5, result, 0, 0x465, 9 );
            CopyArray( prop6, result, 0, 0x46E, 9 );
            CopyArray( prop7, result, 0, 0x477, 9 );
            CopyArray( prop8, result, 0, 0x480, 9 );
            CopyArray( offset0x489, result, 0, 0x489, 3 );
            for( int i = 0; i < 24; i++ )
            {
                if( characters[i] != null )
                {
                    byte[] charBytes = characters[i].ToByteArray();
                    CopyArray( charBytes, result, 0, 0x48C + i * 0x100, 0x100 );
                }
            }
            for( int i = 0; i < 4; i++ )
            {
                if( guests[i] != null )
                {
                    byte[] guestBytes = guests[i].ToByteArray();
                    CopyArray( guestBytes, result, 0, 0x1C8C + i * 0x100, 0x100 );
                }
            }
            CopyArray( inventory, result, 0, 0x208C, 316 );
            CopyArray( furshop, result, 0, 0x21C8, 316 );
            CopyArray( offset0x2304, result, 0, 0x2304, 304 );
            result[0x2434] = (byte)(warFunds & 0xFF);
            result[0x2435] = (byte)((warFunds >> 8) & 0xFF);
            result[0x2436] = (byte)((warFunds >> 16) & 0xFF);
            result[0x2437] = (byte)((warFunds >> 24) & 0xFF);
            CopyArray( offset0x2438, result, 0, 0x2438, 4 );
            result[0x243C] = (byte)month;
            result[0x2440] = (byte)day;
            CopyArray( offset0x2444, result, 0, 0x2444, 4 );
            result[0x2448] = mapPosition;
            CopyArray( offset0x2449, result, 0, 0x2449, 191 );
            result[0x2508] = (byte)(kills & 0xFF);
            result[0x2509] = (byte)((kills >> 8) & 0xFF);
            result[0x250A] = (byte)((kills >> 16) & 0xFF);
            result[0x250B] = (byte)((kills >> 24) & 0xFF);
            result[0x250C] = (byte)(casualties & 0xFF);
            result[0x250D] = (byte)((casualties >> 8) & 0xFF);
            result[0x250E] = (byte)((casualties >> 16) & 0xFF);
            result[0x250F] = (byte)((casualties >> 24) & 0xFF);
            CopyArray( offset0x2510, result, 0, 0x2510, 200 );
            CopyArray( artefacts, result, 0, 0x25D8, 6 );
            CopyArray( wonders, result, 0, 0x25DE, 2 );
            CopyArray( feats, result, 0, 0x25E0, 48 );
            CopyArray( offset0x2610, result, 0, 0x2610, 372 );
            CopyArray( options, result, 0, 0x2784, 4 );
            CopyArray( offset0x2788, result, 0, 0x2788, 692 );

            return result;
        }

        private void CopyArray( byte[] source, byte[] destination, int sourceStart, int destStart, int length )
        {
            for( int i = 0; i < length; i++ )
            {
                destination[i + destStart] = source[i + sourceStart];
            }
        }

        private void CopyArray( byte[] source, byte[] destination, int sourceStart, int length )
        {
            CopyArray( source, destination, sourceStart, 0, length );
        }
    }


}

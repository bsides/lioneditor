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
using System.Windows.Forms;

namespace FFTPatcher.SpriteEditor
{
    static class Program
    {


		#region Methods (1) 


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            foreach( var pi in typeof( TYPE1Sprite ).GetProperties( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public ) )
            {
                if( pi.PropertyType == typeof( TYPE1Sprite ) )
                {
                    var s = pi.GetValue( null, null ) as TYPE1Sprite;
                    s.ToBitmap( true ).Save( pi.Name + ".bmp" );
                }
            }
            foreach( var pi in typeof( TYPE2Sprite ).GetProperties( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public ) )
            {
                if( pi.PropertyType == typeof( TYPE2Sprite ) )
                {
                    var s = pi.GetValue( null, null ) as TYPE2Sprite;
                    s.ToBitmap( true ).Save( pi.Name + ".bmp" );
                }
            }
            foreach( var pi in typeof( MonsterSprite ).GetProperties( System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public ) )
            {
                if( pi.PropertyType == typeof( MonsterSprite ) )
                {
                    var s = pi.GetValue( null, null ) as MonsterSprite;
                    s.ToBitmap( true ).Save( pi.Name + ".bmp" );
                }
            }
            //AbstractSprite s = TYPE1Sprite.AGURI;
            //s.ToBitmap(true).Save( "bmp.bmp" );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new MainForm() );
        }


		#endregion Methods 

    }
}

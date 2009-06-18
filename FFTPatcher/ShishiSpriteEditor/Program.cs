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
            MessageBox.Show( "ADD WEP.SPR" );
            //using (System.IO.Stream s = System.IO.File.Open(@"N:\dev\fft\images\fflw-usa.rearranged - Copy.iso", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
            //{
            //    AllSprites.ExpandPspIso(s);
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new MainForm() );

            //using (System.IO.Stream iso = System.IO.File.OpenRead(@"N:\dev\fft\images\fft-usa - Copy.bin"))
            //using (System.IO.Stream iso = System.IO.File.Open(@"N:\dev\fft\images\fft-usa - Copy.bin", System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite))
            //{
            //    var s =  AllSprites.FromPsxIso( iso );
            //    //foreach (int i in new int[] { 230000, 230001, 230002, 230003, 230004, 230005, 230006 })
            //    //{
            //    //    PatcherLib.Iso.PsxIso.KnownPosition kp = new PatcherLib.Iso.PsxIso.KnownPosition((PatcherLib.Iso.PsxIso.Sectors)i, 0, 2048);
            //    //    byte[] bytes = PatcherLib.Iso.PsxIso.ReadFile(iso, kp);
            //    //    PatcherLib.Datatypes.PatchedByteArray pba = new PatcherLib.Datatypes.PatchedByteArray(i, 0, bytes);
            //    //    PatcherLib.Iso.PsxIso.PatchPsxIso(iso, new PatcherLib.Datatypes.PatchedByteArray[] { pba });
            //    //}

            //}
            //Application.Run(new TestForm());
        }


		#endregion Methods 

    }
}

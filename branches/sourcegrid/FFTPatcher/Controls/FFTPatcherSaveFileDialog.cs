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
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;

namespace FFTPatcher.Controls
{
    public class FFTPatcherSaveFileDialog
    {

		#region Fields (28) 

        private const int BM_GETCHECK = 0x00F0;
        private const int BM_SETCHECK = 0x00F1;
        private const int BS_AUTOCHECKBOX = 0x00000003;
        private const int BST_CHECKED = 1;
        private const int BST_UNCHECKED = 0;
        private const int CDN_FILEOK = -606;
        private int checkboxHandle = -1;
        private const int OFN_CREATEPROMPT = 0x00002000;
        private const int OFN_ENABLEHOOK = 0x00000020;
        private const int OFN_EXPLORER = 0x00080000;
        private const int OFN_FILEMUSTEXIST = 0x00001000;
        private const int OFN_HIDEREADONLY = 0x00000004;
        private const int OFN_NOTESTFILECREATE = 0x00010000;
        private const int OFN_OVERWRITEPROMPT = 0x00000002;
        private const int OFN_PATHMUSTEXIST = 0x00000800;
        private Screen screen = null;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOZORDER = 0x0004;
        private const int WM_DESTROY = 0x2;
        private const int WM_GETFONT = 0x0031;
        private const int WM_INITDIALOG = 0x110;
        private const int WM_NOTIFY = 0x004E;
        private const int WM_SETFONT = 0x0030;
        private const uint WS_CHILD = 0x40000000;
        private const int WS_CLIPCHILDREN = 0x02000000;
        private const uint WS_TABSTOP = 0x00010000;
        private const uint WS_VISIBLE = 0x10000000;

		#endregion Fields 

		#region Properties (4) 

        public string DefaultExtension { get; set; }

        public string Filename { get; set; }

        public string Filter { get; set; }

        public bool PatchSCEAPChecked { get; set; }

		#endregion Properties 

		#region Delegates and Events (1) 


		// Delegates (1) 

        private delegate int OFNHookProcDelegate( int hdlg, int msg, int wParam, int lParam );

		#endregion Delegates and Events 

		#region Methods (14) 


		// Public Methods (1) 

        public DialogResult ShowDialog()
        {
            OPENFILENAME open = new OPENFILENAME();
            open.lStructSize = Marshal.SizeOf( open );
            open.lpstrFilter = Filter.Replace( '|', '\0' ) + '\0';
            open.lpstrFile = Filename + new string( ' ', 512 );
            open.nMaxFile = open.lpstrFile.Length;
            open.lpstrFileTitle = Path.GetFileName( Filename ) + new string( ' ', 512 );
            open.nMaxFileTitle = open.lpstrFileTitle.Length;
            open.lpstrFileTitle = "ISO to patch";
            open.lpstrDefExt = ".iso";
            open.hwndOwner = Application.OpenForms[0].Handle;
            screen = Screen.FromControl( Application.OpenForms[0] );
            open.Flags = ( OFN_EXPLORER | OFN_PATHMUSTEXIST | OFN_NOTESTFILECREATE | OFN_ENABLEHOOK ) & ~OFN_OVERWRITEPROMPT;
            open.lpfnHook = new OFNHookProcDelegate( HookProc );
            if ( Environment.OSVersion.Platform != PlatformID.Win32NT )
            {
                open.lStructSize -= 12;
            }

            if ( !GetSaveFileName( ref open ) )
            {
                int ret = CommDlgExtendedError();
                if ( ret != 0 )
                {
                    throw new ApplicationException( "Couldn't show dialog - " + ret.ToString() );
                }

                return DialogResult.Cancel;
            }

            Filename = open.lpstrFile;
            return DialogResult.OK;
        }



		// Private Methods (13) 

        [DllImport( "Comdlg32.dll" )]
        private static extern int CommDlgExtendedError();

        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern int CreateWindowEx( int dwExStyle, string lpClassName, string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int hMenu, int hInstance, int lpParam );

        [DllImport( "user32.dll" )]
        private static extern bool DestroyWindow( int hwnd );

        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern int GetDlgItem( int hDlg, int nIDDlgItem );

        [DllImport( "user32.dll" )]
        private static extern int GetParent( int hWnd );

        [DllImport( "Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true )]
        private static extern bool GetSaveFileName( ref OPENFILENAME lpofn );

        [DllImport( "user32.dll" )]
        private static extern bool GetWindowRect( int hWnd, ref RECT lpRect );

        private int HookProc( int hdlg, int msg, int wParam, int lParam )
        {
            switch ( msg )
            {
                case WM_INITDIALOG:

                    //we need to centre the dialog
                    Rectangle screenRectangle = screen.WorkingArea;
                    RECT childRectangle = new RECT();
                    int parent = GetParent( hdlg );
                    GetWindowRect( parent, ref childRectangle );

                    int x = 
                        ( screenRectangle.Right + screenRectangle.Left - 
                        ( childRectangle.Right - childRectangle.Left ) ) / 2;
                    int y = 
                        ( screenRectangle.Bottom + screenRectangle.Top - 
                        ( childRectangle.Bottom - childRectangle.Top ) ) / 2;

                    SetWindowPos( parent, 0, x, y, childRectangle.Right - childRectangle.Left, childRectangle.Bottom - childRectangle.Top + 32, SWP_NOZORDER );


                    //we need to find the label to position our new label under

                    int saveWindow = GetDlgItem( parent, 0x441 );

                    RECT aboveRect = new RECT();
                    GetWindowRect( saveWindow, ref aboveRect );

                    //now convert the label's screen co-ordinates to client co-ordinates
                    POINT point = new POINT();
                    point.X = aboveRect.Left;
                    point.Y = aboveRect.Bottom;

                    ScreenToClient( parent, ref point );

                    // Add our checkbox
                    int checkbox = CreateWindowEx( 0, "BUTTON", "sceapCheckbxo", WS_CHILD | WS_VISIBLE | WS_CLIPCHILDREN | BS_AUTOCHECKBOX, point.X, point.Y + 12, 200, 100, parent, 0, 0, 0 );
                    SetWindowText( checkbox, "Patch SCEAP.DAT" );
                    SendMessage( checkbox, BM_SETCHECK, PatchSCEAPChecked ? BST_CHECKED : BST_UNCHECKED, 0 );
                    int fontHandle = SendMessage( saveWindow, WM_GETFONT, 0, 0 );
                    SendMessage( checkbox, WM_SETFONT, fontHandle, 0 );

                    this.checkboxHandle = checkbox;
                    break;
                case WM_DESTROY:
                    //destroy the handles we have created
                    if ( checkboxHandle != 0 )
                    {
                        DestroyWindow( checkboxHandle );
                    }
                    break;
                case WM_NOTIFY:

                    //we need to intercept the CDN_FILEOK message
                    //which is sent when the user selects a filename

                    NMHDR nmhdr = (NMHDR)Marshal.PtrToStructure( new IntPtr( lParam ), typeof( NMHDR ) );

                    if ( nmhdr.Code == CDN_FILEOK )
                    {
                        PatchSCEAPChecked = SendMessage( checkboxHandle, BM_GETCHECK, 0, 0 ) == BST_CHECKED;
                    }
                    break;

            }
            return 0;
        }

        [DllImport( "user32.dll" )]
        private static extern bool ScreenToClient( int hWnd, ref POINT lpPoint );

        [DllImport( "user32.dll" )]
        private static extern int SendMessage( int hWnd, int Msg, int wParam, int lParam );

        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern int SendMessage( int hWnd, int Msg, int wParam, string lParam );

        [DllImport( "user32.dll" )]
        private static extern bool SetWindowPos( int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags );

        [DllImport( "user32.dll", CharSet = CharSet.Auto )]
        private static extern bool SetWindowText( int hWnd, string lpString );


		#endregion Methods 

        [StructLayout( LayoutKind.Sequential, CharSet = CharSet.Auto )]
        private struct OPENFILENAME
        {
            public int lStructSize;
            public IntPtr hwndOwner;
            public int hInstance;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpstrFilter;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpstrCustomFilter;
            public int nMaxCustFilter;
            public int nFilterIndex;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpstrFile;
            public int nMaxFile;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpstrFileTitle;
            public int nMaxFileTitle;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpstrInitialDir;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpstrTitle;
            public int Flags;
            public short nFileOffset;
            public short nFileExtension;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpstrDefExt;
            public int lCustData;
            public OFNHookProcDelegate lpfnHook;
            [MarshalAs( UnmanagedType.LPTStr )]
            public string lpTemplateName;
            //only if on nt 5.0 or higher
            public int pvReserved;
            public int dwReserved;
            public int FlagsEx;
        }
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        private struct POINT
        {
            public int X;
            public int Y;
        }
        private struct NMHDR
        {
            public int HwndFrom;
            public int IdFrom;
            public int Code;
        }

    }
}

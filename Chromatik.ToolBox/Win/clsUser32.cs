using System;
using System.Runtime.InteropServices;

namespace Chromatik.ToolBox.Win
{
    /// <summary>
    /// Déclarations fonctions et constantes pour utilisation "user32.dll"
    /// </summary>
    public static class clsUser32
    {
        // **** Fonctions DLL  "user32.dll"

        /// <summary></summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(int hWnd, uint nCmdShow);

        /// <summary></summary>
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(IntPtr hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        /// <summary></summary>
        [DllImport("user32.dll", EntryPoint = "BringWindowToTop")]
        public static extern int BringWindowToTop(IntPtr hwnd);

        /// <summary></summary>
        [DllImport("shell32.dll")]
        public static extern int ShellExecuteA(IntPtr hwnd, string operation, string file, string paramters,
                                               string directory, int showcmd);
        // /// <summary></summary>
        //[DllImport("User32.dll")]
        //private static extern int RegisterWindowMessage(string lpString);

        /// <summary></summary>
        [DllImport("user32.dll", EntryPoint = "FindWindowA")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        // /// <summary></summary>
        //[DllImport("user32.dll")]
        //public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        /// <summary></summary>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, int lParam);

        /// <summary></summary>
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        /// <summary></summary>
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        /// <summary></summary>
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(int hWnd, uint Msg, int wParam, int lParam);

        /// <summary></summary>
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(int hWnd, uint Msg, int wParam, ref COPYDATASTRUCT lParam);

        /// <summary></summary>
        [DllImport("User32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(int hWnd);

        //[DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        //private static extern int SetForegroundWindow(IntPtr hwnd);

        /// <summary></summary>
        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(HandleRef hWnd, int nCmdShow);

        /// <summary></summary>
        [DllImport("user32.dll")]
        public static extern bool LockWindowUpdate(IntPtr hWndLock);

        public const int SPI_GETWORKAREA = 48;
        public const int SPI_SETWORKAREA = 47;
        public const int SPIF_SENDCHANGE = 2;
        public const int WM_SETREDRAW = 11;


        // *** Constantes Windows
        /// <summary></summary>
        public const int WM_SYSCOMMAND = 0x0112;
        /// <summary></summary>
        public const int SC_CLOSE = 0xF060;
        /// <summary></summary>
        public const int ShowNormal = 1;
        /// <summary></summary>
        public const int ShowMinimized = 2;
        /// <summary></summary>
        public const int Restore = 9;

        /// <summary></summary>
        public const int SWP_HIDEWINDOW = 0x80;
        /// <summary></summary>
        public const int SWP_SHOWWINDOW = 0x40;

        /// <summary></summary>
        public const int WM_USER = 0x400;
        /// <summary></summary>
        public const int WM_COPYDATA = 0x4A;
        /// <summary></summary>
        public const int WM_SETTEXT = 0x000C;


        /// <summary>
        ///  Structure pour message windows WM_COPYDATA
        ///  </summary>
        public struct COPYDATASTRUCT
        {
            /// <summary></summary>
            public IntPtr dwData;
            /// <summary></summary>
            public int cbData;
            /// <summary></summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
    }
}

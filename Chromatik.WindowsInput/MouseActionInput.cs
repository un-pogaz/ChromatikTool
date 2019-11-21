using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace System.Windows.Input
{
    public static class MouseActionInput
    {
        const int MOUSEEVENTF_LEFTDOWN = 0x0002; /*left button down*/
        const int MOUSEEVENTF_LEFTUP = 0x0004; /*left button up*/
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /*right button up*/
        const int MOUSEEVENTF_RIGHTUP = 0x0010; /*right button up*/

        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; /*middle button up*/
        const int MOUSEEVENTF_MIDDLEUP = 0x0040; /*middle button up*/

        const int MOUSEEVENTF_MOVE = 0x0001; /*move mouse*/
        const int MOUSEEVENTF_ABSOLUTE = 0x8000; /*absolute move*/

        const int MOUSEEVENTF_XDOWN = 0x0080;
        const int MOUSEEVENTF_XUP = 0x0100;

        const int MOUSEEVENTF_WHEEL = 0x0800; /*wheel button rolled*/
        const int MOUSEEVENTF_HWHEEL = 0x01000; /*wheel button tilted*/


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        static public void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        static public void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }
        static public void MiddleClick()
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_HWHEEL, 0, 0, 0, 0);
        }
        static public void WhellRotate(int movePixel)
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, movePixel, 0);
        }
        static public void SetPosition(int xMove, int yMove)
        {
            Move(short.MinValue, short.MinValue);
            Move(xMove, yMove);
        }
        static public void Move(int xMove, int yMove)
        {
            mouse_event(MOUSEEVENTF_MOVE, xMove, yMove, 0, 0);
        }
    }
}

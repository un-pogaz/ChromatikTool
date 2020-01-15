using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    static class ControlExtension
    {

        /// <summary>
        /// Paint the background of the control to make transparent.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="e"></param>
        static public void PaintTransparentBackground(this Control control, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (control.Parent != null)
            {
                // Take each control in turn
                int index = control.Parent.Controls.GetChildIndex(control);
                for (int i = control.Parent.Controls.Count - 1; i > index; i--)
                {
                    Control c = control.Parent.Controls[i];

                    // Check it's visible and overlaps this control
                    if (c.Bounds.IntersectsWith(control.Bounds) && c.Visible)
                    {
                        // Load appearance of underlying control and redraw it on this background
                        Bitmap bmp = new Bitmap(c.Width, c.Height, g);
                        c.DrawToBitmap(bmp, c.ClientRectangle);
                        g.TranslateTransform(c.Left - control.Left, c.Top - control.Top);
                        g.DrawImageUnscaled(bmp, Point.Empty);

                        g.TranslateTransform(control.Left - c.Left, control.Top - c.Top);

                        bmp.Dispose();
                    }
                }
            }
        }
    }
}

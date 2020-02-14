using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace System.Windows.Forms
{
    /// <summary>
    /// Static extension class for <see cref="Control"/>
    /// </summary>
    static class ControlExtension
    {
        /// <summary>
        /// Get all <see cref="Control"/>'s below the target.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        static public Control[] IntersectsControlsDown(this Control control) { return IntersectsControlsDown(control,true); }
        /// <summary>
        /// Get all <see cref="Control"/>'s below the target, include the NonVisible's.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="onlyVisible"></param>
        /// <returns></returns>
        static public Control[] IntersectsControlsDown(this Control control, bool onlyVisible)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            List<Control> rslt = new List<Control>();
            if (control.Parent != null)
            {
                rslt.Capacity = control.Parent.Controls.Count;
                // Take each control in turn
                int index = control.Parent.Controls.GetChildIndex(control);
                for (int i = control.Parent.Controls.Count - 1; i > index; i--)
                {
                    Control c = control.Parent.Controls[i];
                    // Check it's visible and overlaps this control
                    if (c.Bounds.IntersectsWith(control.Bounds) && (!onlyVisible || onlyVisible && c.Visible))
                        rslt.Add(c);
                }
            }
            return rslt.ToArray();
        }

        /// <summary>
        /// Get all <see cref="Control"/>'s above the target.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        static public Control[] IntersectsControlsUp(this Control control) { return IntersectsControlsUp(control,true); }
        /// <summary>
        /// Get all <see cref="Control"/>'s above the target, include the NonVisible's.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="onlyVisible"></param>
        /// <returns></returns>
        static public Control[] IntersectsControlsUp(this Control control, bool onlyVisible)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            List<Control> rslt = new List<Control>();
            if (control.Parent != null)
            {
                rslt.Capacity = control.Parent.Controls.Count;
                // Take each control in turn
                int index = control.Parent.Controls.GetChildIndex(control);
                for (int i = index - 1; i >= 0; i--)
                {
                    Control c = control.Parent.Controls[i];
                    // Check it's visible and overlaps this control
                    if (c.Bounds.IntersectsWith(control.Bounds) && (!onlyVisible || onlyVisible && c.Visible))
                        rslt.Add(c);
                }
            }
            return rslt.ToArray();
        }

        /// <summary>
        /// Paints a TRUE transparent background to the control
        /// </summary>
        /// <param name="control"></param>
        static public void PaintTransparentBackground(this Control control) { PaintTransparentBackground(control, null); }
        /// <summary>
        /// Paints a TRUE transparent background to the control
        /// </summary>
        /// <param name="control"></param>
        /// <param name="e"></param>
        static public void PaintTransparentBackground(this Control control, PaintEventArgs e)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            if (e == null)
                e = new PaintEventArgs(control.CreateGraphics(), control.ClientRectangle);

            BorderStyle borderStyle = BorderStyle.None;
            try
            {
                borderStyle = control.GetValueOf<BorderStyle>("BorderStyle");
            }
            catch (Exception)
            {
                borderStyle = BorderStyle.None;
            }

            Graphics g = e.Graphics;

            foreach (Control item in control.IntersectsControlsDown())
            {
                // Check it's visible and overlaps this control
                if (item.Bounds.IntersectsWith(control.Bounds) && item.Visible)
                {
                    // Load appearance of underlying control and redraw it on this background
                    Bitmap bmp = new Bitmap(item.Width, item.Height, g);
                    item.DrawToBitmap(bmp, item.ClientRectangle);
                    switch (borderStyle)
                    {
                        /*
                        SystemInformation.BorderSize
                        SystemInformation.Border3DSize
                        */

                        default: //BorderStyle.None
                            g.TranslateTransform(item.Left - control.Left, item.Top - control.Top);
                            break;
                    }
                    g.DrawImageUnscaled(bmp, System.Drawing.Point.Empty);
                    g.TranslateTransform(control.Left - item.Left, control.Top - item.Top);
                    bmp.Dispose();
                }
            }
        }
    }
}

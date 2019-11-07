using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    /// <summary>
    /// Alternative <see cref="PictureBox"/> with a REAL support of transparent background.
    /// </summary>
    public class PictureBoxTransparent : PictureBox
    {
        /// <summary>
        /// Create a alternative <see cref="PictureBox"/> with a REAL support of transparent background.
        /// </summary>
        public PictureBoxTransparent() : base()
        {
            BackColor = Color.Transparent;
        }

        /// <summary>
        /// Paint the background of the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Paint background with underlying graphics from other controls
            base.OnPaintBackground(e);
            if (BackColor == Color.Transparent)
            {
                Graphics g = e.Graphics;

                if (Parent != null)
                {
                    // Take each control in turn
                    int index = Parent.Controls.GetChildIndex(this);
                    for (int i = Parent.Controls.Count - 1; i > index; i--)
                    {
                        Control c = Parent.Controls[i];

                        // Check it's visible and overlaps this control
                        if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                        {
                            // Load appearance of underlying control and redraw it on this background
                            Bitmap bmp = new Bitmap(c.Width, c.Height, g);
                            c.DrawToBitmap(bmp, c.ClientRectangle);
                            g.TranslateTransform(c.Left - Left, c.Top - Top);
                            g.DrawImageUnscaled(bmp, Point.Empty);
                            g.TranslateTransform(Left - c.Left, Top - c.Top);
                            bmp.Dispose();
                        }
                    }
                }
            }
        }
    }
}

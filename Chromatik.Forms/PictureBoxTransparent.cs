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
            timer.Tick += timer_Tick;
            AutoRefreshBackground = false;
        }
        Timer timer = new Timer();

        /// <summary> </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            AutoRefreshBackground = false;
            timer.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Active the auto refresh of the background
        /// </summary>
        public bool AutoRefreshBackground
        {
            set {
                if (value)
                {
                    if (!timer.Enabled)
                        timer.Start();
                }
                else
                    timer.Stop();
            }
            get { return timer.Enabled; }
        }

        /// <summary>
        /// Interval, in milliseconde, betewen the auto refresh of the background
        /// </summary>
        public int AutoRefreshBackgroundInterval
        {
            set { timer.Interval = value; }
            get { return timer.Interval; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            RefreshBackground();
        }

        /// <summary>
        /// Refresh the background
        /// </summary>
        public void RefreshBackground()
        {
           if (BackColor == Color.Transparent)
                OnPaintBackground(new PaintEventArgs(CreateGraphics(), ClientRectangle));
        }

        /// <summary>
        /// Paint the background of the control (support TRUE transparent background)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Paint background with underlying graphics from other controls
            base.OnPaintBackground(e);

            if (BackColor == Color.Transparent)
                using (Graphics g = e.Graphics)
                {
                    if (Parent != null)
                    {
                        // Take each control in turn
                        int index = Parent.Controls.GetChildIndex(this);
                        for (int i = Parent.Controls.Count - 1; i > index; i--)
                        {
                            using (Control c = Parent.Controls[i])
                            {
                                // Check it's visible and overlaps this control
                                if (c.Bounds.IntersectsWith(Bounds) && c.Visible)
                                {
                                    // Load appearance of underlying control and redraw it on this background
                                    using (Bitmap bmp = new Bitmap(c.Width, c.Height, g))
                                    {
                                        c.DrawToBitmap(bmp, c.ClientRectangle);
                                        g.TranslateTransform(c.Left - Left, c.Top - Top);
                                        g.DrawImageUnscaled(bmp, Point.Empty);
                                        g.TranslateTransform(Left - c.Left, Top - c.Top);
                                    }
                                }
                            }
                        }
                    }
                }
        }
    }
}

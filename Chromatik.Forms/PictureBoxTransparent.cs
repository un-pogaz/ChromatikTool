using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
    /// <summary>
    /// Alternative <see cref="PictureBox"/> with a REAL support of transparent background.
    /// </summary>
    /// <remarks>https://stackoverflow.com/questions/5522337/c-sharp-picturebox-transparent-background-doesnt-seem-to-work <!-- 3eme réponse --></remarks>
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

        /// <summary></summary>
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
                this.PaintTransparentBackground(e);
        }
    }
}

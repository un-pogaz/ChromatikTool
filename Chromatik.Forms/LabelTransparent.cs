using System;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms
{
    /// <summary>
    /// Alternative <see cref="Label"/> with a REAL support of transparent background.
    /// </summary>
    /// <remarks>https://stackoverflow.com/questions/5522337/c-sharp-picturebox-transparent-background-doesnt-seem-to-work <!-- 3eme réponse --></remarks>
    public class LabelTransparent : Label
    {
        /// <summary>
        /// Create a alternative <see cref="Label"/> with a REAL support of transparent background.
        /// </summary>
        public LabelTransparent() : base()
        {
            BackColor = Color.Transparent;
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
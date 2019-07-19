using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.VisualStyles;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Windows.Forms.Layout;

namespace Chromatik.Forms
{

    public class ChromatikToolStrip : ToolStripSystemRenderer
    {
        public static ChromatikToolStrip Defaut { get; } = new ChromatikToolStrip();
        

        public ChromatikToolStrip()
        {
        }
        
        private void RenderStatusStripBorder(ToolStripRenderEventArgs e)
        {
            if (!Application.RenderWithVisualStyles)
            {
                e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, 0, e.ToolStrip.Width, 0);
            }
        }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            ToolStrip toolStrip = e.ToolStrip;
            Graphics g = e.Graphics;
            Rectangle bounds = e.ToolStrip.ClientRectangle;

            if (toolStrip is StatusStrip)
            {
                if (!Application.RenderWithVisualStyles)
                {
                    e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, 0, e.ToolStrip.Width, 0);
                }
            }
            else if (toolStrip is ToolStripDropDown)
            {
                ToolStripDropDown toolStripDropDown = toolStrip as ToolStripDropDown;

                // Paint the border for the window depending on whether or not we have a drop shadow effect. 
                if (toolStripDropDown.DropShadowEnabled && ToolStripManager.VisualStylesEnabled)
                {
                    bounds.Width -= 1;
                    bounds.Height -= 1;
                    e.Graphics.DrawRectangle(new Pen(SystemColors.ControlDark), bounds);
                }
                else {
                    ControlPaint.DrawBorder3D(e.Graphics, bounds, Border3DStyle.Raised);
                }
            }
            else {
                if (ToolStripManager.VisualStylesEnabled)
                {
                    e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, bounds.Bottom - 1, bounds.Width, bounds.Bottom - 1);
                    e.Graphics.DrawLine(SystemPens.InactiveBorder, 0, bounds.Bottom - 2, bounds.Width, bounds.Bottom - 2);
                }
                else {
                    e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, bounds.Bottom - 1, bounds.Width, bounds.Bottom - 1);
                    e.Graphics.DrawLine(SystemPens.ButtonShadow, 0, bounds.Bottom - 2, bounds.Width, bounds.Bottom - 2);
                }
            }
        }

    }
}
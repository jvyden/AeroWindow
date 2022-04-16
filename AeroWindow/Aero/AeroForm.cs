using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace yep.Aero
{
    public class AeroForm : Form
    {
        private Margins margins;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                // Don't render aero in the Windows Forms designer.
                return;
            }

            if (Dwm.IsCompositionEnabled)
            {
                // Paint the glass effect.
                margins = new Margins();

                const int marginInt = 10000;

                margins.Top = marginInt;
                margins.Right = marginInt;
                margins.Left = marginInt;
                margins.Bottom = marginInt;

                Dwm.ExtendAeroIntoForm(this.Handle, ref margins);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                // Don't render aero in the Windows Forms designer.
                return;
            }

            if (Dwm.IsCompositionEnabled)
            {
                // paint background black to enable include glass regions
                e.Graphics.Clear(Color.Black);
                // revert the non-glass rectangle back to it's original colour
                Rectangle clientArea = new Rectangle(
                        margins.Left,
                        margins.Top,
                        this.ClientRectangle.Width - margins.Left - margins.Right,
                        this.ClientRectangle.Height - margins.Top - margins.Bottom
                );
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);
            }
            else
            {
                base.OnPaintBackground(e);
            }
        }
    }
}

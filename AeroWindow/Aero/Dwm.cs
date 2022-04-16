using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace yep.Aero
{
    public class Dwm
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern bool DwmIsCompositionEnabled();

        public static bool IsCompositionEnabled
        {
            get
            {
                try
                {
                    return DwmIsCompositionEnabled();
                }
                catch
                {
                    return false;
                }
            }
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins margins);

        public static void ExtendAeroIntoForm(IntPtr handle, ref Margins margins)
        {
            Debug.Assert(IsCompositionEnabled);
            DwmExtendFrameIntoClientArea(handle, ref margins);
        }
    }
}

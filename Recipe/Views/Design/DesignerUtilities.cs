namespace Recipe.Views.Design
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public static class DesignerUtilities
    {
        public const ContentAlignment AnyTopAlignment = ContentAlignment.TopLeft | ContentAlignment.TopCenter | ContentAlignment.TopRight;
        public const ContentAlignment AnyMiddleAlignment = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;

        [DllImport("gdi32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern bool GetTextMetricsW(IntPtr hdc, ref TEXTMETRICW lptm);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiObj);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TEXTMETRICW
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public ushort tmFirstChar;
            public ushort tmLastChar;
            public ushort tmDefaultChar;
            public ushort tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }

        /// <summary>
        ///  Identifies where the text baseline for our control which should be based on bounds, padding, font, and textalignment.
        /// </summary>
        public static int GetTextBaseline(Control ctrl, ContentAlignment alignment)
        {
            int result = 0;
            IntPtr hDC = IntPtr.Zero;
            IntPtr hFont = IntPtr.Zero;
            IntPtr hFontDefault = IntPtr.Zero;

            Rectangle face = ctrl.ClientRectangle;

            using IDeviceContext g = ctrl.CreateGraphics();
            try
            {
                hDC = g.GetHdc();
                hFont = ctrl.Font.ToHfont();
                hFontDefault = SelectObject(hDC, hFont);

                TEXTMETRICW metrics = new();
                _ = GetTextMetricsW(hDC, ref metrics);

                //get the font metrics via gdi
                // Add the font ascent to the baseline
                int fontAscent = metrics.tmAscent + 1;
                int fontHeight = metrics.tmHeight;

                // Now add it all up
                result = alignment switch
                {
                    ContentAlignment.TopLeft or ContentAlignment.TopCenter or ContentAlignment.TopRight => face.Top + fontAscent,
                    ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight => face.Top + (face.Height / 2) - (fontHeight / 2) + fontAscent,
                    _ => face.Bottom - fontHeight + fontAscent,
                };
            }
            finally
            {
                if (hFontDefault != IntPtr.Zero) _ = SelectObject(hDC, hFontDefault);
                
                if (hFont != IntPtr.Zero) _ = DeleteObject(hFont);

                g.ReleaseHdc();
            }

            return result;
        }
    }
}

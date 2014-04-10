using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

// This file is more or less taken from CodeProject. See:
// http://www.codeproject.com/Articles/25471/Customizable-ComboBox-Drop-Down
// (c) Lea Hayes

namespace AdamsLair.WinForms
{
    public sealed class GripRenderer
    {
        private static Bitmap m_sGripBitmap;
        private static Bitmap GripBitmap
        {
            get { return m_sGripBitmap; }
        }

        private GripRenderer()
        {
        }

        public static void RefreshSystemColors(Graphics g, Size size)
        {
            InitializeGripBitmap(g, size, true);
        }
        public static void Render(Graphics g, Point location, Size size, GripAlignMode mode)
        {
            InitializeGripBitmap(g, size, false);

            // Calculate display size and position of grip.
            switch (mode)
            {
                case GripAlignMode.TopLeft:
                    size.Height = -size.Height;
                    size.Width = -size.Width;
                    break;

                case GripAlignMode.TopRight:
                    size.Height = -size.Height;
                    break;

                case GripAlignMode.BottomLeft:
                    size.Width = -size.Height;
                    break;
            }

            // Reverse size grip for left-aligned.
            if (size.Width < 0)
                location.X -= size.Width;
            if (size.Height < 0)
                location.Y -= size.Height;

            g.DrawImage(GripBitmap, location.X, location.Y, size.Width, size.Height);
        }
        public static void Render(Graphics g, Point location, GripAlignMode mode)
        {
            Render(g, location, new Size(16, 16), mode);
        }

        private static void InitializeGripBitmap(Graphics g, Size size, bool forceRefresh)
        {
            if (m_sGripBitmap == null || forceRefresh || size != m_sGripBitmap.Size)
            {
                // Draw size grip into a bitmap image.
                m_sGripBitmap = new Bitmap(size.Width, size.Height, g);
                using (Graphics gripG = Graphics.FromImage(m_sGripBitmap))
                    ControlPaint.DrawSizeGrip(gripG, SystemColors.ButtonFace, 0, 0, size.Width, size.Height);
            }
        }
    }
}

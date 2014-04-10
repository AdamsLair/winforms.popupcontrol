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
    public enum PopupResizeMode
    {
        None = 0,

        // Individual styles.
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,

        // Combined styles.
        All = (Top | Left | Bottom | Right),
        TopLeft = (Top | Left),
        TopRight = (Top | Right),
        BottomLeft = (Bottom | Left),
        BottomRight = (Bottom | Right),
    }
}

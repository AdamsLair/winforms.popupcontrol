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
    public interface IPopupControlHost
    {
		bool IsDropDownOpened { get; }

        void ShowDropDown();
        void HideDropDown();
    }
}

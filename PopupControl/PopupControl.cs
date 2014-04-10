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
    public class PopupControl
    {
        private ToolStripControlHost m_host;
        private PopupDropDown m_dropDown;
        private Padding m_padding = Padding.Empty;
        private Padding m_margin = new Padding(1);
        private bool m_autoReset = false;
		
        public event ToolStripDropDownClosingEventHandler Closing
        {
            add
            {
                m_dropDown.Closing += value;
            }
            remove
            {
                m_dropDown.Closing -= value;
            }
        }
        public event ToolStripDropDownClosedEventHandler Closed
        {
            add
            {
                m_dropDown.Closed += value;
            }
            remove
            {
                m_dropDown.Closed -= value;
            }
        }

        public bool Visible
        {
            get { return (this.m_dropDown != null && this.m_dropDown.Visible) ? true : false; }
        }
        public Control Control
        {
            get { return (this.m_host != null) ? this.m_host.Control : null; }
        }
        public Padding Padding
        {
            get { return this.m_padding; }
            set { this.m_padding = value; }
        }
        public Padding Margin
        {
            get { return this.m_margin; }
            set { this.m_margin = value; }
        }
        public bool AutoResetWhenClosed
        {
            get { return this.m_autoReset; }
            set { this.m_autoReset = value; }
        }
        /// <summary>
        /// Gets or sets the popup control host, this is used to hide/show popup.
        /// </summary>
        public IPopupControlHost PopupControlHost { get; set; }


        public PopupControl()
        {
            InitializeDropDown();
        }

        private void m_dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (AutoResetWhenClosed)
                DisposeHost();
            
            // Hide drop down within popup control.
            if (PopupControlHost != null)
                PopupControlHost.HideDropDown();
        }

        public void Show(Control parent, Control control, int x, int y)
        {
            Show(parent, control, x, y, PopupResizeMode.None);
        }
        public void Show(Control parent, Control control, int x, int y, PopupResizeMode resizeMode)
        {
            Show(parent, control, x, y, -1, -1, resizeMode);
        }
        public void Show(Control parent, Control control, int x, int y, int width, int height, PopupResizeMode resizeMode)
        {
            Size controlSize = control.Size;

            InitializeHost(control);

            m_dropDown.ResizeMode = resizeMode;
            m_dropDown.Show(parent, x, y, width, height);

            control.Focus();
        }
        public void Hide()
        {
            if (m_dropDown != null && m_dropDown.Visible)
            {
                m_dropDown.Hide();
                DisposeHost();
            }
        }
        public void Reset()
        {
            DisposeHost();
        }

        protected void DisposeHost()
        {
            if (m_host != null)
            {
                // Make sure host is removed from drop down.
                if (m_dropDown != null)
                    m_dropDown.Items.Clear();

                // Dispose of host.
                m_host = null;
            }

            PopupControlHost = null;
        }
        protected void InitializeHost(Control control)
        {
            InitializeDropDown();

            // If control is not yet being hosted then initialize host.
            if (control != Control)
                DisposeHost();

            // Create a new host?
            if (m_host == null)
            {
                m_host = new ToolStripControlHost(control);
                m_host.AutoSize = false;
                m_host.Padding = Padding;
                m_host.Margin = Margin;
            }
            
            // Add control to drop-down.
            m_dropDown.Items.Clear();
            m_dropDown.Padding = m_dropDown.Margin = Padding.Empty;
            m_dropDown.Items.Add(m_host);
        }
        protected void InitializeDropDown()
        {
            // Does a drop down exist?
            if (m_dropDown == null)
            {
                m_dropDown = new PopupDropDown(false);
                m_dropDown.Closed += new ToolStripDropDownClosedEventHandler(m_dropDown_Closed);
            }
        }
    }
}

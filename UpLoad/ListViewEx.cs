using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UpLoad
{
    public class ListViewEx : ListView
    {
        //public delegate void ScrollEventHandler(object sender, EventArgs e);
        public event EventHandler HScroll;
        public event EventHandler VScroll;
        public ListViewEx()
        {
        }
        const int WM_HSCROLL = 0x0114;
        const int WM_VSCROLL = 0x0115;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HSCROLL)
            {
                OnHScroll(this, new EventArgs());
            }
            else if (m.Msg == WM_VSCROLL)
            {
                OnVScroll(this, new EventArgs());
            }
            base.WndProc(ref m);
        }
        virtual protected void OnHScroll(object sender, EventArgs e)
        {
            if (HScroll != null)
                HScroll(this, e);
        }
        virtual protected void OnVScroll(object sender, EventArgs e)
        {
            if (VScroll != null)
                VScroll(this, e);
        }
    }

}

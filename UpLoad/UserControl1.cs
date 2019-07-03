using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpLoad
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public static bool isRefresh = false;
        private void UserControl1_MouseHover(object sender, EventArgs e)
        {
                Graphics g = CreateGraphics();
                PaintEventArgs pe = new PaintEventArgs(g, ClientRectangle);
                Rectangle myRectangle = new Rectangle(0, 0, Width, Height);
                ControlPaint.DrawBorder(pe.Graphics, myRectangle,
                    Color.Gray, 2, ButtonBorderStyle.Solid,
                    Color.Gray, 2, ButtonBorderStyle.Solid,
                    Color.Gray, 2, ButtonBorderStyle.Solid,
                    Color.Gray, 2, ButtonBorderStyle.Solid
                );
            
        }

        private void UserControl1_MouseLeave(object sender, EventArgs e)
        {
            if(!isRefresh)
               this.Refresh();
        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            isRefresh = true;
        }

        private void ContextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            isRefresh = false;
        }

        //private void UserControl1_Click(object sender, EventArgs e)
        //{
        //    Graphics g = CreateGraphics();
        //    PaintEventArgs pe = new PaintEventArgs(g, ClientRectangle);
        //    Rectangle myRectangle = new Rectangle(0, 0, Width, Height);
        //    ControlPaint.DrawBorder(pe.Graphics, myRectangle,
        //        Color.DeepSkyBlue, 3, ButtonBorderStyle.Solid,
        //        Color.DeepSkyBlue, 3, ButtonBorderStyle.Solid,
        //        Color.DeepSkyBlue, 3, ButtonBorderStyle.Solid,
        //        Color.DeepSkyBlue, 3, ButtonBorderStyle.Solid
        //    );
        //}

        //private void UserControl1_Leave(object sender, EventArgs e)
        //{
        //    this.Refresh();
        //}


        //private void UserControl1_MouseEnter(object sender, EventArgs e)
        //{
        //    this.BorderStyle = BorderStyle.FixedSingle;
        //}

        //private void UserControl1_MouseLeave(object sender, EventArgs e)
        //{
        //    this.BorderStyle = BorderStyle.None;
        //}

        //private void UserControl1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    this.BorderStyle = BorderStyle.FixedSingle;
        //}

        //private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        //{
        //    this.BorderStyle = BorderStyle.FixedSingle;
        //}
    }

}

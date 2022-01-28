
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace foldit
{

    public class TransparentLayout : System.Windows.Forms.FlowLayoutPanel
    {
        public TransparentLayout()
        {
            this.BackColor = Color.Transparent;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

            base.OnPaintBackground(e);
        }
    }

    partial class Form1
    {
       
        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Transparent);
        }
        private TransparentLayout transparentLayout1;
        private TransparentLabel transparentLabel4;
    }

    public class TransparentLabel : Control
    {
        public TransparentLabel()
        {
            TabStop = false;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // nothing
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawText();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x000F)
            {
                DrawText();
            }
        }

        private void DrawText()
        {
            using (Graphics graphics = CreateGraphics())
            using (SolidBrush brush = new SolidBrush(this.ForeColor))
            {
                SizeF size = graphics.MeasureString(Text, Font);
                graphics.DrawString(Text, Font, brush, 0, 0);
            }
        }

    }
}


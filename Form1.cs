using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyrcleAndRect
{
    public partial class MainForm : Form
    {
        RectangleF rectangle;
        RectangleF cyrcle;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var rect = ClientRectangle;
            CreateRect(rect);
            CreateCyrcle(rect);
            Invalidate();
        }

        private void CreateRect(RectangleF rect)
        {
            rectangle = new RectangleF(0, 0, rect.Height * 0.5f, rect.Height * 0.3f);
            rectangle.Offset(rect.Width / 4f - rectangle.Width / 2f, rect.Height / 2f - rectangle.Height / 2f);
        }

        private void CreateCyrcle(RectangleF rect)
        {
            cyrcle = new RectangleF(0, 0, rect.Height * 0.4f, rect.Height * 0.4f);
            cyrcle.Offset(3f * rect.Width / 4f - cyrcle.Width / 2f, rect.Height / 2f - cyrcle.Height / 2f);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawRectangles(Pens.Blue, new[] { rectangle });
            using (var brush = new SolidBrush(Color.FromArgb(128, Color.Blue)))
            {
                g.FillRectangle(brush, rectangle);
            }

            g.DrawEllipse(Pens.Red, cyrcle);
            using (var brush = new SolidBrush(Color.FromArgb(128, Color.Red)))
            {
                g.FillEllipse(brush, cyrcle);
            }
        }
    }
}

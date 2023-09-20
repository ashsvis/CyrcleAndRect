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
        List<Figure> figures = new List<Figure>();


        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
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
            var rectFig = new Rect() 
            { 
                Location = rectangle.Location, 
                Width = rectangle.Width, 
                Height = rectangle.Height,
                ForeColor = Color.Blue,
                BackColor = Color.FromArgb(180, Color.DodgerBlue),
            };
            figures.Add(rectFig);
        }

        private void CreateCyrcle(RectangleF rect)
        {
            cyrcle = new RectangleF(0, 0, rect.Height * 0.4f, rect.Height * 0.4f);
            cyrcle.Offset(3f * rect.Width / 4f - cyrcle.Width / 2f, rect.Height / 2f - cyrcle.Height / 2f);
            var cyrcleFig = new Cyrcle()
            {
                Location = cyrcle.Location,
                Diameter = cyrcle.Width,
                ForeColor = Color.Red,
                BackColor = Color.FromArgb(180, Color.DeepPink),
            };
            figures.Add(cyrcleFig);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            figures.ForEach(fig => fig.DrawAt(g));
        }

        bool down;
        Point point;
        Figure fig;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var list = new List<Figure>(figures);
                list.Reverse();
                foreach (var fig in list)
                {
                    if (fig.Contains(e.Location))
                    {
                        this.fig = fig;
                        point = e.Location;
                        down = true;
                        break;
                    }
                }
                // "перемещение" выбранной фигуры, чтобы она была "сверху" стопки
                if (down && fig != null)
                {
                    figures.Remove(fig);
                    figures.Add(fig);
                    Invalidate();
                }
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (down && fig != null)
            {
                fig.Offset(new PointF(e.Location.X - point.X, e.Location.Y - point.Y));
                point = e.Location;
                Invalidate();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }
    }
}

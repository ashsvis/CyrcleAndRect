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
        
        Cyrcle P1 = new Cyrcle() { Name = "P1", Diameter = 8, BackColor = Color.Black };
        Cyrcle P2 = new Cyrcle() { Name = "P2", Diameter = 8, BackColor = Color.Black };
        Cyrcle P3 = new Cyrcle() { Name = "P3", Diameter = 8, BackColor = Color.Black };
        List<Figure> points = new List<Figure>();

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
            CreateBasePoints(rect);
            Invalidate();
        }

        private void CreateBasePoints(Rectangle rect)
        {
            rect.Inflate(-50, -50);
            var rand = new Random();
            P1.Location = new PointF(rand.Next(rect.Left, rect.Right), rand.Next(rect.Top, rect.Bottom));
            P2.Location = new PointF(rand.Next(rect.Left, rect.Right), rand.Next(rect.Top, rect.Bottom));
            P3.Location = new PointF(rand.Next(rect.Left, rect.Right), rand.Next(rect.Top, rect.Bottom));
            points.AddRange(new[] { P1, P2, P3 });
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

            points.ForEach(fig => fig.DrawAt(g));
            points.ForEach(fig => g.DrawString("P" + fig.Name, Font, Brushes.Black, fig.Location));
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
                using (var g = this.CreateGraphics())
                {
                    foreach (var item in figures.Where(item => item != fig))
                    {
                        if (fig.IntersectWith(item, g))
                        {
                            Text = "Пересекаются";
                            return;
                        }
                    }
                }
                Text = "Прямоугольник и круг";
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }
    }
}

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
        readonly List<Figure> figures = new List<Figure>();

        readonly Cyrcle P1 = new Cyrcle() { Name = "P1", Diameter = 8, BackColor = Color.Black };
        readonly Cyrcle P2 = new Cyrcle() { Name = "P2", Diameter = 8, BackColor = Color.Black };
        readonly Cyrcle P3 = new Cyrcle() { Name = "P3", Diameter = 8, BackColor = Color.Black };
        readonly List<Figure> points = new List<Figure>();

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
            CreateBasePoints(rectangle, cyrcle);
            Invalidate();
        }

        private void CreateBasePoints(RectangleF rectangle, RectangleF cyrcle)
        {
            P1.Location = new PointF(rectangle.Left - P1.Diameter / 2f, rectangle.Bottom - P1.Diameter / 2f);
            P2.Location = new PointF(rectangle.Right - P2.Diameter / 2f, rectangle.Top - P2.Diameter / 2f);
            P3.Location = new PointF(cyrcle.Left + cyrcle.Width / 2f - P3.Diameter / 2f, cyrcle.Top + cyrcle.Height / 2f - P3.Diameter / 2f);
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

        private void UpdateRect()
        {
            var rectFig = (Rect)figures.FirstOrDefault(item => item is Rect);
            if (rectFig == null) return;
            rectFig.Location = new PointF(P1.Location.X + P1.Diameter / 2f, P2.Location.Y + P2.Diameter / 2f);
            rectFig.Width = P2.Location.X - P1.Location.X;
            rectFig.Height = P1.Location.Y - P2.Location.Y;
        }

        private void UpdateCyrcle()
        {
            var cyrcleFig = (Cyrcle)figures.FirstOrDefault(item => item is Cyrcle);
            if (cyrcleFig == null) return;
            cyrcleFig.Location = new PointF(P3.Location.X + P3.Diameter / 2f - cyrcleFig.Diameter / 2f, 
                                            P3.Location.Y + P3.Diameter / 2f - cyrcleFig.Diameter / 2f);
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
            points.ForEach(fig => g.DrawString($"P{fig.Name} {fig.Location}", Font, Brushes.Black, fig.Location));
        }

        bool down;
        Point point;
        Figure fig;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (var pt in points)
                {
                    if (pt.Contains(e.Location))
                    {
                        this.fig = pt;
                        point = e.Location;
                        down = true;
                    }
                }
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (down && fig != null)
            {
                fig.Offset(new PointF(e.Location.X - point.X, e.Location.Y - point.Y));
                UpdateRect();
                UpdateCyrcle();
                point = e.Location;
                Invalidate();
                var rectFig = (Rect)figures.FirstOrDefault(item => item is Rect);
                var cyrcleFig = (Cyrcle)figures.FirstOrDefault(item => item is Cyrcle);
                using (var g = this.CreateGraphics())
                {
                    if (rectFig.IntersectWith(cyrcleFig, g))
                    {
                        Text = "Пересекаются";
                        return;
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

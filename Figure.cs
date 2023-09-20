using System.Drawing;

namespace CyrcleAndRect
{
    public abstract class Figure
    {
        public PointF Location { get; set; }
        public Color ForeColor { get; set; } = Color.Black;
        public Color BackColor { get; set; } = Color.White;
        public abstract void DrawAt(Graphics g);
        public abstract bool Contains(PointF point);

        public void Offset(PointF point)
        {
            Location = PointF.Add(Location, new SizeF(point.X, point.Y));
        }
    }
}

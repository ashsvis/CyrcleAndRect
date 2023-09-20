using System.Drawing;

namespace CyrcleAndRect
{
    public class Rect : Figure
    {
        public float Width { get; set; }
        public float Height { get; set; }

        public override void DrawAt(Graphics g)
        {
            var rect = new RectangleF(Location, new SizeF(Width, Height));
            using (var pen = new Pen(ForeColor))
                g.DrawRectangles(pen, new [] { rect });
            using (var brush = new SolidBrush(BackColor))
                g.FillRectangle(brush, rect);
        }

        public override bool Contains(PointF point)
        {
            var rect = new RectangleF(Location, new SizeF(Width, Height));
            return rect.Contains(point);
        }
    }
}

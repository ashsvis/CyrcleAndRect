using System.Drawing;
using System.Drawing.Drawing2D;

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

        protected override GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            var rect = new RectangleF(Location, new SizeF(Width, Height));
            path.AddRectangle(rect);
            return path;
        }
    }
}

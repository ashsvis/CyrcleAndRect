using System.Drawing;
using System.Drawing.Drawing2D;

namespace CyrcleAndRect
{
    public class Cyrcle : Figure
    {
        public float Diameter { get; set; }

        public override void DrawAt(Graphics g)
        {
            var rect = new RectangleF(Location, new SizeF(Diameter, Diameter));
            using (var pen = new Pen(ForeColor))
                g.DrawEllipse(pen, rect);
            using (var brush = new SolidBrush(BackColor))
                g.FillEllipse(brush, rect);
        }

        protected override GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            var rect = new RectangleF(Location, new SizeF(Diameter, Diameter));
            path.AddEllipse(rect);
            return path;
        }

    }
}

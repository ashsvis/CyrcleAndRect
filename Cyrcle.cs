using System.Drawing;

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
            {
                g.FillEllipse(brush, rect);
            }
        }

        public override bool Contains(PointF point)
        {
            var rect = new RectangleF(Location, new SizeF(Diameter, Diameter));
            return rect.Contains(point);
        }

    }
}

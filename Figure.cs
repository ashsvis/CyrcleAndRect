using System.Drawing;
using System.Drawing.Drawing2D;

namespace CyrcleAndRect
{
    public abstract class Figure
    {
        public PointF Location { get; set; }
        public Color ForeColor { get; set; } = Color.Black;
        public Color BackColor { get; set; } = Color.White;
        public abstract void DrawAt(Graphics g);

        public string Name { get; set; }

        public bool Contains(PointF point)
        {
            using (var path = GetPath())
            {
                return path.IsVisible(point);
            }
        }

        public void Offset(PointF point)
        {
            Location = PointF.Add(Location, new SizeF(point.X, point.Y));
        }

        protected abstract GraphicsPath GetPath();

        /// <summary>
        /// Проверка пересечения текущей фигуры относительно другой
        /// </summary>
        /// <param name="otherFigure"></param>
        /// <param name="graphics"></param>
        /// <returns></returns>
        public bool IntersectWith(Figure otherFigure, Graphics graphics)
        {
            using (var path = GetPath())
            using (var otherPath = otherFigure.GetPath())
            {
                using (var region = new Region(path))
                {
                    region.Intersect(otherPath);
                    return !region.IsEmpty(graphics);
                }
            }
        }
    }
}

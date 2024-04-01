using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avitals_project.Controls
{
    public class TriangleDrawable:IDrawable
    {
        public void Draw(ICanvas canvas, RectF F)
        {
            PathF path = new PathF();
            path.LineTo(110, 90);
            path.LineTo(190, 70);
            path.Close();
            canvas.StrokeColor = Colors.Teal;
            canvas.StrokeSize = 6;
            canvas.DrawPath(path);
        }
    }
}

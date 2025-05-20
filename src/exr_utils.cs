using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;

namespace OpenEXR.Interop;

partial struct exr_attr_box2i_t
{
    public static exr_attr_box2i_t FromRectangle(Rectangle rect)
    {
        return new exr_attr_box2i_t
        {
            min = new exr_attr_v2i_t() { x = rect.X, y = rect.Y },
            max = new exr_attr_v2i_t() { x = rect.X + rect.Width - 1, y = rect.Y + rect.Height - 1 }
        };
    }

    public Rectangle ToRectangle()
    {
        return new Rectangle(min.x, min.y, max.x - min.x + 1, max.y - min.y + 1);
    }
}

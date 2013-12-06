using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolecularDynamic
{
    class Box
    {
        int width = 0;
        //видимо будем просто варьировать высоту, тем самым поднимать/опускать крышку
        int height = 0;

        public Box(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int getWidth()
        {
            return this.width;
        }

        public void setWidth(int width)
        {
            this.width = width;
        }

        public int getHeight()
        {
            return this.height;
        }

        public void setHeight(int height)
        {
            this.height = height;
        }

    }
}

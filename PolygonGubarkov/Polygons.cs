using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PolygonGubarkov
{
    class Polygons
    {
        int MAX_SIZE = 1000000;
        List<Polygon> polygons;

        public Polygons()
        {
            polygons = new List<Polygon>();

        }

        public Polygons(PolygonPoint[] points, Color color)
        {
            polygons = new List<Polygon>();
            polygons.Add(new Polygon(points, color));
        }

        public Polygons(List<Polygon> listPoints, Color color)
        {
            polygons = new List<Polygon>();
            foreach (Polygon p in listPoints)
            {
                polygons.Add(new Polygon(p.getPoints(), color));
            }
        }

        //public void addPolygon(PolygonPoint[] polygon)
        //{
        //    polygons.Add(polygon);
        //}

        public void setPolygon(List<Polygon> polygons)
        {
            this.polygons = polygons;
        }

        public List<Polygon> getPolygons()
        {
            return polygons;
        }
        
        
    }
}

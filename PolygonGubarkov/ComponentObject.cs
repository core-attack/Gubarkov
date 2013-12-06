using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PolygonGubarkov
{
    //составной объект, состоящий из других объектов
    class ComponentObject
    {
        List<elementaryObject> objects;

        public ComponentObject() {
            objects = new List<elementaryObject>();
        }

        public List<elementaryObject> getObjects()
        {
            return objects;
        }

        public void setObject(elementaryObject o)
        {
            objects.Add(o);
        }

        public void setObjects(List<elementaryObject> a)
        {
            for (int i = 0; i < a.Count; i++)
            {
                setObject(a[i]);
            }
        }
    }
}

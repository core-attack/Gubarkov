using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refal
{
    class Element
    {
        public Element()
        { }

        public Element(int previos, int next, string content)
        {
            this.content = content;
            this.next = next;
            this.previos = previos;
        }

        //ссылка на предыдущий
        public int previos = -1;
        //ссылка на последующий
        public int next = -1;
        public string content = "";
    }
}

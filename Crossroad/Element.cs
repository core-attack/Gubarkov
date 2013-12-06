using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossroad
{
    class Element
    {
        /// <summary>
        /// Индекс элемента в массиве транспортных средств
        /// </summary>
        int index = -1;
        int previous = -1;
        int next = -1;

        public Element(int index, int previuos, int next)
        {
            setIndex(index);
            setPrevious(previous);
            setNext(next);
        }

        /// <summary>
        /// Задает индекс элемента в массиве транспортных средств
        /// </summary>
        /// <param name="index"></param>
        public void setIndex(int index)
        {
            this.index = index;
        }

        public void setPrevious(int previous)
        {
            this.previous = previous;
        }

        public void setNext(int next)
        {
            this.next = next;
        }

        /// <summary>
        /// Возвращает индекс элемента в массиве транспортных средств
        /// </summary>
        /// <returns></returns>
        public int getIndex()
        {
            return index;
        }

        public int getPrevious()
        {
            return previous;
        }

        public int getNext()
        {
            return next;
        }
    }
}

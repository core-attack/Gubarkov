using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refal
{
    class DualList
    {
        public Element[] list;
        //первая пустая запись
        int firstEmptyIndex = 0;

        public DualList()
        { }

        public DualList(int i)
        {
            list = new Element[i];
        }

        /// <summary>
        /// Помещает в конец списка элемент
        /// </summary>
        /// <param name="s">Помещаемый элемент</param>
        public void setNewElement(string elem)
        {
            if (list != null)
            {
                Element e = new Element(list.Length - 1, -1, elem);
                list[firstEmptyIndex] = e;
                list[list.Length - 2].next = list.Length - 1;
                firstEmptyIndex++;
            }
            else
            {
                list = new Element[100];
                Element e = new Element(list.Length - 1, -1, elem);
                list[firstEmptyIndex] = e;
                firstEmptyIndex++;
            }
        }

        /// <summary>
        /// Вставляет элемент в указанную позицию списка
        /// </summary>
        /// <param name="i">Позиция</param>
        /// <param name="elem">Элемент</param>
        public void insertElementTo(int i, string elem)
        {
            Element e;
            if (i > 0 && i < list.Length)
                e = new Element(i - 1, i + 1, elem); 
            else if (i == 0)
                e = new Element(-1, 1, elem); 
            else
                e = new Element(-1, -1, elem); 
            int firstEmptyIndex = getFirstEmptyIndex();
            if (firstEmptyIndex != -1)
            {
                list[firstEmptyIndex] = e;
                list[e.previos].next = firstEmptyIndex;
                list[e.next].previos = firstEmptyIndex;
            }
            else
            {
                //увеличивает размерность массива и добавляет туда старые записи
                rebuildList(100);
            }
        }

        /// <summary>
        /// Возвращает индекс первой пустой записи в массиве
        /// </summary>
        /// <returns></returns>
        int getFirstEmptyIndex()
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == null)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Удаляет элемент по указанному индексу
        /// </summary>
        /// <param name="i"></param>
        public void removeElementAt(int i)
        {

            list[list[i].previos].next = list[i].next;
            list[list[i].next].previos = list[i].previos;
            list[i] = null;
        }

        /// <summary>
        /// Дополняет список указанным количеством пустых элементов
        /// </summary>
        /// <param name="count"></param>
        void rebuildList(int count)
        {
            Element[] newList = new Element[list.Length];
            for (int i = 0; i < newList.Length; i++)
            {
                newList[i] = list[i];
            }
            list = new Element[list.Length + count];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = newList[i];
            }
        }

        /// <summary>
        /// Распечатывает содержимое списка в строку 
        /// </summary>
        /// <returns>Строка вида content=elem.content previos=elem.previos next=elem.next</returns>
        public string writeList()
        {
            string result = "";
            foreach (Element e in list)
            {
                result += String.Format("content={0} previos={1} next={2}\n", e.content, e.previos, e.next);
            }
            return result;
        }

    }
}

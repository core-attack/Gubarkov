using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crossroad
{
    /// <summary>
    /// Очередь, обслуживающая одну полосу движения
    /// </summary>
    class Queue
    {
        //массив индексов для массива объектов 
        Element[] index;
        public string errors = "";

        /// <summary>
        /// Задаёт размерность массива индексов 
        /// </summary>
        /// <param name="length"></param>
        public void setArrayLength(int length)
        {
            index = new Element[length];
        }

        /// <summary>
        /// Распечатывает содержимое списка в строку 
        /// </summary>
        /// <returns>Строка вида content=elem.index previos=elem.previos next=elem.next</returns>
        public string writeList()
        {
            string result = "";
            foreach (Element e in index)
            {
                result += String.Format("index={0} previos={1} next={2}\n", e.getIndex(), e.getPrevious(), e.getNext());
            }
            return result;
        }

        /// <summary>
        /// Удаляет элемент по указанному индексу из очереди
        /// </summary>
        /// <param name="i"></param>
        public void removeElementAt(int i)
        {
            if (index[i].getPrevious() != -1)
                index[index[i].getPrevious()].setNext(index[i].getNext());
            if (index[i].getNext() != -1)
                index[index[i].getNext()].setPrevious(index[i].getPrevious());
            index[i] = null;
            reBuildQueueAt(i, false);
        }

        /// <summary>
        /// Вставляет элемент по указанному индексу в очередь
        /// </summary>
        /// <param name="i"></param>
        public void insertElementAt(int i, Element element)
        {
            if (i < index.Length)
            {
                Element e = new Element(index[i].getIndex(), index[i].getPrevious(), index[i].getNext());
                index[i] = element;

            }
        }

        /// <summary>
        /// Передвигает очередь вперед, удаляя пустые ссылки
        /// </summary>
        public void moveQueue() 
        {
            for (int i = 0; i < index.Length; i++)
            {
                if (index[i] == null)
                    removeElementAt(i);
            }
        }
        /// <summary>
        /// Перестраивает очередь c определенного индекса (передвигает на один вперед или назад с указанной позиции в зависимости от булевского значения    )
        /// </summary>
        /// <param name="element_index">индекс элемента</param>
        /// <param name="back">true - передвигает на один назад, false - передвигает на один вперед</param>
        public void reBuildQueueAt(int element_index, bool back)
        {
            if (element_index < index.Length && element_index >= 0)
            {
                if (back)
                {
                    int i = index.Length - 1;
                    while (i - 1 >= element_index)
                        index[i] = index[i--];
                }
                else
                {
                    if (index[element_index] == null)
                    {
                        while (element_index < index.Length)
                            if (element_index + 1 < index.Length)
                                index[element_index] = index[element_index++];
                        index[index.Length - 1] = null;
                    }
                }
            }
            else
                errors += "Вызываемый индекс объекта больше размерности массива\n";
            
        }

        /// <summary>
        /// Возвращает индекс первой нулевой записи очереди
        /// </summary>
        /// <returns></returns>
        public int getFirstNullIndex()
        {
            for (int i = 0; i < index.Length; i++)
                if (index[i] == null)
                    return i;
            return -1;
        }


        /// <summary>
        /// Добавляет в конец очереди индекс элемента
        /// </summary>
        /// <param name="element_index"></param>
        public void addToEnd(int element_index)
        {
            int firstNullIndex = getFirstNullIndex();
            if (firstNullIndex != -1)
                index[getFirstNullIndex()] = new Element(element_index, firstNullIndex - 1, -1);
            else
                errors += "Невозможно добавить элемент в очередь, поскольку она полностью заполнена\n";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolecularDynamic
{
    class EventQueue
    {
        Event[] events;
        //первый свободный индекс в массиве событий
        int firstFreeIndex = 0;

        public EventQueue(int countEvents)
        {
            firstFreeIndex = 0;
            events = new Event[countEvents];
        }

        //пустая ли очередь событий
        public bool empty()
        {
            foreach (Event e in events)
                if (e != null)
                    return false;
            return true;
        }

        //возвращает самое раннее событие
        public Event getForwardEvent()
        {
            /*
            for (int i = 0; i < events.Length; i++ )
                if (events[i] != null)
                    return events[i];
            return null;*/
            return events[0];
        }

        //задает событие
        public void setEvent(Event e)
        {
            if (firstFreeIndex < events.Length)
            {
                events[firstFreeIndex] = new Event(e.getAtomId(), e.getWallId(), e.getTime());
                firstFreeIndex++;
            }
            sort();
        }

        //сортирует весь список вставкой по возрастанию времени возникновения событий
        void sort()
        {
            Event e1;
            int i, j;
            for (i = 1; i < events.Length; i++)
            {
                e1 = events[i];
                j = i - 1;
                while (events[j].getTime() > e1.getTime() && j > 0)
                {
                    events[j + 1] = events[j];
                    j = j - 1;
                }
                events[j + 1] = e1;
            }
        }

        //сортирует список по возрастанию времени возникновения событий
        //только последний элемент списка вставляет в определенную позицию
        void reSort()
        {

        }
    }
}

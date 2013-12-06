using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolecularDynamic
{
    class Event
    {
        //идентификаторы молекул
        int atomId = -1;
        //идентификатор стены
        int wallId = -1;
        //момент времени, когда произошло
        DateTime time = new DateTime();
        //индекс последующего события
        int nextEventIndex = 0;
        //индекс предыдущего события
        int previousEventIndex = 0;

        //столкновения молекул
        public Event()
        { }
        
        public Event(int atomId, int wallId, DateTime time) 
        {
            this.atomId = atomId;
            this.wallId = wallId;
            this.time = time;
        }

        public Event(int atomId, int wallId, DateTime time, int nextEventIndex, int previousEventIndex)
        {
            this.atomId = atomId;
            this.wallId = wallId;
            this.time = time;
            this.nextEventIndex = nextEventIndex;
            this.previousEventIndex = previousEventIndex;

        }

        public int getAtomId()
        {
            return atomId;
        }

        public int getWallId()
        {
            return wallId;
        }

        public int getNextEventIndex()
        {
            return nextEventIndex;
        }

        public int getPreviousEventIndex()
        {
            return previousEventIndex;
        }

        public DateTime getTime()
        {
            return time;
        }

        public void setAtomId(int id)
        {
            this.atomId = id;
        }

        public void setWallId(int id)
        {
            this.wallId = id;
        }

        public void setTime(DateTime time)
        {
            this.time = time;
        }

        public void setNextEventIndex(int nextEventIndex)
        {
            this.nextEventIndex = nextEventIndex;
        }

        public void setPreviousEventIndex(int previousEventIndex)
        {
            this.previousEventIndex = previousEventIndex;
        }


    }
}

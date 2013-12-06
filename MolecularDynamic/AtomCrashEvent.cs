using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MolecularDynamic
{
    class AtomCrashEvent : Event
    {
        int secondAtomId = -1;

        public int getSecondAtomId()
        {
            return secondAtomId;
        }

        public void setSecondAtomId(int secondAtomId)
        {
            this.secondAtomId = secondAtomId;
        }

        public AtomCrashEvent(int id)
        {
            secondAtomId = id;
        }
    }
}

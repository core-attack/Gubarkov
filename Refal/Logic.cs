using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refal
{
    class Logic
    {
        DualList list = new DualList();

        //считывает выражение
        /// <summary>
        /// Считывает строку и записывает в двусторонний список 
        /// </summary>
        /// <param name="rows">Анализируемая строка</param>
        public void readExpression(string rows)
        {
            list.setNewElement(rows);
        }

        /// <summary>
        /// Распечатывает содержимое списка в строку 
        /// </summary>
        /// <returns>Строка вида content=elem.content previos=elem.previos next=elem.next</returns>
        public string writeList()
        {
            return list.writeList();
        }

        /// <summary>
        /// Проверяет строку-выражение на соотвествие правилам оформления рефала
        /// </summary>
        /// <param name="expr">Анализируемая строка</param>
        /// <returns></returns>
        public static bool checkExpression(string expr)
        {
            return false;
        }

    }
}

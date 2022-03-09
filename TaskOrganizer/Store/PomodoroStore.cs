using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using TaskOrganizer.Model;

namespace TaskOrganizer.Store
{
    public class PomodoroStore : IEnumerable<int>
    {
        IList<int> amountOfWorkedHours { get; set; }
        public PomodoroStore()
        {
            amountOfWorkedHours = new List<int>();
        }

        public string AmountOfHours()
        {
            var sum = 0;
            if (amountOfWorkedHours == null)
            {
                //do nothing
            }
            else
            {
                sum = amountOfWorkedHours.Sum(x => Convert.ToInt32(x));

            }
            return sum.ToString();
        }
        /// <summary>
        /// Sum hours when user stops clock early
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="declaredTime"></param>
        /// <param name="workedTime"></param>
        public void SumHours(DispatcherTimer timer, int declaredTime, int workedTime = 0)
        {
            if(timer != null)
            {
                workedTime = workedTime / 60;
                int sum = declaredTime - workedTime;
                amountOfWorkedHours.Add(sum);
            }
            else
            {
                amountOfWorkedHours.Add(declaredTime);
            }
        }
 
        public IEnumerator<int> GetEnumerator()
        {
            foreach(int time in amountOfWorkedHours)
            {
                yield return time;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

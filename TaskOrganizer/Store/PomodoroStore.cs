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
        IList<int> AmountOfWorkedHours { get; set; }
        public PomodoroStore()
        {
            AmountOfWorkedHours = new List<int>();
        }

        public string AmountOfHours()
        {
            var sum = 0;
            if (AmountOfWorkedHours == null)
            {
                //do nothing
            }
            else
            {
                sum = AmountOfWorkedHours.Sum(x => Convert.ToInt32(x));
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
                AmountOfWorkedHours.Add(sum);
            }
            else
            {
                AmountOfWorkedHours.Add(declaredTime);
            }
        }
 
        public IEnumerator<int> GetEnumerator()
        {
            foreach(int time in AmountOfWorkedHours)
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

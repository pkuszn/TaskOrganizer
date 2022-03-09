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
        public void SumHours(int model, DispatcherTimer timer)
        {
            if(timer != null)
            {
                int sum = model / 60;
                amountOfWorkedHours.Add(sum);
            }
            else
            {
                amountOfWorkedHours.Add(model);
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

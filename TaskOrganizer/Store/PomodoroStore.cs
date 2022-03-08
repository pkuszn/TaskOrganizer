using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            if (amountOfWorkedHours != null)
            {
                sum = amountOfWorkedHours.Sum(x => Convert.ToInt32(x));
            }
            Debug.WriteLine(sum);
            return sum.ToString();
        }
        public void SumHours(int model) => amountOfWorkedHours.Add(model);
 
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

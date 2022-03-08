using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TaskOrganizer.Model;

namespace TaskOrganizer.Store
{
    public class PomodoroStore : IEnumerable<PomodoroModel>
    {
        IList<PomodoroModel> amountOfWorkedHours { get; set; }
        public PomodoroStore()
        {
            amountOfWorkedHours = new List<PomodoroModel>();
        }

        public void SumHours(PomodoroModel model) => amountOfWorkedHours.Add(model);
 
        public IEnumerator<PomodoroModel> GetEnumerator()
        {
            foreach(PomodoroModel time in amountOfWorkedHours)
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

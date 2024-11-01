using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace TaskOrganizer.Store;

public class PomodoroStore : IEnumerable<int>
{
    private readonly IMapper Mapper;
    public IList<int> SummedHours { get; set; }
    public PomodoroStore(IMapper mapper)
    {
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        SummedHours = [];
    }

    public string GetSummedHours()
    {
        int sum = SummedHours.Sum(Convert.ToInt32);
        return sum.ToString();
    }

    public void SumHours(DispatcherTimer timer, int declaredTime, int workedTime = 0)
    {
        if(timer != null)
        {
            workedTime /= 60;
            int sum = declaredTime - workedTime;
            SummedHours.Add(sum);
        }
        else
        {
            SummedHours.Add(declaredTime);
        }
    }

    public IEnumerator<int> GetEnumerator()
    {
        foreach(int time in SummedHours)
        {
            yield return time;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

using System;
using System.Collections.Generic;

namespace WpfSplitGrid.ViewModels
{
    class MainVm : Vm
    {
        public List<WeekVm> Weeks { get; } = new List<WeekVm>();

        public MainVm()
        {
            // Считаем что неделя относится к тому году,
            //  к которому относится понедельник
            var date = new DateTime(2000, 1, 1);
            while (date.DayOfWeek != DayOfWeek.Monday)
                date = date.AddDays(1);
            var endDate = new DateTime(2010, 1, 1);
            int weekNum = 0;
            while (date < endDate)
            {
                int year = date.Year;
                var week = new WeekVm(year - 2000, weekNum);
                Weeks.Add(week);
                date = date.AddDays(7);
                if (date.Year > year) weekNum = 0;
                else weekNum++;
            }
        }
    }

    class WeekVm : Vm
    {
        public int YearNum { get; }
        public int WeekNum { get; }

        public WeekVm(int yearNum, int weekNum)
        {
            YearNum = yearNum;
            WeekNum = weekNum;
        }
    }
}

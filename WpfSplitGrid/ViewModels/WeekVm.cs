namespace WpfSplitGrid.ViewModels
{
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

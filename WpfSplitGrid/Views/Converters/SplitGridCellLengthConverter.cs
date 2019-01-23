using System;
using System.Globalization;

namespace WpfSplitGrid.Views.Converters
{
    class SplitGridCellLengthConverter : MultiConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var totalLength = (double)values[0];
            var cells = (int)values[1];
            var shortSpace = (double)values[2];
            var longSpace = (double)values[3];
            var longSpacePeriod = (int)values[4];
            var totalNum = Math.Max(cells - 1, 0);
            var longNum = totalNum / longSpacePeriod;
            var shortNum = totalNum - longNum;
            var totalSpace = longNum * longSpace + shortNum * shortSpace;
            return Math.Max(totalLength - totalSpace, 0) / cells;
        }
    }
}

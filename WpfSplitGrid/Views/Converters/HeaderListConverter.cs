using System;
using System.Globalization;
using System.Linq;

namespace WpfSplitGrid.Views.Converters
{
    class HeaderListConverter : MultiConverterBase
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var start = (int)values[0];
            var count = (int)values[1];
            var delta = (int)values[2];
            var headerList = Enumerable
                                .Range(start, count)
                                .Where(h => h % 5 == 0)
                                .Select(h => new HeaderItem(h - delta, $"{h}"));
            return headerList;
        }
    }

    class HeaderItem
    {
        public int Index { get; }
        public string Text { get; }
        public HeaderItem(int index, string text)
        {
            Index = index;
            Text = text;
        }
    }
}

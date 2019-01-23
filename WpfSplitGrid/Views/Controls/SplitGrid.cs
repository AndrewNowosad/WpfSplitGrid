using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfSplitGrid.Views.Controls
{
    public class SplitGrid : Panel
    {
        #region AP
        public static int GetColumn(DependencyObject obj)
            => (int)obj.GetValue(ColumnProperty);

        public static void SetColumn(DependencyObject obj, int value)
            => obj.SetValue(ColumnProperty, value);

        public static readonly DependencyProperty ColumnProperty =
            DependencyProperty.RegisterAttached("Column", typeof(int),
                typeof(SplitGrid), new PropertyMetadata(0));

        public static int GetRow(DependencyObject obj)
            => (int)obj.GetValue(RowProperty);

        public static void SetRow(DependencyObject obj, int value)
            => obj.SetValue(RowProperty, value);

        public static readonly DependencyProperty RowProperty =
            DependencyProperty.RegisterAttached("Row", typeof(int),
                typeof(SplitGrid), new PropertyMetadata(0));
        #endregion AP

        #region DP
        public int Columns
        {
            get => (int)GetValue(ColumnsProperty);
            set => SetValue(ColumnsProperty, value);
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register(nameof(Columns), typeof(int), typeof(SplitGrid),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidateColumns);

        private static bool ValidateColumns(object value) => (int)value > 0;

        public int Rows
        {
            get => (int)GetValue(RowsProperty);
            set => SetValue(RowsProperty, value);
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register(nameof(Rows), typeof(int), typeof(SplitGrid),
                new FrameworkPropertyMetadata(1, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidateRows);

        private static bool ValidateRows(object value) => (int)value > 0;

        public double ShortSpace
        {
            get => (double)GetValue(ShortSpaceProperty);
            set => SetValue(ShortSpaceProperty, value);
        }

        public static readonly DependencyProperty ShortSpaceProperty =
            DependencyProperty.Register(nameof(ShortSpace), typeof(double), typeof(SplitGrid),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double LongSpace
        {
            get => (double)GetValue(LongSpaceProperty);
            set => SetValue(LongSpaceProperty, value);
        }

        public static readonly DependencyProperty LongSpaceProperty =
            DependencyProperty.Register(nameof(LongSpace), typeof(double), typeof(SplitGrid),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public int LongSpacePeriod
        {
            get { return (int)GetValue(LongSpacePeriodProperty); }
            set { SetValue(LongSpacePeriodProperty, value); }
        }

        public static readonly DependencyProperty LongSpacePeriodProperty =
            DependencyProperty.Register(nameof(LongSpacePeriod), typeof(int), typeof(SplitGrid),
                new FrameworkPropertyMetadata(5, FrameworkPropertyMetadataOptions.AffectsMeasure), ValidateLongSpacePeriod);

        private static bool ValidateLongSpacePeriod(object value) => (int)value > 0;

        public double CellWidth
        {
            get => (double)GetValue(CellWidthProperty);
            set => SetValue(CellWidthProperty, value);
        }

        public static readonly DependencyProperty CellWidthProperty =
            DependencyProperty.Register(nameof(CellWidth), typeof(double), typeof(SplitGrid),
                new FrameworkPropertyMetadata(double.PositiveInfinity, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double CellHeight
        {
            get => (double)GetValue(CellHeightProperty);
            set => SetValue(CellHeightProperty, value);
        }

        public static readonly DependencyProperty CellHeightProperty =
            DependencyProperty.Register(nameof(CellHeight), typeof(double), typeof(SplitGrid),
                new FrameworkPropertyMetadata(double.PositiveInfinity, FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion DP

        // Этап подсчета занимаемого места
        protected override Size MeasureOverride(Size constraint)
        {
            columns = Columns;
            rows = Rows;
            shortSpace = ShortSpace;
            longSpace = LongSpace;
            longSpacePeriod = LongSpacePeriod;
            cellWidth = CellWidth;
            cellHeight = CellHeight;
            var cellSize = new Size(cellWidth, cellHeight);

            // Обязанность панели запросить желаемое место элементов
            foreach (UIElement child in InternalChildren)
                child.Measure(cellSize);

            // Если размеры ячейки не заданы, выбираем размер наибольшего элемента
            if (double.IsInfinity(cellWidth))
                cellWidth = InternalChildren.Cast<UIElement>().Max(child => child.DesiredSize.Width);
            if (double.IsInfinity(cellHeight))
                cellHeight = InternalChildren.Cast<UIElement>().Max(child => child.DesiredSize.Height);

            // Итоговые желаемые размеры панели
            double width = CalcTotalSpace(columns - 1) + columns * cellWidth;
            double height = CalcTotalSpace(rows - 1) + rows * cellHeight;
            return new Size(width, height);
        }

        // Этап размещения элементов
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            foreach (UIElement child in InternalChildren)
            {
                int column = GetColumn(child);
                int row = GetRow(child);
                double x = column * cellWidth + CalcTotalSpace(column);
                double y = row * cellHeight + CalcTotalSpace(row);
                var childBounds = new Rect(x, y, cellWidth, cellHeight);
                // Размещаем
                child.Arrange(childBounds);
            }

            double width = CalcTotalSpace(columns - 1) + columns * cellWidth;
            double height = CalcTotalSpace(rows - 1) + rows * cellHeight;
            return new Size(width, height);
        }

        private int columns;
        private int rows;
        private double shortSpace;
        private double longSpace;
        private int longSpacePeriod;
        private double cellWidth;
        private double cellHeight;

        private double CalcTotalSpace(int totalNum)
        {
            if (totalNum < 0) return 0;
            int longNum = totalNum / longSpacePeriod;
            int shortNum = totalNum - longNum;
            return longNum * longSpace + shortNum * shortSpace;
        }
    }
}

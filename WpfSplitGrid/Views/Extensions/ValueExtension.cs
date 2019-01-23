using System;
using System.Windows.Markup;

namespace WpfSplitGrid.Views.Extensions
{
    class ValueExtension<T> : MarkupExtension
    {
        public T Value { get; set; }
        public ValueExtension() { }
        public ValueExtension(T value) => Value = value;
        public override object ProvideValue(IServiceProvider serviceProvider) => Value;
    }

    class BoolExtension : ValueExtension<bool>
    {
        public BoolExtension() { }
        public BoolExtension(bool value) : base(value) { }
    }

    class IntExtension : ValueExtension<int>
    {
        public IntExtension() { }
        public IntExtension(int value) : base(value) { }
    }

    class DoubleExtension : ValueExtension<double>
    {
        public DoubleExtension() { }
        public DoubleExtension(double value) : base(value) { }
    }
}

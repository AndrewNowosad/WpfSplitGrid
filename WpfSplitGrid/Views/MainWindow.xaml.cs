using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace WpfSplitGrid.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0231: // WM_ENTERSIZEMOVE
                    content = Content;
                    Content = new TextBlock
                    {
                        Text = "Обновление...",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    break;
                case 0x0232: // WM_EXITSIZEMOVE
                    Content = content;
                    break;
            }
            return IntPtr.Zero;
        }

        private object content;
    }
}

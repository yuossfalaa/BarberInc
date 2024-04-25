using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
namespace BarberInc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WindowInteropHelper _windowInteropHelper;
        public MainWindow(MainViewModel MainViewModel)
        {
            InitializeComponent();
            DataContext = MainViewModel;
            _windowInteropHelper = new WindowInteropHelper(this);
            size();
        }
        public void size()
        {

            int width = (int)System.Windows.SystemParameters.PrimaryScreenWidth;
            int hight = (int)System.Windows.SystemParameters.PrimaryScreenHeight;
            this.MaxHeight = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight;

            if (width <= 1280 || hight <= 690)
            {
                this.Width = System.Windows.SystemParameters.MaximizedPrimaryScreenWidth;
                this.Height = System.Windows.SystemParameters.MaximizedPrimaryScreenHeight;
            }
        }
        private void CloseApplication(object sender, RoutedEventArgs e)
        {
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.1));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void MaximizeWindow(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {

                this.WindowState = WindowState.Maximized;
                MaximizeWindowPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                MaximizeWindowPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit;
            }
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TriggerMoveWindow(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var screen = GetScreenAt();

                if (WindowState == System.Windows.WindowState.Maximized)
                {
                    WindowState = System.Windows.WindowState.Normal;

                    double pct = e.GetPosition(this).X / screen.Bounds.Width;
                    Top = screen.Bounds.Top;
                    Left = e.GetPosition(this).X - (pct * Width);
                }

                DragMove();
            }
        }

        private Screen GetScreenAt()
        {
            return Screen.FromHandle(_windowInteropHelper.Handle);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                MaximizeWindowPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.FullscreenExit;

            }
            else
            {
                MaximizeWindowPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.WindowMaximize;

            }
        }

    }
}
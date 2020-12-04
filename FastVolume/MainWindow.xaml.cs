using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FastVolume.Annotations;
using GradeWinLib;

namespace FastVolume
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _cursorCoordinatesString;
        private bool _showToolWindow = false;

        public PointXY ScreenSize
        {
            get
            {
                var w = SystemParameters.PrimaryScreenWidth;
                var h = SystemParameters.PrimaryScreenHeight;
                return new PointXY((int)w, (int)h);
            }
        }

        public bool ShowToolWindow
        {
            get => _showToolWindow;
            set
            {
                _showToolWindow = value;
                if (value)
                {
                    //ColorfulBorder.Background = new SolidColorBrush(Color.FromRgb(0x00, 0xff, 0x00));
                    Visibility = Visibility.Visible;
                }
                else
                {
                    //ColorfulBorder.Background = new SolidColorBrush(Color.FromRgb(0xff, 0x00, 0x00));
                    Visibility = Visibility.Hidden;
                }
            }
        }

        public string CursorCoordinatesString
        {
            get => _cursorCoordinatesString;
            set
            {
                if (value == _cursorCoordinatesString) return;
                _cursorCoordinatesString = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            //ResTb.Text = $"{ScreenSize.X};\n{ScreenSize.Y}";

            SetStartupWindowLocation();

            TraceCursor();
        }

        private void SetStartupWindowLocation()
        {
            var width = ScreenSize.X - this.Width - 30;
            var height = ((double)ScreenSize.Y) / 2 - this.Height / 2;
            this.Left = width;
            this.Top = height;
        }

        private async void TraceCursor()
        {
            while (true)
            {
                //CursorCoordinatesString = $"X = {GetCursorPosition().X}; Y = {GetCursorPosition().Y}";
                var xZone = ShowToolWindow ? 30 + Width + 30 : 4;
                ShowToolWindow = Screen.CursorPosition.X > ScreenSize.X - xZone &&
                                 (double)ScreenSize.Y / 2 - Height / 2 - 30 <= Screen.CursorPosition.Y &&
                                 (double)ScreenSize.Y / 2 + Height / 2 + 30 >= Screen.CursorPosition.Y;

                var p = Screen.CursorPosition;
                CursorCoordinatesString = Convert.ToString(p.X) + ";\n" + Convert.ToString(p.Y);
                await Task.Delay(10);
            }
        }

        private Point GetCursorPosition()
        {
            var windowPosition = Mouse.GetPosition(this);
            var screenPosition = this.PointToScreen(windowPosition);
            return screenPosition;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

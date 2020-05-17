using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ColorPicker
{
    public partial class MainWindow : Window
    {
        private static readonly Regex REGEX = new Regex("[^0-9]+");

        public ColorHolder ColorHolder { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ColorHolder = new ColorHolder();
            DataContext = ColorHolder;
        }

        private void ValidateInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = REGEX.IsMatch(e.Text);
        }

        private void ColorPicker_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                PickColor(sender as Image);
            }
        }

        private void ColorPicker_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            PickColor(sender as Image);
        }

        private void PickColor(Image colorPicker)
        {
            Point mousePosition = Mouse.GetPosition(colorPicker);
            BitmapSource source = colorPicker.Source as BitmapSource;

            try
            {
                CroppedBitmap fragment = new CroppedBitmap(source, new Int32Rect((int)(source.PixelWidth * mousePosition.X / colorPicker.ActualWidth), (int)(source.PixelHeight * mousePosition.Y / colorPicker.ActualHeight), 1, 1));
                byte[] pixel = new byte[source.Format.BitsPerPixel / 8];
                fragment.CopyPixels(pixel, source.Format.BitsPerPixel / 8, 0);

                ColorHolder.RGB.R = pixel[2];
                ColorHolder.RGB.G = pixel[1];
                ColorHolder.RGB.B = pixel[0];
            }
            catch (ArgumentException) { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ColorPicker
{
    public class RGBColor
    {
        private ColorHolder holder;

        public float Red { get; private set; }
        public float Green { get; private set; }
        public float Blue { get; private set; }

        public byte R
        {
            get { return (byte)(255 * Red);}
            set
            {
                Red = (float)value / 255;
                holder.OnPropertyChanged("RGB");
                holder.UpdateCMYK();
            }
        }
        public byte G
        {
            get { return (byte)(255 * Green); }
            set
            {
                Green = (float)value / 255;
                holder.OnPropertyChanged("RGB");
                holder.UpdateCMYK();
            }
        }
        public byte B
        {
            get { return (byte)(255 * Blue); }
            set
            {
                Blue = (float)value / 255;
                holder.OnPropertyChanged("RGB");
                holder.UpdateCMYK();
            }
        }

        public Brush Brush
        {
            get { return new SolidColorBrush(Color.FromRgb(R, G, B)); }
        }

        public RGBColor(ColorHolder holder)
        {
            this.holder = holder;
            Red = 1;
            Green = 0;
            Blue = 0;
        }

        public void FromCMYK(CMYKColor color)
        {
            Red = 1 - Math.Min(1, color.Cyan * (1 - color.Black) + color.Black);
            Green = 1 - Math.Min(1, color.Magenta * (1 - color.Black) + color.Black);
            Blue = 1 - Math.Min(1, color.Yellow * (1 - color.Black) + color.Black);
        }
    }
}

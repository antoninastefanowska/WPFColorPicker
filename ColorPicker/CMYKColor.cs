using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker
{
    public class CMYKColor
    {
        private ColorHolder holder;

        public float Cyan { get; private set; }
        public float Magenta { get; private set; }
        public float Yellow { get; private set; }
        public float Black { get; private set; }

        public byte C
        {
            get { return (byte)(255 * Cyan); }
            set
            {
                Cyan = (float)value / 255;
                holder.OnPropertyChanged("CMYK");
                holder.UpdateRGB();
            }
        }
        public byte M
        {
            get { return (byte)(255 * Magenta); }
            set
            {
                Magenta = (float)value / 255;
                holder.OnPropertyChanged("CMYK");
                holder.UpdateRGB();
            }
        }
        public byte Y
        {
            get { return (byte)(255 * Yellow); }
            set
            {
                Yellow = (float)value / 255;
                holder.OnPropertyChanged("CMYK");
                holder.UpdateRGB();
            }
        }
        public byte K
        {
            get { return (byte)(255 * Black); }
            set
            {
                Black = (float)value / 255;
                holder.OnPropertyChanged("CMYK");
                holder.UpdateRGB();
            }
        }

        public CMYKColor(ColorHolder holder)
        {
            this.holder = holder;
            Cyan = 0;
            Magenta = 0;
            Yellow = 0;
            Black = 0;
        }

        public void FromRGB(RGBColor color)
        {
            Black = Math.Min(1 - color.Red, Math.Min(1 - color.Green, 1 - color.Blue));
            Cyan = (1 - color.Red - Black) / (1 - Black);
            Magenta = (1 - color.Green - Black) / (1 - Black);
            Yellow = (1 - color.Blue - Black) / (1 - Black);
        }
    }
}

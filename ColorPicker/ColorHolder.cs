using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker
{
    public class ColorHolder : INotifyPropertyChanged
    {
        public RGBColor RGB { get; set; }
        
        public CMYKColor CMYK { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ColorHolder()
        {
            RGB = new RGBColor(this);
            CMYK = new CMYKColor(this);
            UpdateCMYK();
        }

        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void UpdateRGB()
        {
            RGB.FromCMYK(CMYK);
            OnPropertyChanged("RGB");
        }

        public void UpdateCMYK()
        {
            CMYK.FromRGB(RGB);
            OnPropertyChanged("CMYK");
        }
    }
}

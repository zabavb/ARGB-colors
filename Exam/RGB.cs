using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Exam
{
    internal class RGB
    {
        public Color Rgb{ get; set; }
        public RGB() { }

        public RGB(Color tmpRgb)
        {
            this.Rgb = tmpRgb;
        }
    }
}
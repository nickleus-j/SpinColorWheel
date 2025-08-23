using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WheelOfNames
{
    public class NameSegment
    {
        public string Name { get; set; } = string.Empty;
        public Brush Fill { get; set; } = Brushes.LightGray;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfNames.ColorSets
{
    public class DefaultColors
    {
        private List<string> ColorList = new List<string>
        {
            "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "AliceBlue",
            "AntiqueWhite",
            "Aquamarine",
            "Azure",
            "Beige",
            "DarkKhaki",
            "Black","Tomato",
            "BlanchedAlmond",
            "BlueViolet",
            "Brown",
            "BurlyWood",
            "Seagreen","Violet",
            "Chartreuse",
            "Chocolate",
            "Coral",
            "CornflowerBlue",
            "Crimson","Gold","Silver",
            "Cyan"
        };
        public virtual List<string> GetColors()=>ColorList.Shuffle();
    }
}

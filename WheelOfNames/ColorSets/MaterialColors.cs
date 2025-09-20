using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfNames.ColorSets
{
    public class MaterialColors:DefaultColors
    {
        private List<string> MaterialColorList = new List<string>
        {
            "Crimson","Pink","Purple","violet","Indigo","Blue","LightBlue","Cyan","Teal","Green","LightGreen","Lime","Yellow","#9fc149","Orange","LightSalmon","Brown","Gray","IndianRed"
            ,"Coral","Magenta","Lavender","HoneyDew","FireBrick","DodgerBlue","Magenta","DarkSalmon","DarkOrange","DarkMagenta"
        };
        public override List<string> GetColors() => MaterialColorList;
    }
}

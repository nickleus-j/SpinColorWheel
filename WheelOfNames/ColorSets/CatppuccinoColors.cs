using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfNames.ColorSets
{
    public class CatppuccinoColors : DefaultColors
    {
        private List<string> CatppuccinoColorsList = new List<string>
        {
            "#DC8A78","#DD7878","#787878","Red","#8839EF","Pink","Teal","Green","Lime","Yellow","#9fc149","violet","Orange","Lavender","Brown","Gray","IndianRed","#209FBF"
            ,"#77330f","LightBlue","#724e2c","HoneyDew","FireBrick","#563517","Magenta","DarkOrange","DarkMagenta"
        };
        public override List<string> GetColors() => CatppuccinoColorsList;
    }
}

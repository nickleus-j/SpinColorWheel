using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WheelOfNames.ColorSets
{
    public class ColorSetListings
    {
        public static IList<string> GetDefaultColors(){
            return new DefaultColors().GetColors();
        }
        public static IList<string> GetMaterialColors()
        {
            return new MaterialColors().GetColors();
        }
        public static IList<string> GetCatppuccinoColors()
        {
            return new CatppuccinoColors().GetColors();
        }
        public static IList<string> GetColorSetNames() => ["Default", "Material", "Catppuccino"];
    }
}

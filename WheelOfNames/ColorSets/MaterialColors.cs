namespace WheelOfNames.ColorSets
{
    public class MaterialColors:DefaultColors
    {
        private List<string> MaterialColorList = new List<string>
        {
            "Crimson","LightGreen","Purple","Blue","DarkSalmon","Cyan","Teal","Green","Lime","Yellow","#9fc149","violet","Orange","LightSalmon","Brown","Gray","IndianRed","Pink"
            ,"Coral","Magenta","LightBlue","Lavender","HoneyDew","FireBrick","DodgerBlue","Magenta","DarkOrange","DarkMagenta","Indigo"
        };
        public override List<string> GetColors() => MaterialColorList;
    }
}

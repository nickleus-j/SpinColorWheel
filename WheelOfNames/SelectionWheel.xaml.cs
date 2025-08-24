using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WheelOfNames
{
    /// <summary>
    /// Interaction logic for SelectionWheel.xaml
    /// </summary>
    public partial class SelectionWheel : UserControl
    {
        private readonly List<string> colors = new List<string>
        {
            "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Pink", "Cyan"
        };

        private readonly List<Path> slicePaths = new List<Path>();
        private readonly Random rand = new Random();
        public SelectionWheel()
        {
            InitializeComponent();
            DrawWheel();
        }
        private void DrawWheel()
        {
            WheelCanvas.Children.Clear();
            slicePaths.Clear();

            double centerX = WheelCanvas.Width / 2;
            double centerY = WheelCanvas.Height / 2;
            double radius = WheelCanvas.Width / 2;
            double anglePerSlice = 360.0 / colors.Count;

            for (int i = 0; i < colors.Count; i++)
            {
                double startAngle = i * anglePerSlice;
                double endAngle = startAngle + anglePerSlice;

                PathFigure fig = new PathFigure { StartPoint = new Point(centerX, centerY) };
                double x1 = centerX + radius * Math.Cos(startAngle * Math.PI / 180);
                double y1 = centerY + radius * Math.Sin(startAngle * Math.PI / 180);
                double x2 = centerX + radius * Math.Cos(endAngle * Math.PI / 180);
                double y2 = centerY + radius * Math.Sin(endAngle * Math.PI / 180);

                fig.Segments.Add(new LineSegment(new Point(x1, y1), true));
                fig.Segments.Add(new ArcSegment(new Point(x2, y2),
                    new Size(radius, radius), anglePerSlice, anglePerSlice >= 180, SweepDirection.Clockwise, true));
                fig.Segments.Add(new LineSegment(new Point(centerX, centerY), true));

                PathGeometry geom = new PathGeometry(new[] { fig });
                var solidBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(colors[i]);
                Path path = new Path
                {
                    Fill = solidBrush,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Data = geom
                };
                WheelCanvas.Children.Add(path);
                slicePaths.Add(path);

                // Label
                double midAngle = startAngle + anglePerSlice / 2;
                double labelX = centerX + (radius / 1.5) * Math.Cos(midAngle * Math.PI / 180);
                double labelY = centerY + (radius / 1.5) * Math.Sin(midAngle * Math.PI / 180);
                var label = new System.Windows.Controls.TextBlock
                {
                    Text = colors[i],
                    Foreground = new SolidColorBrush(GetContrastColor(solidBrush.Color)),
                    FontWeight = FontWeights.Bold
                };
                Canvas.SetLeft(label, labelX - 20);
                Canvas.SetTop(label, labelY - 10);
                WheelCanvas.Children.Add(label);
            }
        }
        private void Spin()
        {
            int selectedIndex = rand.Next(colors.Count);
            string selectedColor = colors[selectedIndex];

            double anglePerSlice = 360.0 / colors.Count;
            double targetAngle = 360 - (selectedIndex * anglePerSlice + anglePerSlice / 2);

            double currentRotation = ((RotateTransform)WheelCanvas.RenderTransform).Angle;
            double spins = 5 * 360; // extra full rotations
            double finalAngle = currentRotation + spins + targetAngle;
            ResetSliceStrokes();
            var anim = new DoubleAnimation
            {
                From = currentRotation,
                To = finalAngle,
                Duration = TimeSpan.FromSeconds(4),
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut }
            };

            anim.Completed += (s, _) =>
            {
                ResultLabel.Content = $"Picked: {selectedColor}";
                ((RotateTransform)WheelCanvas.RenderTransform).Angle = finalAngle % 360;
                HighlightSlice(selectedIndex);
            };

            ((RotateTransform)WheelCanvas.RenderTransform).BeginAnimation(System.Windows.Media.RotateTransform.AngleProperty, anim);
        }
        private void ResetSliceStrokes()
        {
            // Reset all slices
            foreach (var slice in slicePaths)
            {
                slice.Stroke = Brushes.Black;
                slice.StrokeThickness = 1;
                slice.Effect = null;
            }
        }
        private void HighlightSlice(int index)
        {
            // Highlight selected slice
            var chosen = slicePaths[index];
            chosen.Stroke = Brushes.White;
            chosen.StrokeThickness = 4;

            // Add a glow effect
            chosen.Effect = new System.Windows.Media.Effects.DropShadowEffect
            {
                Color = Colors.White,
                BlurRadius = 20,
                ShadowDepth = 0
            };
        }
        private void Spin_Click(object sender, RoutedEventArgs e)
        {
            Spin();
        }
        private System.Windows.Media.Color GetContrastColor(System.Windows.Media.Color color)
        {
            // Calculate luminance (perceived brightness)
            double luminance = (0.299 * color.R) + (0.587 * color.G) + (0.114 * color.B);

            // If light background → return black text, otherwise white text
            return luminance > 128 ? Colors.Black : Colors.White;
        }
    }
}

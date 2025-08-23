using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WheelControl.xaml
    /// </summary>
    public partial class WheelControl : UserControl
    {
        public WheelControl()
        {
            InitializeComponent();
            Loaded += (s, e) => Redraw();
        }


        public ObservableCollection<NameSegment> Segments
        {
            get => (ObservableCollection<NameSegment>)GetValue(SegmentsProperty);
            set => SetValue(SegmentsProperty, value);
        }


        public static readonly DependencyProperty SegmentsProperty =
        DependencyProperty.Register(
        nameof(Segments),
        typeof(ObservableCollection<NameSegment>),
        typeof(WheelControl),
        new PropertyMetadata(new ObservableCollection<NameSegment>(), OnSegmentsChanged));


        private static void OnSegmentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WheelControl wc)
                wc.Redraw();
        }


        private double _currentAngle;


        public void Redraw()
        {
            WheelCanvas.Children.Clear();
            if (Segments == null || Segments.Count == 0) return;


            double size = 350;
            WheelCanvas.Width = size;
            WheelCanvas.Height = size;
            double cx = size / 2.0;
            double cy = size / 2.0;
            double r = size / 2.0;
            double anglePer = 360.0 / Segments.Count;


            for (int i = 0; i < Segments.Count; i++)
            {
                double start = i * anglePer;
                var slice = CreateSlice(cx, cy, r, start, anglePer, Segments[i].Fill);
                WheelCanvas.Children.Add(slice);


                // Name label
                double mid = (start + anglePer / 2.0) * Math.PI / 180.0;
                double rx = cx + (r * 0.6) * Math.Cos(mid);
                double ry = cy + (r * 0.6) * Math.Sin(mid);


                var tb = new TextBlock { Text = Segments[i].Name, FontWeight = FontWeights.Bold };
                Canvas.SetLeft(tb, rx - 40);
                Canvas.SetTop(tb, ry - 10);
                tb.Width = 80;
                tb.TextAlignment = TextAlignment.Center;
                WheelCanvas.Children.Add(tb);
            }
        }


        private Path CreateSlice(double cx, double cy, double r, double startAngle, double sweep, Brush fill)
        {
            double sa = startAngle * Math.PI / 180.0;
            double ea = (startAngle + sweep) * Math.PI / 180.0;
            Point p1 = new(cx + r * Math.Cos(sa), cy + r * Math.Sin(sa));
            Point p2 = new(cx + r * Math.Cos(ea), cy + r * Math.Sin(ea));
            bool largeArc = sweep > 180;


            var fig = new PathFigure { StartPoint = new Point(cx, cy) };
            fig.Segments.Add(new LineSegment(p1, true));
            fig.Segments.Add(new ArcSegment(p2, new Size(r, r), 0, largeArc, SweepDirection.Clockwise, true));
            fig.Segments.Add(new LineSegment(new Point(cx, cy), true));


            var geom = new PathGeometry();
            geom.Figures.Add(fig);


            return new Path { Data = geom, Fill = fill, Stroke = Brushes.Black, StrokeThickness = 1 };
        }


        public Task SpinToIndexAsync(int targetIndex)
        {
            if (Segments == null || Segments.Count == 0) return Task.CompletedTask;


            double anglePer = 360.0 / Segments.Count;
            // Ensure the pointer (fixed at top) lands in the center of the chosen segment.
            double targetAngle = targetIndex * anglePer + anglePer / 2.0;
            int spins = new Random().Next(5, 8);
            // Rotate so pointer is aligned with chosen segment center.
            double finalAngle = _currentAngle - (spins * 360 + targetAngle);


            var anim = new DoubleAnimation
            {
                From = _currentAngle,
                To = finalAngle,
                Duration = TimeSpan.FromSeconds(4),
                EasingFunction = new QuinticEase { EasingMode = EasingMode.EaseOut }
            };


            var tcs = new TaskCompletionSource();
            anim.Completed += (s, e) => { _currentAngle = finalAngle % 360; tcs.SetResult(); };
            WheelRotate.BeginAnimation(RotateTransform.AngleProperty, anim);


            return tcs.Task;
        }
    }
}

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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WheelOfNames
{
    /// <summary>
    /// Interaction logic for ListCompo.xaml
    /// </summary>
    public partial class ListCompo : UserControl
    {
        private ObservableCollection<string> _names { get; set; }
        public IList<string> NamesListed => NamesBox != null? _names : ["no one"];
        public string ConcatenatedNames=> NamesListed!=null&&NamesListed?.Count>0 ? String.Empty: String.Join(", ", NamesListed);
        public event EventHandler ChangeNames;
        public ListCompo()
        {
            InitializeComponent();
            _names = new ObservableCollection<string>();
            DataContext = this;
            RaiseNameChangeEvent();
        }
        public void AddNamefromTextBox()
        {
            if (!String.IsNullOrWhiteSpace(StringBox.Text))
            {
                NamesListed.Add(StringBox.Text);
                NamesBox.ItemsSource = _names;
                //NamesBox.Items.Add(StringBox.Text.ToString());
                StringBox.Text = "";
                RaiseNameChangeEvent();
            }
        }
        public void RemoveSelectedName()
        {
            if (NamesBox.SelectedItem != null)
            {
                int index = NamesBox.SelectedIndex;
                NamesListed.Remove(NamesListed.ElementAt(index));
                //NamesBox.Items.RemoveAt(index);
                RaiseNameChangeEvent();
            }
        }
        public void UnselectNames()
        {
            NamesBox.UnselectAll();
        }
        private void AddNameButton_Click(object sender, RoutedEventArgs e)
        {
            AddNamefromTextBox();
        }
        private void RaiseNameChangeEvent()
        {
            if (ChangeNames != null)
            {
                ChangeNames(this, new EventArgs());
            }
            Remove.IsEnabled = _names.Count > 1;
        }
        private void RemoveNameButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveSelectedName();
        }
        public static readonly DependencyProperty NamesProperty = DependencyProperty.Register("ConcatenatedNames", typeof(string), typeof(ListCompo), new PropertyMetadata(""));

        private void StringBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter && e.Key != Key.Return && e.Key != Key.BrowserRefresh) return;
            AddNamefromTextBox();
        }
    }
}

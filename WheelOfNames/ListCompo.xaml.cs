using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<NameEntry> NameItems { get; set; }
        public string ConcatenatedNames=> NameItems != null&& NameItems?.Count>0 ? String.Empty: String.Join(", ", NameItems.Select(s=>s.Value));
        public event EventHandler ChangeNames;
        public ListCompo()
        {
            InitializeComponent();
            NameItems = new ObservableCollection<NameEntry>();
            DataContext = this;
            RaiseNameChangeEvent();
            NameItems.CollectionChanged += (_, __) => HookItemEvents();
        }
        public void AddNamefromTextBox()
        {
            if (!String.IsNullOrWhiteSpace(StringBox.Text))
            {
                NameItems.Add(new NameEntry { Value= StringBox.Text });
                NamesBox.ItemsSource = NameItems;
                StringBox.Text = "";
                RaiseNameChangeEvent();
            }
        }
        public void RemoveSelectedName()
        {
            if (NamesBox.SelectedItem != null)
            {
                int index = NamesBox.SelectedIndex;
                NameItems.Remove(NameItems.ElementAt(index));
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
            Remove.IsEnabled = NameItems.Count > 1;
        }
        private void HookItemEvents()
        {
            foreach (NameEntry item in NameItems)
            {
                item.PropertyChanged -= ItemChanged;
                item.PropertyChanged += ItemChanged;
            }
            RaiseNameChangeEvent();
        }
        private void ItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NameEntry.Value))
                RaiseNameChangeEvent();
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

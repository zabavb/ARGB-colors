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

namespace Exam
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AlphaSlider.IsEnabled = false;
            RedSlider.IsEnabled = false;
            GreenSlider.IsEnabled = false;
            BlueSlider.IsEnabled = false;
        }

        public Color Rectangle; ObservableCollection<RGB> ObservableCollectionColors = new ObservableCollection<RGB> { };

        public void UnIdentical()
        {
            byte tmpAlpha = (byte)AlphaSlider.Value; byte tmpRed = (byte)RedSlider.Value;
            byte tmpGreen = (byte)GreenSlider.Value; byte tmpBlue = (byte)BlueSlider.Value;
            byte[] tmpRgb = { tmpAlpha, tmpRed, tmpGreen, tmpBlue };

            if (AlphaCheckBox.IsChecked != true)
                tmpRgb[0] = 0;
            if (RedCheckBox.IsChecked != true)
                tmpRgb[1] = 0;
            if (GreenCheckBox.IsChecked != true)
                tmpRgb[2] = 0;
            if (BlueCheckBox.IsChecked != true)
                tmpRgb[3] = 0;
            bool returner = true;
            RGB Rgb = new RGB(Color.FromArgb(tmpRgb[0], tmpRgb[1], tmpRgb[2], tmpRgb[3]));
            Color tmpColor = Rgb.Rgb;
            for (int i = 0; i < ObservableCollectionColors.Count; i++)
                if (ObservableCollectionColors[i].Rgb == tmpColor)
                    returner = false;
            buttonAdd.IsEnabled = returner;
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            T parent = parentObject as T;
            if (parent != null)
                return parent;

            return FindVisualParent<T>(parentObject);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            byte tmpAlpha = (byte)AlphaSlider.Value;    byte tmpRed = (byte)RedSlider.Value;
            byte tmpGreen = (byte)GreenSlider.Value;    byte tmpBlue = (byte)BlueSlider.Value;
            byte[] tmpRgb = { tmpAlpha, tmpRed, tmpGreen, tmpBlue};

            if (AlphaCheckBox.IsChecked != true)
                tmpRgb[0] = 0;
            if (RedCheckBox.IsChecked != true)
                tmpRgb[1] = 0;
            if (GreenCheckBox.IsChecked != true)
                tmpRgb[2] = 0;
            if (BlueCheckBox.IsChecked != true)
                tmpRgb[3] = 0;
            RGB Rgb = new RGB(Color.FromArgb(tmpRgb[0], tmpRgb[1], tmpRgb[2], tmpRgb[3]));
            Rectangle = Rgb.Rgb;
            
            ShowColor.Fill = new SolidColorBrush(Rectangle);

            UnIdentical();
        }

        private void Slider_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (checkBox.Name == "AlphaCheckBox")
                    AlphaSlider.IsEnabled = (bool)checkBox.IsChecked;
                else if (checkBox.Name == "RedCheckBox")
                    RedSlider.IsEnabled = (bool)checkBox.IsChecked;
                else if (checkBox.Name == "GreenCheckBox")
                    GreenSlider.IsEnabled = (bool)checkBox.IsChecked;
                else if (checkBox.Name == "BlueCheckBox")
                    BlueSlider.IsEnabled = (bool)checkBox.IsChecked;
            }

            UnIdentical();

            byte tmpAlpha = (byte)AlphaSlider.Value; byte tmpRed = (byte)RedSlider.Value;
            byte tmpGreen = (byte)GreenSlider.Value; byte tmpBlue = (byte)BlueSlider.Value;
            byte[] tmpRgb = { tmpAlpha, tmpRed, tmpGreen, tmpBlue };

            if (AlphaCheckBox.IsChecked != true)
                tmpRgb[0] = 0;
            if (RedCheckBox.IsChecked != true)
                tmpRgb[1] = 0;
            if (GreenCheckBox.IsChecked != true)
                tmpRgb[2] = 0;
            if (BlueCheckBox.IsChecked != true)
                tmpRgb[3] = 0;

            RGB Rgb = new RGB(Color.FromArgb(tmpRgb[0], tmpRgb[1], tmpRgb[2], tmpRgb[3]));
            Rectangle = Rgb.Rgb;

            ShowColor.Fill = new SolidColorBrush(Rectangle);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            byte tmpAlpha = (byte)AlphaSlider.Value; byte tmpRed = (byte)RedSlider.Value;
            byte tmpGreen = (byte)GreenSlider.Value; byte tmpBlue = (byte)BlueSlider.Value;
            byte[] tmpRgb = { tmpAlpha, tmpRed, tmpGreen, tmpBlue };

            if (AlphaCheckBox.IsChecked != true)
                tmpRgb[0] = 0;
            if (RedCheckBox.IsChecked != true)
                tmpRgb[1] = 0;
            if (GreenCheckBox.IsChecked != true)
                tmpRgb[2] = 0;
            if (BlueCheckBox.IsChecked != true)
                tmpRgb[3] = 0;
            buttonAdd.IsEnabled = false;

            ObservableCollectionColors.Add(new RGB(Color.FromArgb(tmpRgb[0], tmpRgb[1], tmpRgb[2], tmpRgb[3])));
            ListBox.ItemsSource = null;
            ListBox.ItemsSource = ObservableCollectionColors;

            InvalidateVisual();
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            object dataContext = button.DataContext;
            ListBoxItem element = FindVisualParent<ListBoxItem>(button);
            var position = ListBox.Items.IndexOf(element.DataContext);

            ObservableCollectionColors.RemoveAt(position);
            
            ListBox.ItemsSource = null;     ListBox.ItemsSource = ObservableCollectionColors;

            InvalidateVisual();
            
            UnIdentical();
        }
    }
}
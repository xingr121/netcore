using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
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

namespace AllInput
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int _sliderValue;
        public int SliderValue
        {
            get { return _sliderValue; }
            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    OnPropertyChanged(nameof(SliderValue));
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = TbxName.Text;
            string ageGroup = "";
            if (RbnAgeBelow18.IsChecked == true)
            {
                ageGroup = "below 18";
            }
            else if (RbnAge1835.IsChecked == true)
            {
                ageGroup = "18-35";
            }
            else if (RbnAge36AndUp.IsChecked == true)
            {
                ageGroup = "36 and up";
            }

            string pets = "";
            if (CbxPetCat.IsChecked == true)
            {
                pets += "cat,";
            }
            if (CbxPetDog.IsChecked == true)
            {
                pets += "dog,";
            }
            if (CbxPetOther.IsChecked == true)
            {
                pets += "other,";
            }
            pets = pets.TrimEnd(',');

            string continent = (ComboContinent.SelectedItem as ComboBoxItem).Content.ToString();
            string preferredTemp = ((int)slValue.Value).ToString();

            string record = $"{name};{ageGroup};{pets};{continent};{preferredTemp}";

            try
            {
                using (StreamWriter writer = new StreamWriter(@"..\..\data.txt", true))
                {
                    writer.WriteLine(record);
                    MessageBox.Show("Record added successfully!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("errors occur while writing to file!", "writing error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

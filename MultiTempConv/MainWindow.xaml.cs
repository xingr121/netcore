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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiTempConv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ConvertValue()
        {
            // Get the input value
            if (double.TryParse(TbxInput.Text, out double inputValue))
            {
                // Convert input value based on selected input scale
                if (RbnInputC.IsChecked == true)
                {
                    // Convert from Celsius to the selected output scale
                    if (RbnOutputF.IsChecked == true)
                    {
                        TbxOutput.Text = ((inputValue * 9 / 5) + 32).ToString();
                    }
                    else if (RbnOutputK.IsChecked == true)
                    {
                        TbxOutput.Text = (inputValue + 273.15).ToString();
                    }
                    else
                    {
                        // Output scale is Celsius, no conversion needed
                        TbxOutput.Text = TbxInput.Text;
                    }
                }
                else if (RbnInputF.IsChecked == true)
                {
                    // Convert from Fahrenheit to the selected output scale
                    if (RbnOutputC.IsChecked == true)
                    {
                        TbxOutput.Text = ((inputValue - 32) * 5 / 9).ToString();
                    }
                    else if (RbnOutputK.IsChecked == true)
                    {
                        TbxOutput.Text = (((inputValue - 32) * 5 / 9) + 273.15).ToString();
                    }
                    else
                    {
                        // Output scale is Fahrenheit, no conversion needed
                        TbxOutput.Text = TbxInput.Text;
                    }
                }
                else if (RbnInputK.IsChecked == true)
                {
                    // Convert from Kelvin to the selected output scale
                    if (RbnOutputC.IsChecked == true)
                    {
                        TbxOutput.Text = (inputValue - 273.15).ToString();
                    }
                    else if (RbnOutputF.IsChecked == true)
                    {
                        TbxOutput.Text = ((inputValue - 273.15) * 9 / 5 + 32).ToString();
                    }
                    else
                    {
                        // Output scale is Kelvin, no conversion needed
                        TbxOutput.Text = TbxInput.Text;
                    }
                }
            }
            else
            {
                // Clear output if input is invalid
                if (TbxOutput != null)
                {
                    TbxOutput.Text = string.Empty;
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ConvertValue();
        }

        private void TbxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConvertValue();
        }
    }
}

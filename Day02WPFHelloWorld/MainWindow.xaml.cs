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

namespace Day02WPFHelloWorld
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

        private void BtnHelloLabel_Click(object sender, RoutedEventArgs e)
        {
            string name=TbxName.Text;
            if (name == "")
            {
                MessageBox.Show("Name must not be empty","input error",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            LblGreeting.Content = $"hello {name}, nice to meet you";
        }

        private void BtnHelloMessageBox_Click(object sender, RoutedEventArgs e)
        {
  string name=TbxName.Text;
            if (name == "")
            {
                MessageBox.Show("Name must not be empty","input error",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show($"hello {name}, nice to meet you","greeting",MessageBoxButton.OK,MessageBoxImage.Information); 
        }
    }
}

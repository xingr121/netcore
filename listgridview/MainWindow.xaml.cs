using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace listgridview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string DataFileName = @"..\..\people.txt";
        List<Person> peoplelist = new List<Person>();
        public MainWindow()
        {
            InitializeComponent();
            lvPeople.ItemsSource = peoplelist;
            LoadDataFromFile();
        }

        private void BtnAddPeople_Click(object sender, RoutedEventArgs e)
        {
           
            string name= TbxName.Text;
            int.TryParse(TbxAge.Text, out int age);
            string ageError = "";
            if (!Person.IsNameValid(name, out string nameError) || !Person.IsAgeValid(age, out  ageError))
            {
                MessageBox.Show(nameError ?? ageError ?? "");
                return;
            }
            
            peoplelist.Add(new Person(name, age));
            lvPeople.Items.Refresh();
            TbxName.Text = "";
            TbxAge.Text = "";

            // Update status bar
            UpdateStatusBar("Person added.");
        }

        private void BtnUpadatePeolple_Click(object sender, RoutedEventArgs e)
        {
        
            Person selectedPerson =lvPeople.SelectedItem as Person;
            string name = TbxName.Text;
            int.TryParse(TbxAge.Text, out int age);
            string ageError = "";
            // Validate input
            if (!Person.IsNameValid(name, out string nameError) || !Person.IsAgeValid(age, out  ageError))
            {
                MessageBox.Show(nameError ?? ageError ?? "");
                return;
            }


            // Update the selected person and refresh the ListView
            selectedPerson.Name = name;
            selectedPerson.Age = age;
            lvPeople.Items.Refresh();
            lvPeople.SelectedIndex = -1;

            // Clear textboxes after updating
            TbxName.Text = "";
            TbxAge.Text = "";

            // Update status bar
            UpdateStatusBar("Person updated.");
        }

        private void BtnDeletePeople_Click(object sender, RoutedEventArgs e)
        {
            // Delete the selected person and refresh the ListView
            Person selectedPerson = (Person)lvPeople.SelectedItem;
            peoplelist.Remove(selectedPerson);
            lvPeople.Items.Refresh();
            // Clear textboxes after deleting
            TbxName.Text = "";
            TbxAge.Text = "";
            UpdateStatusBar("Person deleted.");
        }

        private void lvPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvPeople.SelectedItem != null)
            {
                Person selectedPerson =lvPeople.SelectedItem as Person;
                TbxName.Text = selectedPerson.Name;
                TbxAge.Text = selectedPerson.Age.ToString();
            }
        }

        private void LoadDataFromFile() //call when window is loaded
        {
            try
            {
                if (!File.Exists(DataFileName)) return;
                List<String> errorList = new List<string>();
                string[] lines = File.ReadAllLines(DataFileName);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] data = line.Split(';');
                    if (data.Length != 2) {
                        errorList.Add("each line must have exactly 2 fields" + "\n" + line);
                        continue;
                    }
                    string name = data[0];
                    string agestr = data[1];
                    int.TryParse(agestr, out int age);
                    string ageError = "";
                    if (!Person.IsNameValid(name, out string nameError) || !Person.IsAgeValid(age, out ageError))
                    {
                        errorList.Add(nameError ?? ageError ?? "");

                        continue;
                    }
                    peoplelist.Add(new Person(name, age));
                }
                lvPeople.Items.Refresh();
                if (errorList.Count != 0)
                {
                    string allErrors = string.Join("\n", errorList);
                    MessageBox.Show(this, "warning:somelines were ignored due to format errors" + allErrors, "warning", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch(SystemException ex)
            {
                MessageBox.Show(this, "error reading files"+ex.Message,"file access error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        
            /* OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"F:\Project\netcore\listgridview\people.txt";
            openFileDialog.Filter = "Data files (*.data)|*.data|Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int age))
                    {
                        peoplelist.Add(new Person(parts[0], age));
                    }
                }
                lvPeople.Items.Refresh();
            }
           */
        }
        private void SaveDataToFile()//call when window is closing
        {
            List<string> lines = new List<string>();

 foreach (Person person in peoplelist)
            {
                lines.Add($"{person.Name};{person.Age}");
            }
           try
            {
                File.WriteAllLines(DataFileName, lines);
            }catch(SystemException ex) {
                MessageBox.Show(this, "error wirting to file\n" + ex.Message, "file save error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MiOpen_Click(object sender, RoutedEventArgs e)
        {
            LoadDataFromFile();
            UpdateStatusBar("File opened.");

        }

        private void MiSave_Click(object sender, RoutedEventArgs e)
        {
            SaveDataToFile();
            UpdateStatusBar("File saved.");
        }

        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void UpdateStatusBar(string message)
        {
            lblStatus.Text = message;
        }

        private void MiName_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvPeople.ItemsSource).SortDescriptions.Clear();

            // Apply new sorting by name
            CollectionViewSource.GetDefaultView(lvPeople.ItemsSource).SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            UpdateStatusBar("List sorted by Name.");
        }

        private void MiAge_Click(object sender, RoutedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvPeople.ItemsSource).SortDescriptions.Clear();

            // Apply new sorting by age
            CollectionViewSource.GetDefaultView(lvPeople.ItemsSource).SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));

            UpdateStatusBar("List sorted by Age.");
        }

      


        //implement sorting
        //after every operation (app stated, item added/deleted/updated,)upadate status bar with short message
    }
}

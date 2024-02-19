using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace toDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Globals.dbContext = new TodoDbContext();
                LvToDo.ItemsSource = Globals.dbContext.Todos.ToList();
            }catch (SystemException ex) {
            MessageBox.Show(ex.Message);
                Environment.Exit(1);
            }
        }

        private void LvToDo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Todo currSelectedToDo = LvToDo.SelectedItem as Todo;
            if (currSelectedToDo == null)
            {
                ResetField();
            }
            else
            {
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
                TaskIuput.Text = currSelectedToDo.Task;
                SlDifficulty.Value = currSelectedToDo.Difficulty;
                DueDate.SelectedDate = currSelectedToDo.DueDate;
                CbxStatus.SelectedIndex = (int)currSelectedToDo.Status;
            }
        }

        private void ResetField()
        {
            TaskIuput.Text = "";
            SlDifficulty.Value = 1;
            DueDate.SelectedDate = DateTime.Today;
            CbxStatus.SelectedIndex = 0;
            btnDelete.IsEnabled = false;
            btnUpdate .IsEnabled = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DueDate.SelectedDate == null)
                {
                    throw new ArgumentException("please select a due date");
                }
                Todo newTodo = new Todo(TaskIuput.Text, (int)SlDifficulty.Value, (DateTime)DueDate.SelectedDate, (Todo.StatusEnum)CbxStatus.SelectedIndex);
                Globals.dbContext.Todos.Add(newTodo);
                Globals.dbContext.SaveChanges();
                LvToDo.ItemsSource = Globals.dbContext.Todos.ToList();
                ResetField();
            }catch(ArgumentException ex) { 
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Todo currSelectedToDo= LvToDo.SelectedItem as Todo;
            if (currSelectedToDo == null) return;
            var result = MessageBox.Show(this, "are you sure to delete this item", "confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Question
                );
            if(result==MessageBoxResult.No) return;
            try
            {
                Globals.dbContext.Todos.Remove(currSelectedToDo);
                Globals.dbContext.SaveChanges();
                LvToDo.ItemsSource = Globals.dbContext.Todos.ToList();
                ResetField();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            Todo currSelectedToDo = LvToDo.SelectedItem as Todo;
            if (currSelectedToDo == null) return;
            try
            {
                currSelectedToDo.Task = TaskIuput.Text;
                currSelectedToDo.Difficulty = (int)SlDifficulty.Value;
                currSelectedToDo.DueDate = (DateTime)DueDate.SelectedDate;
                currSelectedToDo.Status = (Todo.StatusEnum)CbxStatus.SelectedIndex;
                Globals.dbContext.SaveChanges();
                LvToDo.ItemsSource = Globals.dbContext.Todos.ToList();
                ResetField();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this,"input error" + ex.Message,"input error",MessageBoxButton.OK,MessageBoxImage.Error);
            }catch(SystemException ex)
            {
MessageBox.Show(this,"database access error" + ex.Message,"database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File (*.txt)|*.txt|All files (*.*)|*.*";
            if(saveFileDialog.ShowDialog()!=true) { return; }
            List<Todo> list = Globals.dbContext.Todos.ToList();
            List<string> lines=new List<string>();
            foreach(Todo todo in list)
            {
                lines.Add($"{todo.Task};{todo.Difficulty};{todo.DueDate};{todo.Status}");
            }
            try
            {
                File.WriteAllLines(saveFileDialog.FileName, lines);
                MessageBox.Show(this, "export complete", "export status", MessageBoxButton.OK, MessageBoxImage.Information);
            }catch(Exception ex) when (ex is IOException || ex is SystemException)
            {
 MessageBox.Show(this, "export failed", "export status", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

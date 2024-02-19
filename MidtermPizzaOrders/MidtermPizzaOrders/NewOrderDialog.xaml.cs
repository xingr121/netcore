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
using System.Windows.Shapes;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace MidtermPizzaOrders
{
    /// <summary>
    /// Interaction logic for NewOrderDialog.xaml
    /// </summary>
    public partial class NewOrderDialog : Window
    {
        public NewOrderDialog()
        {
            InitializeComponent();
            TimeInput.Text = DateTime.Now.AddHours(1).ToString("HH:mm");
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime datePickerDate = DueDate.SelectedDate ?? DateTime.MinValue; 
                string timeInputText = TimeInput.Text;

                DateTime deliveryDeadline;
                if (DateTime.TryParse(datePickerDate.ToShortDateString() + " " + timeInputText, out deliveryDeadline))
                {
                    
                    if (deliveryDeadline < DateTime.Now.AddMinutes(45))
                    {
                         throw new ArgumentException("Please select a delivery deadline that is at least 45 minutes past the current date/time.");
                    }
                   
                }
                else
                {
                    throw new ArgumentException("Invalid delivery deadline format. Please provide a valid date and time.");
                }
                string extras="";
                if (Cheese.IsChecked == true)
                {
                    extras += "ExtraCheese;";
                }
                if (Deep.IsChecked == true)
                {
                    extras += "DeepDish;";
                }
                if (Wheat.IsChecked == true)
                {
                    extras += "WholeWheat;";
                }
                extras = extras.TrimEnd(';');
                PizzaOrder.SizeEnum selectedSize = (PizzaOrder.SizeEnum)((ComboBoxItem)Size.SelectedItem).Tag;
                PizzaOrder newOrder = new PizzaOrder(NameInput.Text, PostInput.Text, deliveryDeadline, selectedSize, (int)SlBakingTime.Value, extras, PizzaOrder.OrderStatusEnum.Placed);
                Globals.dbContextAuto.PizzaOrders.Add(newOrder);
                Globals.dbContextAuto.SaveChanges();
                this.DialogResult=true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, "datainput error" + ex.Message, "input error", MessageBoxButton.OK, MessageBoxImage.Error);
            }catch(SystemException ex)
            {
                MessageBox.Show(this, "database reading error" + ex.Message, "database error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
     
    }
}

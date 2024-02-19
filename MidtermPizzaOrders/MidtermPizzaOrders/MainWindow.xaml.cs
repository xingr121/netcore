using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using static MidtermPizzaOrders.PizzaOrder;

namespace MidtermPizzaOrders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closing += MainWindow_Closing;

            try
            {
               // Globals.dbContextAuto = new PizzaOrderContext();
                LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();
                LblStatusMessage.Text = "Data loaded";
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(1);
            }
        }

        private void MiOrder_Click(object sender, RoutedEventArgs e)
        {
            NewOrderDialog dialog= new NewOrderDialog();
            dialog.Owner = this;
            if(dialog.ShowDialog() == true)
            {
                LvOrders.ItemsSource= Globals.dbContextAuto.PizzaOrders.ToList();
                LblStatusMessage.Text = "order placed";
            }
        }

        private void MiExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (var writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                    using (var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(Globals.dbContextAuto.PizzaOrders.ToList());
                    }
                    LblStatusMessage.Text = "Items exported";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting to CSV: {ex.Message}");
                }
            }
        }
        private void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string status = menuItem.Header.ToString();
            if (status == "Placed" || status == "Fulfilled")
            {
                var selectedOrders = LvOrders.SelectedItems.Cast<PizzaOrder>();
                foreach (var order in selectedOrders)
                {
                    // Update the status of selected orders to Placed or Fulfilled
                    order.OrderStatus = (status == "Placed") ? OrderStatusEnum.Placed : OrderStatusEnum.Fulfilled;

                    // Find the corresponding order in the database
                    var dbOrder = Globals.dbContextAuto.PizzaOrders.Find(order.Id);
                    if (dbOrder != null)
                    {
                        // Update the status in the database
                        dbOrder.OrderStatus = order.OrderStatus;
                    }
                    else
                    {
                        MessageBox.Show($"Order with ID {order.Id} not found in the database.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                // Save changes to the database
                Globals.dbContextAuto.SaveChanges();
                LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();
                LblStatusMessage.Text = $"Orders marked {status}";
            }
        }
        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
               Close();
            }
        }
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void CmPlace_Click(object sender, RoutedEventArgs e)
        {
            ChangeOrderStatus(OrderStatusEnum.Placed);
        }

        private void CmFulfill_Click(object sender, RoutedEventArgs e)
        {
            ChangeOrderStatus(OrderStatusEnum.Fulfilled);
        }

        private void ChangeOrderStatus(OrderStatusEnum newStatus)
        {
            var selectedOrders = LvOrders.SelectedItems.Cast<PizzaOrder>();
            foreach (var order in selectedOrders)
            {
                order.OrderStatus = newStatus;
            }

            // Save changes to the database
            try
            {
                Globals.dbContextAuto.SaveChanges();
                LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.ToList();
                LblStatusMessage.Text = $"Orders marked {newStatus}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Place_Click(object sender, RoutedEventArgs e)
        {
            FilterOrdersByStatus(PizzaOrder.OrderStatusEnum.Placed);
            LblStatusMessage.Text = "Filtered orders by Placed status.";
        }

        private void Fulfill_Click(object sender, RoutedEventArgs e)
        {
 FilterOrdersByStatus(PizzaOrder.OrderStatusEnum.Fulfilled);
            LblStatusMessage.Text = "Filtered orders by Placed status.";
        }
        private void FilterOrdersByStatus(PizzaOrder.OrderStatusEnum status)
        {
            LvOrders.ItemsSource = Globals.dbContextAuto.PizzaOrders.Where(order => order.OrderStatus == status).ToList();
        }
    }
}

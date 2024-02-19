using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MidtermPizzaOrders
{
    public class PizzaOrder
    {
        public int Id { get; set; }
        private string _clientName;
        public string ClientName
        {
            get
            {
                return _clientName;
            }
            set
            {
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentException("task description length must be 1-100 characters long");
                }
                _clientName = value;
            }
        }
        private string _clientPostalCode;

        public string ClientPostalCode
        {
            get
            {
                return _clientPostalCode;
            }
            set
            {
                if (IsValidPostalCode(value))
                {
                    _clientPostalCode = value;
                }
                else
                {
                    throw new ArgumentException("Invalid postal code format. Please enter a valid postal code in the format A1A 1A1.");
                }
            }
        }
        private bool IsValidPostalCode(string postalCode)
        {
            // Regular expression pattern for Canadian postal codes (A1A 1A1 format)
            string postalCodePattern = @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$";

            // Check if the provided postal code matches the pattern
            return Regex.IsMatch(postalCode, postalCodePattern);
        }
        public DateTime DeliveryDeadline { get; set; }

         // UI must verify it's at least 45 minutes past current date/time, otherwise order can't be placed

    public enum SizeEnum { Small = 3, Medium = 7, Large = 12 };
        public SizeEnum Size { get; set; }  // use ComboBox with Tags to match integer values of the enum

        private int _bakingTimeMinutes;// 12-18, slider with label displaying current value,
        public int BakingTimeMinutes
        {
            get
            {
                return _bakingTimeMinutes;
            }
            set
            {
                if (value < 12 || value > 18)
                {
                    throw new ArgumentException("bakingtime must fall in 12-18 range");
                }
                _bakingTimeMinutes = value;
            }
        }

       public string Extras { get; set; }

        public enum OrderStatusEnum
        {
            Placed,
            Fulfilled
        }
        public OrderStatusEnum OrderStatus { get; set; } = OrderStatusEnum.Placed;

        public PizzaOrder(string clientName, string clientPostalCode, DateTime deliveryDeadline, SizeEnum size, int bakingTimeMinutes, string extras, OrderStatusEnum orderStatus)
        {
            ClientName = clientName;
            ClientPostalCode = clientPostalCode;
            DeliveryDeadline = deliveryDeadline;
            Size = size;
            BakingTimeMinutes = bakingTimeMinutes;
            Extras = extras;
            OrderStatus = orderStatus;
        }

        public PizzaOrder()
        {
        }
    }
    
    
}

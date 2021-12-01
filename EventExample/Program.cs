using System;
using System.Threading;
using System.Timers;
using Microsoft.VisualBasic;
using Timer = System.Timers.Timer;

namespace EventExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            Waiter waiter = new Waiter();
            // Subscribe
            //customer.Order += waiter.Serve;
            customer.OnOder();
            customer.PayBill();

            Customer customer2 = new Customer();
            OrderEventArgs e = new OrderEventArgs()
            {
                DishName = "Chicken",
                DishSize = 1
            };
            customer2.orderEventHandler.Invoke(customer, e);
        }
    }

    public class OrderEventArgs : EventArgs
    {
        public string DishName { get; set; }
        public int DishSize { get; set; }
    }

    // 声明委托，用于 Event 和 Event Handler 之间的约定
    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);

    // Event Source
    public class Customer
    {
        // Event field-like declare:
        // public event OrderEventHandler Order;
        public OrderEventHandler orderEventHandler;

        // Event
        /*public event OrderEventHandler Order
        {
            add
            {
                this.orderEventHandler += value;
            }
            remove
            {
                this.orderEventHandler -= value;
            }
        }*/

        public double Bill { get; set; }

        public void PayBill()
        {
            Console.WriteLine("I will eat!");
            Thread.Sleep(3000);
            Console.WriteLine("I will pay ${0} bills.", Bill);
        }

        // 事件源触发事件
        public void OnOder()
        {
            Console.ReadLine();
            Console.WriteLine("Walk in restaurant...");
            Console.WriteLine("Sit down seat...");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Let me think {0}s...", i);
                Thread.Sleep(1000);
            }
            // should be 'this.Order' here when field-like declare event
            if (this.orderEventHandler != null)
            {
                OrderEventArgs e = new OrderEventArgs()
                {
                    DishName = "Chicken",
                    DishSize = 1
                };
                // Event Source 触发 Event
                this.orderEventHandler.Invoke(this, e);
            }
        }
    }

    // Event Subscriber
    public class Waiter
    {
        // Event Handler
        internal void Serve(Customer customer, OrderEventArgs e)
        {
            Console.WriteLine("I will serve the dish {0}", e.DishName);
            double price = 10;
            switch (e.DishSize)
            {
                case -1:
                    customer.Bill = price * 0.5;
                    break;
                case 1:
                    customer.Bill = price * 1.5;
                    break;
                default:
                    customer.Bill = price;
                    break;
            }
        }
    }
}

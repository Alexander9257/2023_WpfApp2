using System.Windows;
using System.Collections.Generic;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, int> drinks = new Dictionary<string, int>();
        Dictionary<string , int> order = new Dictionary<string , int>();
        public MainWindow()
        {
            
            InitializeComponent();
            
            //新增飲料品項
            AddNewDrink(drinks);

            //顯示所有飲料品項
            DisplayDrinkMenu(drinks);
        }

        private void DisplayDrinkMenu(Dictionary<string, int> myDrinks)
        {
            foreach (var drink in myDrinks)
            {
             StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                Slider sl = new Slider();
                Label lb = new Label();

                cb.Content =$"{drink.Key} ： {drink.Value}元";
                cb.FontFamily = new FontFamily("Consolas");
                cb.FontSize = 18;
                cb.Foreground = Brushes.Blue;
                cb.Width = 200;
                //cb.Height = 60;
                cb.Margin = new Thickness(5);

                sl.Width = 100;
                //sl.Height = 60;
                sl.Value = 0;
                sl.Minimum = 0;
                sl.Maximum = 10;

                lb.Width = 50;
                //lb.Height = 60;
                lb.Content = "0";
                lb.FontFamily = new FontFamily("Consolas");
                lb.FontSize = 18;

                sp.Orientation = Orientation.Horizontal;
                //sp.Height = 60;
                sp.Children.Add(cb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);

                stackpanel_DrinkMenu.Children.Add(sp);
                //stackpanel_DrinkMenu.Height = myDrinks.Count * 60;
                GroupBox_DrinkMenu.Height = stackpanel_DrinkMenu.Height;

            }

        }

        private void AddNewDrink(Dictionary<string, int> myDrinks)
        {
            myDrinks.Add("紅茶大杯", 60);
            myDrinks.Add("紅茶小杯", 40);
            myDrinks.Add("綠茶大杯", 60);
            myDrinks.Add("綠茶小杯", 40);
            myDrinks.Add("咖啡大杯", 80);
            myDrinks.Add("咖啡小杯", 40);
            myDrinks.Add("可樂大杯", 30);
            myDrinks.Add("可樂小杯", 20);

        }

        private void PlaceOrder(object sender, TextChangedEventArgs e)
        {
            var targetTextBox = sender as TextBox;

            bool success = int.TryParse(targetTextBox.Text, out int amount);

            if (!success) MessageBox.Show("請輸入整數", "輸入錯誤");
            else if (amount <= 0) MessageBox.Show("請輸入正整數", "輸入錯誤");
            else
            {
                var targetstackPanel = targetTextBox.Parent as StackPanel;
                var targetLabel = targetstackPanel.Children[0] as Label;
                string drinkName = targetLabel.Content.ToString();

                if (order.ContainsKey(drinkName)) order.Remove(drinkName);
                order.Add(drinkName, amount);
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            double total = 0.0;
            double sellPrice = 0.0;
            string discountMessage = " ";
            string displayMessage = "訂購清單如下：\n";
            
            foreach (var item in order) 
            {
                string drinkName = item.Key;
                int quantity = order[drinkName];
                int price = drinks[drinkName];

                total+= quantity * price;
                displayMessage += $"{drinkName} X {quantity}杯，每杯{price}元，總共{price * quantity}元\n";
            }

            if (total > 500)
            {
                discountMessage = "訂購滿500元以上者打8折";
                sellPrice = total * 0.8;
            }
            else if (total > 300)
            {
                discountMessage = "訂購滿300元以上者打85折";
                sellPrice = total * 0.85;
            }
            else if (total > 200)
            {
                discountMessage = "訂購滿200元以上者打9折";
                sellPrice = total * 0.9;
            }
            else
            {
                discountMessage = "訂購未滿200元以上者不打折";
                sellPrice = total;
            }

            displayMessage += $"本次訂購總共{order.Count}項，總共{total}元，{discountMessage}，售價{sellPrice}元。\n";
            TextBlock1.Text = displayMessage;
        }
    }
}

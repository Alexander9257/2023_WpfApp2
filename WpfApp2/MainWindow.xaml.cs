using System.Windows;
using System.Collections.Generic;
using System;
using System.Windows.Controls;

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
            AddNewDrink(drinks);
        }

        private void AddNewDrink(Dictionary<string, int> myDrinks)
        {
            myDrinks.Add("紅茶大杯", 60);
            myDrinks.Add("紅茶小杯", 40);
            myDrinks.Add("綠茶大杯", 60);
            myDrinks.Add("綠茶小杯", 40);
            myDrinks.Add("咖啡大杯", 80);
            myDrinks.Add("咖啡小杯", 40);
        }

        private void PlaceOrder(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var targetTextBox = sender as TextBox;

            bool success = int.TryParse(targetTextBox.Text, out int amount);

            if (!success) MessageBox.Show("請輸入整數", "輸入錯誤");
            else if (amount > 0) MessageBox.Show("請輸入正整數", "輸入錯誤");
            else
            {
                StackPanel targetstackPanel = targetTextBox.Parent as StackPanel;
               
                Label targetLabel = targetstackPanel.Children[0] as Label;
                string drinkName = targetLabel.Content.ToString();

                if (order.ContainsKey(drinkName)) order.Remove(drinkName);
                order.Add(drinkName, amount);
            }
        }
    }
}

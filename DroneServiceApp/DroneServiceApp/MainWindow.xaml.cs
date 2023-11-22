// Author: Damien Eddington
// Title: Drone Service Application
// Date: 1/11/2023
/* Description: Program that allows uses to add Drone repair orders to a regular or express queue that is displayed in a list view.
   Items data can be displayed and removed items are added to a finished list. Double clicked finished items are removed entirely. 
   The cost of the express queue is increased by 15%*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DroneServiceApp
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
        // 6.2 Create a global List<T> of type Drone called “FinishedList”.  
        List<Drone> FinishList = new List<Drone>();
        // 6.4 Create a global Queue<T> of type Drone called “ExpressService”. 
        Queue<Drone> ExpressService = new Queue<Drone>();
        // 6.3 Create a global Queue<T> of type Drone called “RegularService”. 
        Queue<Drone> RegularService = new Queue<Drone>();

        /* 6.7 Create a custom method called “GetServicePriority” which returns the value of the priority radio group.
           This method must be called inside the “AddNewItem” method before the new service item is added to a queue. */
        public string getServicePriority()
        {
            if (rbtnExpress.IsChecked == true)
            {
                return "Express";
            }
            else if (rbtnRegular.IsChecked == true)
            {
                return "Regular";
            }
            else
            {
                return "None";
            }
        }
        // Add button calls 6.5 method.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (/*string.IsNullOrEmpty(txtClientInput.Text) || */
                string.IsNullOrEmpty(txtModelInput.Text) ||
                string.IsNullOrEmpty(txtProblemInput.Text) ||
                string.IsNullOrEmpty(txtCostInput.Text) ||
                getServicePriority() == "None" ||
                txtCostInput.Text == "")
            {
                stsBar.Text = "Please populate all data fields.";
            }
            else {
                if (rbtnExpress.IsChecked == true)
                {
                    AddNewItem(ExpressService, double.Parse(txtCostInput.Text));
                }
                else if (rbtnRegular.IsChecked == true)
                {
                    AddNewItem(RegularService, double.Parse(txtCostInput.Text));
                }
                ClearInputs();
                stsBar.Text = "Data added";
            }
        }
        /* 6.5 Create a button method called “AddNewItem” that will add a new service item to a Queue<> based on the priority.
           Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost. Use a numeric control for the Service Tag.
           The new service item will be added to the appropriate Queue based on the Priority radio button. */
        private void AddNewItem(Queue<Drone>Queue, double cost)
        {
            Drone addItem = new Drone();
            addItem.SetName(txtClientInput.Text);
            addItem.SetModel(txtModelInput.Text);
            addItem.SetProblem(txtProblemInput.Text);
            addItem.SetTag(int.Parse(uidTag.Text));
            if (rbtnExpress.IsChecked == true)
            {
                // 6.6 Before a new service item is added to the Express Queue the service cost must be increased by 15%. 
                cost = cost * 1.15; cost = Math.Round(cost, 2);
                addItem.SetCost(cost);
            }
            else if (rbtnRegular.IsChecked == true)
            {
                addItem.SetCost(cost);
            }
            IncreaseTag();
            Queue.Enqueue(addItem);
            ExpressDisplay();
            RegularDisplay();
            
        }
        // 6.17 Create a custom method that will clear all the textboxes after each service item has been added. 
        private void ClearInputs()
        {
            txtClientInput.Text = "Client Name";
            txtModelInput.Text = "Drone Model";
            txtProblemInput.Text = "Service Problem";
            txtCostInput.Text = "Service Cost";
            rbtnExpress.IsChecked = false;
            rbtnRegular.IsChecked = false;
        }
        // 6.9 Create a custom method that will display all the elements in the ExpressService queue. The display must use a List View and with appropriate column headers. 
        public void ExpressDisplay()
        {
            lstvExpress.Items.Clear();
            foreach (Drone drone in ExpressService)
            {
                lstvExpress.Items.Add(new
                {
                    expressN = drone.GetName(),
                    expressM = drone.GetModel(),
                    expressP = drone.GetProblem(),
                    expressC = "$" + drone.GetCost(),
                    expressT = drone.GetTag(),
                });
            }
        }
        // 6.8 Create a custom method that will display all the elements in the RegularService queue. The display must use a List View and with appropriate column headers. 
        public void RegularDisplay()
        {
            lstvRegular.Items.Clear();
            foreach (Drone drone in RegularService)
            {
                lstvRegular.Items.Add(new
                {
                    regularN = drone.GetName(),
                    regularM = drone.GetModel(),
                    regularP = drone.GetProblem(),
                    regularC = "$" + drone.GetCost(),
                    regularT = drone.GetTag(),
                });
            }
        }
        /* 6.15 Create a button click method that will remove a service item from the express ListView and dequeue the express service Queue<T> data structure.
           The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items. */
        private void btnExpressRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ExpressService.Count > 0)
            {
                Drone removeItem = ExpressService.Dequeue();
                FinishList.Add(removeItem);
                lstFinishedItems.Items.Add(removeItem.DisplayFinishedOrders());
                ExpressDisplay();
                stsBar.Text = "Item moved to finish list.";
            }
            else
            {
                stsBar.Text = "Nothing to dequeue";
            }
        }
        /* 6.14 Create a button click method that will remove a service item from the regular ListView and dequeue the regular service Queue<T> data structure.
           The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items. */
        private void btnRegularRemove_Click(object sender, RoutedEventArgs e)
        {
            if (RegularService.Count > 0)
            {
                Drone removeItem = RegularService.Dequeue();
                FinishList.Add(removeItem);
                lstFinishedItems.Items.Add(removeItem.DisplayFinishedOrders());
                RegularDisplay();
                stsBar.Text = "Item moved to finish list.";
            }
            else
            {
                stsBar.Text = "Nothing to dequeue";
            }
        }
        // 6.11 Create a custom method to increment the service tag control, this method must be called inside the “AddNewItem” method before the new service item is added to a queue. 
        private void IncreaseTag()
        {
            int increase = int.Parse(uidTag.Text) + 10;
            if (increase <= 900) 
            {
                uidTag.Text = increase.ToString();
            }
        }
        // 6.16 Create a double mouse click method that will delete a service item from the finished listbox and remove the same item from the List<T>. 
        private void lstFinishedItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int item = lstFinishedItems.SelectedIndex;
            if (item >= 0)
            {
                lstFinishedItems.Items.RemoveAt(item);
                FinishList.RemoveAt(item);
                stsBar.Text = "Item removed from Finished Items List.";
            }
        }
        // 6.13 Create a mouse click method for the express service ListView that will display the Client Name and Service Problem in the related textboxes. 
        private void lstvExpress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            int index = lstvExpress.SelectedIndex;
            lstvRegular.SelectedItem = null;
            SelectDisplay(ExpressService, index);
        }
        // 6.12 Create a mouse click method for the regular service ListView that will display the Client Name and Service Problem in the related textboxes. 
        private void lstvRegular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lstvRegular.SelectedIndex;
            lstvExpress.SelectedItem = null;
            SelectDisplay(RegularService, index);
        }
        // 6.12, 6.13 Code called by other methods. Written here to avoid writting it twice.
        private void SelectDisplay(Queue<Drone>Queue, int index)
        {
            if(index >= 0)
            {
                List<Drone> list = Queue.ToArray().ToList();
                txtClientInput.Text = list[index].GetName();
                txtModelInput.Text = list[index].GetModel();
                txtProblemInput.Text = list[index].GetProblem();
                txtCostInput.Text = list[index].GetCost().ToString();
                uidTag.Text = list[index].GetTag().ToString();
                stsBar.Text = "Item selected";
            }
            else
            {
                stsBar.Text = "Listview empty";
            }
        }
        // 6.10 Create a custom method to ensure the Service Cost textbox can only accept a double value with two decimal point. 
        private void txtCostInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rx = new Regex(@"^\d*\.?\d{0,2}$");
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = !rx.IsMatch(text);
        }
        // Clears Model textbox input.
        private void txtModelInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtModelInput.Clear();
        }
        // Clears Name textbox input.
        private void txtClientInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtClientInput.Clear();
        }
        // Clears Problem textbox input.
        private void txtProblemInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtProblemInput.Clear();
        }
        // Clears Cost textbox input.
        private void txtCostInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCostInput.Clear();
        }
    }
}

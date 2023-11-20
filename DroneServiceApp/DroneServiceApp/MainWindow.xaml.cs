// Author: Damien Eddington
// Title: Drone Service Application
// Date: 1/11/2023
// Description: 
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        List<Drone> FinishList = new List<Drone>();
        Queue<Drone> ExpressService = new Queue<Drone>();
        Queue<Drone> RegularService = new Queue<Drone>();

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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtClientInput.Text) ||
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
        private void AddNewItem(Queue<Drone>Queue, double cost)
        {
            Drone addItem = new Drone();
            addItem.SetName(txtClientInput.Text);
            addItem.SetModel(txtModelInput.Text);
            addItem.SetProblem(txtProblemInput.Text);
            addItem.SetTag(int.Parse(uidTag.Text));
            if (rbtnExpress.IsChecked == true)
            {
                cost = cost * 1.15; cost = Math.Round(cost, 2);
                addItem.SetCost(cost);
            }
            IncreaseTag();
            Queue.Enqueue(addItem);
            ExpressDisplay();
            RegularDisplay();
            
        }
        private void ClearInputs()
        {
            txtClientInput.Text = "Client Name";
            txtModelInput.Text = "Drone Model";
            txtProblemInput.Text = "Service Problem";
            txtCostInput.Text = "Service Cost";
            rbtnExpress.IsChecked = false;
            rbtnRegular.IsChecked = false;
        }

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
                    rexpressT = drone.GetTag(),
                });
            }
        }
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

        private void btnExpressRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ExpressService.Count > 0)
            {
                Drone removeItem = ExpressService.Dequeue();
                FinishList.Add(removeItem);
                lstFinishedItems.Items.Add(removeItem);
                ExpressDisplay();
                stsBar.Text = "Item moved to finish list.";
            }
        }

        private void btnRegularRemove_Click(object sender, RoutedEventArgs e)
        {
            if (RegularService.Count > 0)
            {
                Drone removeItem = RegularService.Dequeue();
                FinishList.Add(removeItem);
                lstFinishedItems.Items.Add(removeItem);
                RegularDisplay();
                stsBar.Text = "Item moved to finish list.";
            }
        }
        private void IncreaseTag()
        {
            int increase = int.Parse(uidTag.Text) + 10;
            if (increase <= 900) 
            {
                uidTag.Text = increase.ToString();
            }
        }

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

        private void lstvExpress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstvExpress.SelectedItems.Count > 0)
            {
                lstvRegular.SelectedItem = null;
                foreach (var item in ExpressService)
                {
                    // Fix?
                    if (lstvExpress.SelectedItem == item)
                    {
                        txtClientInput.Text = item.GetName();
                        txtModelInput.Text = item.GetModel();
                        txtProblemInput.Text = item.GetProblem();
                        txtCostInput.Text = item.GetCost().ToString();
                        rbtnExpress.IsChecked = true;
                        uidTag.Text = item.GetTag().ToString();
                    }
                }
            }
        }

        private void lstvRegular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstvRegular.SelectedItems.Count > 0)
            {
                lstvExpress.SelectedItem = null;
                foreach (var item in ExpressService)
                {
                    // Fix?
                    if (lstvRegular.SelectedItem == item)
                    {
                        txtClientInput.Text = item.GetName();
                        txtModelInput.Text = item.GetModel();
                        txtProblemInput.Text = item.GetProblem();
                        txtCostInput.Text = item.GetCost().ToString();
                        rbtnRegular.IsChecked = true;
                        uidTag.Text = item.GetTag().ToString();
                    }
                }
            }
        }

        private void txtCostInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex rx = new Regex(@"^\d*\.?\d{0,2}$");
            var text = ((TextBox)sender).Text + e.Text;
            e.Handled = !rx.IsMatch(text);
        }

        private void txtModelInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtModelInput.Clear();
        }

        private void txtClientInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtClientInput.Clear();
        }

        private void txtProblemInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtProblemInput.Clear();
        }

        private void txtCostInput_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCostInput.Clear();
        }



        // On focus clear textboxes.
        // One method per textbox.
    }
}

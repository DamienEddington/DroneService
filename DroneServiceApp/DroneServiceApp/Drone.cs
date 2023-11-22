using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServiceApp
{
    /* 6.1 Create a separate class file to hold the data items of the Drone.
       Use separate getter and setter methods, ensure the attributes are private and the accessor methods are public.
       Add a display method that returns a string for Client Name and Service Cost.
       Add suitable code to the Client Name and Service Problem accessor methods so the data is formatted as Title case or Sentence case. Save the class as “Drone.cs”. */
    internal class Drone
    {
        private string name;
        private string model;
        private string problem;
        private double cost;
        private int tag;

        public Drone() { }

        public string GetName()
        {
            return name;
        }
        public void SetName(string addName)
        {
            if (addName == "")
            {
                name = "Unknown";
            }
            else
            {
                name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(addName);
            }
        }
        public string GetModel()
        {
            return model;
        }
        public void SetModel(string addModel)
        {
            model = addModel;
        }
        public string GetProblem()
        {
            return problem;
        }
        public void SetProblem(string addProblem)
        {
            problem = addProblem;
        }
        public double GetCost()
        {
            return cost;
        }
        public void SetCost(double addCost)
        {
            if (addCost == 0)
            {
                cost = 50;
            }
            else
            {
                cost = addCost;
            }
        }
        public int GetTag()
        {
            return tag;
        }
        public void SetTag(int addTag)
        {
            tag = addTag;
        }
        public string DisplayFinishedOrders()
        {
            return "Name: " + GetName() + ", Problem: " + GetProblem() + ", Cost: " + GetCost().ToString();
        }
    }
}

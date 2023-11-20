using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneServiceApp
{
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
            if (addName == null)
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
            return "Name: " + GetName() + ", Problem " + GetProblem() + ", Cost: " + GetCost().ToString();
        }
    }
}

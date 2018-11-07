using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch13.Shared
{
    public class MenuItem
    {
        public MenuItem(string name, string type, decimal price, string description, int calories)
        {
            Name = name;
            Type = type;
            Price = price;
            Description = description;
            Calories = calories;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
    }
}

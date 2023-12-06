using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    class PizzaInCart
    {
        public PizzaInCart(string namePizza, string soursePizzaIco, string[] nameDops, bool[] addIngrid, int size, int resultPrice)
        {
            this.namePizza = namePizza;
            this.soursePizzaIco = soursePizzaIco;
            this.nameDops = nameDops;
            this.addIngrid = addIngrid;
            this.size = size;
            this.resultPrice = resultPrice;
        }

        public Grid thiSPizza;

        public string namePizza;
        public string soursePizzaIco;
        public int resultPrice;
        public int size;
        public string[] nameDops = new string[4];
        public bool[] addIngrid = new bool[4];
    }
}

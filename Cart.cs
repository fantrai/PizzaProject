using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    static class Cart
    {
        const string BASEFONT = "Caveat-Regular";

        public static int AllPrice = 0;

        public static List<PizzaInCart> pizzasInCart = new List<PizzaInCart>();

        public static void AddCart(PizzaCart pizzaPrefab)
        {
            PizzaInCart pizza = new PizzaInCart(pizzaPrefab.namePizza, pizzaPrefab.soursePizzaIco, pizzaPrefab.nameDops, pizzaPrefab.addIngrid, pizzaPrefab.size, pizzaPrefab.resultPrice);
            pizzasInCart.Add(pizza);
            SendPrise();
            if (CartPage.cart!=null) CartPage.cart.Update();
        }

        public static void SendPrise()
        {
            int price = 0;
            foreach (var p in pizzasInCart)
            {
                price += p.resultPrice;
            }
             AllPrice = price;
        }

        public static void PrintPizzas(VerticalStackLayout pizzasConteiner)
        {
            pizzasConteiner.Clear();
            foreach (var p in pizzasInCart)
            {
                Grid container = new Grid();
                p.thiSPizza = container;
                pizzasConteiner.Add(container);

                Grid pizza = new Grid
                {
                    RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(){ Height = GridLength.Auto},
                },

                    ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = new GridLength(1.5, GridUnitType.Star) },
                    new ColumnDefinition(),
                },
                };

                Button del = new Button
                {
                    Text = "Убрать",
                    BackgroundColor = Color.FromArgb("#c45454"),
                    FontFamily = BASEFONT,
                    VerticalOptions = LayoutOptions.Start,
                    TextColor = Colors.Black,
                    FontSize = 16,
                    HorizontalOptions = LayoutOptions.Start,
                };
                Adder(del, pizza, 4, 0);
                del.Clicked += DelSwipe;

                container.Add(pizza);

                BoxView box = new BoxView()
                {
                    BackgroundColor = Color.FromArgb("#517C4F"),
                };
                Adder(box, pizza, 0, 0, 4, 2);

                Label nameText = new Label
                {
                    Text = p.namePizza,
                    TextColor = Colors.White,
                    Padding = new Thickness(15, 0),
                    VerticalOptions = LayoutOptions.Center,
                    FontFamily = BASEFONT,
                    FontSize = 20,
                };
                Adder(nameText, pizza, 0, 0);

                Label textDop = new Label
                {
                    Text = "Дополниткльные ингредиенты:",
                    TextColor = Colors.White,
                    Padding = new Thickness(15, 0,0,0),
                    VerticalOptions = LayoutOptions.End,
                    FontFamily = BASEFONT,
                    FontSize = 16,
                };
                Adder(textDop, pizza, 1, 0);

                Label dops = new Label
                {
                    TextColor = Colors.White,
                    Padding = new Thickness(15, 0, 0, 0),
                    VerticalOptions = LayoutOptions.Start,
                    FontFamily = BASEFONT,
                    FontSize = 16,
                };
                Adder(dops, pizza, 2, 0);

                for (int i = 0; i < 4; i++)
                {
                    if (p.addIngrid[i])
                    {
                        dops.Text += p.nameDops[i] + " ,";
                    }
                }

                Label cons = new Label
                {
                    Text = "Размер: " + p.size,
                    TextColor = Colors.White,
                    Padding = new Thickness(15, 0, 0, 15),
                    VerticalOptions = LayoutOptions.Center,
                    FontFamily = BASEFONT,
                    FontSize = 16,
                };
                Adder(cons, pizza, 3, 0);

                Image piico = new Image
                {
                    Source = p.soursePizzaIco,
                };
                Adder(piico, pizza, 0, 1, 3);

                Label priceText = new Label
                {
                    Text = p.resultPrice.ToString() + "₽ ",
                    TextColor = Colors.White,
                    Padding = new Thickness(15, 0),
                    VerticalOptions = LayoutOptions.Center,
                    FontFamily = BASEFONT,
                    FontSize = 20,
                    HorizontalTextAlignment = TextAlignment.Center,
                };
                Adder(priceText, pizza, 3, 1);
            }
            SendPrise();
        }

        private static void DelSwipe(object? sender, EventArgs e)
        {
            foreach (var item in pizzasInCart)
            {
                if (item.thiSPizza.Equals(((Grid)((Button)sender).Parent.Parent)))
                {
                    pizzasInCart.Remove(item);
                    break;
                }
            }
            SendPrise();
            if (CartPage.cart != null) CartPage.cart.Update();
        }

        static void Adder(View @object, Grid grid, int row, int column, int rowSpan = 0, int columnSpan = 0)
        {
            grid.Add(@object, column, row);
            Grid.SetColumnSpan(@object, columnSpan);
            Grid.SetRowSpan(@object, rowSpan);
        }
    }
}

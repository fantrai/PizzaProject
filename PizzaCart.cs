using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Pizza
{
    internal class PizzaCart
    {
        public static bool DontOpenWindows = false;
        Grid? morePan = null;

        const string BASEFONT = "Caveat-Regular";

        Label priceText;
        int count = 0;
        Label countInCart;
        public int resultPrice = 0;
        public int size = 0;
        int dopPrice = 0;
        int sizePrice = 0;

        public string namePizza;
        public string soursePizzaIco;
        string previewIngridients;
        string fullIngridients;
        string KBGU;
        Button currentSizeButton;
        public int[] sizes = new int[3];
        public int[] prices = new int[3];
        public string[] nameDops = new string[4];
        public int[] priceDops = new int[4];
        public bool[] addIngrid = new bool[4];
        Grid moreOfPizzaConteiner;
        SwipeView swipeView;
        VerticalStackLayout morePizzas;
        Grid thiSPizza;

        public PizzaCart(string namePiz, string sourseOnPizzaIco, string previewIngridient, string fullIngridient, string KBGU, int size1, int size2,int size3, 
            int price1, int price2, int price3, string nameDop1, string nameDop2, string nameDop3, string nameDop4, 
            int priceDop1, int priceDop2, int priceDop3, int priceDop4, VerticalStackLayout morePizzaPan, Grid moreOfPizza)
        {
            morePizzas = morePizzaPan;
            namePizza = namePiz;
            soursePizzaIco = sourseOnPizzaIco;
            previewIngridients = previewIngridient;
            fullIngridients = fullIngridient;
            this.KBGU = KBGU;
            sizes[0] = size1;
            sizes[1] = size2;
            sizes[2] = size3;
            prices[0] = price1;
            prices[1] = price2;
            prices[2] = price3;
            nameDops[0] = nameDop1;
            nameDops[1] = nameDop2;
            nameDops[2] = nameDop3;
            nameDops[3] = nameDop4;
            priceDops[0] = priceDop1;
            priceDops[1] = priceDop2;
            priceDops[2] = priceDop3;
            priceDops[3] = priceDop4;
            moreOfPizzaConteiner = moreOfPizza;
            PrintPizzaCart(morePizzaPan);
        }

        void PrintPizzaCart(VerticalStackLayout pizzasConteiner)
        {
            Grid pizza = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                },

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) },
                },
            };
            Grid pizzaCart = new Grid();
            pizzaCart.Add(pizza);
            pizzasConteiner.Add(pizzaCart);
            thiSPizza = pizzaCart;
            size = sizes[0];

            swipeView = new()
            {
                Content = pizza,
            };
            pizzaCart.Add(swipeView);
            swipeView.SwipeStarted += SendSwipe;
            SwipeItems swipeItems = new SwipeItems();
            swipeView.LeftItems = swipeItems;
            SwipeItem swipeItem = new SwipeItem()
            {
                IconImageSource = "sendcart",
                BackgroundColor = Color.FromArgb("#CFE6D6")
            };
            swipeItems.Add(swipeItem);

            BoxView more = new BoxView
            {
                BackgroundColor = Color.FromArgb("#517C4F"),
            };
            Adder(more, pizza, 0, 0, 4, 4);

            Label namePizzaText = new Label
            {
                Text = namePizza,
                TextColor = Colors.White,
                Padding = new Thickness(15, 0),
                VerticalOptions = LayoutOptions.Center,
                FontFamily = BASEFONT,
                FontSize = 20,
            };
            Adder(namePizzaText, pizza, 0, 0, 0, 3);

            Label previewIngridientText = new Label
            {
                Text = previewIngridients,
                TextColor = Colors.White,
                Padding = new Thickness(15, 0),
                VerticalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Start,
                FontFamily = BASEFONT,
                FontSize = 16,
            };
            Adder(previewIngridientText, pizza, 1, 0, 0, 3);

            Button sendBut = new Button
            {
                BackgroundColor = Color.FromArgb("#CF973A"),
                ZIndex = -1,
            };
            sendBut.Clicked += SendBut;
            Adder(sendBut, pizza, 0, 3, 4, 4);

            VerticalStackLayout lay = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(5),
            };
            Adder(lay, pizza, 0, 4, 4, 0);

            

            Image cartw = new Image
            {
                Source = "cartw",
            };
            lay.Children.Add(cartw);

            countInCart = new Label
            {
                Text = "",
                TextColor = Colors.White,
                VerticalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                FontFamily = BASEFONT,
                FontSize = 18,

            };
            lay.Children.Add(countInCart);

            currentSizeButton = new Button
            {
                BackgroundColor = Color.FromArgb("#CF973A"),
                Text = sizes[0].ToString(),
                FontFamily = BASEFONT,
                CornerRadius = 100,
                Margin = new Thickness(0, 0, 0, 10),
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 15,
                BorderColor = Colors.White,
                BorderWidth = 2,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            currentSizeButton.Clicked += SizeUpdate1;
            Adder(currentSizeButton, pizza, 3, 0, 0, 0);
            Button size2Button = new Button
            {
                BackgroundColor = Color.FromArgb("#CF973A"),
                Text = sizes[1].ToString(),
                FontFamily = BASEFONT,
                CornerRadius = 100,
                Margin = new Thickness(0, 0, 0, 10),
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 15,
                BorderColor = Colors.White,
                BorderWidth = 0,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            size2Button.Clicked += SizeUpdate2;
            Adder(size2Button, pizza, 3, 1, 0, 0);
            Button size3Button = new Button
            {
                BackgroundColor = Color.FromArgb("#CF973A"),
                Text = sizes[2].ToString(),
                FontFamily = BASEFONT,
                CornerRadius = 100,
                Margin = new Thickness(0, 0, 0, 10),
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 15,
                BorderColor = Colors.White,
                BorderWidth = 0,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            size3Button.Clicked += SizeUpdate3;
            Adder(size3Button, pizza, 3, 2, 0, 0);

            VerticalStackLayout lay2 = new VerticalStackLayout
            {
                Margin = new Thickness(5),
            };
            Adder(lay2, pizza, 0, 3, 4);

            Image pizzaIco = new Image
            {
                Source = soursePizzaIco,
                Margin = new Thickness(5),
            };
            lay2.Children.Add(pizzaIco);

            priceText = new Label
            {
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
                FontFamily = BASEFONT,
                FontSize = 18,
            };
            lay2.Children.Add(priceText);
            UpdatePrice(prices[0]);
        }

        private void SizeUpdate1(object? sender, EventArgs e)
        {
            UpdatePrice(prices[0]);
            size = sizes[0];
            SizeUpdate(sender, e);
        }

        private void SizeUpdate2(object? sender, EventArgs e)
        {
            UpdatePrice(prices[1]);
            size = sizes[1];

            SizeUpdate(sender, e);
        }

        private void SizeUpdate3(object? sender, EventArgs e)
        {
            UpdatePrice(prices[2]);
            size = sizes[2];

            SizeUpdate(sender, e);
        }

        private void SizeUpdate(object? sender, EventArgs e)
        {
            currentSizeButton.BorderWidth = 0;
            currentSizeButton = sender != null ? (Button)sender : currentSizeButton;
            currentSizeButton.BorderWidth = 2;
        }

        void UpdatePrice(int sizePr = 0, int dopPr = 0)
        {
            sizePrice = sizePr == 0 ? sizePrice : sizePr;
            dopPrice = dopPr == 0 ? dopPrice : dopPr;
            resultPrice = (sizePrice + dopPrice);
            priceText.Text = (sizePrice + dopPrice).ToString() + "₽ ";
        }

        void PizzaPan()
        {
            if (!DontOpenWindows)
            {
                morePizzas.IsVisible = false;
                DontOpenWindows = true;
                if (morePan == null)
                {
                    morePan = new Grid
                    {
                        VerticalOptions = LayoutOptions.Center,

                        RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                    new RowDefinition(),
                },

                        ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
                    };
                    moreOfPizzaConteiner.Add(morePan);
                    BoxView box = new BoxView
                    {
                        BackgroundColor = Color.FromArgb("#517C4F"),
                        CornerRadius = 9,
                    };
                    Adder(box, morePan, 0, 0, 3, 2);

                    Label nameL = new Label
                    {
                        Text = namePizza,
                        TextColor = Colors.White,
                        Padding = new Thickness(5, 0),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = BASEFONT,
                        FontSize = 24,
                    };
                    Adder(nameL, morePan, 0, 0, 0, 2);

                    Label description = new Label
                    {
                        Text = fullIngridients,
                        TextColor = Colors.White,
                        Padding = new Thickness(10),
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        FontFamily = BASEFONT,
                        FontSize = 20,
                    };
                    Adder(description, morePan, 1, 0);

                    Button closeBut = new Button
                    {
                        Text = "<",
                        VerticalOptions = LayoutOptions.Start,
                        HorizontalOptions = LayoutOptions.Start,
                        FontFamily = BASEFONT,
                        FontSize = 28,
                        BackgroundColor = Color.FromArgb("#CF973A"),
                    };
                    closeBut.Clicked += close;
                    Adder(closeBut, morePan, 0, 0);

                    Image pizzaIco = new Image
                    {
                        Source = soursePizzaIco,
                        Margin = new Thickness(10),
                    };
                    Adder(pizzaIco, morePan, 1, 1);

                    Label kbguText = new Label
                    {
                        Text = "КБЖУ на 100г: " + KBGU,
                        TextColor = Colors.White,
                        Padding = new Thickness(5),
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontFamily = BASEFONT,
                        FontSize = 20,
                    };
                    Adder(kbguText, morePan, 2, 0, 0, 2);

                    BoxView box2 = new BoxView
                    {
                        BackgroundColor = Color.FromArgb("#CF973A"),
                        CornerRadius = 9,
                    };
                    Adder(box2, morePan, 3, 0, 2, 2);

                    Label AddIngridientText = new Label
                    {
                        Text = "Добавьте ингредиенты",
                        TextColor = Colors.White,
                        Padding = new Thickness(5),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = BASEFONT,
                        FontSize = 22,
                    };
                    Adder(AddIngridientText, morePan, 3, 0, 0, 2);

                    Grid dops = new Grid
                    {
                        RowDefinitions =
                {
                    new RowDefinition(),
                    new RowDefinition(),
                },

                        ColumnDefinitions =
                {
                    new ColumnDefinition() { Width = GridLength.Auto},
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition() { Width = GridLength.Auto},
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                },
                    };
                    Adder(dops, morePan, 4, 0, 0, 2);

                    for (int i = 0; i < 4; i++)
                    {
                        Label dopText = new Label
                        {
                            Text = nameDops[i],
                            TextColor = Colors.White,
                            Padding = new Thickness(5),
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            FontFamily = BASEFONT,
                            FontSize = 20,
                        };
                        Adder(dopText, dops, i < 2 ? i : i - 2, i < 2 ? 0 : 3);
                    }

                    CheckBox dopCheck1 = new CheckBox
                    {
                        Color = Color.FromArgb("#35916A"),
                        HorizontalOptions = LayoutOptions.End,
                    };
                    dopCheck1.CheckedChanged += dop1;
                    Adder(dopCheck1, dops, 0, 1);

                    CheckBox dopCheck2 = new CheckBox
                    {
                        Color = Color.FromArgb("#35916A"),
                        HorizontalOptions = LayoutOptions.End,
                    };
                    dopCheck2.CheckedChanged += dop2;
                    Adder(dopCheck2, dops, 1, 1);

                    CheckBox dopCheck3 = new CheckBox
                    {
                        Color = Color.FromArgb("#35916A"),
                        HorizontalOptions = LayoutOptions.End,
                    };
                    dopCheck3.CheckedChanged += dop3;
                    Adder(dopCheck3, dops, 0, 4);

                    CheckBox dopCheck4 = new CheckBox
                    {
                        Color = Color.FromArgb("#35916A"),
                        HorizontalOptions = LayoutOptions.End,
                    };
                    dopCheck4.CheckedChanged += dop4;
                    Adder(dopCheck4, dops, 1, 4);

                    for (int i = 0; i < 4; i++)
                    {
                        Label dopText = new Label
                        {
                            Text = "+" + priceDops[i].ToString() + "₽ ",
                            TextColor = Colors.White,
                            Padding = new Thickness(5),
                            HorizontalTextAlignment = TextAlignment.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            FontFamily = BASEFONT,
                            FontSize = 20,
                        };
                        Adder(dopText, dops, (i < 2 ? i : i - 2), (i < 2 ? 2 : 5));
                    }

                    Button sendBut = new Button
                    {
                        BackgroundColor = Color.FromArgb("#35916A"),
                        FontFamily = BASEFONT,
                        Text = "В корзину",
                        TextColor = Colors.Black,
                    };
                    sendBut.Released += By;
                    Adder(sendBut, morePan, 5, 0, 0, 2);

                }
                else
                {
                    morePan.IsVisible = true;
                }
            }
        }

        private void close(object? sender, EventArgs e)
        {
            if (morePan != null)
            {
                morePan.IsVisible = false;
            }
            morePizzas.IsVisible = true;
            DontOpenWindows = false;
        }

        private void dop1(object? sender, CheckedChangedEventArgs e)
        {
            dopPrice = ((CheckBox)sender).IsChecked == false ? dopPrice - priceDops[0] : dopPrice + priceDops[0];
            addIngrid[0] = !addIngrid[0];
            UpdatePrice(0, dopPrice);
        }
        private void dop2(object? sender, CheckedChangedEventArgs e)
        {
            dopPrice = ((CheckBox)sender).IsChecked == false ? dopPrice - priceDops[1] : dopPrice + priceDops[1];
            addIngrid[1] = !addIngrid[1];
            UpdatePrice(0, dopPrice);

        }
        private void dop3(object? sender, CheckedChangedEventArgs e)
        {
            dopPrice = ((CheckBox)sender).IsChecked == false ? dopPrice - priceDops[2] : dopPrice + priceDops[2];
            addIngrid[2] = !addIngrid[2];
            UpdatePrice(0, dopPrice);

        }
        private void dop4(object? sender, CheckedChangedEventArgs e)
        {
            dopPrice = ((CheckBox)sender).IsChecked == false ? dopPrice - priceDops[3] : dopPrice + priceDops[3];
            addIngrid[3] = !addIngrid[3];
            UpdatePrice(0, dopPrice);

        }

        private void By(object? sender, EventArgs e)
        {
            if (morePan != null)
            {
                morePan.IsVisible = false;
            }
            morePizzas.IsVisible = true;
            DontOpenWindows = false;

            Cart.AddCart((PizzaCart)this.MemberwiseClone());
        }

        private void SendBut(object? sender, EventArgs e)
        {
            PizzaPan();
        }

        static void Adder(View @object, Grid grid , int row, int column, int rowSpan = 0, int columnSpan = 0)
        {
            grid.Add(@object, column, row);
            Grid.SetColumnSpan(@object, columnSpan);
            Grid.SetRowSpan(@object, rowSpan);
        }

        private async void SendSwipe(object? sender, EventArgs e)
        {
            PizzaPan();
            await Task.Delay(100);
            swipeView.Close();
        }

        private async void DelSwipe(object? sender, EventArgs e)
        {
            PizzaPan();
            await Task.Delay(100);
            swipeView.Close();
        }
    }
}
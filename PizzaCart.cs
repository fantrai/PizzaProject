using Android.Icu.Number;
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

        int sizeId = 0;
        Label priceText;
        int count = 0;
        Label countInCart;

        string namePizza;
        string soursePizzaIco;
        string previewIngridients;
        string fullIngridients;
        string KBGU;
        Button currentSizeButton;
        int[] sizes = new int[3];
        int[] prices = new int[3];
        string[] nameDops = new string[4];
        int[] priceDops = new int[4];
        bool[] addIngridiesFlaf = new bool[4];
        Grid moreOfPizzaConteiner;
        SwipeView swipeView;

        public PizzaCart(string namePiz, string sourseOnPizzaIco, string previewIngridient, string fullIngridient, string KBGU, int size1, int size2,int size3, 
            int price1, int price2, int price3, string nameDop1, string nameDop2, string nameDop3, string nameDop4, 
            int priceDop1, int priceDop2, int priceDop3, int priceDop4, VerticalStackLayout morePizzaPan, Grid moreOfPizza)
        {
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
            morePizzaPan.Add(pizzaCart);

            swipeView = new()
            {
                Content = pizza,
            };
            Adder(swipeView, pizzaCart, 0, 0);
            swipeView.SwipeStarted += SendSwipe;
            SwipeItems swipeItems = new SwipeItems();
            swipeView.LeftItems = swipeItems;
            SwipeItem swipeItem = new SwipeItem() 
            { 
                IconImageSource = "sendcart", BackgroundColor = Color.FromArgb("#CFE6D6") 
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
                Text = previewIngridient,
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
                VerticalTextAlignment= TextAlignment.Start,
                FontFamily = BASEFONT,
                FontSize = 18,

            };
            lay.Children.Add(countInCart);

            currentSizeButton = new Button
            {
                BackgroundColor = Color.FromArgb("#CF973A"),
                Text = size1.ToString(),
                FontFamily = BASEFONT,
                CornerRadius = 100,
                Margin= new Thickness(0,0,0,10),
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 15,
                BorderColor = Colors.White,
                BorderWidth = 2,
                VerticalOptions= LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            currentSizeButton.Clicked += SizeUpdate;
            Adder(currentSizeButton, pizza, 3, 0, 0, 0);
            Button size2Button = new Button
            {
                BackgroundColor = Color.FromArgb("#CF973A"),
                Text = size2.ToString(),
                FontFamily = BASEFONT,
                CornerRadius = 100,
                Margin= new Thickness(0,0,0,10),
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 15,
                BorderColor = Colors.White,
                BorderWidth = 0,
                VerticalOptions= LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            size2Button.Clicked += SizeUpdate;
            Adder(size2Button, pizza, 3, 1, 0, 0);
            Button size3Button = new Button
            {
                BackgroundColor = Color.FromArgb("#CF973A"),
                Text = size2.ToString(),
                FontFamily = BASEFONT,
                CornerRadius = 100,
                Margin= new Thickness(0,0,0,10),
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = 15,
                BorderColor = Colors.White,
                BorderWidth = 0,
                VerticalOptions= LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            size3Button.Clicked += SizeUpdate;
            Adder(size3Button, pizza, 3, 2, 0, 0);

            VerticalStackLayout lay2 = new VerticalStackLayout 
            {
                Margin = new Thickness(5),
            };
            Adder(lay2, pizza, 0, 3, 4);

            Image pizzaIco = new Image
            {
                Source = sourseOnPizzaIco,
                Margin = new Thickness(5),
            };
            lay2.Children.Add(pizzaIco);

            priceText = new Label
            {
                Text = price1.ToString() + "₽ ",
                TextColor = Colors.White,
                HorizontalOptions = LayoutOptions.Center,
                FontFamily = BASEFONT,
                FontSize = 18,
            };
            lay2.Children.Add(priceText);
        }

        private void SizeUpdate(object? sender, EventArgs e)
        {
            currentSizeButton.BorderWidth = 0;
            currentSizeButton = sender != null ? (Button)sender : currentSizeButton;
            //придумать ченьть с размерами
            currentSizeButton.BorderWidth = 2;
        }

        private void PizzaPan()
        {
            if (!DontOpenWindows)
            {
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

                    Image pizzaIco = new Image
                    {
                        Source = soursePizzaIco,
                        Margin = new Thickness(10),
                    };
                    Adder(pizzaIco, morePan, 1, 1);

                    Label kbguText = new Label
                    {
                        Text = "КБЖУ" + KBGU,
                        TextColor = Colors.White,
                        Padding = new Thickness(5),
                        HorizontalTextAlignment = TextAlignment.Start,
                        FontFamily = BASEFONT,
                        FontSize = 20,
                    };
                    Adder(kbguText, morePan, 2, 0, 0, 1);

                    BoxView box2 = new BoxView
                    {
                        BackgroundColor = Color.FromArgb("#CF973A"),
                        CornerRadius = 9,
                    };
                    Adder(box2, morePan, 3, 0, 2, 2);

                    Label AddIngridientText = new Label
                    {
                        Text = "Добавьте ингридиенты",
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
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
                    new ColumnDefinition(),
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
                    for (int i = 0; i < 4; i++)
                    {
                        CheckBox dopCheck = new CheckBox
                        {
                            Color = Color.FromArgb("#35916A"),
                            HorizontalOptions = LayoutOptions.End,
                        };
                        Adder(dopCheck, dops, (i < 2 ? i : i - 2), (i < 2 ? 1 : 4));
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        Label dopText = new Label
                        {
                            Text = priceDops[i].ToString(),
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

        private void By(object? sender, EventArgs e)
        {
            count++;
            countInCart.Text = count.ToString();
            if (morePan != null)
            {
                morePan.IsVisible = false;
            }
            DontOpenWindows = false;
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
    }
}
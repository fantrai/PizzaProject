using System.Collections;

namespace Pizza
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            PizzaCart pizza1 = new PizzaCart("Пицца гавайская", "pizza", "Курица жареная, ананас, сыр моцарелла",
                "Курица жареная, ананас консервированный, орегано, сыр Моцарелла, соус белый",
                "232/11/12/20", 23, 45, 50, 504, 702, 900, "Сыр", "Ананас", "Курица", "Соус", 25, 23, 23, 23, Pizzas, pizzaPan);
        }
    }
}

namespace Pizza;

public partial class CartPage : ContentPage
{
	public static CartPage cart;

	public CartPage()
	{
		InitializeComponent();
		cart = this;
        Update();
    }

    public void Update()
	{
        Cart.PrintPizzas(cart.Pizzas);
        SendBut.Text = "Всего: " + Cart.AllPrice + ". Нажмите для заказа";
    }

    private void Good(object sender, EventArgs e)
    {
        el.IsVisible = true;
        Ready.IsVisible = false;
    }

    private void Send(object sender, EventArgs e)
    {
        el.IsVisible = false;
        if (Cart.pizzasInCart.Count > 0)
        {
            Ready.IsVisible = true;
            Cart.pizzasInCart.Clear();
            Update();
        }
        else Error.IsVisible = true;
    }

    private void Err(object sender, EventArgs e)
    {
        el.IsVisible = true;
        Error.IsVisible = false;
    }
}
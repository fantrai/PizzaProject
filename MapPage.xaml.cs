namespace Pizza;

public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();
	}

    private async void Inst(object sender, EventArgs e)
    {
		await Browser.Default.OpenAsync("https://vk.com/legoshandia");
    }

    private async void Map(object sender, EventArgs e)
    {
        await Browser.Default.OpenAsync("https://t.me/legoshandia");
    }

    private async void Bk(object sender, EventArgs e)
    {
        await Browser.Default.OpenAsync("https://discord.gg/wFN2dQcJH4");
    }
}
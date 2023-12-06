namespace Pizza;

public partial class ProfilePage : ContentPage
{
    bool polcof = false;
    bool preppersdata = false;

	public ProfilePage()
	{
		InitializeComponent();
        PersonalData.Load();
        Name.Text = PersonalData.nameUser;
        Phone.Text = PersonalData.phoneNumber;
        Email.Text = PersonalData.email;
        Data.Text = PersonalData.data;
        Login.Text = PersonalData.login;
        Parol.Text = PersonalData.password;
	}

    private void NewParol(object sender, EventArgs e)
    {
        PersonalData.password = ((Entry)sender).Text;
    }

    private void NewLogin(object sender, EventArgs e)
    {
        PersonalData.login = ((Entry)sender).Text;
    }

    private void NewData(object sender, EventArgs e)
    {
        PersonalData.data = ((Entry)sender).Text;
    }

    private void NewEmail(object sender, EventArgs e)
    {
        PersonalData.email = ((Entry)sender).Text;
    }

    private void NewTelephone(object sender, EventArgs e)
    {
        PersonalData.phoneNumber = ((Entry)sender).Text;
    }

    private void NewName(object sender, EventArgs e)
    {
        PersonalData.nameUser = ((Entry)sender).Text;
    }

    private void PolCof(object sender, CheckedChangedEventArgs e)
    {
        polcof = !polcof;
        UpdateSaveBut();
    }

    private void PrepPersData(object sender, CheckedChangedEventArgs e)
    {
        preppersdata = !preppersdata;
        UpdateSaveBut();
    }

    void UpdateSaveBut()
    {
        if (polcof && preppersdata) 
        {
            SaveBut.IsEnabled = true;
        }
        else 
        { 
            SaveBut.IsEnabled = false; 
        }
    }

    private void SaveData(object sender, EventArgs e)
    {
        El.IsVisible = false;
        if (Name.Text != string.Empty && Phone.Text != string.Empty && Login.Text != string.Empty && Parol.Text != string.Empty)
        {
            PersonalData.Save();
            Ready.IsVisible = true;
        }
        else
        {
            Error.IsVisible = true;
        }
    }

    private void Saved(object sender, EventArgs e)
    {
        El.IsVisible = true;
        Ready.IsVisible = false;
    }

    private void Err(object sender, EventArgs e)
    {
        El.IsVisible = true;
        Error.IsVisible = false;
    }
}
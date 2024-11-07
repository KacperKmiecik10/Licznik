namespace Licznik;

public partial class Licznik : ContentPage
{
	public string _counterName { get; set; }
    public int _defaultValue { get; set; }
    public int _currentValue { get; set; }
	public string _color { get; set; }

    public Licznik()
	{
		InitializeComponent();
    }

    public Licznik(string counterName, int defaultValue, int currentValue, string color) : this()
    {
        _counterName = counterName;
        _defaultValue = defaultValue;
        _currentValue = currentValue;
        _color = color;

        nameLabel.Text = _counterName;
        valueLabel.TextColor = Color.FromHex(_color);
        valueLabel.Text = _currentValue.ToString();
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        _currentValue++;
        valueLabel.Text = _currentValue.ToString();
        saveState();
    }

    private void OnSubsractClicked(object sender, EventArgs e)
    {
        _currentValue--;
        valueLabel.Text = _currentValue.ToString();
        saveState();
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        _currentValue = _defaultValue;
        valueLabel.Text = _currentValue.ToString();
        saveState();
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (App.Current.MainPage is AppShell shell)
        {
            for (int i = 0; i < MainPage.SavedCounters.Count; i++)
            {
                if (MainPage.SavedCounters[i]._counterName == this._counterName)
                {
                    MainPage.SavedCounters.RemoveAt(i);
                    MainPage.WriteData();
                }
            }

            shell.RemoveShellContent(_counterName);
        }
    }

    private void saveState()
    {
        for (int i = 0; i < MainPage.SavedCounters.Count; i++)
        {
            if (MainPage.SavedCounters[i]._counterName == this._counterName)
            {
                MainPage.SavedCounters[i]._currentValue = this._currentValue;
                MainPage.WriteData();
            }
        }
    }
}
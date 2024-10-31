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
            string FolderDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Licznik\\";
            string FileDir = "data.json";

            string json = File.ReadAllText(FolderDir + FileDir);
            string json2 = json.Replace("[", ""); json2 = json2.Replace("]", "");
            string[] jsonArray = json2.Split('{');
            for (int i = 0; i < jsonArray.Length; i++)
            {
                if (jsonArray[i].Contains("\"counterName\":\"" + _counterName + "\""))
                {
                    jsonArray[i] = "";
                }
            }
            string saveStr = "";
            saveStr += "[";
            foreach (string element in jsonArray)
            {
                saveStr += "{";
                saveStr += element;
                saveStr += ",";
            }
            saveStr += "]";
            saveStr = saveStr.Replace(",]", "]");
            saveStr = saveStr.Replace("[{,{", "[");
            saveStr = saveStr.Replace(",,", ",");
            saveStr = saveStr.Replace("[\"", "[{\"");
            saveStr = saveStr.Replace("{,", "{");
            saveStr = saveStr.Replace("{{", "{");
            saveStr = saveStr.Replace("[,", "[");
            saveStr = saveStr.Replace(",{]", "]");
            
            File.WriteAllText(FolderDir + FileDir, saveStr);

            MainPage.counterNames.Remove(_counterName);
            shell.RemoveShellContent(_counterName);
        }
    }

    private void saveState()
    {
        string FolderDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Licznik\\";
        string FileDir = "data.json";

        string json = File.ReadAllText(FolderDir + FileDir);
        string json2 = json.Replace("[", ""); json2 = json2.Replace("]", "");
        string[] jsonArray = json2.Split('{');
        for (int i = 0; i < jsonArray.Length; i++)
        {
            if (jsonArray[i].Contains("\"counterName\":\"" + _counterName + "\""))
            {
                jsonArray[i] = "{\"counterName\":\"" + _counterName + "\",\"defaultValue\":" + _defaultValue + ",\"currentValue\":" + _currentValue + ",\"color\":\"" + _color + "\"}";
            }
        }

        string saveStr = "";
        saveStr += "[";
        foreach (string element in jsonArray)
        {
            saveStr += "{";
            saveStr += element;
            saveStr += ",";
        }
        saveStr += "]";
        saveStr = saveStr.Replace(",]", "]");
        saveStr = saveStr.Replace("[{,{", "[");
        saveStr = saveStr.Replace(",,", ",");
        saveStr = saveStr.Replace("[\"", "[{\"");
        saveStr = saveStr.Replace("{{", "{");
        File.WriteAllText(FolderDir + FileDir, saveStr);
    }
}
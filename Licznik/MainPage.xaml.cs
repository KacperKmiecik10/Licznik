using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Licznik
{
    public partial class MainPage : ContentPage
    {
        public static List<string> counterNames = new List<string>();
        public static List<string> counterDatas = new List<string>();

        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            string FolderDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Licznik\\";
            string FileDir = "data.json";

            if (!Directory.Exists(FolderDir))
            {
                Directory.CreateDirectory(FolderDir);
            }

            if (!File.Exists(FolderDir + FileDir))
            {
                File.WriteAllText(FolderDir + FileDir, "[]");
            }
            else 
            {
                
                string json = File.ReadAllText(FolderDir + FileDir);
                json = json.Replace("\n", ""); json = json.Replace("\r", ""); json = json.Replace(" ", ""); json = json.Replace("[", ""); json = json.Replace("]", "");json = json.Replace("\"", ""); json = json.Replace(":", ""); json = json.Replace("counterName", ""); json = json.Replace("defaultValue", ""); json = json.Replace("currentValue", ""); json = json.Replace("color", ""); json = json.Replace("}", "");
                string[] array1 = json.Split('{');

                foreach (string element in array1)
                {
                    string[] array2 = element.Split(',');
                    counterNames.Add(array2[0]);
                    counterDatas.Add(element);
                    
                }
                counterNames.RemoveAt(0);
                counterDatas.RemoveAt(0);

                foreach(string element in counterDatas)
                {
                    string[] dataArr = element.Split(',');

                    if (App.Current.MainPage is AppShell shell)
                    {
                        shell.AddNewShellContent(dataArr[0], int.Parse(dataArr[1]), int.Parse(dataArr[2]), dataArr[3]);
                    }
                }
            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            string counterName = null;
            int defaultVal = 0;
            string color = null;

            bool validationChallange = true;
            color = entryColor.Text;
            counterName = entryNameValue.Text;

            try
            {
                defaultVal = int.Parse(entryDefaultValue.Text);
            }
            catch { validationChallange = false; infoText.Text = "Podano niepoprawne dane!"; }

            if (color != null && defaultVal != null && counterName != null) { color = color.ToUpper(); } else 
            {
                validationChallange = false;
                color = string.Empty;
            }

            int tagCount = 0;
            foreach (char element in color)
            {
                if (element == '#') { tagCount++; };
            }

            if (color.Length == 7 && color[0] == '#' && tagCount == 1)
            {
                

                foreach (char element in color)
                {
                    if (!new List<char>() {'#','0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'}.Contains(element)) { validationChallange = false; infoText.Text = "Podano niepoprawne dane!"; break; };
                }
            }
            else { validationChallange = false; infoText.Text = "Podano niepoprawne dane!"; }

            if (counterNames.Contains(counterName)) { validationChallange = false; infoText.Text = "Licznik o tej nazwie już isnieje!"; }

            if (validationChallange)
            {
                if (App.Current.MainPage is AppShell shell)
                {
                    shell.AddNewShellContent(counterName,defaultVal,defaultVal,color);
                    counterNames.Add(counterName);
                    string FolderDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Licznik\\";
                    string FileDir = "data.json";
                    string json = File.ReadAllText(FolderDir + FileDir);
                    string json2 = json.Replace("]", "");
                    json2 += ",{\"counterName\":\"" + counterName + "\",\"defaultValue\":" + defaultVal.ToString() + ",\"currentValue\":" + defaultVal.ToString() + ",\"color\":\"" + color + "\"}]";
                    json2 = json2.Replace("[,","[");
                    File.WriteAllText(FolderDir + FileDir, json2);

                }

                infoText.Text = "Pomyślnie dodano nowy licznik!";
            }
        }
    }

}

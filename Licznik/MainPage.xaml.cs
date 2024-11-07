using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Serialization;

namespace Licznik
{
    public partial class MainPage : ContentPage
    {
        public static string FolderDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Licznik\\";
        public static string FileDir = "data.xml";
        public static List<Models.LicznikModel> SavedCounters = new List<Models.LicznikModel> ();

        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            if (!Directory.Exists(FolderDir))
            {
                Directory.CreateDirectory(FolderDir);
            }
            if (!File.Exists(FolderDir + FileDir))
            {
                File.Create(FolderDir + FileDir);
            }
            else 
            {
                ReadData();

                foreach(Models.LicznikModel element in SavedCounters)
                {
                    if (App.Current.MainPage is AppShell shell)
                    {
                        shell.AddNewShellContent(element._counterName, element._defaultValue, element._currentValue, element._color);
                    }
                }
            }
        }

        public static void WriteData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Models.LicznikModel>));
            using (FileStream fileStream = new FileStream(FolderDir + FileDir, FileMode.Create))
            {
                serializer.Serialize(fileStream, SavedCounters);
            }
        }

        public static void ReadData()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Models.LicznikModel>));
                using (FileStream fileStream = new FileStream(FolderDir + FileDir, FileMode.Open))
                {
                    SavedCounters = (List<Models.LicznikModel>)serializer.Deserialize(fileStream);
                }
            }
            catch { }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            string counterName = null;
            int defaultVal = 0;
            string color = null;

            bool validationChallange = true;
            color = entryColor.Text;
            counterName = entryNameValue.Text;

            if (ValidateData(counterName,defaultVal,color))
            {
                if (App.Current.MainPage is AppShell shell)
                {
                    shell.AddNewShellContent(counterName,defaultVal,defaultVal,color);
                    SavedCounters.Add(new Models.LicznikModel(counterName, defaultVal, defaultVal, color));
                    WriteData();
                }
                infoText.Text = "Pomyślnie dodano nowy licznik!";
            }
        }

        private bool ValidateData(string counterName, int defaultVal, string color) 
        {
            try { defaultVal = int.Parse(entryDefaultValue.Text); }catch {  infoText.Text = "Podano niepoprawne dane!"; return false; }
            if (color != null && defaultVal != null && counterName != null) { color = color.ToUpper(); }else{return false;}

            int tagCount = 0;
            foreach (char element in color)
            {
                if (element == '#') { tagCount++; };
            }

            if (color.Length == 7 && color[0] == '#' && tagCount == 1)
            {
                foreach (char element in color)
                {
                    if (!new List<char>() { '#', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' }.Contains(element)) {infoText.Text = "Podano niepoprawne dane!"; return false; };
                }
            }
            else { infoText.Text = "Podano niepoprawne dane!"; return false; }

            foreach (Models.LicznikModel element in SavedCounters)
            {
                if (counterName == element._counterName) { infoText.Text = "Licznik o tej nazwie już isnieje!"; return false; }
            }
            return true;
        }
    }
}

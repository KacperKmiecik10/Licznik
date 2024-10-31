namespace Licznik
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public void AddNewShellContent(string counterName, int defaultValue, int currentValue, string color)
        {
            var contentTemplate = new DataTemplate(() =>
            {
                var counterPage = new Licznik(counterName,defaultValue, currentValue, color);
                return counterPage;
            });

            var newShellContent = new ShellContent
            {
                ContentTemplate = contentTemplate,
                Title = counterName,
                Route = "MainPage"
            };

            if (Items[0] is TabBar tabBar)
            {
                tabBar.Items.Add(newShellContent);
              
            }
        }

        public void RemoveShellContent(string title)
        {
            Items[0].Items.Remove(Items[0].Items.FirstOrDefault(item => item.Title == title));
        }
    
    }
}

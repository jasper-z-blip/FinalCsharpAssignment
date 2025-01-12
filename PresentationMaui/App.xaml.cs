namespace PresentationMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Sätt AppShell som huvudlayouten
        MainPage = new AppShell();
    }
}


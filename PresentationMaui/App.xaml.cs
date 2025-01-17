namespace PresentationMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        /* AppShell som huvudlayout.
        AppShell är bra för navigering mellan sidor, så perfekt till min grafiska appplikation.*/
        MainPage = new AppShell();
    }
}
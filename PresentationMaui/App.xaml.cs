namespace PresentationMaui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        /*ChatGPT sa: Sätt AppShell som huvudlayouten. Jag hade en bug som gjorde att jag inte kunde starta programmet.
        Då föreslog chatGPT att sätta appshell som huvudlayout, så fungerade det? Jag vet bara att appshell gör appen enkel och bra för navigering.
        Så jag kan inte förklara varför Appshell istället för MainPage som det var först. Har du tid att skriva varför får du gärna det :-)*/
        MainPage = new AppShell();
    }
}


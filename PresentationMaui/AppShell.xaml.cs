namespace PresentationMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrerar "vägar" för sid-navigering mellan olika sidor, navigering via Shell.
        Routing.RegisterRoute("MainPage", typeof(Pages.MainPage));
        Routing.RegisterRoute("SavedCustomerPage", typeof(Pages.SavedCustomerPage));
        Routing.RegisterRoute("EditCustomerPage", typeof(Pages.EditCustomerPage));
    }
}


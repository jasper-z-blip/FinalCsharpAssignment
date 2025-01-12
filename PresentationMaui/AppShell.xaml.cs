namespace PresentationMaui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Mellan olika sidor
        Routing.RegisterRoute("MainPage", typeof(Pages.MainPage));
        Routing.RegisterRoute("SavedCustomerPage", typeof(Pages.SavedCustomerPage));
        Routing.RegisterRoute("EditCustomerPage", typeof(Pages.EditCustomerPage));
    }
}


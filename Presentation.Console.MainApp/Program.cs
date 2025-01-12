using Presentation.Console.MainApp.UI;
using Business.Services;

namespace Presentation.Console.MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "contacts.json";
            var jsonService = new JsonService(filePath);
            var contactService = new ContactService(jsonService);
            var menuService = new MenuService(contactService);

            menuService.ShowMainMenu();
        }
    }
}

using PresentationMaui.ViewModels;
using Shared.Interfaces;

namespace PresentationMaui.Pages
{
    public partial class EditCustomerPage : ContentPage
    {
        // Viewmodel som hanterar logiken f�r customers.
        private readonly EditCustomerViewModel _viewModel;

        public EditCustomerPage(ICustomerService customerService)
        {
            InitializeComponent();
            _viewModel = new EditCustomerViewModel(customerService);

            // BindingContext kopplar denna sida till ViewModel s� att UI kan uppdateras automatiskt n�r data �ndras.
            BindingContext = _viewModel;
        }

        private async void LoadCustomerButton_Clicked(object sender, EventArgs e)
        {
            if (int.TryParse(_viewModel.CustomerNumber, out int customerNumber))
            {
                // Ska hitta customer med hj�lp av dennes kundnummer som anv�ndaren skrivit in.
                // Om numret finns s� g�r den in i if-satsen, om numret inte finns s� blir de Error och om anv�ndaren fyller i tex. 2.5 s� blir de Validation Error.
                await _viewModel.LoadCustomerByNumberAsync(customerNumber);
               
                if (_viewModel.CustomerToEdit != null)
                {
                    
                }
                else
                {
                    await DisplayAlert("Error", "Customer not found.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Validation Error", "Please enter a valid customer number.", "OK");

            }
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            // Ska spara kund �ndringar via ViewModel.
            await _viewModel.SaveCustomerAsync();
            if (_viewModel.CustomerToEdit != null)
            {
                // Om sparning lyckas, visa framg�ngsmeddelande och rensa formul�ret.
                await DisplayAlert("Success", "Customer details updated!", "OK");
                ClearForm();
            }
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            // N�r anv�ndaren klickar p� Delete knappen s� g�rs en extra koll att denne verkligen vill radera kontakten, om anv�ndaren klickar "Yes" s� ��r den in i if-satsen och kunden raderas.
            var confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this customer?", "Yes", "No");
            if (confirm)
            {
                await _viewModel.DeleteCustomerAsync();
                await DisplayAlert("Success", "Customer deleted successfully!", "OK");

                _viewModel.CustomerToEdit = null;

                ClearForm();
            }
        }

            private void ClearForm()
        {
            _viewModel.CustomerNumber = string.Empty;
            _viewModel.CustomerToEdit = null;
        }
    }
}
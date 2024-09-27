using SignupPage.Models;
using SignupPage.Data;

namespace SignupPage
{
    public partial class LoginPage : ContentPage
    {
        Database database;
        public LoginPage()
        {
            InitializeComponent();
            database = new Database();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            // Check if user exists in the database
            bool isLoginSuccessful = await database.ValidateUser(email, password);

            if (isLoginSuccessful)
            {
                await DisplayAlert("Success", "Login successful", "OK");
                // Navigate to the next page or dashboard after login
                //await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password. Please try again.", "OK");
            }
        }
    }
}

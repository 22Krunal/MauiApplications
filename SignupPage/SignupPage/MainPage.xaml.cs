using SignupPage.Models;
using SignupPage.Data;

namespace SignupPage
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        Database database;
        public MainPage()
        {
            InitializeComponent();
            database = new Database();
        }

        private async void OnSignupClicked(object sender, EventArgs e)
        {
            // Create a new user from the entered details
            User user = new User()
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text,
                Name = NameEntry.Text,
            };

            // Attempt to add the user to the database
            bool isSignupSuccessful = await AsyncAddUser(user);

            // If signup is successful, navigate to the login page
            if (isSignupSuccessful)
            {
                await DisplayAlert("Success", "Signup Successfull", "OK");
                await Shell.Current.GoToAsync("//LoginPage");
            }
            else
            {
                await DisplayAlert("Error", "Signup failed. Please try again.", "OK");
            }
        }

        private async Task<bool> AsyncAddUser(User user)
        {
            try
            {
                await database.AddUser(user);
                return true; // Return true if the user is successfully added
            }
            catch (Exception ex)
            {
                // Log or handle the exception if something goes wrong
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

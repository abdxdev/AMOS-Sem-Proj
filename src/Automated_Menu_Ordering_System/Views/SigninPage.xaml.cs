using Automated_Menu_Ordering_System.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Automated_Menu_Ordering_System.Services;
using Automated_Menu_Ordering_System.Contracts.Services;
using System.Diagnostics;
using Npgsql;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class SigninPage : Page
{
    public SigninViewModel ViewModel
    {
        get;
    }

    private readonly ILocalSettingsService _localSettingsService;
    private readonly INavigationService _navigationService;

    public SigninPage()
    {
        InitializeComponent();
        ViewModel = App.GetService<SigninViewModel>();
        _localSettingsService = App.GetService<ILocalSettingsService>();
        _navigationService = App.GetService<INavigationService>();
        _ = LoadSignInSettingsAsync();
    }

    private async Task LoadSignInSettingsAsync()
    {
        // Use await for async calls
        var keepSignIn = await _localSettingsService.ReadSettingAsync<bool>("keepSignIn");

        if (keepSignIn)
        {
            var userId = await _localSettingsService.ReadSettingAsync<string>("userId");
            var userType = await _localSettingsService.ReadSettingAsync<string>("userType");
            var userPassword = await _localSettingsService.ReadSettingAsync<string>("userPassword");

            checkAndNavigate(userType, userId, userPassword);
            ErrorTextBlock.Visibility = Visibility.Collapsed;
        }
    }

    private void UserTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (string)UserTypeComboBox.SelectedItem;
        if (UserTypeTextBlock != null)
        {
            UserTypeTextBlock.Text = $"Sign in as {selectedItem}";
        }

        if (UserIdBox != null && UserPasswordBox != null)
        {
            UserIdBox.PlaceholderText = (selectedItem == "Customer") ? "Table Id" : $"{selectedItem} Username";
            UserIdBox.Header = (selectedItem == "Customer") ? "Enter Table Id" : "Enter Username";
            UserPasswordBox.Visibility = (selectedItem == "Customer") ? Visibility.Collapsed : Visibility.Visible;
            UserPasswordBox.PlaceholderText = $"{selectedItem} Password";
        }
    }
    private bool checkUser(string userType, string userId, string? userPassword)
    {
        NpgsqlDataReader? reader;
        if (userType == "customer")
        {
            reader = App.GetService<DatabaseService>().get_sittingtables(userId);
            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = "Table not found!";
                return false;
            }
        }
        else
        {
            reader = App.GetService<DatabaseService>().get_account(userType, userId, userPassword);
            if (reader.HasRows)
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = "Invalid username or password!";
                return false;
            }
        }
    }

    private void checkAndNavigate(string userType, string userId, string userPassword)
    {
        if (string.IsNullOrEmpty(userType) || string.IsNullOrEmpty(userId) || (string.IsNullOrEmpty(userPassword) && userType != "Customer"))
        {
            ErrorTextBlock.Visibility = Visibility.Visible;
            ErrorTextBlock.Text = "Please fill all fields!";
            return;
        }
        try
        {
            if (checkUser(userType.ToLower(), userId, userPassword))
            {
                _localSettingsService.SaveSettingAsync("userId", userId);
                _localSettingsService.SaveSettingAsync("userType", userType);
                _localSettingsService.SaveSettingAsync("userPassword", userPassword);
                _localSettingsService.SaveSettingAsync("keepSignIn", keepSignInCheckBox.IsChecked);
                if (userType == "Customer")
                {
                    _navigationService.NavigateTo(pageKey: typeof(HomeViewModel).FullName);
                }
                else if (userType == "Admin")
                {
                    _navigationService.NavigateTo(pageKey: typeof(CartViewModel).FullName);
                }
                else if (userType == "Manager")
                {
                    _navigationService.NavigateTo(pageKey: typeof(SettingsViewModel).FullName);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
    private void PrimaryButton_Click(object sender, RoutedEventArgs e)
    {
        ErrorTextBlock.Visibility = Visibility.Collapsed;
        var userType = (string)UserTypeComboBox.SelectedItem;
        var userId = UserIdBox.Text;
        var userPassword = UserPasswordBox.Password;
        checkAndNavigate(userType, userId, userPassword);
    }
}

using Automated_Menu_Ordering_System.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Automated_Menu_Ordering_System.Services;
using Automated_Menu_Ordering_System.Contracts.Services;
using System.Diagnostics;
using Npgsql;
using Automated_Menu_Ordering_System.Helpers;
using CommunityToolkit.Mvvm.Messaging;

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
        AppName_TextBlock.Text = "AppDisplayName".GetLocalized();
        AppDescription_TextBlock.Text = "AppDescription".GetLocalized();
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
    private int CheckUserAndGetAccountId(string userType, string userId, string? userPassword)
    {
        if (userType == "customer")
        {
            int tableId;
            try
            {
                tableId = int.Parse(userId);
            }
            catch (Exception e)
            {
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = "Invalid table id!";
                return -1;
            }
            if (App.GetService<DatabaseService>().does_table_exist(tableId))
            {
                return tableId;
            }
            else
            {
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = "Table not found!";
                return -1;
            }
        }
        else
        {
            int accountId = App.GetService<DatabaseService>().get_account_id(userType, userId, userPassword);
            if (accountId != -1)
            {
                return accountId;
            }
            else
            {
                ErrorTextBlock.Visibility = Visibility.Visible;
                ErrorTextBlock.Text = "Invalid username or password!";
                return -1;
            }
        }
    }

    private async void checkAndNavigate(string userType, string userId, string userPassword)
    {
        if (string.IsNullOrEmpty(userType) || string.IsNullOrEmpty(userId) || (string.IsNullOrEmpty(userPassword) && userType != "Customer"))
        {
            ErrorTextBlock.Visibility = Visibility.Visible;
            ErrorTextBlock.Text = "Please fill all fields!";
            return;
        }
        try
        {
            var accountId = CheckUserAndGetAccountId(userType.ToLower(), userId, userPassword);
            if (accountId != -1)
            {
                //TODO Confusing as userId is actually username and accountId is userId
                await _localSettingsService.SaveSettingAsync("userId", userId);
                await _localSettingsService.SaveSettingAsync("userType", userType);
                await _localSettingsService.SaveSettingAsync("accountId", accountId.ToString());
                await _localSettingsService.SaveSettingAsync("userPassword", userPassword);
                await _localSettingsService.SaveSettingAsync("keepSignIn", keepSignInCheckBox.IsChecked);

                ShowPageFor(userType);
                if (userType == "Customer")
                {
                    _navigationService.NavigateTo(pageKey: typeof(HomeViewModel).FullName);
                }
                else if (userType == "Admin")
                {
                    _navigationService.NavigateTo(pageKey: typeof(AccountsViewModel).FullName);
                }
                else if (userType == "Manager")
                {
                    // Visibility.Visible for all manager pane items
                    var branchId = App.GetService<DatabaseService>().get_branch_id_by_manager_id(accountId);
                    await _localSettingsService.SaveSettingAsync("branchId", branchId.ToString());
                    _navigationService.NavigateTo(pageKey: typeof(OrdersViewModel).FullName);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }

    private void ShowPageFor(string accountType)
    {

        var navigationItems = new Dictionary<string, List<string>>
        {
            {
                "Customer", new List<string>
                {
                    "HomeNavItem",
                    "PizzasNavItem",
                    "BurgersNavItem",
                    "DessertsNavItem",
                    "DrinksNavItem",
                    "CartNavItem"
                }
            },
            {
                "Manager", new List<string>
                {
                    "OrdersNavItem",
                    "MenuNavItem",
                    "HistoryNavItem"
                }
            },
            {
                "Admin", new List<string>
                {
                    "AccountsNavItem",
                    "BranchesNavItem",
                    "SittingTablesNavItem",
                    "ProductsNavItem",
                    "DealsNavItem",
                    "DealProductsNavItem"
                }
            }
        };

        foreach (var item in navigationItems)
        {
            if (item.Key == accountType)
            {
                foreach (var navItem in item.Value)
                {
                    WeakReferenceMessenger.Default.Send(new NavigationItemVisibilityMessage(navItem, true));
                }
            }
            else
            {
                foreach (var navItem in item.Value)
                {
                    WeakReferenceMessenger.Default.Send(new NavigationItemVisibilityMessage(navItem, false));
                }
            }
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

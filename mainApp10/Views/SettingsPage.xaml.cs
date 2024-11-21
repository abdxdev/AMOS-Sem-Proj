using mianApp10.Contracts.Services;
using mianApp10.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.UserActivities;
using Windows.Security.Credentials;
using Windows.Security.Cryptography;
using Windows.System;

namespace mianApp10.Views;

// TODO: Set the URL for your privacy policy by updating SettingsPage_PrivacyTermsLink.NavigateUri in Resources.resw.
public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }


    private readonly ILocalSettingsService _localSettingsService;
    private readonly INavigationService _navigationService;

    public SettingsPage()
    {
        _localSettingsService = App.GetService<ILocalSettingsService>();
        _navigationService = App.GetService<INavigationService>();
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }
    private async void SignOutCommand(object sender, RoutedEventArgs e)
    {
        var LocalUserType = await _localSettingsService.ReadSettingAsync<string>("userType");
        if (LocalUserType == "Customer")
        {
            var LocalUserId = await _localSettingsService.ReadSettingAsync<string>("userId");
            var dialog = new ContentDialog
            {
                Title = "Sign out",
                PrimaryButtonText = "Sign out",
                CloseButtonText = "Cancel",
                XamlRoot = XamlRoot,
                DefaultButton = ContentDialogButton.Primary
            };

            var passwordBox = new PasswordBox();
            passwordBox.Header = "Please enter the manager password to sign out";
            dialog.Content = passwordBox;
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                var reader = App.DatabaseService.get_manager_by_table_id(LocalUserId);
                reader.Read();
                var password = reader["password"].ToString();
                reader.Close();
                if (passwordBox.Password == password)
                {
                    await _localSettingsService.SaveSettingAsync<string>("userId", "");
                    await _localSettingsService.SaveSettingAsync<string>("userType", "");
                    await _localSettingsService.SaveSettingAsync<string>("userPassword", "");

                    _navigationService.NavigateTo(pageKey: typeof(SigninViewModel).FullName);
                }
                else
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = "Incorrect password",
                        CloseButtonText = "Ok",
                        XamlRoot = XamlRoot,
                        DefaultButton = ContentDialogButton.Close
                    };

                    await errorDialog.ShowAsync();
                }
            }
        }
        else if (LocalUserType == "Manager" || LocalUserType == "Admin")
        {
            await _localSettingsService.SaveSettingAsync<string>("userId", "");
            await _localSettingsService.SaveSettingAsync<string>("userType", "");
            await _localSettingsService.SaveSettingAsync<string>("userPassword", "");

            _navigationService.NavigateTo(pageKey: typeof(SigninViewModel).FullName);
        }

        //var keyCredentialRetrievalResult = await KeyCredentialManager.OpenAsync("mianApp10");
        //if (keyCredentialRetrievalResult.Status == KeyCredentialStatus.NotFound)
        //{
        //    var keyCredentialCreationResult = await KeyCredentialManager.RequestCreateAsync("mianApp10", KeyCredentialCreationOption.ReplaceExisting);
        //    if (keyCredentialCreationResult.Status == KeyCredentialStatus.Success)
        //    {
        //        keyCredentialRetrievalResult = await KeyCredentialManager.OpenAsync("mianApp10");
        //    }
        //}

        //if (keyCredentialRetrievalResult.Status == KeyCredentialStatus.Success)
        //{
        //    var keyCredential = keyCredentialRetrievalResult.Credential;
        //    var userConsentResult = await keyCredential.RequestSignAsync(
        //        CryptographicBuffer.ConvertStringToBinary("Please confirm your identity", BinaryStringEncoding.Utf8));

        //    if (userConsentResult.Status == KeyCredentialStatus.Success)
        //    {
        //        await _localSettingsService.SaveSettingAsync<string>("userId", "");
        //        await _localSettingsService.SaveSettingAsync<string>("userType", "");
        //        await _localSettingsService.SaveSettingAsync<string>("userPassword", "");

        //        _navigationService.NavigateTo(pageKey: typeof(SigninViewModel).FullName);
        //    }
        //}
    }
}
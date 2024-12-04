using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Automated_Menu_Ordering_System.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.DataGrid;
using WinUIEx;
using Microsoft.UI.Xaml;
using Npgsql;

namespace Automated_Menu_Ordering_System.Views;

public class Account
{
    public int Id
    {
        get; set;
    }
    public string Username
    {
        get; set;
    }
    public string Password
    {
        get; set;
    }
    public string AccountType
    {
        get; set;
    }
    public DateTime CreatedAt
    {
        get; set;
    }
    public int BranchId
    {
        get; set;
    }
}

public sealed partial class AccountsPage : Page
{

    public ObservableCollection<Account> Accounts
    {
        get; private set;
    }

    private bool CurrentlyAddingNewItem
    {
        get; set;
    }

    private Account? AccountBeingAdded
    {
        get; set;
    }

    private bool IsEditingNew
    {
        get; set;
    }

    public AccountsPage()
    {
        this.InitializeComponent();
        Accounts = new ObservableCollection<Account>();
        LoadData();
    }

    public async void LoadData()
    {
        var reader = App.GetService<DatabaseService>().get_accounts();
        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        Accounts.Clear();
        while (reader.Read())
        {
            var account = new Account
            {
                Id = Convert.ToInt32(reader["id"]),
                Username = reader["username"].ToString(),
                Password = reader["password"].ToString(),
                AccountType = reader["acc_type"].ToString(),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                BranchId = reader["branch_id"].ToString() == "" ? 0 : Convert.ToInt32(reader["branch_id"])
            };
            Accounts.Add(account);
        }
        reader.Close();
        sfDataGrid.ItemsSource = Accounts;
    }

    private void StartEdit(Account account)
    {
        AccountBeingAdded = account;
        CurrentlyAddingNewItem = true;
        sfDataGrid.SelectedItem = account;
        sfDataGrid.View.MoveCurrentTo(account);
        sfDataGrid.AllowDeleting = false;
        sfDataGrid.AllowEditing = true;
        sfDataGrid.Columns[0].IsReadOnly = true;

        HiddenButtons.Visibility = Visibility.Visible;
        ButtonsPanel.Visibility = Visibility.Collapsed;
    }

    private void EndEdit()
    {
        AccountBeingAdded = null;
        CurrentlyAddingNewItem = false;
        sfDataGrid.AllowEditing = false;
        sfDataGrid.AllowDeleting = true;

        HiddenButtons.Visibility = Visibility.Collapsed;
        ButtonsPanel.Visibility = Visibility.Visible;
    }

    private void Delete(Account account)
    {
        App.GetService<DatabaseService>().delete_account(account.Id);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var newAccount = new Account
        {
            Id = 0,
            Username = "",
            Password = "",
            AccountType = "",
            CreatedAt = DateTime.Now,
            BranchId = 0
        };
        Accounts.Add(newAccount);
        IsEditingNew = true;
        StartEdit(newAccount);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (sfDataGrid.SelectedItem is Account account)
        {
            IsEditingNew = false;
            StartEdit(account);
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        var errorDialog = new ContentDialog
        {
            Title = "Error",
            CloseButtonText = "OK",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = this.XamlRoot
        };
        try
        {
            foreach (var item in sfDataGrid.SelectedItems)
            {
                if (item is Account account)
                {
                    Delete(account);
                    Accounts.Remove(account);
                }
            }
        }
        catch (Exception ex)
        {
            errorDialog.Content = ex.Message;
            await errorDialog.ShowAsync();
        }
    }

    private async void DoneButton_Click(object sender, RoutedEventArgs e)
    {
        if (!CurrentlyAddingNewItem) return;
        var errorDialog = new ContentDialog
        {
            Title = "Error",
            CloseButtonText = "OK",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = this.XamlRoot
        };


        if (sfDataGrid.SelectedItem is Account account)
        {
            try
            {
                if (IsEditingNew)
                    App.GetService<DatabaseService>().insert_account(account.Username, account.Password, account.AccountType, account.BranchId);
                else
                    App.GetService<DatabaseService>().update_account(account.Id, account.Username, account.Password, account.AccountType, account.BranchId);
            }
            catch (Exception ex)
            {
                errorDialog.Content = ex.Message;
                await errorDialog.ShowAsync();
                Accounts.Remove(account);
            }
        }
        EndEdit();
        Accounts.Clear();
        LoadData();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            Accounts.Remove(AccountBeingAdded);
        }
        EndEdit();
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        Accounts.Clear();
        LoadData();
    }

    private void sfDataGrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grids.GridSelectionChangedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            sfDataGrid.SelectedItem = AccountBeingAdded;
        }
    }

    private async void sfDataGrid_RecordDeleting(object sender, RecordDeletingEventArgs e)
    {
        var errorDialog = new ContentDialog
        {
            Title = "Error",
            CloseButtonText = "OK",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = this.XamlRoot
        };
        try
        {
            foreach (var item in sfDataGrid.SelectedItems)
            {
                if (item is Account account)
                {
                    Delete(account);
                }
            }
        }
        catch (Exception ex)
        {
            errorDialog.Content = ex.Message;
            await errorDialog.ShowAsync();
        }
    }
    //private readonly ILocalSettingsService _localSettingsService;

    //this.sfDataGrid.RowValidating += sfDataGrid_RowValidating;
    //this.sfDataGrid.RowValidated += sfDataGrid_RowValidated;
    //this.sfDataGrid.AddNewRowInitiating += SfDataGrid_AddNewRowInitiating;

    //var rowIndex = sfDataGrid.View.Records.Count;
    //var rowColumnIndex = new Syncfusion.UI.Xaml.Grids.ScrollAxis.RowColumnIndex(rowIndex, 0);
    //sfDataGrid.ScrollInView(rowColumnIndex);

    //private void sfDataGrid_RowValidating(object sender, RowValidatingEventArgs e)
    //{
    //    try
    //    {
    //        if (e.RowData is Account account)
    //        {
    //            // Example validation: Ensure the username is not empty and password is valid
    //            if (string.IsNullOrWhiteSpace(account.Username) || string.IsNullOrWhiteSpace(account.Password))
    //            {
    //                e.IsValid = false;
    //                e.ErrorMessages.Add("RequiredFields", "Username and Password are required.");
    //            }

    //            if (account.Id <= 0)
    //            {
    //                e.IsValid = false;
    //                e.ErrorMessages.Add("IDGreaterThanZero", "ID must be greater than 0.");
    //            }

    //            if (!IsUnique(account.Id, "Id", account))
    //            {
    //                e.IsValid = false;
    //                e.ErrorMessages.Add("UniqueID", "ID must be unique.");
    //            }

    //            if (!IsUnique(account.Username, "Username", account))
    //            {
    //                e.IsValid = false;
    //                e.ErrorMessages.Add("UniqueUsername", "Username must be unique.");
    //            }

    //            if (account.AccountType == "manager" && account.BranchId == null)
    //            {
    //                e.IsValid = false;
    //                e.ErrorMessages.Add("ManagerRequiresBranch", "Manager account type requires a branch ID.");
    //            }

    //            if (account.AccountType != "manager" && account.BranchId != null)
    //            {
    //                e.IsValid = false;
    //                e.ErrorMessages.Add("BranchIdOnlyForManager", "Branch ID is only for manager account type.");
    //            }

    //            if (account.AccountType != "admin" && account.AccountType != "manager")
    //            {
    //                e.IsValid = false;
    //                e.ErrorMessages.Add("InvalidAccountType", "Invalid account type.");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log the exception details for debugging
    //        Debug.WriteLine($"Error during RowValidating: {ex.Message}");
    //        Debug.WriteLine(ex.StackTrace);
    //        // Optionally, you can mark the row as invalid with a generic error message
    //        e.IsValid = false;
    //        e.ErrorMessages.Add("ValidationError", "An unexpected error occurred during validation.");
    //    }
    //}

    //private void sfDataGrid_RowValidated(object sender, RowValidatedEventArgs e)
    //{
    //    Debug.WriteLine("RowValidated");
    //}
}

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

public class SittingTable
{
    public int Id
    {
        get; set;
    }
    public int BranchId
    {
        get; set;
    }
}

public sealed partial class SittingTablesPage : Page
{

    public ObservableCollection<SittingTable> SittingTables
    {
        get; private set;
    }

    private bool CurrentlyAddingNewItem
    {
        get; set;
    }

    private SittingTable? SittingTableBeingAdded
    {
        get; set;
    }

    private bool IsEditingNew
    {
        get; set;
    }

    public SittingTablesPage()
    {
        this.InitializeComponent();
        SittingTables = new ObservableCollection<SittingTable>();
        LoadData();
    }

    public async void LoadData()
    {
        var reader = App.GetService<DatabaseService>().get_sitting_tables();
        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        SittingTables.Clear();
        while (reader.Read())
        {
            var sittingTable = new SittingTable
            {
                Id = Convert.ToInt32(reader["id"]),
                BranchId = Convert.ToInt32(reader["branch_id"])
            };
            SittingTables.Add(sittingTable);
        }
        reader.Close();
        sfDataGrid.ItemsSource = SittingTables;
    }

    private void StartEdit(SittingTable sittingTable)
    {
        SittingTableBeingAdded = sittingTable;
        CurrentlyAddingNewItem = true;
        sfDataGrid.SelectedItem = sittingTable;
        sfDataGrid.View.MoveCurrentTo(sittingTable);
        sfDataGrid.AllowDeleting = false;
        sfDataGrid.AllowEditing = true;
        sfDataGrid.Columns[0].IsReadOnly = true;

        HiddenButtons.Visibility = Visibility.Visible;
        ButtonsPanel.Visibility = Visibility.Collapsed;
    }

    private void EndEdit()
    {
        SittingTableBeingAdded = null;
        CurrentlyAddingNewItem = false;
        sfDataGrid.AllowEditing = false;
        sfDataGrid.AllowDeleting = true;

        HiddenButtons.Visibility = Visibility.Collapsed;
        ButtonsPanel.Visibility = Visibility.Visible;
    }

    private void Delete(SittingTable sittingTable)
    {
        App.GetService<DatabaseService>().delete_sitting_table(sittingTable.Id);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var newSittingTable = new SittingTable
        {
            Id = 0,
            BranchId = 0
        };
        SittingTables.Add(newSittingTable);
        IsEditingNew = true;
        StartEdit(newSittingTable);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (sfDataGrid.SelectedItem is SittingTable sittingTable)
        {
            IsEditingNew = false;
            StartEdit(sittingTable);
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
                if (item is SittingTable sittingTable)
                {
                    Delete(sittingTable);
                    SittingTables.Remove(sittingTable);
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


        if (sfDataGrid.SelectedItem is SittingTable sittingTable)
        {
            try
            {
                if (IsEditingNew)
                    App.GetService<DatabaseService>().insert_sitting_table(sittingTable.BranchId);
                else
                    App.GetService<DatabaseService>().update_sitting_table(sittingTable.Id, sittingTable.BranchId);
            }
            catch (Exception ex)
            {
                errorDialog.Content = ex.Message;
                await errorDialog.ShowAsync();
                SittingTables.Remove(sittingTable);
            }
        }
        EndEdit();
        SittingTables.Clear();
        LoadData();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            SittingTables.Remove(SittingTableBeingAdded);
        }
        EndEdit();
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        SittingTables.Clear();
        LoadData();
    }

    private void sfDataGrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grids.GridSelectionChangedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            sfDataGrid.SelectedItem = SittingTableBeingAdded;
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
                if (item is SittingTable sittingTable)
                {
                    Delete(sittingTable);
                }
            }
        }
        catch (Exception ex)
        {
            errorDialog.Content = ex.Message;
            await errorDialog.ShowAsync();
        }
    }
}

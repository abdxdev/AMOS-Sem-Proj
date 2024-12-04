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

public class Deal
{
    public int Id
    {
        get; set;
    }
    public string Name
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }
    public string ImageUrl
    {
        get; set;
    }
    public decimal Price
    {
        get; set;
    }
}

public sealed partial class DealsPage : Page
{

    public ObservableCollection<Deal> Deals
    {
        get; private set;
    }

    private bool CurrentlyAddingNewItem
    {
        get; set;
    }

    private Deal? DealBeingAdded
    {
        get; set;
    }

    private bool IsEditingNew
    {
        get; set;
    }

    public DealsPage()
    {
        this.InitializeComponent();
        Deals = new ObservableCollection<Deal>();
        LoadData();
    }

    public async void LoadData()
    {
        var reader = App.GetService<DatabaseService>().get_deals();
        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        Deals.Clear();
        while (reader.Read())
        {
            var deal = new Deal
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["name"].ToString(),
                Description = reader["description"]?.ToString(),
                ImageUrl = reader["image_url"]?.ToString(),
                Price = Convert.ToDecimal(reader["price"])
            };
            Deals.Add(deal);
        }
        reader.Close();
        sfDataGrid.ItemsSource = Deals;
    }

    private void StartEdit(Deal deal)
    {
        DealBeingAdded = deal;
        CurrentlyAddingNewItem = true;
        sfDataGrid.SelectedItem = deal;
        sfDataGrid.View.MoveCurrentTo(deal);
        sfDataGrid.AllowDeleting = false;
        sfDataGrid.AllowEditing = true;
        sfDataGrid.Columns[0].IsReadOnly = true;

        HiddenButtons.Visibility = Visibility.Visible;
        ButtonsPanel.Visibility = Visibility.Collapsed;
    }

    private void EndEdit()
    {
        DealBeingAdded = null;
        CurrentlyAddingNewItem = false;
        sfDataGrid.AllowEditing = false;
        sfDataGrid.AllowDeleting = true;

        HiddenButtons.Visibility = Visibility.Collapsed;
        ButtonsPanel.Visibility = Visibility.Visible;
    }

    private void Delete(Deal deal)
    {
        App.GetService<DatabaseService>().delete_deal(deal.Id);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var newDeal = new Deal
        {
            Id = 0,
            Name = "",
            Description = "",
            ImageUrl = "",
            Price = 0.0m
        };
        Deals.Add(newDeal);
        IsEditingNew = true;
        StartEdit(newDeal);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (sfDataGrid.SelectedItem is Deal deal)
        {
            IsEditingNew = false;
            StartEdit(deal);
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
                if (item is Deal deal)
                {
                    Delete(deal);
                    Deals.Remove(deal);
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


        if (sfDataGrid.SelectedItem is Deal deal)
        {
            try
            {
                if (IsEditingNew)
                    App.GetService<DatabaseService>().insert_deal(deal.Name, deal.Description, deal.ImageUrl, deal.Price);
                else
                    App.GetService<DatabaseService>().update_deal(deal.Id, deal.Name, deal.Description, deal.ImageUrl, deal.Price);
            }
            catch (Exception ex)
            {
                errorDialog.Content = ex.Message;
                await errorDialog.ShowAsync();
                Deals.Remove(deal);
            }
        }
        EndEdit();
        Deals.Clear();
        LoadData();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            Deals.Remove(DealBeingAdded);
        }
        EndEdit();
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        Deals.Clear();
        LoadData();
    }

    private void sfDataGrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grids.GridSelectionChangedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            sfDataGrid.SelectedItem = DealBeingAdded;
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
                if (item is Deal deal)
                {
                    Delete(deal);
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

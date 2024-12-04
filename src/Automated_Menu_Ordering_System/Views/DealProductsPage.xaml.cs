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

public class DealProduct
{
    public int DealId
    {
        get; set;
    }
    public int ProductId
    {
        get; set;
    }
}

public sealed partial class DealProductsPage : Page
{

    public ObservableCollection<DealProduct> DealProducts
    {
        get; private set;
    }

    private bool CurrentlyAddingNewItem
    {
        get; set;
    }

    private DealProduct? DealProductBeingAdded
    {
        get; set;
    }

    private bool IsEditingNew
    {
        get; set;
    }

    public DealProductsPage()
    {
        this.InitializeComponent();
        DealProducts = new ObservableCollection<DealProduct>();
        LoadData();
    }

    public async void LoadData()
    {
        var reader = App.GetService<DatabaseService>().get_deal_products();
        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        DealProducts.Clear();
        while (reader.Read())
        {
            var dealProduct = new DealProduct
            {
                DealId = Convert.ToInt32(reader["deal_id"]),
                ProductId = Convert.ToInt32(reader["product_id"])
            };
            DealProducts.Add(dealProduct);
        }
        reader.Close();
        sfDataGrid.ItemsSource = DealProducts;
    }

    private void StartEdit(DealProduct dealProduct)
    {
        DealProductBeingAdded = dealProduct;
        CurrentlyAddingNewItem = true;
        sfDataGrid.SelectedItem = dealProduct;
        sfDataGrid.View.MoveCurrentTo(dealProduct);
        sfDataGrid.AllowDeleting = false;
        sfDataGrid.AllowEditing = true;
        //sfDataGrid.Columns[0].IsReadOnly = true;

        HiddenButtons.Visibility = Visibility.Visible;
        ButtonsPanel.Visibility = Visibility.Collapsed;
    }

    private void EndEdit()
    {
        DealProductBeingAdded = null;
        CurrentlyAddingNewItem = false;
        sfDataGrid.AllowEditing = false;
        sfDataGrid.AllowDeleting = true;

        HiddenButtons.Visibility = Visibility.Collapsed;
        ButtonsPanel.Visibility = Visibility.Visible;
    }

    private void Delete(DealProduct dealProduct)
    {
        App.GetService<DatabaseService>().delete_deal_product(dealProduct.DealId, dealProduct.ProductId);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var newDealProduct = new DealProduct
        {
            DealId = 0,
            ProductId = 0
        };
        DealProducts.Add(newDealProduct);
        IsEditingNew = true;
        StartEdit(newDealProduct);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (sfDataGrid.SelectedItem is DealProduct dealProduct)
        {
            IsEditingNew = false;
            StartEdit(dealProduct);
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
                if (item is DealProduct dealProduct)
                {
                    Delete(dealProduct);
                    DealProducts.Remove(dealProduct);
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


        if (sfDataGrid.SelectedItem is DealProduct dealProduct)
        {
            try
            {
                if (IsEditingNew)
                    App.GetService<DatabaseService>().insert_deal_product(dealProduct.DealId, dealProduct.ProductId);
                else
                {
                    //App.GetService<DatabaseService>().update_deal_product(dealProduct.ProductId, dealProduct.DealId); TODO: Implement this
                }
            }
            catch (Exception ex)
            {
                errorDialog.Content = ex.Message;
                await errorDialog.ShowAsync();
                DealProducts.Remove(dealProduct);
            }
        }
        EndEdit();
        DealProducts.Clear();
        LoadData();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            DealProducts.Remove(DealProductBeingAdded);
        }
        EndEdit();
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        DealProducts.Clear();
        LoadData();
    }

    private void sfDataGrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grids.GridSelectionChangedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            sfDataGrid.SelectedItem = DealProductBeingAdded;
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
                if (item is DealProduct dealProduct)
                {
                    Delete(dealProduct);
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

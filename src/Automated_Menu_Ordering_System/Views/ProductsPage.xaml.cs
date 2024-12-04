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

public class Product
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
    public int EstimatedTime
    {
        get; set;
    }
    public string Category
    {
        get; set;
    }
    public string Subcategory
    {
        get; set;
    }
    public decimal DiscountPercent
    {
        get; set;
    }
    public DateTime CreatedAt
    {
        get; set;
    }
}

public sealed partial class ProductsPage : Page
{

    public ObservableCollection<Product> Products
    {
        get; private set;
    }

    private bool CurrentlyAddingNewItem
    {
        get; set;
    }

    private Product? ProductBeingAdded
    {
        get; set;
    }

    private bool IsEditingNew
    {
        get; set;
    }

    public ProductsPage()
    {
        this.InitializeComponent();
        Products = new ObservableCollection<Product>();
        LoadData();
    }

    public async void LoadData()
    {
        var reader = App.GetService<DatabaseService>().get_products();
        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        Products.Clear();
        while (reader.Read())
        {
            var product = new Product
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["name"].ToString(),
                Description = reader["description"]?.ToString(),
                ImageUrl = reader["image_url"]?.ToString(),
                Price = Convert.ToDecimal(reader["price"]),
                EstimatedTime = Convert.ToInt32(reader["estimated_time"]),
                Category = reader["category"].ToString(),
                Subcategory = reader["subcategory"]?.ToString(),
                DiscountPercent = reader["discount_percent"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["discount_percent"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"])
            };
            Products.Add(product);
        }
        reader.Close();
        sfDataGrid.ItemsSource = Products;
    }

    private void StartEdit(Product product)
    {
        ProductBeingAdded = product;
        CurrentlyAddingNewItem = true;
        sfDataGrid.SelectedItem = product;
        sfDataGrid.View.MoveCurrentTo(product);
        sfDataGrid.AllowDeleting = false;
        sfDataGrid.AllowEditing = true;
        sfDataGrid.Columns[0].IsReadOnly = true;

        HiddenButtons.Visibility = Visibility.Visible;
        ButtonsPanel.Visibility = Visibility.Collapsed;
    }

    private void EndEdit()
    {
        ProductBeingAdded = null;
        CurrentlyAddingNewItem = false;
        sfDataGrid.AllowEditing = false;
        sfDataGrid.AllowDeleting = true;

        HiddenButtons.Visibility = Visibility.Collapsed;
        ButtonsPanel.Visibility = Visibility.Visible;
    }

    private void Delete(Product product)
    {
        App.GetService<DatabaseService>().delete_product(product.Id);
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        var newProduct = new Product
        {
            Id = 0,
            Name = "",
            Description = "",
            ImageUrl = "",
            Price = 0.0m,
            EstimatedTime = 0,
            Category = "",
            Subcategory = "",
            DiscountPercent = 0.0m,
            CreatedAt = DateTime.Now
        };
        Products.Add(newProduct);
        IsEditingNew = true;
        StartEdit(newProduct);
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (sfDataGrid.SelectedItem is Product product)
        {
            IsEditingNew = false;
            StartEdit(product);
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
                if (item is Product product)
                {
                    Delete(product);
                    Products.Remove(product);
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


        if (sfDataGrid.SelectedItem is Product product)
        {
            try
            {
                if (IsEditingNew)
                    App.GetService<DatabaseService>().insert_product(product.Name, product.Description, product.ImageUrl, product.Price, product.EstimatedTime, product.Category, product.Subcategory, product.DiscountPercent);
                else
                    App.GetService<DatabaseService>().update_product(product.Id, product.Name, product.Description, product.ImageUrl, product.Price, product.EstimatedTime, product.Category, product.Subcategory, product.DiscountPercent);
            }
            catch (Exception ex)
            {
                errorDialog.Content = ex.Message;
                await errorDialog.ShowAsync();
                Products.Remove(product);
            }
        }
        EndEdit();
        Products.Clear();
        LoadData();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            Products.Remove(ProductBeingAdded);
        }
        EndEdit();
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        Products.Clear();
        LoadData();
    }

    private void sfDataGrid_SelectionChanged(object sender, Syncfusion.UI.Xaml.Grids.GridSelectionChangedEventArgs e)
    {
        if (CurrentlyAddingNewItem)
        {
            sfDataGrid.SelectedItem = ProductBeingAdded;
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
                if (item is Product product)
                {
                    Delete(product);
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

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Automated_Menu_Ordering_System.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.DataGrid;
using WinUIEx;

namespace Automated_Menu_Ordering_System.Views;

public class MenuItem
{
    public int Id
    {
        get; set;
    }
    public string ItemName
    {
        get; set;
    }
    public decimal Price
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
    public string Description
    {
        get; set;
    }
    public string Availability
    {
        get; set;
    }
}

public sealed partial class MenuPage : Page
{
    private readonly ILocalSettingsService _localSettingsService;

    public ObservableCollection<MenuItem> MenuItems
    {
        get; private set;
    }


    public MenuPage()
    {
        this.InitializeComponent();
        _localSettingsService = App.GetService<ILocalSettingsService>();
        MenuItems = new ObservableCollection<MenuItem>();
        LoadData();
    }

    public async void LoadData()
    {
        var BranchId = await _localSettingsService.ReadSettingAsync<int>("branchId");

        var reader = App.GetService<DatabaseService>().get_menu_by_branch_id(BranchId);
        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        MenuItems.Clear();
        while (reader.Read())
        {
            var manuItem = new MenuItem
            {
                Id = Convert.ToInt32(reader["product_id"]),
                ItemName = reader["product_name"].ToString(),
                Price = Convert.ToDecimal(reader["product_price"]),
                Category = reader["product_category"].ToString(),
                Subcategory = reader["product_subcategory"].ToString(),
                Description = reader["product_description"].ToString(),
                Availability = reader["is_out_of_stock"].ToString() == "False" ? "In Stock" : "Out of Stock"
            };
            MenuItems.Add(manuItem);
        }
        reader.Close();
        sfDataGrid.ItemsSource = MenuItems;
    }

    private async void MakeInStockButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var BranchId = await _localSettingsService.ReadSettingAsync<int>("branchId");

        var selectedItems = sfDataGrid.SelectedItems.OfType<MenuItem>().ToList();
        var itemsNotInStock = selectedItems.Where(x => x.Availability == "Out of Stock").ToList();
        foreach (var item in itemsNotInStock)
        {
            App.GetService<DatabaseService>().change_item_out_of_stock_value(item.Id, BranchId, false);
        }
        LoadData();
    }

    private async void MakeOutOfStockButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var BranchId = await _localSettingsService.ReadSettingAsync<int>("branchId");

        var selectedItems = sfDataGrid.SelectedItems.OfType<MenuItem>().ToList();
        var itemsInStock = selectedItems.Where(x => x.Availability == "In Stock").ToList();
        foreach (var item in itemsInStock)
        {
            App.GetService<DatabaseService>().change_item_out_of_stock_value(item.Id, BranchId, true);
        }
        LoadData();
    }
}

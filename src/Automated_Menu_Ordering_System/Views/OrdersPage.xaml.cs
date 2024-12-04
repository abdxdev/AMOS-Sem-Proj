using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Automated_Menu_Ordering_System.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.DataGrid;
using WinUIEx;

namespace Automated_Menu_Ordering_System.Views;

public class PlacedOrder
{
    public int Id
    {
        get; set;
    }
    public string ItemName
    {
        get; set;
    }
    public int TableId
    {
        get; set;
    }
    public int Quantity
    {
        get; set;
    }
    public int TotalPrice
    {
        get; set;
    }
    public DateTime CreatedAt
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }
    public bool IsPaid
    {
        get; set;
    }
    public string Status
    {
        get; set;
    }
}

public sealed partial class OrdersPage : Page
{
    private readonly ILocalSettingsService _localSettingsService;

    public ObservableCollection<PlacedOrder> Orders
    {
        get; private set;
    }

    public OrdersPage()
    {
        this.InitializeComponent();
        _localSettingsService = App.GetService<ILocalSettingsService>();
        Orders = new ObservableCollection<PlacedOrder>();
        LoadData();
    }

    public async void LoadData()
    {
        var BranchId = await _localSettingsService.ReadSettingAsync<int>("branchId");

        var reader = App.GetService<DatabaseService>().get_placed_orders_by_branch_id(BranchId);

        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        Orders.Clear();
        while (reader.Read())
        {
            var order = new PlacedOrder
            {
                Id = Convert.ToInt32(reader["order_id"]),
                ItemName = reader["is_deal"].ToString() == "True" ? reader["deal_name"].ToString() : reader["product_name"].ToString(),
                TableId = Convert.ToInt32(reader["table_id"]),
                Quantity = Convert.ToInt32(reader["quantity"]),
                TotalPrice = Convert.ToInt32(reader["total_price"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                Description = reader["description"].ToString(),
                IsPaid = Convert.ToBoolean(reader["is_paid"]),
                Status = reader["status"].ToString()
            };
            Orders.Add(order);
        }
        reader.Close();
        sfDataGrid.ItemsSource = Orders;
    }

    private void ChangeStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var newStatus = (string)ChangeStatusComboBox.SelectedItem;
        if (newStatus == null)
        {
            ChangeStatusComboBox.SelectedIndex = 0;
            return;
        }
        if (newStatus == "Choose Status")
        {
            ChangeStatusComboBox.SelectedIndex = 0;
            return;
        }
        var selectedOrders = sfDataGrid.SelectedItems.OfType<PlacedOrder>().ToList();
        foreach (var selectedOrder in selectedOrders)
        {
            App.GetService<DatabaseService>().update_order_status(selectedOrder.Id, newStatus);
        }
        LoadData();
        ChangeStatusComboBox.SelectedIndex = 0;
    }
}

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Automated_Menu_Ordering_System.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.DataGrid;
using WinUIEx;

namespace Automated_Menu_Ordering_System.Views;

public class HistoryItem
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
    public int Rating
    {
        get; set;
    }
    public string Description
    {
        get; set;
    }
}

public sealed partial class HistoryPage : Page
{
    private readonly ILocalSettingsService _localSettingsService;

    public ObservableCollection<HistoryItem> HistoryItems
    {
        get; private set;
    }

    public HistoryPage()
    {
        this.InitializeComponent();
        _localSettingsService = App.GetService<ILocalSettingsService>();
        HistoryItems = new ObservableCollection<HistoryItem>();
        LoadData();
    }

    public async void LoadData()
    {
        var BranchId = await _localSettingsService.ReadSettingAsync<int>("branchId");
        var reader = App.GetService<DatabaseService>().get_closed_orders_by_branch_id(BranchId);

        if (!reader.HasRows)
        {
            reader.Close();
            return;
        }
        HistoryItems.Clear();
        while (reader.Read())
        {
            var historyItem = new HistoryItem
            {
                Id = Convert.ToInt32(reader["order_id"]),
                ItemName = reader["is_deal"].ToString() == "True" ? reader["deal_name"].ToString() : reader["product_name"].ToString(),
                TableId = Convert.ToInt32(reader["table_id"]),
                Quantity = Convert.ToInt32(reader["quantity"]),
                TotalPrice = Convert.ToInt32(reader["total_price"]),
                CreatedAt = Convert.ToDateTime(reader["created_at"]),
                Rating = Convert.ToInt32(reader["rating"]),
                Description = reader["description"].ToString(),
            };
            HistoryItems.Add(historyItem);
        }
        reader.Close();
        sfDataGrid.ItemsSource = HistoryItems;
    }
}

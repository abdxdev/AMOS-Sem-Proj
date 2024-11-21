using Automated_Menu_Ordering_System.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

namespace Automated_Menu_Ordering_System.Views;

public class Order
{
    public string? Id
    {
        get; set;
    }
    public string? Title
    {
        get; set;
    }
    public string? ImageUrl
    {
        get; set;
    }
    public string? EstimatedTime
    {
        get; set;
    }
    public string? Description
    {
        get; set;
    }
    public string? Price
    {
        get; set;
    }
}

public sealed partial class CartPage : Page
{
    private readonly ILocalSettingsService _localSettingsService;

    public CartPage()
    {
        this.InitializeComponent();
        _localSettingsService = App.GetService<ILocalSettingsService>();
        LoadOrders();
    }

    private async void LoadOrders()
    {
        try
        {
            var tableId = await _localSettingsService.ReadSettingAsync<int>("tableId");
            var reader = App.GetService<DatabaseService>().get_orders_by_table_id(tableId);
            if (!reader.HasRows)
            {
                EmptyOrdersTextBlock.Visibility = Visibility.Visible;
                reader.Close();
                return;
            }
            while (reader.Read())
            {
                var order = new Order
                {
                    Id = reader["order_id"].ToString(),
                    Title = reader["product_name"].ToString(),
                    ImageUrl = reader["product_image_url"].ToString(),
                    Description = $"{reader["product_description"]}\n{reader["description"]}",
                    EstimatedTime = $"{reader["estimated_time"]} mins",
                    Price = $"Rs. {reader["total_price"]}",
                };

                var card = new CartCard
                {
                    Order = order
                };

                if (reader["is_paid"].ToString() == "False")
                {
                    UnpaidOrdersPanel.Children.Add(card);
                    UnpaidOrdersPanel.Visibility = Visibility.Visible;
                }
                else if (reader["status"].ToString() == "in_progress")
                {
                    InProgressOrdersPanel.Children.Add(card);
                    InProgressOrdersPanel.Visibility = Visibility.Visible;
                }
                else if (reader["ready"].ToString() == "ready")
                {
                    ReadyOrdersPanel.Children.Add(card);
                    ReadyOrdersPanel.Visibility = Visibility.Visible;
                }
                else if (reader["status"].ToString() == "completed")
                {
                    CompletedOrdersPanel.Children.Add(card);
                    CompletedOrdersPanel.Visibility = Visibility.Visible;
                }
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

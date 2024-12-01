using Automated_Menu_Ordering_System.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using Npgsql;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class CartPage : Page
{
    private readonly ILocalSettingsService _localSettingsService;

    public CartPage()
    {
        this.InitializeComponent();
        _localSettingsService = App.GetService<ILocalSettingsService>();
        LoadOrders();
    }

    public async void LoadOrders()
    {
        try
        {
            CollapseEveryThing();
            var tableId = await _localSettingsService.ReadSettingAsync<int>("userId");
            var reader = App.GetService<DatabaseService>().get_orders_by_table_id(tableId);
            if (!reader.HasRows)
            {
                EmptyOrdersTextBlock.Visibility = Visibility.Visible;
                reader.Close();
                return;
            }
            float totalPrice = 0;
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
                    Order = order,
                    ParentPage = this
                };

                if (reader["is_paid"].ToString() == "False")
                {
                    UnpaidOrdersPanel.Children.Add(card);
                    UnpaidOrdersPanel.Visibility = Visibility.Visible;
                    totalPrice += float.Parse(reader["total_price"].ToString());
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

            if (totalPrice == 0)
            {
                PaymentArea.Visibility = Visibility.Collapsed;
                return;
            }
            PaymentArea.Visibility = Visibility.Visible;
            PaymentAreaBar.Visibility = Visibility.Visible;
            PayButton.Content = $"Pay Rs. {totalPrice}";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private float CalculateTotal(NpgsqlDataReader reader)
    {
        float total = 0;
        while (reader.Read())
        {
            total += float.Parse(reader["total_price"].ToString());
        }
        return total;
    }

    async private void PayButton_Click(object sender, RoutedEventArgs e)
    {
        var tableId = await _localSettingsService.ReadSettingAsync<int>("userId");
        var dialog = new ContentDialog();
        dialog.Title = "Payment";
        dialog.Content = "Are you sure you want to pay?";
        dialog.PrimaryButtonText = "Yes";
        dialog.CloseButtonText = "No";
        dialog.DefaultButton = ContentDialogButton.Primary;
        dialog.XamlRoot = XamlRoot;
        dialog.DataContext = dialog;
        var result = await dialog.ShowAsync();
        if (result != ContentDialogResult.Primary)
        {
            dialog.Title = "Payment";
            dialog.Content = "Payment failed";
            dialog.CloseButtonText = "Close";
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.PrimaryButtonText = null;
            await dialog.ShowAsync();
            return;
        }
        App.GetService<DatabaseService>().pay_bill(tableId);
        UnpaidOrdersPanel.Children.Clear();
        LoadOrders();
    }
    private void CollapseEveryThing()
    {
        UnpaidOrdersPanel.Visibility = Visibility.Collapsed;
        UnpaidOrdersPanel.Children.Clear();
        InProgressOrdersPanel.Visibility = Visibility.Collapsed;
        InProgressOrdersPanel.Children.Clear();
        ReadyOrdersPanel.Visibility = Visibility.Collapsed;
        ReadyOrdersPanel.Children.Clear();
        CompletedOrdersPanel.Visibility = Visibility.Collapsed;
        CompletedOrdersPanel.Children.Clear();
        EmptyOrdersTextBlock.Visibility = Visibility.Collapsed;
        PaymentArea.Visibility = Visibility.Collapsed;
        PaymentAreaBar.Visibility = Visibility.Collapsed;
    }
}

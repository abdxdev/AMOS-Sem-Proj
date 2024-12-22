using Automated_Menu_Ordering_System.Contracts.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Npgsql;
using System.Diagnostics;

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
                var order = new Item
                {
                    Id = reader["order_id"].ToString(),
                    Title = reader["is_deal"].ToString() == "True" ? reader["deal_name"].ToString() : reader["product_name"].ToString(),
                    ImageUrl = reader["is_deal"].ToString() == "True" ? reader["deal_image_url"].ToString() : reader["product_image_url"].ToString(),
                    Description = $"{(reader["is_deal"].ToString() == "True" ? reader["deal_description"] : reader["product_description"])}\n{reader["description"]}",
                    EstimatedTime = $"{reader["estimated_time"]} mins",
                    Price = $"Rs. {reader["total_price"]}",
                };

                var card = new CartCard
                {
                    Item = order,
                    ParentPage = this
                };

                if (reader["is_paid"].ToString() == "False")
                {
                    card.ChangeToUnpaid();
                    UnpaidOrdersPanel.Children.Add(card);
                    ParentUnpaidOrdersPanel.Visibility = Visibility.Visible;
                    totalPrice += float.Parse(reader["total_price"].ToString());
                }
                else if (reader["status"].ToString() == "in_progress")
                {
                    card.ChangeToInProgress();
                    InProgressOrdersPanel.Children.Add(card);
                    ParentInProgressOrdersPanel.Visibility = Visibility.Visible;
                }
                else if (reader["status"].ToString() == "ready")
                {
                    card.ChangeToReady();
                    ReadyOrdersPanel.Children.Add(card);
                    ParentReadyOrdersPanel.Visibility = Visibility.Visible;
                }
                else if (reader["status"].ToString() == "completed")
                {
                    var rating = reader["rating"].ToString();
                    if (rating == "")
                        card.ChangeToCompleted(-1);
                    else
                        card.ChangeToCompleted(float.Parse(rating));
                    CompletedOrdersPanel.Children.Add(card);
                    ParentCompletedOrdersPanel.Visibility = Visibility.Visible;
                }
            }
            reader.Close();
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
        ParentUnpaidOrdersPanel.Visibility = Visibility.Collapsed;
        UnpaidOrdersPanel.Children.Clear();
        ParentInProgressOrdersPanel.Visibility = Visibility.Collapsed;
        InProgressOrdersPanel.Children.Clear();
        ParentReadyOrdersPanel.Visibility = Visibility.Collapsed;
        ReadyOrdersPanel.Children.Clear();
        ParentCompletedOrdersPanel.Visibility = Visibility.Collapsed;
        CompletedOrdersPanel.Children.Clear();
        EmptyOrdersTextBlock.Visibility = Visibility.Collapsed;
    }
}

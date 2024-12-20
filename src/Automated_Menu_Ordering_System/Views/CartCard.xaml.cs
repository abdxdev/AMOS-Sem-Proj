using System.Diagnostics;
using Automated_Menu_Ordering_System.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

//public class Item
//{
//    public string? Id
//    {
//        get; set;
//    }
//    public string? Title
//    {
//        get; set;
//    }
//    public string? ImageUrl
//    {
//        get; set;
//    }
//    public string? EstimatedTime
//    {
//        get; set;
//    }
//    public string? Description
//    {
//        get; set;
//    }
//    public string? Price
//    {
//        get; set;
//    }
//}

public sealed partial class CartCard : UserControl
{
    public Item Item
    {
        get => (Item)GetValue(OrderProperty);
        set => SetValue(OrderProperty, value);
    }

    public static readonly DependencyProperty OrderProperty =
        DependencyProperty.Register("Order", typeof(Item), typeof(CartCard), new PropertyMetadata(null));

    public CartPage ParentPage
    {
        get; set;
    }

    public CartCard()
    {
        this.InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        int orderId = int.Parse((string?)Item.Id);
        App.GetService<DatabaseService>().delete_order(orderId);
        ParentPage?.LoadOrders();
    }
    public void ChangeToInProgress()
    {
        DeleteButton.Visibility = Visibility.Collapsed;
        StatusTextBlock.Visibility = Visibility.Visible;
        StatusTextBlock.Text = $"Estimated Time: {Item.EstimatedTime}";
    }
    public void ChangeToReady()
    {
        DeleteButton.Visibility = Visibility.Collapsed;
        StatusTextBlock.Visibility = Visibility.Visible;
        StatusTextBlock.Text = "Ready";
    }
    public void ChangeToUnpaid()
    {
        DeleteButton.Visibility = Visibility.Visible;
        StatusTextBlock.Visibility = Visibility.Collapsed;
    }
    public void ChangeToCompleted(float rating)
    {
        DeleteButton.Visibility = Visibility.Collapsed;
        StatusTextBlock.Visibility = Visibility.Collapsed;
        if (rating != -1)
        {
            RatingControl.Visibility = Visibility.Visible;
            RateThisMealButton.Visibility = Visibility.Collapsed;
            RatingControl.Value = rating;
        }
        else
        {
            RatingControl.Visibility = Visibility.Collapsed;
            RateThisMealButton.Visibility = Visibility.Visible;
        }
    }
    private async void RateThisMealButton_Click(object sender, RoutedEventArgs e)
    {
        var ratingControl = new RatingControl();
        var ratingDialog = new ContentDialog
        {
            Title = "Rate this meal",
            Content = ratingControl,
            PrimaryButtonText = "Rate",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            XamlRoot = XamlRoot
        };
        var result = await ratingDialog.ShowAsync();
        if (result != ContentDialogResult.Primary)
            return;
        if (ratingControl.Value == 0 || ratingControl.Value == null || ratingControl.Value == -1)
            return;
        App.GetService<DatabaseService>().rate_order(int.Parse((string?)Item.Id), (float)ratingControl.Value);
        ParentPage?.LoadOrders();
    }
}

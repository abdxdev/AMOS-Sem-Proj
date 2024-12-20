using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class FlipViewItem : UserControl
{
    public Item Item
    {
        get => (Item)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register("Item", typeof(Item), typeof(FlipViewItem), new PropertyMetadata(null));

    public FlipViewItem()
    {
        this.InitializeComponent();
    }

    private async void CheckOutButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = (sender as Button)?.DataContext as Item;
        var dialog = new DealPopupTemplate
        {
            Id = selectedItem.Id,
            Title = selectedItem.Title,
            EstimatedTime = selectedItem.EstimatedTime,
            ImageUrl = selectedItem.ImageUrl,
            AvgRating = selectedItem.AvgRating,
            TotalRating = selectedItem.TotalRatings,
            Description = selectedItem.Description,
            Price = selectedItem.Price,
            XamlRoot = XamlRoot,
        };

        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            Debug.WriteLine("User confirmed");
        }
    }

    private void DetailButton_Click(object sender, RoutedEventArgs e)
    {

    }
}

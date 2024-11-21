using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace mianApp10.Views;

public sealed partial class CardStyle2 : UserControl
{

    public Item Item
    {
        get => (Item)GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register("Item", typeof(Item), typeof(CardStyle2), new PropertyMetadata(null));

    public CardStyle2()
    {
        this.InitializeComponent();
    }

    private async void MainButton_Click(object sender, RoutedEventArgs e)
    {
        var selectedItem = (sender as Button)?.DataContext as Item;
        var dialog = new PizzaPopupTemplate
        {
            Id = selectedItem.Id,
            Title = selectedItem.Title,
            ImageUrl = selectedItem.ImageUrl,
            EstimatedTime = selectedItem.EstimatedTime,
            Description = selectedItem.Description,
            Price = selectedItem.Price,
            AvgRating = selectedItem.AvgRating,
            XamlRoot = XamlRoot,
        };
        dialog.LoadToppings();
        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            Debug.WriteLine("User confirmed");
        }
    }
}

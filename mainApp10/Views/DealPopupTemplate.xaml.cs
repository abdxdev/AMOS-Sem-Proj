using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Diagnostics;
using mianApp10.Contracts.Services;

namespace mianApp10.Views;

public sealed partial class DealPopupTemplate : ContentDialog
{
    public string? Id
    {
        get; set;
    }
    public string? Title
    {
        get; set;
    }
    public string? EstimatedTime
    {
        get; set;
    }
    public string? ImageUrl
    {
        get; set;
    }
    public string? AvgRating
    {
        get; set;
    }
    public string? TotalRating
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

    private readonly ILocalSettingsService _localSettingsService;

    public DealPopupTemplate()
    {
        _localSettingsService = App.GetService<ILocalSettingsService>();
        DataContext = this;
        this.InitializeComponent();
    }
    private void UpdateTextElements()
    {
        if (string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(EstimatedTime))
        {
            return;
        }
        var finalPrice = float.Parse(Price.Split(" ")[1]);
        FinalPrice_TextBlock.Text = $"Rs. {(finalPrice * int.Parse(QuantityTextBlock.Text))}";
    }

    private void QuantityButton_Click(object sender, RoutedEventArgs e)
    {
        var upperLimit = 6;
        var currentQuantity = int.Parse(QuantityTextBlock.Text);
        if (sender is Button { Name: "QuantityButtonPlus" })
        {
            currentQuantity += 1;
        }
        else if (sender is Button { Name: "QuantityButtonMinus" })
        {
            currentQuantity -= 1;
        }
        if (currentQuantity == 1)
        {
            QuantityButtonMinus.IsEnabled = false;
        }
        else if (currentQuantity == upperLimit)
        {
            QuantityButtonPlus.IsEnabled = false;
        }
        else
        {
            QuantityButtonMinus.IsEnabled = true;
            QuantityButtonPlus.IsEnabled = true;
        }
        QuantityTextBlock.Text = currentQuantity.ToString();
        UpdateTextElements();
    }

    public async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var finalPrice = float.Parse(FinalPrice_TextBlock.Text.Split(" ")[1]);
        var estimatedTime = int.Parse(EstimatedTime.Split(" ")[0]);
        var tableId = await _localSettingsService.ReadSettingAsync<int>("userId");
        var itemId = int.Parse(Id);
        var quantity = int.Parse(QuantityTextBlock.Text);
        try
        {
            App.DatabaseService.insert_an_order(tableId, itemId, quantity, finalPrice, estimatedTime, "");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
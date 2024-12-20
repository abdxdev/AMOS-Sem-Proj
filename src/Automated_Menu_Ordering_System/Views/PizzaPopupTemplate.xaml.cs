using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Automated_Menu_Ordering_System.Contracts.Services;

namespace Automated_Menu_Ordering_System.Views;

public class ToppingItem
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
    public string? Price
    {
        get; set;
    }
}

public sealed partial class PizzaPopupTemplate : ContentDialog
{
    public ObservableCollection<ToppingItem> Toppings { get; set; } = new ObservableCollection<ToppingItem>();
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

    public PizzaPopupTemplate()
    {
        _localSettingsService = App.GetService<ILocalSettingsService>();
        DataContext = this;
        this.InitializeComponent();
    }

    private void SizeToggleButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton { Name: "SizeToggleButtonS" })
        {
            SizeToggleButtonL.IsChecked = false;
            SizeToggleButtonM.IsChecked = false;
            SizeToggleButtonS.IsChecked = true;
        }
        else if (sender is ToggleButton { Name: "SizeToggleButtonM" })
        {
            SizeToggleButtonL.IsChecked = false;
            SizeToggleButtonS.IsChecked = false;
            SizeToggleButtonM.IsChecked = true;
        }
        else if (sender is ToggleButton { Name: "SizeToggleButtonL" })
        {
            SizeToggleButtonM.IsChecked = false;
            SizeToggleButtonS.IsChecked = false;
            SizeToggleButtonL.IsChecked = true;
        }
        UpdateTextElements();
    }
    private void ToppingsGridView_SelectionChanged(object sender, RoutedEventArgs e)
    {
        UpdateTextElements();
    }
    private void UpdateTextElements()
    {
        if (string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(EstimatedTime))
        {
            return;
        }
        var finalPrice = float.Parse(Price.Split(" ")[1]);
        if (SizeToggleButtonS.IsChecked == true)
        {
            finalPrice *= 0.8f;
        }
        else if (SizeToggleButtonL.IsChecked == true)
        {
            finalPrice *= 1.2f;
        }

        foreach (var selectedItem in ToppingsGridView.SelectedItems)
        {
            if (selectedItem is ToppingItem topping)
            {
                finalPrice += float.Parse(topping.Price.Split(" ")[1]);
            }
        }
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

    private void ToppingsToggleButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton { Name: "ToppingsToggleButtonY" })
        {
            ToppingsToggleButtonN.IsChecked = false;
            ToppingsToggleButtonY.IsChecked = true;
            ToppingsGridView.Visibility = Visibility.Visible;

        }
        else if (sender is ToggleButton { Name: "ToppingsToggleButtonN" })
        {
            ToppingsToggleButtonY.IsChecked = false;
            ToppingsToggleButtonN.IsChecked = true;
            ToppingsGridView.Visibility = Visibility.Collapsed;
            ToppingsGridView.SelectedItems.Clear();
        }
        UpdateTextElements();
    }
    public void LoadToppings()
    {
        try
        {
            var reader = App.GetService<DatabaseService>()!.get_product_by_category_and_subcategory("topping", null);
            while (reader.Read())
            {
                Toppings.Add(new ToppingItem
                {
                    Id = reader["id"].ToString(),
                    Title = reader["name"].ToString(),
                    ImageUrl = reader["image_url"].ToString(),
                    Price = $"Rs. {reader["price"]}",
                });
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        var description = "Size: ";
        if (SizeToggleButtonS.IsChecked == true)
        {
            description += "Small ";
        }
        else if (SizeToggleButtonM.IsChecked == true)
        {
            description += "Medium ";
        }
        else if (SizeToggleButtonL.IsChecked == true)
        {
            description += "Large ";
        }
        description += "\n";
        description += "Toppings: ";
        if (ToppingsToggleButtonY.IsChecked == true)
        {
            foreach (var selectedItem in ToppingsGridView.SelectedItems)
            {
                if (selectedItem is ToppingItem topping)
                {
                    description += $"{topping.Title}, ";
                }
            }
        }

        description = description.TrimEnd(',', ' ', '\n');

        var finalPrice = float.Parse(FinalPrice_TextBlock.Text.Split(" ")[1]);
        var estimatedTime = int.Parse(EstimatedTime.Split(" ")[0]);
        var tableId = await _localSettingsService.ReadSettingAsync<int>("userId");
        var itemId = int.Parse(Id);
        var quantity = int.Parse(QuantityTextBlock.Text);
        try
        {
            App.GetService<DatabaseService>().insert_an_order(tableId, itemId, quantity, finalPrice, estimatedTime, description);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
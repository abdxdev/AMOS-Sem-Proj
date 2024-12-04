using System.Diagnostics;
using Automated_Menu_Ordering_System.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Npgsql;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class HomePage : Page
{
    public HomeViewModel ViewModel
    {
        get;
    }

    public HomePage()
    {
        ViewModel = App.GetService<HomeViewModel>();
        InitializeComponent();
        DataContext = this;
        FetchItemData();
    }

    private Item MakeItemFromReader(NpgsqlDataReader reader)
    {
        return new Item
        {
            Id = reader["id"].ToString(),
            Title = reader["name"].ToString(),
            ImageUrl = reader["image_url"].ToString(),
            Description = reader["description"].ToString(),
            AvgRating = reader["avg_rating"].ToString(),
            EstimatedTime = $"{reader["estimated_time"]} mins",
            Price = $"Rs. {reader["price"]}",
        };
    }

    private void FetchItemData()
    {
        try
        {
            var reader = App.GetService<DatabaseService>().get_product_by_category_and_subcategory("pizza", null);
            while (reader.Read())
            {
                TrendingPanel.Children.Add(new CardStyle1
                {
                    Item = MakeItemFromReader(reader)
                });
                TopRatedPanel.Children.Add(new CardStyle2
                {
                    Item = MakeItemFromReader(reader)
                });
            }
            reader.Close();
            reader = App.GetService<DatabaseService>().get_all_deals();
            while (reader.Read())
            {
                DealPanel.Items.Add(new FlipViewItem
                {
                    Item = MakeItemFromReader(reader)
                });
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
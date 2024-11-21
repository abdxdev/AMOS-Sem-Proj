using mianApp10.ViewModels;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Npgsql;

namespace mianApp10.Views;

public sealed partial class BurgersPage : Page
{
    public BurgersViewModel ViewModel
    {
        get;
    }

    public BurgersPage()
    {
        ViewModel = App.GetService<BurgersViewModel>();
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
            var reader = App.DatabaseService.get_product_by_category_and_subcategory("burger");
            while (reader.Read())
            {
                var subcategory = reader["subcategory"].ToString();
                if (subcategory == "specialty_burger")
                {
                    SpecialtyBurgersPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "beef")
                {
                    BeefBurgersPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "chicken_burger")
                {
                    ChickenBurgersPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "veggie_burger")
                {
                    VeggieBurgersPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}

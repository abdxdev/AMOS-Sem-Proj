using Automated_Menu_Ordering_System.ViewModels;
using System.Diagnostics;
using Microsoft.UI.Xaml;

using Microsoft.UI.Xaml.Controls;
using Npgsql;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class DessertsPage : Page
{
    public DessertsViewModel ViewModel
    {
        get;
    }

    public DessertsPage()
    {
        ViewModel = App.GetService<DessertsViewModel>();
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
            var reader = App.GetService<DatabaseService>().get_product_by_category_and_subcategory("dessert");
            while (reader.Read())
            {
                var subcategory = reader["subcategory"].ToString();
                if (subcategory == "specialty_dessert")
                {
                    SpecialtyDessertsPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "cake_pastry")
                {
                    CakesAndPastriesPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "frozen_dessert")
                {
                    FrozenDessertsPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "pie")
                {
                    PiesPanel.Children.Add(new CardStyle1
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